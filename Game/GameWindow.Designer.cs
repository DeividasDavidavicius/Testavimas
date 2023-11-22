namespace Game
{
    partial class GameWindow
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startingPanel = new Panel();
            GameNameLabel = new Label();
            nameTextBox = new TextBox();
            joinServerBtn = new Button();
            joinServerTextBox = new TextBox();
            createServerBtn = new Button();
            createServerPanel = new Panel();
            startServerBtn = new Button();
            serverCodeLabel = new Label();
            joinedServerPanel = new Panel();
            waitingLabel = new Label();
            joinedServerLabel = new Label();
            gamePanel = new Panel();
            gameInfoPanel = new Panel();
            undoBtn = new Button();
            endTurnBtn = new Button();
            gameInfoLabel = new Label();
            gridPanel = new Panel();
            endPanel = new Panel();
            endText = new Label();
            startingPanel.SuspendLayout();
            createServerPanel.SuspendLayout();
            joinedServerPanel.SuspendLayout();
            gamePanel.SuspendLayout();
            gameInfoPanel.SuspendLayout();
            endPanel.SuspendLayout();
            SuspendLayout();
            // 
            // startingPanel
            // 
            startingPanel.Controls.Add(GameNameLabel);
            startingPanel.Controls.Add(nameTextBox);
            startingPanel.Controls.Add(joinServerBtn);
            startingPanel.Controls.Add(joinServerTextBox);
            startingPanel.Controls.Add(createServerBtn);
            startingPanel.Dock = DockStyle.Fill;
            startingPanel.Location = new Point(0, 0);
            startingPanel.Name = "startingPanel";
            startingPanel.Size = new Size(909, 611);
            startingPanel.TabIndex = 0;
            // 
            // endPanel
            // ;
            endPanel.Controls.Add(endText);
            endPanel.Dock = DockStyle.Fill;
            endPanel.Location = new Point(0, 0);
            endPanel.Name = "endPanel";
            endPanel.Size = new Size(909, 611);
            endPanel.TabIndex = 0;
            // 
            // GameNameLabel
            // 
            GameNameLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            GameNameLabel.Location = new Point(5, 59);
            GameNameLabel.Margin = new Padding(0);
            GameNameLabel.Name = "GameNameLabel";
            GameNameLabel.Size = new Size(900, 54);
            GameNameLabel.TabIndex = 4;
            GameNameLabel.Text = "Kantaplis";
            GameNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // endText
            // 
            endText.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            endText.Location = new Point(5, 59);
            endText.Margin = new Padding(0);
            endText.Name = "endText";
            endText.Size = new Size(900, 54);
            endText.TabIndex = 4;
            endText.Text = "GAME ENDED";
            endText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameTextBox
            // 
            nameTextBox.BackColor = SystemColors.Window;
            nameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nameTextBox.Location = new Point(350, 120);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Username";
            nameTextBox.Size = new Size(200, 29);
            nameTextBox.TabIndex = 3;
            nameTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // joinServerBtn
            // 
            joinServerBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            joinServerBtn.Location = new Point(350, 290);
            joinServerBtn.Name = "joinServerBtn";
            joinServerBtn.Size = new Size(200, 33);
            joinServerBtn.TabIndex = 2;
            joinServerBtn.Text = "Join Server";
            joinServerBtn.UseVisualStyleBackColor = true;
            joinServerBtn.Click += joinServerBtn_Click;
            // 
            // joinServerTextBox
            // 
            joinServerTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            joinServerTextBox.Location = new Point(350, 253);
            joinServerTextBox.Name = "joinServerTextBox";
            joinServerTextBox.PlaceholderText = "Server Id";
            joinServerTextBox.Size = new Size(200, 29);
            joinServerTextBox.TabIndex = 1;
            joinServerTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // createServerBtn
            // 
            createServerBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            createServerBtn.Location = new Point(350, 164);
            createServerBtn.Name = "createServerBtn";
            createServerBtn.Size = new Size(200, 35);
            createServerBtn.TabIndex = 0;
            createServerBtn.Text = "Create Server";
            createServerBtn.UseVisualStyleBackColor = true;
            createServerBtn.Click += createServerBtn_Click;
            // 
            // createServerPanel
            // 
            createServerPanel.Controls.Add(startServerBtn);
            createServerPanel.Controls.Add(serverCodeLabel);
            createServerPanel.Dock = DockStyle.Fill;
            createServerPanel.Location = new Point(0, 0);
            createServerPanel.Name = "createServerPanel";
            createServerPanel.Size = new Size(909, 611);
            createServerPanel.TabIndex = 3;
            // 
            // startServerBtn
            // 
            startServerBtn.Location = new Point(368, 224);
            startServerBtn.Name = "startServerBtn";
            startServerBtn.Size = new Size(75, 23);
            startServerBtn.TabIndex = 1;
            startServerBtn.Text = "Start Server";
            startServerBtn.UseVisualStyleBackColor = true;
            startServerBtn.Click += startServerBtn_Click;
            // 
            // serverCodeLabel
            // 
            serverCodeLabel.AutoSize = true;
            serverCodeLabel.Location = new Point(368, 195);
            serverCodeLabel.Name = "serverCodeLabel";
            serverCodeLabel.Size = new Size(73, 15);
            serverCodeLabel.TabIndex = 0;
            serverCodeLabel.Text = "Server Code:";
            // 
            // joinedServerPanel
            // 
            joinedServerPanel.Controls.Add(waitingLabel);
            joinedServerPanel.Controls.Add(joinedServerLabel);
            joinedServerPanel.Dock = DockStyle.Fill;
            joinedServerPanel.Location = new Point(0, 0);
            joinedServerPanel.Name = "joinedServerPanel";
            joinedServerPanel.Size = new Size(909, 611);
            joinedServerPanel.TabIndex = 2;
            // 
            // waitingLabel
            // 
            waitingLabel.AutoSize = true;
            waitingLabel.Location = new Point(346, 203);
            waitingLabel.Name = "waitingLabel";
            waitingLabel.Size = new Size(132, 15);
            waitingLabel.TabIndex = 1;
            waitingLabel.Text = "Waiting for host to start";
            // 
            // joinedServerLabel
            // 
            joinedServerLabel.AutoSize = true;
            joinedServerLabel.Location = new Point(368, 172);
            joinedServerLabel.Name = "joinedServerLabel";
            joinedServerLabel.Size = new Size(82, 15);
            joinedServerLabel.TabIndex = 0;
            joinedServerLabel.Text = "Joined Server: ";
            // 
            // gamePanel
            // 
            gamePanel.Controls.Add(gameInfoPanel);
            gamePanel.Controls.Add(gridPanel);
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.Location = new Point(0, 0);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(909, 611);
            gamePanel.TabIndex = 3;
            // 
            // gameInfoPanel
            // 
            gameInfoPanel.Controls.Add(undoBtn);
            gameInfoPanel.Controls.Add(endTurnBtn);
            gameInfoPanel.Controls.Add(gameInfoLabel);
            gameInfoPanel.Location = new Point(605, 0);
            gameInfoPanel.Name = "gameInfoPanel";
            gameInfoPanel.Size = new Size(300, 600);
            gameInfoPanel.TabIndex = 1;
            // 
            // undoBtn
            // 
            undoBtn.Enabled = false;
            undoBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            undoBtn.Location = new Point(3, 557);
            undoBtn.Name = "undoBtn";
            undoBtn.Size = new Size(88, 40);
            undoBtn.TabIndex = 2;
            undoBtn.Text = "Undo";
            undoBtn.UseVisualStyleBackColor = true;
            undoBtn.Click += undoBtn_Click;
            // 
            // endTurnBtn
            // 
            endTurnBtn.Enabled = false;
            endTurnBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            endTurnBtn.Location = new Point(97, 557);
            endTurnBtn.Name = "endTurnBtn";
            endTurnBtn.Size = new Size(200, 40);
            endTurnBtn.TabIndex = 1;
            endTurnBtn.Text = "End Turn";
            endTurnBtn.UseVisualStyleBackColor = true;
            endTurnBtn.Click += endTurnBtn_Click;
            // 
            // gameInfoLabel
            // 
            gameInfoLabel.CausesValidation = false;
            gameInfoLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            gameInfoLabel.Location = new Point(3, 9);
            gameInfoLabel.MaximumSize = new Size(1000, 1000);
            gameInfoLabel.Name = "gameInfoLabel";
            gameInfoLabel.Size = new Size(290, 33);
            gameInfoLabel.TabIndex = 0;
            gameInfoLabel.Text = "Game Info Panel";
            gameInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gridPanel
            // 
            gridPanel.BorderStyle = BorderStyle.FixedSingle;
            gridPanel.Location = new Point(0, 0);
            gridPanel.Name = "gridPanel";
            gridPanel.Size = new Size(600, 600);
            gridPanel.TabIndex = 0;
            gridPanel.MouseDown += OnMouseClick;
            // 
            // GameWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(909, 611);
            Controls.Add(gamePanel);
            Controls.Add(startingPanel);
            Controls.Add(joinedServerPanel);
            Controls.Add(createServerPanel);
            Controls.Add(endPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "GameWindow";
            RightToLeftLayout = true;
            Text = "Kantaplis";
            MouseClick += OnMouseClick;
            startingPanel.ResumeLayout(false);
            startingPanel.PerformLayout();
            endPanel.ResumeLayout(false);
            endPanel.PerformLayout();
            createServerPanel.ResumeLayout(false);
            createServerPanel.PerformLayout();
            joinedServerPanel.ResumeLayout(false);
            joinedServerPanel.PerformLayout();
            gamePanel.ResumeLayout(false);
            gameInfoPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel startingPanel;
        private Panel createServerPanel;
        private Button joinServerBtn;
        private TextBox joinServerTextBox;
        private Button createServerBtn;
        private Button startServerBtn;
        private Label serverCodeLabel;
        private Panel joinedServerPanel;
        private Label waitingLabel;
        private Label joinedServerLabel;
        private Panel gamePanel;
        private Panel gridPanel;
        private Panel gameInfoPanel;
        private Label gameInfoLabel;
        private TextBox nameTextBox;
        private Label GameNameLabel;
        private Button undoBtn;
        private Button endTurnBtn;
        private Panel endPanel;
        private Label endText;
    }
}