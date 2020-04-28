using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Board
    {
        private Soldier[,] m_TheBoard;
        private int        m_SizeBoard;

        public Board(int i_SizeBoard) // C'TOR
        {
            m_SizeBoard = i_SizeBoard;
            m_TheBoard = new Soldier[m_SizeBoard, m_SizeBoard];
        }

        public int BoardSize
        {
            get
            {
                return m_SizeBoard;
            }
        }

        public void UpdateAllBoard(Player[] i_ThePlayers)
        {
            int x, y;

            for (int i = 0; i < i_ThePlayers[0].SoldiersList.Count; i++)
            {
                x = i_ThePlayers[0].SoldiersList[i].GetXCoordination();
                y = i_ThePlayers[0].SoldiersList[i].GetYCoordination();
                m_TheBoard[x, y] = i_ThePlayers[0].SoldiersList[i];
            }

            for (int j = 0; j < i_ThePlayers[1].SoldiersList.Count; j++)
            {
                x = i_ThePlayers[1].SoldiersList[j].GetXCoordination();
                y = i_ThePlayers[1].SoldiersList[j].GetYCoordination();
                m_TheBoard[x, y] = i_ThePlayers[1].SoldiersList[j];
            }
        }

        public void UpdateOneSoldierOnBoard(Soldier i_Soldier, Coordination i_BaseCoordination, Coordination i_SoldierDestination)
        {
            m_TheBoard[i_BaseCoordination.GetX(), i_BaseCoordination.GetY()] = null;
            m_TheBoard[i_SoldierDestination.GetX(), i_SoldierDestination.GetY()] = i_Soldier;
        }

        public void RemoveOneSoldierOnBoard(Coordination i_Coordination)
        {
            m_TheBoard[i_Coordination.GetX(), i_Coordination.GetY()] = null;
        }

        public Soldier[,] GetBoard()
        {
            return m_TheBoard;
        }

        public Soldier GetSoliderFromCoordination(int i_X, int i_Y)
        {
            return m_TheBoard[i_X, i_Y];
        }

        public void CleanBoardMatrix()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    m_TheBoard[i, j] = null;
                }
            }
        }
    }
}
