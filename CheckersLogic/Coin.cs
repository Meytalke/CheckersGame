namespace CheckersLogic
{
    public class Coin
    {
        private eCoinType m_CoinType;

        public Coin(eCoinType i_CoinType)
        {
            this.m_CoinType = i_CoinType;
        }

        public eCoinType CoinType
        {
            get
            {
                return m_CoinType;
            }

            set
            {
                m_CoinType = value;
            }
        }

        public void ChangeTheTypeToKing()
        {
            switch (m_CoinType)
            {
                case eCoinType.X:
                    m_CoinType = eCoinType.K;
                    break;
                case eCoinType.O:
                    m_CoinType = eCoinType.U;
                    break;
                default:
                    break;
            }
        }

        public void DestroyCoin()
        {
            m_CoinType = eCoinType.Empty;
        }
    }
}