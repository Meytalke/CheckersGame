using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersLogic
{
    public class Position
    {
        private int m_RowPosition;
        private int m_ColPosition;

        public Position(int i_Row, int i_Col)
        {
            this.m_RowPosition = i_Row;
            this.m_ColPosition = i_Col;
        }

        public int RowPosition
        {
            get
            {
                return m_RowPosition;
            }

            set
            {
                m_RowPosition = value;
            }
        }

        public int ColPosition
        {
            get
            {
                return m_ColPosition;
            }

            set
            {
                m_ColPosition = value;
            }
        }
    }
}
