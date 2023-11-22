using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Models;
using Moq;
using Moq.Protected;
using ProjServer;

namespace Tests
{
    [TestClass]
    public class ServerHubTests
    {
        private Mock<IGlobalData> mockGlobalData;
        private ServerHub serverHub;

        [TestInitialize]
        public void TestInitialize()
        {
            mockGlobalData = new Mock<IGlobalData>();
            serverHub = new ServerHub(mockGlobalData.Object);
        }

        [TestMethod]
        public void ValidateUser_ValidUser_ReturnsTrue()
        {
            mockGlobalData.Setup(g => g.FindPlayer(It.IsAny<string>())).Returns(new Player());
            mockGlobalData.Setup(g => g.FindGameSessionByPlayerId(It.IsAny<string>())).Returns(new GameSession());

            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns("testConnectionId");
            serverHub.Context = mockContext.Object;

            var result = serverHub.ValidateUser();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUser_InvalidUserId_ReturnsFalse()
        {
            var mockContext = new Mock<HubCallerContext>();
            mockContext.SetupGet(c => c.ConnectionId).Returns((string)null);
            serverHub.Context = mockContext.Object;

            var result = serverHub.ValidateUser();

            Assert.IsFalse(result);
        }
    }
}