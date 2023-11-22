using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Cells;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Game
{
    public partial class GameWindow : Form
    {
        private GameClientFacade gameClientFacade;

        //USE THIS FOR GETTING PLAYER LABEL
        private Dictionary<string, Label> playerLabelDict = new Dictionary<string, Label>();

        private Action onCreateServerHandler;
        private Action OnConnectToServerActionHandler;
        private Action OnNewPlayerConnectedToServerHandler;
        private Action OnGameStartHandler;
        private Action OnPlayerTurnHandler;
        private Action OnPlayerTurnInfoHanlder;
        private Action OnPlayerAvailableEndTurnHanlder;


        public GameWindow()
        {
            gameClientFacade = new GameClientFacade();
            gameClientFacade.InitializeConnection("https://localhost:7028/serverHub");
            setupConnectionHandlers();
            InitializeComponent();
            createServerPanel.Hide();
            joinedServerPanel.Hide();
            gamePanel.Hide();
            endPanel.Hide();
        }

        private void setupConnectionHandlers()
        {
            onCreateServerHandler = () =>
            {
                this.serverCodeLabel.Invoke((MethodInvoker)(() => this.serverCodeLabel.Text = $"Server Code: {gameClientFacade.GetSessionCode()}"));
                SetupInfoLabels(gameClientFacade.GetAllPlayers());
            };

            OnConnectToServerActionHandler = () =>
            {
                this.joinedServerLabel.Invoke((MethodInvoker)(() => this.joinedServerLabel.Text = $"Joined Server: {gameClientFacade.GetSessionCode()}"));
                SetupInfoLabels(gameClientFacade.GetAllPlayers());
            };

            OnNewPlayerConnectedToServerHandler = () =>
            {
                if (gameClientFacade.GetPlayer() == null) return;
                SetupInfoLabels(gameClientFacade.GetAllPlayers());
            };

            OnGameStartHandler = () =>
            {
                this.joinedServerPanel.Invoke((MethodInvoker)(() => this.joinedServerPanel.Hide()));
                this.gamePanel.Invoke((MethodInvoker)(() => this.gamePanel.Show()));
                SetupInfoLabels(gameClientFacade.GetAllPlayers());
                Invalidate();
            };

            OnPlayerTurnHandler = () =>
            {
                SetupInfoLabels(gameClientFacade.GetAllPlayers());
                if (gameClientFacade.clientManager.gameSession?.gameEnd != null)
                {
                    this.gamePanel.Invoke((MethodInvoker)(() => this.gamePanel.Hide()));
                    this.endPanel.Invoke((MethodInvoker)(() => this.endPanel.Show()));
                    this.endText.Invoke((MethodInvoker)(() => this.endText.Text = gameClientFacade.clientManager.gameSession.gameEnd));
                }
                Invalidate();
            };

            OnPlayerAvailableEndTurnHanlder = () =>
            {
                this.endTurnBtn.Invoke((MethodInvoker)(() => this.endTurnBtn.Enabled = true));
                this.undoBtn.Invoke((MethodInvoker)(() => this.undoBtn.Enabled = true));

                if(gameClientFacade.clientManager.gameSession?.gameEnd != null)
                {
                    this.gamePanel.Invoke((MethodInvoker)(() => this.gamePanel.Hide()));
                    this.endPanel.Invoke((MethodInvoker)(() => this.endPanel.Show()));
                    this.endText.Invoke((MethodInvoker)(() => this.endText.Text = gameClientFacade.clientManager.gameSession.gameEnd));
                }

                SetupInfoLabels(gameClientFacade.GetAllPlayers());
                Invalidate();
            };

            OnPlayerTurnInfoHanlder = () =>
            {
                //TODO: show turn info
            };

            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.onCreateServer, onCreateServerHandler);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnConnectToServer, OnConnectToServerActionHandler);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnNewPlayerConnectedToServer, OnNewPlayerConnectedToServerHandler);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnGameStart, OnGameStartHandler);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnPlayerTurn, OnPlayerTurnHandler);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnPlayerTurnInfo, OnPlayerTurnInfoHanlder);
            gameClientFacade.SetupConnectionEndpoint(ConnectionEndpointEnum.OnPlayerAvailableEndTurnHanlder, OnPlayerAvailableEndTurnHanlder);
        }

        private Label CreatePlayerLabel(Player player, int index)
        {
            Label playerInfoLabel = new Label();
            gameInfoPanel.Invoke((MethodInvoker)(() => this.gameInfoPanel.Controls.Add(playerInfoLabel)));
            playerInfoLabel.Invoke((MethodInvoker)(() =>
            {
                playerInfoLabel.Text = String.Format("Player: {0} {1} HP, {2} Damage", player.username, player.healthPoints, player.damagePoints);
                playerInfoLabel.CausesValidation = false;
                playerInfoLabel.Location = new Point(3, 30 + 15 * (index + 1) + 5);
                playerInfoLabel.MaximumSize = new Size(1000, 1000);
                playerInfoLabel.Size = new Size(290, 15);
                playerInfoLabel.TabIndex = 0;
                playerInfoLabel.Padding = new Padding(10, 0, 0, 0);
                playerInfoLabel.TextAlign = ContentAlignment.MiddleLeft;
            }));
            return playerInfoLabel;
        }

        private void SetupInfoLabels(List<Player> players)
        {
            foreach (Label label in playerLabelDict.Values)
            {
                gameInfoPanel.Invoke((MethodInvoker)(() => this.gameInfoPanel.Controls.Remove(label)));
            }

            playerLabelDict.Clear();
            for (int i = 0; i < players.Count; i++)
            {
                Label playerLabel = CreatePlayerLabel(players[i], i);
                playerLabelDict.Add(players[i].id, playerLabel);
            }
        }

        private void createServerBtn_Click(object sender, EventArgs e)
        {
            string username = nameTextBox.Text.Trim();
            if (username.Length == 0)
            {
                return;
            }
            gameClientFacade.OnCreateServer(username);
            startingPanel.Hide();
            joinedServerPanel.Hide();
            gamePanel.Hide();
            createServerPanel.Show();
            endPanel.Hide();
        }

        private void joinServerBtn_Click(object sender, EventArgs e)
        {
            string username = nameTextBox.Text.Trim();
            if (joinServerTextBox.Text.Trim().Length == 0 || username.Length == 0)
            {
                return;
            }
            gameClientFacade.OnJoinServer(joinServerTextBox.Text, username);
            startingPanel.Hide();
            createServerPanel.Hide();
            gamePanel.Hide();
            joinedServerPanel.Show();
            endPanel.Hide();
        }

        private void startServerBtn_Click(object sender, EventArgs e)
        {
            gameClientFacade.OnStartServer();
            startingPanel.Hide();
            createServerPanel.Hide();
            joinedServerPanel.Hide();
            gamePanel.Show();
            endPanel.Hide();
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            gameClientFacade.OnMouseClick(sender, e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            gameClientFacade.DrawGrid(gridPanel);
        }

        private void endTurnBtn_Click(object sender, EventArgs e)
        {
            gameClientFacade.OnEndTurn();
            endTurnBtn.Enabled = false;
            undoBtn.Enabled = false;
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            gameClientFacade.OnUndo();
            endTurnBtn.Enabled = false;
            undoBtn.Enabled = false;
        }
    }
}