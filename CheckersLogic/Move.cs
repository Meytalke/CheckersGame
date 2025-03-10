namespace CheckersLogic
{
    public class Move
    {
        private Position m_StartPosition;
        private Position m_EndPosition;

        public Move(Position i_StartPosition, Position i_EndPosition)
        {
            this.m_StartPosition = i_StartPosition;
            this.m_EndPosition = i_EndPosition;
        }

        public Position StartPosition
        {
            get
            {
                return m_StartPosition;
            }

            set
            {
                m_StartPosition = value;
            }
        }

        public Position EndPosition
        {
            get
            {
                return m_EndPosition;
            }

            set
            {
                m_EndPosition = value;
            }
        }
    }
}