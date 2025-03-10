using System.Windows.Forms;
using System.Drawing;

namespace CheckersUI
{
    partial class FormGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_LabelPlayerOne = new Label();
            this.m_LabelPlayerTwo = new Label();
            this.m_LabelPlayerOneScore = new Label();
            this.m_LabelPlayerTwoScore = new Label();

            this.m_LabelPlayerOne.AutoSize = true;
            this.m_LabelPlayerOne.Name = "m_LabelPlayerOne";
            this.m_LabelPlayerOne.TabIndex = 0;
            this.m_LabelPlayerOne.Text = "Player 1:";
            this.m_LabelPlayerOne.Font = new Font(m_LabelPlayerOne.Font.FontFamily, 12, FontStyle.Bold);

            this.m_LabelPlayerOneScore.AutoSize = true;
            this.m_LabelPlayerOneScore.Name = "m_LabelPlayerOneScore";
            this.m_LabelPlayerOneScore.Size = new Size(20, 20);
            this.m_LabelPlayerOneScore.TabIndex = 1;
            this.m_LabelPlayerOneScore.Text = "0";
            this.m_LabelPlayerOneScore.Font = new Font(m_LabelPlayerOneScore.Font.FontFamily, 12, FontStyle.Bold);

            this.m_LabelPlayerTwo.AutoSize = true;
            this.m_LabelPlayerTwo.Name = "m_LabelPlayerTwo";
            this.m_LabelPlayerTwo.Size = new Size(50, 50);
            this.m_LabelPlayerTwo.TabIndex = 2;
            this.m_LabelPlayerTwo.Text = "Player 2:";
            this.m_LabelPlayerTwo.Font = new Font(m_LabelPlayerTwo.Font.FontFamily, 12, FontStyle.Bold);

            this.m_LabelPlayerTwoScore.AutoSize = true;
            this.m_LabelPlayerTwoScore.Name = "m_LabelPlayerTwoScore";
            this.m_LabelPlayerTwoScore.Size = new System.Drawing.Size(20, 20);
            this.m_LabelPlayerTwoScore.TabIndex = 3;
            this.m_LabelPlayerTwoScore.Text = "0";
            this.m_LabelPlayerTwoScore.Font = new Font(m_LabelPlayerTwoScore.Font.FontFamily, 12, FontStyle.Bold);

            this.SuspendLayout();
            this.Controls.Add(m_LabelPlayerOne);
            this.Controls.Add(m_LabelPlayerOneScore);
            this.Controls.Add(m_LabelPlayerTwo);
            this.Controls.Add(m_LabelPlayerTwoScore);
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 450);
            this.Location = new Point(300, 300);
            this.Name = "GameForm";
            this.Text = "Damka";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion
        private Label m_LabelPlayerOne;
        private Label m_LabelPlayerTwo;
        private Label m_LabelPlayerOneScore;
        private Label m_LabelPlayerTwoScore;
        private Button[,] m_BoardButtons;
        private const int k_ButtonSize = 50;
    }
}