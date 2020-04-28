using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    // This struct is for concentrate 2 Coordinations (Base to move, and destantion of move).
    public struct BaseAndDestinationCoordinations
    {
        private Coordination m_BaseCoordination;
        private Coordination m_DestinationCoordination;

        public BaseAndDestinationCoordinations(int i_XBase, int i_YBase, int i_XDestination, int i_YDestination)
        {
            m_BaseCoordination = new Coordination(i_XBase, i_YBase);
            m_DestinationCoordination = new Coordination(i_XDestination, i_YDestination);
        }

        public Coordination BaseCoordination
        {
            get
            {
                return m_BaseCoordination;
            }
        }

        public Coordination DestantionCoordination
        {
            get
            {
                return m_DestinationCoordination;
            }
        }
    }
}