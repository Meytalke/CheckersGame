using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CheckersLogic;

namespace CheckersUI
{
    public partial class FormGame : Form
    {
        private Position m_StartPosition;
        private List<Position> m_ValidMoves;
        private Timer m_ComputersMoveTimer;
        private Move m_ComputerMove;
        private Timer m_ComputersMoveCaptureTimer;
        private Position m_CurrentCapturePosition;
        private bool m_IsCaptureSequence = false;
        private Game m_Game;
        private FormGameSettings m_FormGameSettings = new FormGameSettings();

        public FormGame()
        {
            InitializeComponent();
            m_FormGameSettings.OnStartGameRequested += validateAndStartGame;
        }

        private void validateAndStartGame()
        {
            if (validateSettings(m_FormGameSettings))
            {
                m_FormGameSettings.DialogResult = DialogResult.OK;
                m_FormGameSettings.Close();
                Player playerOne = new Player(m_FormGameSettings.PlayerOneName, eCoinType.X, ePlayerType.HumanPlayer);
                Player playerTwo = new Player(
                    m_FormGameSettings.IsHumenPlayer ? m_FormGameSettings.PlayerTwoName : "Computer",
                    eCoinType.O,
                    m_FormGameSettings.IsHumenPlayer ? ePlayerType.HumanPlayer : ePlayerType.ComputerPlayer);

                m_Game = new Game(m_FormGameSettings.BoardSize, playerOne, playerTwo);
                m_LabelPlayerOne.BorderStyle = BorderStyle.FixedSingle;
                initializeBoardButtons();
                startNewGame();
            }
        }

        public bool ShowGameSettings()
        {
            return m_FormGameSettings.ShowDialog() == DialogResult.OK;
        }

        private bool validateSettings(FormGameSettings i_GameSettings)
        {
            bool isValid = true;

            if (i_GameSettings.BoardSize == 0)
            {
                MessageBox.Show("Please select a valid board size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            else if (!isValidName(i_GameSettings.PlayerOneName))
            {
                isValid = false;
            }
            else if (i_GameSettings.IsHumenPlayer && !isValidName(i_GameSettings.PlayerTwoName))
            {
                isValid = false;
            }

            if (isValid)
            {
                i_GameSettings.DialogResult = DialogResult.OK;
                i_GameSettings.Close();
            }

            return isValid;
        }

        private bool isValidName(string i_PlayerName)
        {
            bool isValidName = false;

            if (!Player.IsValidName(i_PlayerName))
            {
                MessageBox.Show($"{i_PlayerName} has an invalid name!{Environment.NewLine} Please enter a name (less than 20 characters, no spaces, only letters).",
            "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                isValidName = true;
            }

            return isValidName;
        }

        private void initializeBoardButtons()
        {
            if (m_Game != null)
            {
                m_BoardButtons = new Button[m_Game.Board.Size, m_Game.Board.Size];
                for (int row = 0; row < m_Game.Board.Size; row++)
                {
                    for (int col = 0; col < m_Game.Board.Size; col++)
                    {
                        m_BoardButtons[row, col] = new Button();
                        m_BoardButtons[row, col].Width = k_ButtonSize;
                        m_BoardButtons[row, col].Height = k_ButtonSize;
                        m_BoardButtons[row, col].Location = new Point(col * k_ButtonSize, row * k_ButtonSize + k_ButtonSize);
                        m_BoardButtons[row, col].Click += boardButton_Click;
                        m_BoardButtons[row, col].FlatStyle = FlatStyle.Flat;
                        m_BoardButtons[row, col].FlatAppearance.BorderSize = 1;
                        m_BoardButtons[row, col].FlatAppearance.BorderColor = Color.Black;
                        if ((row + col) % 2 == 0)
                        {
                            m_BoardButtons[row, col].BackColor = Color.Gray;
                            m_BoardButtons[row, col].Enabled = false;
                        }
                        else
                        {
                            m_BoardButtons[row, col].BackColor = Color.White;
                        }

                        this.Controls.Add(m_BoardButtons[row, col]);
                    }
                }

                setFormSizeAndPositions();
                updateBoardDisplay();
            }
        }

        private void setFormSizeAndPositions()
        {
            int totalHeight = (m_Game.Board.Size * k_ButtonSize) + (k_ButtonSize * 2);
            int totalWidth = (int)(totalHeight - k_ButtonSize * 1.5);
            int labelPlayerOneLocation = m_Game.Board.Size == 6 ? k_ButtonSize / 2 : k_ButtonSize;
            int labelPlayerTwoLocation = m_Game.Board.Size == 6 ? m_Game.Board.Size * k_ButtonSize - (int)(2.5 * k_ButtonSize) :
                m_Game.Board.Size * k_ButtonSize - 3 * k_ButtonSize;

            this.m_LabelPlayerOne.Location = new Point(labelPlayerOneLocation, 20);
            this.m_LabelPlayerOne.Text = m_Game.PlayerOne.Name + ":";
            this.m_LabelPlayerOneScore.Location = new Point(m_LabelPlayerOne.Location.X + this.m_LabelPlayerOne.Text.Length * 10 + 10, 20);
            this.m_LabelPlayerTwo.Location = new Point(labelPlayerTwoLocation, 20);
            this.m_LabelPlayerTwo.Text = m_Game.PlayerTwo.Name + ":";
            this.m_LabelPlayerTwoScore.Location = new Point(m_LabelPlayerTwo.Location.X + this.m_LabelPlayerTwo.Text.Length * 10 + 10, 20);
            this.Size = new Size(totalWidth, totalHeight);
            this.Location = new Point(300, 300);
        }

        private void updateBoardDisplay()
        {
            if (m_Game != null)
            {
                for (int row = 0; row < m_Game.Board.Size; row++)
                {
                    for (int col = 0; col < m_Game.Board.Size; col++)
                    {
                        Button currentButton = m_BoardButtons[row, col];

                        currentButton.Image = null;
                        currentButton.AutoSize = false;
                        switch (m_Game.Board.CoinBoard[row, col].CoinType)
                        {
                            case eCoinType.X:
                                m_BoardButtons[row, col].Image = Properties.Resources.Xsoldier;
                                break;
                            case eCoinType.O:
                                m_BoardButtons[row, col].Image = Properties.Resources.Osoldier;

                                break;
                            case eCoinType.K:
                                m_BoardButtons[row, col].Image = Properties.Resources.Ksoldier;
                                break;
                            case eCoinType.U:
                                m_BoardButtons[row, col].Image = Properties.Resources.Usoldier;
                                break;
                            default:
                                m_BoardButtons[row, col].Text = "";
                                break;
                        }
                    }
                }
            }
        }

        private void startNewGame()
        {
            updateBoardDisplay();
            if (m_Game.CurrentPlayer.PlayerType == ePlayerType.ComputerPlayer)
            {
                handleComputerTurn();
            }
        }

        private void highlightValidMoves()
        {
            if (m_ValidMoves != null)
            {
                foreach (Position move in m_ValidMoves)
                {
                    m_BoardButtons[move.RowPosition, move.ColPosition].BackColor = Color.CadetBlue;
                }
            }
        }

        private void resetMoveSelection()
        {
            resetButtonColors();
            m_StartPosition = null;
            m_ValidMoves = null;
        }

        private void resetButtonColors()
        {
            for (int row = 0; row < m_Game.Board.Size; row++)
            {
                for (int col = 0; col < m_Game.Board.Size; col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        m_BoardButtons[row, col].BackColor = Color.FromArgb(100, 100, 100);
                    }
                    else
                    {
                        m_BoardButtons[row, col].BackColor = Color.White;
                    }
                }
            }
        }

        private void boardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Position clickedPosition = getButtonClickedPosition(clickedButton);

            if (clickedPosition != null)
            {
                if (m_Game.CurrentPlayer.PlayerType == ePlayerType.HumanPlayer)
                {
                    if (m_StartPosition == null)
                    {
                        handleFirstClick(clickedPosition);
                    }
                    else
                    {
                        animateButtonPress(clickedButton);
                        handleSecondClick(clickedPosition);
                        resetMoveSelection();
                    }
                }
            }
        }

        private void animateButtonPress(Button i_Button)
        {
            int originalWidth = i_Button.Width;
            int originalHeight = i_Button.Height;
            Point originalLocation = i_Button.Location;
            int currentFrame = 0;
            Timer timer = new Timer();

            timer.Interval = 30;
            timer.Tick += (s, args) =>
            {
                timer_Tick(i_Button, ref currentFrame, originalWidth, originalHeight, originalLocation, timer);
            };

            timer.Start();
        }

        private void timer_Tick(Button i_Button, ref int io_CurrentFrame, int i_OriginalWidth, int i_OriginalHeight,
            Point i_OriginalLocation, Timer i_Timer)
        {
            if (io_CurrentFrame < 16)
            {
                int offset = (io_CurrentFrame < 8) ? 1 : -1;

                i_Button.Width += offset;
                i_Button.Height += offset;
                i_Button.Location = new Point(i_Button.Location.X - offset, i_Button.Location.Y - offset);
                io_CurrentFrame++;
            }
            else
            {
                i_Button.Width = i_OriginalWidth;
                i_Button.Height = i_OriginalHeight;
                i_Button.Location = i_OriginalLocation;
                i_Timer.Stop();
                i_Timer.Dispose();
            }
        }

        private Position getButtonClickedPosition(Button i_ClickedButton)
        {
            Position position = null;

            for (int row = 0; row < m_Game.Board.Size; row++)
            {
                for (int col = 0; col < m_Game.Board.Size; col++)
                {
                    if (m_BoardButtons[row, col] == i_ClickedButton)
                    {
                        position = new Position(row, col);
                    }
                }
            }

            return position;
        }

        private void handleFirstClick(Position i_ClickedPosition)
        {
            if (isCurrentPlayerCoin(i_ClickedPosition))
            {
                resetButtonColors();
                m_StartPosition = i_ClickedPosition;
                m_BoardButtons[i_ClickedPosition.RowPosition, i_ClickedPosition.ColPosition].BackColor = Color.Blue;
                getValidMovesFromPosition();
                highlightValidMoves();
            }
        }

        private void getValidMovesFromPosition()
        {
            m_ValidMoves = m_Game.getValidMovesFromPosition(m_StartPosition, eMoveType.Regular);
            m_ValidMoves.AddRange(m_Game.getValidMovesFromPosition(m_StartPosition, eMoveType.Capture));
        }

        private bool isCurrentPlayerCoin(Position i_Position)
        {
            eCoinType coinTypeAtPosition = m_Game.Board.CoinBoard[i_Position.RowPosition, i_Position.ColPosition].CoinType;
            eCoinType currentPlayerCoinType = m_Game.CurrentPlayer.CoinType;
            bool isCurrentPlayerCoin = false;

            switch (currentPlayerCoinType)
            {
                case eCoinType.X:
                    isCurrentPlayerCoin = coinTypeAtPosition == eCoinType.X || coinTypeAtPosition == eCoinType.K;
                    break;
                case eCoinType.O:
                    isCurrentPlayerCoin = coinTypeAtPosition == eCoinType.O || coinTypeAtPosition == eCoinType.U;
                    break;
            }

            return isCurrentPlayerCoin;
        }

        private void handleSecondClick(Position i_ClickedPosition)
        {
            if (m_IsCaptureSequence)
            {
                handleCaptureMove(i_ClickedPosition);
            }
            else if (m_ValidMoves.Count > 0)
            {
                if (i_ClickedPosition.RowPosition != m_StartPosition.RowPosition ||
                    i_ClickedPosition.ColPosition != m_StartPosition.ColPosition)
                {
                    if (performMove(i_ClickedPosition))
                    {
                        handleTurnEnd();
                    }
                }
                else
                {
                    resetMoveSelection();
                }
            }
            else
            {
                MessageBox.Show("Invalid move!");
                resetMoveSelection();
            }
        }

        private void handleCaptureMove(Position i_ClickedPosition)
        {
            bool isSuccessfulCapture = false;

            do
            {
                Move captureMove = new Move(m_CurrentCapturePosition, i_ClickedPosition);

                if (m_StartPosition.RowPosition != captureMove.StartPosition.RowPosition ||
                    m_StartPosition.ColPosition != captureMove.StartPosition.ColPosition)
                {
                    MessageBox.Show($"Invalid move! please capture from ({m_CurrentCapturePosition.RowPosition},{m_CurrentCapturePosition.ColPosition}).");
                    resetMoveSelection();
                }
                else
                {
                    isSuccessfulCapture = processMoveCapture(ref m_CurrentCapturePosition, captureMove);
                    if (isSuccessfulCapture)
                    {
                        m_StartPosition = i_ClickedPosition;

                        if (m_Game.HasAdditionalCapture(m_StartPosition))
                        {
                            resetMoveSelection();
                            highlightValidMoves();
                        }
                        else
                        {
                            m_IsCaptureSequence = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid move!");
                        resetMoveSelection();
                    }
                }

            } while (isSuccessfulCapture && m_Game.HasAdditionalCapture(m_StartPosition));

            if (isSuccessfulCapture)
            {
                handleTurnEnd();
            }
        }

        private void handleComputerTurn()
        {
            m_ComputersMoveTimer = new Timer();
            m_ComputersMoveTimer.Interval = 400;
            m_ComputersMoveTimer.Tick += performComputerTurn;
            m_ComputerMove = m_Game.GetBestMoveForAI(m_Game.CurrentPlayer);
            if (m_ComputerMove == null)
            {
                m_Game.GameStatus = eGameStatus.VictoryPlayerOne;
                gameSummary();
            }
            else
            {
                m_ComputersMoveTimer.Start();
            }       
        }

        private void performComputerTurn(object sender, EventArgs e)
        {
            m_ComputersMoveTimer.Stop();
            m_StartPosition = m_ComputerMove.StartPosition;
            performMove(m_ComputerMove.EndPosition);
            handleTurnEnd();
        }

        private bool performMove(Position i_ClickedPosition)
        {
            bool isValid = false;

            m_Game.ExecuteMoveWithResult(m_Game.Board, m_Game.CurrentPlayer, m_StartPosition, i_ClickedPosition,
                out bool isCapture, out bool validMove, out bool mustCapture, true);
            if (!validMove)
            {
                if (m_Game.CurrentPlayer.PlayerType == ePlayerType.HumanPlayer)
                {
                    if (mustCapture)
                    {
                        MessageBox.Show("You must capture when a jump is available.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid move");
                    }
                }
            }
            else
            {
                Button endButton = m_BoardButtons[i_ClickedPosition.RowPosition, i_ClickedPosition.ColPosition];

                animateButtonPress(endButton);
                updateBoardDisplay();
                if (isCapture && m_Game.HasAdditionalCapture(i_ClickedPosition))
                {
                    handleMultipleCaptures(ref i_ClickedPosition);
                }

                isValid = true;
            }

            resetMoveSelection();

            return isValid;
        }

        private void handleMultipleCaptures(ref Position io_CurrentPosition)
        {
            m_CurrentCapturePosition = io_CurrentPosition;
            m_IsCaptureSequence = true;
            if (m_Game.CurrentPlayer.PlayerType == ePlayerType.ComputerPlayer && m_Game.HasAdditionalCapture(m_CurrentCapturePosition))
            {
                m_ComputerMove = m_Game.GetBestMoveForAI(m_Game.CurrentPlayer, io_CurrentPosition, true);
                if (m_ComputerMove != null &&
                    m_ComputerMove.StartPosition.RowPosition == m_CurrentCapturePosition.RowPosition &&
                    m_ComputerMove.StartPosition.ColPosition == m_CurrentCapturePosition.ColPosition)
                {
                    startComputerCaptureTimer();
                }
            }
        }

        private void startComputerCaptureTimer()
        {
            if (m_ComputersMoveCaptureTimer == null)
            {
                m_ComputersMoveCaptureTimer = new Timer();
                m_ComputersMoveCaptureTimer.Interval = 500;
                m_ComputersMoveCaptureTimer.Tick += computersMoveCaptureTimer_Tick; ;
            }
            else
            {
                m_ComputersMoveCaptureTimer.Stop();
            }

            m_ComputersMoveCaptureTimer.Start();
        }

        private void computersMoveCaptureTimer_Tick(object sender, EventArgs e)
        {
            bool isValid = true;

            m_ComputersMoveCaptureTimer.Stop();
            if (m_ComputerMove != null &&
                m_ComputerMove.StartPosition.RowPosition == m_CurrentCapturePosition.RowPosition &&
                m_ComputerMove.StartPosition.ColPosition == m_CurrentCapturePosition.ColPosition)
            {
                if (processMoveCapture(ref m_CurrentCapturePosition, m_ComputerMove))
                {
                    animateButtonPress(m_BoardButtons[m_ComputerMove.EndPosition.RowPosition, m_ComputerMove.EndPosition.ColPosition]);
                    updateBoardDisplay();
                    if (m_Game.HasAdditionalCapture(m_CurrentCapturePosition))
                    {
                        m_ComputerMove = m_Game.GetBestMoveForAI(m_Game.CurrentPlayer, m_CurrentCapturePosition, true);
                        if (m_ComputerMove != null &&
                            m_ComputerMove.StartPosition.RowPosition == m_CurrentCapturePosition.RowPosition &&
                            m_ComputerMove.StartPosition.ColPosition == m_CurrentCapturePosition.ColPosition)
                        {
                            startComputerCaptureTimer();
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            if (!isValid)
            {
                m_IsCaptureSequence = false;
                m_ComputerMove = null;
                handleTurnEnd();
            }
        }

        private bool processMoveCapture(ref Position io_EndPosition, Move i_Move)
        {
            bool successfulCapture = m_Game.ExecuteCaptureMove(m_Game.Board, i_Move.StartPosition, i_Move.EndPosition,
                out bool isCapture, out Position newEndPosition);

            if (successfulCapture)
            {
                io_EndPosition = newEndPosition;
                animateButtonPress(m_BoardButtons[io_EndPosition.RowPosition, io_EndPosition.ColPosition]);
                updateBoardDisplay();
            }

            return successfulCapture;
        }

        private string gameSummary()
        {
            string message = string.Empty;

            switch (m_Game.GameStatus)
            {
                case eGameStatus.Draw:
                    message = "Tie";
                    break;
                case eGameStatus.VictoryPlayerOne:
                    message = $"{m_Game.PlayerOne.Name} Won";
                    m_Game.UpadateScoreForWinner(m_Game.PlayerOne);
                    break;
                case eGameStatus.VictoryPlayerTwo:
                    message = $"{m_Game.PlayerTwo.Name} Won";
                    m_Game.UpadateScoreForWinner(m_Game.PlayerTwo);
                    break;
                default:
                    break;
            }

            return message;
        }

        private bool anotherGameRound()
        {
            string message = gameSummary();

            DialogResult dialogResult = MessageBox.Show($"{message}!{Environment.NewLine}Another Round?",
                "Damke", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            m_Game.GameStatus = (dialogResult == DialogResult.Yes) ? eGameStatus.InProgress : eGameStatus.End;

            return dialogResult == DialogResult.Yes;
        }

        private void handleTurnEnd()
        {
            m_Game.UpdateGameState();
            if (m_Game.GameStatus != eGameStatus.InProgress)
            {
                if (anotherGameRound())
                {
                    this.m_LabelPlayerOneScore.Text = m_Game.PlayerOne.Score.ToString();
                    this.m_LabelPlayerTwoScore.Text = m_Game.PlayerTwo.Score.ToString();
                    m_Game.Board.InitializeBoard();

                    startNewGame();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                if (!m_IsCaptureSequence)
                {
                    m_Game.SwitchPlayer();
                    Label currentPlayerLabel = m_Game.CurrentPlayer == m_Game.PlayerOne ? m_LabelPlayerOne : m_LabelPlayerTwo;
                    Label otherPlayerLabel = m_Game.CurrentPlayer == m_Game.PlayerOne ? m_LabelPlayerTwo : m_LabelPlayerOne;

                    currentPlayerLabel.BorderStyle = BorderStyle.FixedSingle;
                    otherPlayerLabel.BorderStyle = BorderStyle.None;
                }

                if (m_Game.CurrentPlayer.PlayerType == ePlayerType.ComputerPlayer)
                {
                    if (m_IsCaptureSequence)
                    {
                        handleMultipleCaptures(ref m_CurrentCapturePosition);
                    }
                    else
                    {
                        handleComputerTurn();
                    }

                    m_IsCaptureSequence = false;
                }
            }
        }
    }
}
