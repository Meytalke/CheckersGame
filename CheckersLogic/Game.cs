using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class Game
    {
        private readonly Board r_Board;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;
        private Player m_CurrentPlayer;
        private eGameStatus m_GameStatus;

        public Game(int i_BoradSize, Player i_Playerone, Player i_PlayerTwo)
        {
            this.r_Board = new Board(i_BoradSize);
            this.r_PlayerOne = i_Playerone;
            this.r_PlayerTwo = i_PlayerTwo;
            this.m_CurrentPlayer = r_PlayerOne;
            this.m_GameStatus = eGameStatus.InProgress;
        }

        public Board Board
        {
            get
            {
                return r_Board;
            }
        }

        public Player PlayerOne
        {
            get
            {
                return r_PlayerOne;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return r_PlayerTwo;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }

            set
            {
                m_GameStatus = value;
            }
        }

        public bool IsMoveValidForPlayer(eMoveType i_MoveType, Position i_StartPosition, Position i_EndPosition,
            bool i_OpponentPlayer = false)
        {
            bool isValid = false;
            eCoinType coinType = this.r_Board.GetCoinAt(i_StartPosition);
            eCoinType currentPlayer = CurrentPlayer.CoinType;
            eCoinType kingOfCurrentPlayer = getKingCoinType(currentPlayer);

            if (i_OpponentPlayer)
            {
                currentPlayer = CurrentPlayer.CoinType == eCoinType.X ? eCoinType.O : eCoinType.X;
                kingOfCurrentPlayer = getKingCoinType(currentPlayer);
            }

            if (currentPlayer == coinType || kingOfCurrentPlayer == coinType)
            {
                isValid = checkPlayerMoveValidity(i_MoveType, i_StartPosition, i_EndPosition, coinType);
            }

            return isValid;
        }

        private bool checkPlayerMoveValidity(eMoveType i_MoveType, Position i_StartPosition, Position i_EndPosition,
            eCoinType i_CoinType)
        {
            bool isValid = false;

            if (r_Board.GetCoinAt(i_EndPosition) == eCoinType.Empty)
            {
                int rowDiff = i_EndPosition.RowPosition - i_StartPosition.RowPosition;
                int colDiff = i_EndPosition.ColPosition - i_StartPosition.ColPosition;
                int expectedRowDiff = i_MoveType == eMoveType.Regular ? 1 : 2;

                switch (i_CoinType)
                {
                    case eCoinType.X:
                        isValid = (rowDiff == -expectedRowDiff && Math.Abs(colDiff) == expectedRowDiff);
                        break;
                    case eCoinType.O:
                        isValid = (rowDiff == expectedRowDiff && Math.Abs(colDiff) == expectedRowDiff);
                        break;
                    case eCoinType.U:
                    case eCoinType.K:
                        isValid = (Math.Abs(rowDiff) == expectedRowDiff && Math.Abs(colDiff) == expectedRowDiff);
                        break;
                    default:
                        isValid = false;
                        break;
                }

                if (isValid && i_MoveType == eMoveType.Capture)
                {
                    isValid = validateCaptureMove(i_StartPosition, i_EndPosition, i_CoinType);
                }
            }

            return isValid;
        }

        private bool validateCaptureMove(Position i_StartPosition, Position i_EndPosition, eCoinType i_CoinType)
        {
            bool isValid = false;
            Position middlePosition = getmiddlePosition(i_StartPosition, i_EndPosition);
            eCoinType middleCoin = r_Board.GetCoinAt(middlePosition);

            if (middleCoin != eCoinType.Empty && IsOpponentCoin(i_CoinType, middleCoin))
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool IsOpponentCoin(eCoinType i_MyCoinType, eCoinType i_OpponentCoinType, bool i_CheckIsKing = false)
        {
            bool isOpponent = false;

            if (i_MyCoinType != eCoinType.Empty && i_OpponentCoinType != eCoinType.Empty)
            {
                isOpponent = (i_MyCoinType == eCoinType.X || i_MyCoinType == eCoinType.K)
                        ? (i_OpponentCoinType == eCoinType.O || i_OpponentCoinType == eCoinType.U)
                        : (i_OpponentCoinType == eCoinType.X || i_OpponentCoinType == eCoinType.K);

                isOpponent = isOpponent && (!i_CheckIsKing || i_OpponentCoinType == eCoinType.K || i_OpponentCoinType == eCoinType.U);
            }

            return isOpponent;
        }

        public List<Position> getValidMovesFromPosition(Position i_StartPosition, eMoveType i_MoveType,
            bool i_OpponentPlayer = false)
        {
            List<Position> validMoves = new List<Position>();

            if (i_StartPosition != null)
            {
                int stepSize = (i_MoveType == eMoveType.Regular) ? 1 : 2;
                int row = i_StartPosition.RowPosition;
                int col = i_StartPosition.ColPosition;

                for (int rowOffset = -stepSize; rowOffset <= stepSize; rowOffset += stepSize)
                {
                    for (int colOffset = -stepSize; colOffset <= stepSize; colOffset += stepSize)
                    {
                        int targetRow = row + rowOffset;
                        int targetCol = col + colOffset;

                        if (targetRow >= 0 && targetRow < r_Board.Size &&
                            targetCol >= 0 && targetCol < r_Board.Size)
                        {
                            Position endPosition = new Position(targetRow, targetCol);

                            if (IsMoveValidForPlayer(i_MoveType, i_StartPosition, endPosition, i_OpponentPlayer))
                            {
                                validMoves.Add(endPosition);
                            }
                        }
                    }
                }
            }

            return validMoves;
        }

        private List<Move> getAllValidMoveOptions(eCoinType i_CoinType, eMoveType i_MoveType,
            bool i_OpponentPlayer = false)
        {
            List<Move> validMoves = new List<Move>();

            for (int row = 0; row < r_Board.Size; row++)
            {
                for (int col = 0; col < r_Board.Size; col++)
                {
                    if (r_Board.CoinBoard[row, col].CoinType == i_CoinType)
                    {
                        Position startPosition = new Position(row, col);
                        List<Position> possibleMovesFromPosition = getValidMovesFromPosition(startPosition, i_MoveType,
                            i_OpponentPlayer);

                        for (int i = 0; i < possibleMovesFromPosition.Count; i++)
                        {
                            validMoves.Add(new Move(startPosition, possibleMovesFromPosition[i]));
                        }
                    }
                }
            }

            return validMoves;
        }

        private bool hasValidMoveOptions(eCoinType i_CoinType, eMoveType i_MoveType, bool i_OpponentPlayer = false)
        {
            return getAllValidMoveOptions(i_CoinType, i_MoveType, i_OpponentPlayer).Count > 0;
        }

        private bool hasValidMovesForPlayer(eCoinType i_CoinType, bool i_OpponentPlayer = false)
        {
            bool hasValidMoves = false;
            eCoinType[] coinTypes = i_CoinType == eCoinType.X ? new eCoinType[] { eCoinType.X, eCoinType.K } : new eCoinType[] { eCoinType.O, eCoinType.U };

            foreach (eCoinType coinType in coinTypes)
            {
                if (hasValidMoveOptions(coinType, eMoveType.Regular, i_OpponentPlayer) ||
                    hasValidMoveOptions(coinType, eMoveType.Capture, i_OpponentPlayer))
                {
                    hasValidMoves = true;
                    break;
                }
            }

            return hasValidMoves;
        }

        private eMoveType getMoveType(Player i_CurrentPlayer)
        {
            eMoveType returnMoveType = eMoveType.Regular;
            eCoinType kingCoin = i_CurrentPlayer.CoinType == eCoinType.O ? eCoinType.U : eCoinType.K;

            if (hasValidMoveOptions(i_CurrentPlayer.CoinType, eMoveType.Capture) || hasValidMoveOptions(kingCoin,
                eMoveType.Capture))
            {
                returnMoveType = eMoveType.Capture;
            }

            return returnMoveType;
        }

        private eCoinType getKingCoinType(eCoinType i_CoinType)
        {
            return i_CoinType == eCoinType.X ? eCoinType.K : eCoinType.U;
        }

        private Move getRandomMove(List<Move> i_Moves)
        {
            Random random = new Random();
            int randomIndex = random.Next(i_Moves.Count);
            return i_Moves[randomIndex];
        }

        public Move GetBestMoveForAI(Player i_Player, Position i_LastPosition = null, bool i_UseLastPosition = false)
        {
            Move bestMove = null;
            eCoinType kingCoinOfPlayer = getKingCoinType(i_Player.CoinType);
            List<Move> allRegularMoves = getAllMovesAccordingMoveType(i_Player.CoinType, kingCoinOfPlayer, eMoveType.Regular);
            List<Move> allCaptureMoves = getAllMovesAccordingMoveType(i_Player.CoinType, kingCoinOfPlayer, eMoveType.Capture);
            List<Move> kingingMoves = new List<Move>();
            List<Move> kingCaptureMoves = new List<Move>();
            List<Move> regularCaptureMoves = new List<Move>();
            List<Move> doubleCaptureMoves = new List<Move>();

            classificationMoves(allRegularMoves, allCaptureMoves, i_Player, kingCaptureMoves, regularCaptureMoves,
                doubleCaptureMoves, kingingMoves);
            if (i_UseLastPosition && i_LastPosition != null)
            {
                bestMove = getMoveFromLastPosition(allCaptureMoves, i_LastPosition);
            }

            if (bestMove == null)
            {
                bestMove = selectBestMove(doubleCaptureMoves, kingCaptureMoves, regularCaptureMoves,
                    kingingMoves, allRegularMoves);
            }

            return bestMove;
        }

        private Move getMoveFromLastPosition(List<Move> i_AllCaptureMoves, Position i_LastPosition)
        {
            List<Move> validMovesFromLastPosition = new List<Move>();

            foreach (var move in i_AllCaptureMoves)
            {
                if (move.StartPosition.RowPosition == i_LastPosition.RowPosition && move.StartPosition.ColPosition == i_LastPosition.ColPosition)
                {
                    validMovesFromLastPosition.Add(move);
                }
            }

            return validMovesFromLastPosition.Count > 0 ? getRandomMove(validMovesFromLastPosition) : null;
        }

        private Move selectBestMove(List<Move> i_DoubleCaptureMoves, List<Move> i_KingCaptureMoves, List<Move> i_RegularCaptureMoves,
            List<Move> i_KingingMoves, List<Move> i_AllRegularMoves)
        {
            Move move = null;

            if (i_DoubleCaptureMoves.Count > 0)
            {
                move = getRandomMove(i_DoubleCaptureMoves);
            }
            else if (i_KingCaptureMoves.Count > 0)
            {
                move = getRandomMove(i_KingCaptureMoves);
            }
            else if (i_RegularCaptureMoves.Count > 0)
            {
                move = getRandomMove(i_RegularCaptureMoves);
            }
            else if (i_KingingMoves.Count > 0)
            {
                move = getRandomMove(i_KingingMoves);
            }
            else if (i_AllRegularMoves.Count > 0)
            {
                move = getRandomMove(i_AllRegularMoves);
            }

            return move;
        }

        private List<Move> getAllMovesAccordingMoveType(eCoinType i_RegularCoinType, eCoinType i_KingCoinType, eMoveType i_MoveType)
        {
            List<Move> regularMoves = getAllValidMoveOptions(i_RegularCoinType, i_MoveType);
            List<Move> regularMovesAsKing = getAllValidMoveOptions(i_KingCoinType, i_MoveType);
            List<Move> allMoves = new List<Move>(regularMoves.Count + regularMovesAsKing.Count);

            foreach (Move move in regularMoves)
            {
                allMoves.Add(move);
            }

            foreach (Move move in regularMovesAsKing)
            {
                allMoves.Add(move);
            }

            return allMoves;
        }
        private void classificationMoves(List<Move> i_AllRegularMoves, List<Move> i_AllCaptureMoves,
            Player i_Player, List<Move> i_KingCaptureMoves, List<Move> i_RegularCaptureMoves, List<Move> i_DoubleCaptureMoves,
            List<Move> i_KingingMoves)
        {
            eCoinType playerKing = getKingCoinType(i_Player.CoinType);

            foreach (Move move in i_AllCaptureMoves)
            {
                if (canPerformDoubleCapture(move))
                {
                    i_DoubleCaptureMoves.Add(move);
                }
                else
                {
                    Position middlePosition = getmiddlePosition(move.StartPosition, move.EndPosition);
                    eCoinType middleCoin = r_Board.GetCoinAt(middlePosition);

                    if (middleCoin == (playerKing == eCoinType.K ? eCoinType.U : eCoinType.X))
                    {
                        i_KingCaptureMoves.Add(move);
                    }
                    else
                    {
                        i_RegularCaptureMoves.Add(move);
                    }
                }
            }

            foreach (Move move in i_AllRegularMoves)
            {
                if (isKingingMove(i_Player.CoinType, move) && r_Board.GetCoinAt(move.StartPosition) != playerKing)
                {
                    i_KingingMoves.Add(move);
                }
            }
        }

        private bool isKingingMove(eCoinType i_CoinType, Move i_Move)
        {
            return (i_CoinType == eCoinType.X && i_Move.EndPosition.RowPosition == 0) ||
          (i_CoinType == eCoinType.O && i_Move.EndPosition.RowPosition == r_Board.Size - 1);
        }

        private bool canPerformDoubleCapture(Move i_Move)
        {
            Board boardCopy = r_Board.Clone();
            List<Position> subsequentCapturePositions = new List<Position>(); ;

            executeMove(boardCopy, i_Move.StartPosition, i_Move.EndPosition, out bool isCapture);
            if (isCapture)
            {
                subsequentCapturePositions = getValidMovesFromPosition(i_Move.EndPosition, eMoveType.Capture);
            }

            return subsequentCapturePositions.Count > 0;
        }

        private void executeMove(Board i_Board, Position i_StartPosition, Position i_EndPosition, out bool o_IsCapture)
        {
            o_IsCapture = checkAndPerformCapture(i_Board, i_StartPosition, i_EndPosition);
            performMove(i_Board, i_StartPosition, i_EndPosition);
            if (i_Board.IsAtLastRow(i_EndPosition))
            {
                promoteToKing(i_Board, i_EndPosition);
            }
        }

        private void performMove(Board i_Board, Position i_StartPosition, Position i_EndPosition)
        {
            Coin currentCoin = i_Board.CoinBoard[i_StartPosition.RowPosition, i_StartPosition.ColPosition];

            i_Board.CoinBoard[i_EndPosition.RowPosition, i_EndPosition.ColPosition] = currentCoin;
            i_Board.CoinBoard[i_StartPosition.RowPosition, i_StartPosition.ColPosition] = new Coin(eCoinType.Empty);
        }

        private bool isCaptureMove(Position i_StartPosition, Position i_EndPosition)
        {
            return Math.Abs(i_StartPosition.RowPosition - i_EndPosition.RowPosition) == 2 &&
                   Math.Abs(i_StartPosition.ColPosition - i_EndPosition.ColPosition) == 2;
        }

        private Position getmiddlePosition(Position i_StartPosition, Position i_EndPosition)
        {
            return new Position((i_StartPosition.RowPosition + i_EndPosition.RowPosition) / 2,
                                (i_StartPosition.ColPosition + i_EndPosition.ColPosition) / 2);
        }

        private bool checkAndPerformCapture(Board i_Board, Position i_StartPosition, Position i_EndPosition)
        {
            bool isCapture = false;

            if (isCaptureMove(i_StartPosition, i_EndPosition))
            {
                Position middlePosition = getmiddlePosition(i_StartPosition, i_EndPosition);
                Coin middleCoin = i_Board.CoinBoard[middlePosition.RowPosition, middlePosition.ColPosition];

                if (middleCoin != null)
                {
                    middleCoin.DestroyCoin();
                    isCapture = true;
                }
            }

            return isCapture;
        }

        private void promoteToKing(Board i_Board, Position i_EndPosition)
        {
            i_Board.CoinBoard[i_EndPosition.RowPosition, i_EndPosition.ColPosition].ChangeTheTypeToKing();
        }

        private bool isWinnerByVictory(eCoinType i_CoinType)
        {
            bool isVictory = false;
            eCoinType anotherPlayer = i_CoinType == eCoinType.X ? eCoinType.O : eCoinType.X;
            eCoinType kingOfAnotherPlayer = getKingCoinType(anotherPlayer);

            if (r_Board.CountCoins(anotherPlayer) == 0 && r_Board.CountCoins(kingOfAnotherPlayer) == 0)
            {
                isVictory = true;
            }
            else if (!hasValidMovesForPlayer(anotherPlayer, true))
            {
                isVictory = true;
            }

            return isVictory;
        }

        private bool isDraw()
        {
            return (!hasValidMovesForPlayer(eCoinType.X) && !hasValidMovesForPlayer(eCoinType.O));
        }

        public int UpadateScoreForWinner(Player i_Winner)
        {
            eCoinType winnerRegular = i_Winner.CoinType == eCoinType.X ? eCoinType.X : eCoinType.O;
            eCoinType winnerKing = getKingCoinType(i_Winner.CoinType);
            eCoinType opponentRegular = i_Winner.CoinType == eCoinType.X ? eCoinType.O : eCoinType.X;
            eCoinType opponentKing = i_Winner.CoinType == eCoinType.X ? eCoinType.U : eCoinType.K;
            int winnerRegularCoins = r_Board.CountCoins(winnerRegular);
            int winnerKings = r_Board.CountCoins(winnerKing);
            int opponentRegularCoins = r_Board.CountCoins(opponentRegular);
            int opponentKings = r_Board.CountCoins(opponentKing);
            int calculatedScore = Math.Abs((winnerRegularCoins - opponentRegularCoins) + ((winnerKings - opponentKings) * 4));

            i_Winner.Score += calculatedScore;

            return calculatedScore;
        }

        public void UpdateGameState()
        {
            if (isDraw())
            {
                m_GameStatus = eGameStatus.Draw;
            }
            else if (isWinnerByVictory(m_CurrentPlayer.CoinType))
            {
                m_GameStatus = m_CurrentPlayer == r_PlayerOne ? eGameStatus.VictoryPlayerOne : eGameStatus.VictoryPlayerTwo;
            }
            else
            {
                m_GameStatus = eGameStatus.InProgress;
            }
        }

        public void SwitchPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == r_PlayerOne ? r_PlayerTwo : r_PlayerOne;
        }

        public void ExecuteMoveWithResult(Board i_Board, Player i_Player, Position i_StartPosition, Position i_EndPosition,
            out bool i_IsCapture, out bool i_IsValidMove, out bool i_MustCapture, bool i_IsHuman)
        {
            eMoveType moveType = getMoveType(i_Player);

            i_MustCapture = false;
            i_IsCapture = false;
            i_IsValidMove = IsMoveValidForPlayer(moveType, i_StartPosition, i_EndPosition);

            if (i_IsHuman && moveType == eMoveType.Capture && !i_IsValidMove)
            {
                i_MustCapture = true;
            }

            if (i_IsValidMove)
            {
                executeMove(i_Board, i_StartPosition, i_EndPosition, out i_IsCapture);
            }
        }

        public bool ExecuteCaptureMove(Board i_Board, Position i_CurrentPosition, Position i_TargetPosition,
            out bool i_IsCapture, out Position i_NewEndPosition)
        {
            i_NewEndPosition = i_TargetPosition;
            bool isValid = false;
            eMoveType moveType = eMoveType.Capture;

            if (IsMoveValidForPlayer(moveType, i_CurrentPosition, i_TargetPosition))
            {
                executeMove(i_Board, i_CurrentPosition, i_TargetPosition, out i_IsCapture);
                isValid = true;
            }
            else
            {
                i_IsCapture = false;
            }

            return isValid;
        }

        public bool HasAdditionalCapture(Position i_EndPosition)
        {
            return getValidMovesFromPosition(i_EndPosition, eMoveType.Capture).Count > 0;
        }
    }
}