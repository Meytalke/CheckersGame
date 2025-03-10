namespace CheckersLogic
{
    public class Player
    {
        private readonly string r_Name;
        private readonly eCoinType r_CoinType;
        private readonly ePlayerType r_PlayerType;
        private int m_Score;

        public Player(string i_Name, eCoinType i_CoinType, ePlayerType i_PlayerType)
        {
            this.r_Name = i_Name;
            this.r_CoinType = i_CoinType;
            this.r_PlayerType = i_PlayerType;
            this.m_Score = 0;
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public eCoinType CoinType
        {
            get
            {
                return r_CoinType;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public static bool IsValidName(string i_PlayerName)
        {
            bool isValidLength = i_PlayerName.Length > 0 && i_PlayerName.Length <= 20;
            bool isValidContent = isContainsOnlyLetters(i_PlayerName) && isContainsNoSpaces(i_PlayerName);

            return isValidLength && isValidContent;
        }

        private static bool isContainsOnlyLetters(string i_PlayerName)
        {
            bool isContains = true;

            foreach (char c in i_PlayerName)
            {
                if (!(char.IsUpper(c) || char.IsLower(c)))
                {
                    isContains = false;
                }
            }

            return isContains;
        }

        private static bool isContainsNoSpaces(string i_PlayerName)
        {
            bool isContains = true;

            foreach (char c in i_PlayerName)
            {
                if (char.IsWhiteSpace(c))
                {
                    isContains = false;
                }
            }

            return isContains;
        }
    }
}