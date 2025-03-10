using System.Windows.Forms;

namespace CheckersUI
{
    partial class FormGameSettings
    {
        private System.ComponentModel.IContainer components = null;
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
        private void InitializeComponent()
        {
            this.m_LabelPlayers = new System.Windows.Forms.Label();
            this.m_LabelPlayerOne = new System.Windows.Forms.Label();
            this.m_LabelPlayerTwo = new System.Windows.Forms.Label();
            this.m_LabelBoardSize = new System.Windows.Forms.Label();
            this.m_TextBoxPlayerOneName = new System.Windows.Forms.TextBox();
            this.m_TextBoxPlayerTwoName = new System.Windows.Forms.TextBox();
            this.m_CheckBoxPlayerTwoType = new System.Windows.Forms.CheckBox();
            this.m_RadioButtonBoardSize6x6 = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonBoardSize8x8 = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonBoardSize10x10 = new System.Windows.Forms.RadioButton();
            this.m_ButtonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.m_LabelPlayers.AutoSize = true;
            this.m_LabelPlayers.Location = new System.Drawing.Point(20, 60);
            this.m_LabelPlayers.Name = "m_LabelPlayers";
            this.m_LabelPlayers.Size = new System.Drawing.Size(64, 20);
            this.m_LabelPlayers.TabIndex = 0;
            this.m_LabelPlayers.Text = "Players:";

            this.m_LabelPlayerOne.AutoSize = true;
            this.m_LabelPlayerOne.Location = new System.Drawing.Point(35, 90);
            this.m_LabelPlayerOne.Name = "m_LabelPlayerOne";
            this.m_LabelPlayerOne.Size = new System.Drawing.Size(69, 20);
            this.m_LabelPlayerOne.TabIndex = 1;
            this.m_LabelPlayerOne.Text = "Player 1:";

            this.m_LabelPlayerTwo.AutoSize = true;
            this.m_LabelPlayerTwo.Location = new System.Drawing.Point(55, 120);
            this.m_LabelPlayerTwo.Name = "m_LabelPlayerTwo";
            this.m_LabelPlayerTwo.Size = new System.Drawing.Size(69, 20);
            this.m_LabelPlayerTwo.TabIndex = 2;
            this.m_LabelPlayerTwo.Text = "Player 2:";

            this.m_LabelBoardSize.AutoSize = true;
            this.m_LabelBoardSize.Location = new System.Drawing.Point(20, 10);
            this.m_LabelBoardSize.Name = "m_LabelBoardSize";
            this.m_LabelBoardSize.Size = new System.Drawing.Size(91, 20);
            this.m_LabelBoardSize.TabIndex = 3;
            this.m_LabelBoardSize.Text = "Board Size:";

            this.m_TextBoxPlayerOneName.Location = new System.Drawing.Point(122, 84);
            this.m_TextBoxPlayerOneName.Name = "m_TextBoxPlayerOneName";
            this.m_TextBoxPlayerOneName.Size = new System.Drawing.Size(94, 26);
            this.m_TextBoxPlayerOneName.TabIndex = 4;

            this.m_TextBoxPlayerTwoName.Enabled = false;
            this.m_TextBoxPlayerTwoName.Location = new System.Drawing.Point(122, 120);
            this.m_TextBoxPlayerTwoName.Name = "m_TextBoxPlayerTwoName";
            this.m_TextBoxPlayerTwoName.Size = new System.Drawing.Size(94, 26);
            this.m_TextBoxPlayerTwoName.TabIndex = 5;
            this.m_TextBoxPlayerTwoName.Text = "[Computer]";

            this.m_CheckBoxPlayerTwoType.AutoSize = true;
            this.m_CheckBoxPlayerTwoType.Location = new System.Drawing.Point(35, 120);
            this.m_CheckBoxPlayerTwoType.Name = "m_CheckBoxPlayerTwoType";
            this.m_CheckBoxPlayerTwoType.CheckedChanged += checkBoxPlayerTwoType_CheckedChanged;
            this.m_CheckBoxPlayerTwoType.Size = new System.Drawing.Size(22, 21);

            this.m_RadioButtonBoardSize6x6.AutoSize = true;
            this.m_RadioButtonBoardSize6x6.Location = new System.Drawing.Point(32, 33);
            this.m_RadioButtonBoardSize6x6.Name = "m_RadioButtonBoardSize6x6";
            this.m_RadioButtonBoardSize6x6.Size = new System.Drawing.Size(59, 24);
            this.m_RadioButtonBoardSize6x6.TabIndex = 7;
            this.m_RadioButtonBoardSize6x6.TabStop = true;
            this.m_RadioButtonBoardSize6x6.Text = "6 x 6";
            this.m_RadioButtonBoardSize6x6.Checked = true;
            this.m_RadioButtonBoardSize6x6.UseVisualStyleBackColor = true;

            this.m_RadioButtonBoardSize8x8.AutoSize = true;
            this.m_RadioButtonBoardSize8x8.Location = new System.Drawing.Point(97, 33);
            this.m_RadioButtonBoardSize8x8.Name = "m_RadioButtonBoardSize8x8";
            this.m_RadioButtonBoardSize8x8.Size = new System.Drawing.Size(59, 24);
            this.m_RadioButtonBoardSize8x8.TabIndex = 8;
            this.m_RadioButtonBoardSize8x8.TabStop = true;
            this.m_RadioButtonBoardSize8x8.Text = "8 x 8";
            this.m_RadioButtonBoardSize8x8.UseVisualStyleBackColor = true;

            this.m_RadioButtonBoardSize10x10.AutoSize = true;
            this.m_RadioButtonBoardSize10x10.Location = new System.Drawing.Point(162, 33);
            this.m_RadioButtonBoardSize10x10.Name = "m_RadioButtonBoardSize10x10";
            this.m_RadioButtonBoardSize10x10.Size = new System.Drawing.Size(77, 24);
            this.m_RadioButtonBoardSize10x10.TabIndex = 9;
            this.m_RadioButtonBoardSize10x10.TabStop = true;
            this.m_RadioButtonBoardSize10x10.Text = "10 x 10";
            this.m_RadioButtonBoardSize10x10.UseVisualStyleBackColor = true;

            this.m_ButtonStart.Location = new System.Drawing.Point(139, 155);
            this.m_ButtonStart.Name = "m_ButtonStart";
            this.m_ButtonStart.Size = new System.Drawing.Size(77, 25);
            this.m_ButtonStart.TabIndex = 8;
            this.m_ButtonStart.Text = "Done";
            this.m_ButtonStart.Click += buttonStart_Click;

            this.ClientSize = new System.Drawing.Size(226, 192);
            this.Controls.Add(this.m_LabelPlayers);
            this.Controls.Add(this.m_LabelPlayerOne);
            this.Controls.Add(this.m_LabelPlayerTwo);
            this.Controls.Add(this.m_LabelBoardSize);
            this.Controls.Add(this.m_TextBoxPlayerOneName);
            this.Controls.Add(this.m_TextBoxPlayerTwoName);
            this.Controls.Add(this.m_CheckBoxPlayerTwoType);
            this.Controls.Add(this.m_RadioButtonBoardSize6x6);
            this.Controls.Add(this.m_RadioButtonBoardSize8x8);
            this.Controls.Add(this.m_RadioButtonBoardSize10x10);
            this.Controls.Add(this.m_ButtonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private Label m_LabelPlayers;
        private Label m_LabelPlayerOne;
        private Label m_LabelPlayerTwo;
        private Label m_LabelBoardSize;

        private TextBox m_TextBoxPlayerOneName;
        private TextBox m_TextBoxPlayerTwoName;

        private CheckBox m_CheckBoxPlayerTwoType;

        private RadioButton m_RadioButtonBoardSize6x6;
        private RadioButton m_RadioButtonBoardSize8x8;
        private RadioButton m_RadioButtonBoardSize10x10;

        private Button m_ButtonStart;
    }
}