namespace CheckersLogic
{
    public class Board
    {
        private readonly Coin[,] r_CoinBoard;
        private readonly int r_Size;

        public Board(int i_Size)
        {
            this.r_Size = i_Size;
            this.r_CoinBoard = new Coin[r_Size, r_Size];

            InitializeBoard();
        }

        public Coin[,] CoinBoard
        {
            get
            {
                return r_CoinBoard;
            }
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public void InitializeBoard()
        {
            int rowToFill = r_Size == 6 ? 2 : r_Size == 8 ? 3 : 4;

            for (int row = 0; row < r_Size; row++)
            {
                for (int col = 0; col < r_Size; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        if (row < rowToFill)
                        {
                            r_CoinBoard[row, col] = new Coin(eCoinType.O);
                        }
                        else if (row >= r_Size - rowToFill)
                        {
                            r_CoinBoard[row, col] = new Coin(eCoinType.X);
                        }
                        else
                        {
                            r_CoinBoard[row, col] = new Coin(eCoinType.Empty);
                        }
                    }
                    else
                    {
                        r_CoinBoard[row, col] = new Coin(eCoinType.Empty);
                    }
                }
            }
        }

        public Board Clone()
        {
            Board clonedBoard = new Board(Size);

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Coin originalCoin = r_CoinBoard[row, col];
                    if (originalCoin != null)
                    {
                        clonedBoard.r_CoinBoard[row, col] = new Coin(originalCoin.CoinType);
                    }
                }
            }

            return clonedBoard;
        }

        public bool IsAtLastRow(Position i_Position)
        {
            return (GetCoinAt(i_Position) == eCoinType.X && i_Position.RowPosition == 0) ||
            (GetCoinAt(i_Position) == eCoinType.O && i_Position.RowPosition == this.Size - 1);
        }

        public int CountCoins(eCoinType i_CoinType)
        {
            int count = 0;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CoinBoard[row, col].CoinType == i_CoinType)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public eCoinType GetCoinAt(Position i_Position)
        {
            return r_CoinBoard[i_Position.RowPosition, i_Position.ColPosition].CoinType;
        }
    }
}