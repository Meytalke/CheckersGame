using System;
using System.Windows.Forms;

namespace CheckersUI
{
    public partial class FormGameSettings : Form
    {
        public event Action OnStartGameRequested;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        public string PlayerOneName
        {
            get
            {
                return m_TextBoxPlayerOneName.Text;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return m_TextBoxPlayerTwoName.Text;
            }
        }

        public bool IsHumenPlayer
        {
            get
            {
                return m_CheckBoxPlayerTwoType.Checked;
            }
        }

        public int BoardSize
        {
            get
            {
                int size = 0;

                if (m_RadioButtonBoardSize6x6.Checked)
                {
                    size = 6;
                }
                else if (m_RadioButtonBoardSize8x8.Checked)
                {
                    size = 8;
                }
                else if (m_RadioButtonBoardSize10x10.Checked)
                {
                    size = 10;
                }

                return size;
            }
        }

        private void checkBoxPlayerTwoType_CheckedChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayerTwoName.Enabled = m_CheckBoxPlayerTwoType.Checked;
            if (!m_CheckBoxPlayerTwoType.Checked)
            {
                m_TextBoxPlayerTwoName.Text = "[Computer]";
            }
            else
            {
                m_TextBoxPlayerTwoName.Text = string.Empty;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            OnStartGameRequested?.Invoke();
        }
    }
}
