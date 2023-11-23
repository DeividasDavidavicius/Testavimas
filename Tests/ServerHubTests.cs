using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Decorator;
using Moq;
using Moq.Protected;
using ProjServer;


namespace Tests
{
    [TestClass]
    public class ServerHubTests
    {
        private Mock<IHubCallerClients<IGameClient>> mockClients;
        private Mock<HubCallerContext> mockContext;
        private Mock<IGlobalData> mockGlobalData;
        private ServerHub serverHub;

        private Mock<IGameSession> mockGameSession;
        private Mock<Random> mockRandom;
        private Mock<Armor> mockArmor;
        private Mock<IJsonConvertFacade> mockJsonConvert;


        [TestInitialize]
        public void TestInitialize()
        {
            mockClients = new Mock<IHubCallerClients<IGameClient>>();
            var mockGameClient = new Mock<IGameClient>();
            mockGameSession = new Mock<IGameSession>();
            mockClients.Setup(c => c.Caller).Returns(mockGameClient.Object);
            mockClients.Setup(c => c.All).Returns(mockGameClient.Object);
            mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns("testConnectionId");

            mockGlobalData = new Mock<IGlobalData>();
            mockRandom = new Mock<Random>();
            mockArmor = new Mock<Armor>();
            mockJsonConvert = new Mock<IJsonConvertFacade>();

            serverHub = new ServerHub(mockGlobalData.Object, mockJsonConvert.Object)
            {
                Clients = mockClients.Object,
                Context = mockContext.Object
            };
        }

        [TestMethod]
        public void ValidateUser_ValidUser_ReturnsTrue()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(new GameSession());

            // Act
            var result = serverHub.ValidateUser();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUser_InvalidUserId_ReturnsFalse()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            // Act
            var result = serverHub.ValidateUser();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateUser_InvalidPlayer_ReturnsFalse()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns<Player>(null);

            // Act
            var result = serverHub.ValidateUser();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateUser_InvalidGameSession_ReturnsFalse()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns<GameSession>(null);

            // Act
            var result = serverHub.ValidateUser();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OnConnectedAsync_ReturnsValue()
        {
            // Arrange, Act
            var result = serverHub.OnConnectedAsync();

            // Assert
            Assert.AreEqual(result, Task.CompletedTask);
        }

        [TestMethod]
        public void OnEndTurn_ValidUser_ReturnsFalse()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            // Act
            var result = serverHub.OnEndTurn();

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task OnEndTurn_SetNewTurnUnsuccesful()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.SetNewTurn(It.IsAny<string>())).Returns(false);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.OnEndTurn();

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task OnEndTurn_SetNewTurnSuccesful()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.SetNewTurn(It.IsAny<string>())).Returns(true);

            Player tempPlayer = new Player();
            Player tempPlayer2 = new Player();
            tempPlayer.healthPoints = -1;
            tempPlayer2.healthPoints = 1;

            mockGameSession.Setup(g => g.players).Returns(new List<Player> { tempPlayer, tempPlayer2});
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.OnEndTurn();

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Never);
            mockClients.Verify(c => c.All.OnPlayerTurn(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void OnUndo_ValidUser_ReturnsFalse()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            // Act
            var result = serverHub.OnUndo();

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task OnUndo_UndoTurnSuccesful()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.UndoCommand()).Returns(false);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.OnUndo();

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Never);
            mockClients.Verify(c => c.All.OnPlayerTurn(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void PlayerTurn_ValidUser_ReturnsFalse()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            // Act
            var result = serverHub.PlayerTurn("2", "2");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task PlayerTurn_ExecutePlayerTurnUnsuccessful()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.ExecuteTurn(It.IsAny<Player>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(false);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.PlayerTurn("2", "2");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
            mockClients.Verify(c => c.Caller.OnPlayerAvailableEndTurn(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task PlayerTurn_ExecutePlayerTurnSuccessful()
        {
            // Arrange
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.ExecuteTurn(It.IsAny<Player>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.PlayerTurn("2", "2");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Never);
            mockClients.Verify(c => c.Caller.OnPlayerAvailableEndTurn(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void StartServer_ValidUser_ReturnsFalse()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.UserIdentifier).Returns((string)null);
            serverHub.Context = mockContext.Object;
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns<GameSession>(null);


            // Act
            var result = serverHub.StartServer("1");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task StartServer_SuccessfullyStartsServer()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.UserIdentifier).Returns("123");
            serverHub.Context = mockContext.Object;
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByCode(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.ExecuteTurn(It.IsAny<Player>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.StartServer("2");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Never);
            mockClients.Verify(c => c.All.OnGameStart(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task JoinServer_DoesntFindUser()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            // Act
            await serverHub.JoinServer("2", "5");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(403), Times.Once);
        }

        [TestMethod]
        public async Task JoinServer_DoesntFindGameSession()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns("123");
            serverHub.Context = mockContext.Object;
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByCode(It.IsAny<string>())).Returns((GameSession)null);

            // Act
            await serverHub.JoinServer("2", "5");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(404), Times.Once);
        }

        [TestMethod]
        public async Task JoinServer_SuccessfullyJoinsServer()
        {
            // Arrange
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns("123");
            serverHub.Context = mockContext.Object;
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByCode(It.IsAny<string>())).Returns(mockGameSession.Object);
            mockGameSession.Setup(g => g.ExecuteTurn(It.IsAny<Player>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            mockJsonConvert.Setup(j => j.Serialize(It.IsAny<GameSession>())).Returns("serializedGameSession");

            // Act
            await serverHub.JoinServer("2", "5");

            // Assert
            mockClients.Verify(c => c.Caller.BadRequest(403), Times.Never);
            mockClients.Verify(c => c.All.OnNewPlayerConnectedToServer(It.IsAny<string>()), Times.Once);
            mockClients.Verify(c => c.Caller.OnConnectToServer(It.IsAny<string>()), Times.Once);
        }
    }
}