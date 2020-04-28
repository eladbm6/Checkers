using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Soldier
    {
        private Player       m_ThePlayer;
        private SoldierType  m_SoldierType = SoldierType.RegularSoldier; // Regular Or King
        private Coordination m_XYCoordinationOnBoard = new Coordination();

        public Soldier(Player i_ThePlayer)
        {
            m_ThePlayer = i_ThePlayer;
        }

        public Player PlayerOfSoldier
        {
            get
            {
                return m_ThePlayer;
            }
        }

        public SoldierType TypeOfSoldier
        {
            get
            {
                return m_SoldierType;
            }

            set
            {
                m_SoldierType = value;
            }
        }

        public void SetCoordination(int i_X, int i_Y)
        {
            // Setter with 2 ints so I chose to exercise the Setter in this way
            m_XYCoordinationOnBoard.SetCoordination(i_X, i_Y);
        }

        public int GetXCoordination()
        {
            return m_XYCoordinationOnBoard.GetX();
        }

        public int GetYCoordination()
        {
            return m_XYCoordinationOnBoard.GetY();
        }

        public PlayerType SoldierPlayerType
        {
            get
            {
                return m_ThePlayer.TypePlayer;
            }
        }

        public bool CheckMoveLeftDown(Board i_TheBoard)
        {
            int  XCoordinationOfChooseSoldier = this.GetXCoordination();
            int  YCoordinationOfChooseSoldier = this.GetYCoordination();
            bool validMove = false;

            if (XCoordinationOfChooseSoldier - 1 >= 0 && YCoordinationOfChooseSoldier + 1 < i_TheBoard.BoardSize)
            {
                if (i_TheBoard.GetSoliderFromCoordination(XCoordinationOfChooseSoldier - 1, YCoordinationOfChooseSoldier + 1) == null)
                {
                    validMove = true;
                }
            }

            return validMove;
        }

        public bool CheckMoveRightDown(Board i_TheBoard) // Check if soldier can move Right & Down.
        {
            int  xCoordinationOfChooseSoldier = this.GetXCoordination();
            int  yCoordinationOfChooseSoldier = this.GetYCoordination();
            bool validMove = false;

            if (xCoordinationOfChooseSoldier + 1 < i_TheBoard.BoardSize && yCoordinationOfChooseSoldier + 1 < i_TheBoard.BoardSize)
            {
                if (i_TheBoard.GetSoliderFromCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier + 1) == null)
                {
                    validMove = true;
                }
            }

            return validMove;
        }

        public bool CheckMoveRightUp(Board i_TheBoard) // Check if soldier can move Right & Up.
        {
            int  xCoordinationOfChooseSoldier = this.GetXCoordination();
            int  yCoordinationOfChooseSoldier = this.GetYCoordination();
            bool validMove = false;

            if (xCoordinationOfChooseSoldier + 1 < i_TheBoard.BoardSize && yCoordinationOfChooseSoldier - 1 >= 0)
            {
                if (i_TheBoard.GetSoliderFromCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier - 1) == null)
                {
                    validMove = true;
                }
            }

            return validMove;
        }

        public bool CheckMoveLeftUp(Board i_TheBoard) // Check if soldier can move Left & Up.
        {
            int  xCoordinationOfChooseSoldier = this.GetXCoordination();
            int  yCoordinationOfChooseSoldier = this.GetYCoordination();
            bool validMove = false;

            if (xCoordinationOfChooseSoldier - 1 >= 0 && yCoordinationOfChooseSoldier - 1 >= 0)
            {
                if (i_TheBoard.GetSoliderFromCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier - 1) == null)
                {
                    validMove = true;
                }
            }

            return validMove;
        }

        public bool CheckIfSoldierNeedEat(List<BaseAndDestinationCoordinations> i_EatsList, Board i_Board)
        {
            bool mustToEat = false;
            int  xSoldier = this.GetXCoordination();
            int  ySoldier = this.GetYCoordination();
            bool soldierIsKing = false;

            if (this.TypeOfSoldier == SoldierType.KingSoldier)
            {
                soldierIsKing = true;
            }

            if (this.PlayerOfSoldier.TypePlayer == PlayerType.MainRealPlayer || soldierIsKing == true)
            {
                if (this.GetXCoordination() - 2 >= 0 && this.GetYCoordination() - 2 >= 0)
                {
                    if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 1, this.GetYCoordination() - 1) != null && i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 2, this.GetYCoordination() - 2) == null)
                    {
                        if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 1, this.GetYCoordination() - 1).SoldierPlayerType != this.PlayerOfSoldier.TypePlayer)
                        {
                            mustToEat = true;
                            i_EatsList.Add(new BaseAndDestinationCoordinations(xSoldier, ySoldier, xSoldier - 2, ySoldier - 2));
                        }
                    }
                }

                if (this.GetXCoordination() + 2 <= i_Board.BoardSize - 1 && this.GetYCoordination() - 2 >= 0)
                {
                    if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 1, this.GetYCoordination() - 1) != null && i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 2, this.GetYCoordination() - 2) == null)
                    {
                        if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 1, this.GetYCoordination() - 1).SoldierPlayerType != this.PlayerOfSoldier.TypePlayer)
                        {
                            mustToEat = true;
                            i_EatsList.Add(new BaseAndDestinationCoordinations(xSoldier, ySoldier, xSoldier + 2, ySoldier - 2));
                        }
                    }
                }
            }

            if (this.PlayerOfSoldier.TypePlayer == PlayerType.RivalComputerPlayer || this.PlayerOfSoldier.TypePlayer == PlayerType.RivalRealPlayer || soldierIsKing == true)
            {
                if (this.GetXCoordination() - 2 >= 0 && this.GetYCoordination() + 2 <= i_Board.BoardSize - 1)
                {
                    if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 1, this.GetYCoordination() + 1) != null && i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 2, this.GetYCoordination() + 2) == null)
                    {
                        if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() - 1, this.GetYCoordination() + 1).SoldierPlayerType != this.PlayerOfSoldier.TypePlayer)
                        {
                            mustToEat = true;
                            i_EatsList.Add(new BaseAndDestinationCoordinations(xSoldier, ySoldier, xSoldier - 2, ySoldier + 2));
                        }
                    }
                }

                if (this.GetXCoordination() + 2 <= i_Board.BoardSize - 1 && this.GetYCoordination() + 2 <= i_Board.BoardSize - 1)
                {
                    if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 1, this.GetYCoordination() + 1) != null && i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 2, this.GetYCoordination() + 2) == null)
                    {
                        if (i_Board.GetSoliderFromCoordination(this.GetXCoordination() + 1, this.GetYCoordination() + 1).SoldierPlayerType != this.PlayerOfSoldier.TypePlayer)
                        {
                            mustToEat = true;
                            i_EatsList.Add(new BaseAndDestinationCoordinations(xSoldier, ySoldier, xSoldier + 2, ySoldier + 2));
                        }
                    }
                }
            }

            return mustToEat;
        }

        public void UpdateSoldierOnBoard(Board i_Board, Coordination i_BaseCoordination, Coordination i_SoldierDestination)
        {
            this.SetCoordination(i_SoldierDestination.GetX(), i_SoldierDestination.GetY());
            i_Board.UpdateOneSoldierOnBoard(this, i_BaseCoordination, i_SoldierDestination);
            if (this.PlayerOfSoldier.TypePlayer == PlayerType.MainRealPlayer && this.GetYCoordination() == 0)
            {
                this.TypeOfSoldier = SoldierType.KingSoldier;
            }

            if (this.PlayerOfSoldier.TypePlayer != PlayerType.MainRealPlayer && this.GetYCoordination() == i_Board.BoardSize - 1)
            {
                this.TypeOfSoldier = SoldierType.KingSoldier;
            }
        }
    }
}
