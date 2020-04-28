using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    // This Struct, No Class !
    public struct Coordination
    {
        private int m_X;
        private int m_Y;

        public Coordination(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public void SetCoordination(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public int GetX()
        {
            return m_X;
        }

        public int GetY()
        {
            return m_Y;
        }
    }
}
