using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class Player
    {
        private string        m_PlayerName;
        private int           m_NumberSoldiers;
        private int           m_WinPoints;
        private PlayerType    m_PlayerType;
        private List<Soldier> m_SoldiersList;

        public Player(string i_NameOfPlayer, PlayerType i_TypeOfPlayer, int i_BoardSize) // C'TOR
        {
            m_PlayerName = i_NameOfPlayer;
            m_PlayerType = i_TypeOfPlayer;
            m_WinPoints = 0;
            m_NumberSoldiers = checkTheNumberOfSoldiers(i_BoardSize);
            m_SoldiersList = new List<Soldier>(m_NumberSoldiers);
            BuildSoldiersList(i_BoardSize);
        }

        public PlayerType TypePlayer
        {
            get
            {
                return m_PlayerType;
            }
        }

        public int WinPoints
        {
            get
            {
                return m_WinPoints;
            }

            set
            {
                m_WinPoints = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }
        }

        public List<Soldier> SoldiersList
        {
            get
            {
                return m_SoldiersList;
            }
        }

        private int checkTheNumberOfSoldiers(int i_BoardSize)
        {
            int soldiersNumber = 0;

            if (i_BoardSize == 6)
            {
                soldiersNumber = 6;
            }
            else
                if (i_BoardSize == 8)
            {
                soldiersNumber = 12;
            }

            if (i_BoardSize == 10)
            {
                soldiersNumber = 20;
            }

            return soldiersNumber;
        }

        public void BuildSoldiersList(int i_BoardSize)
        {
            if (i_BoardSize == 6)
            {
                buildSoldiersListForSizeBoardSix6();
            }
            else
               if (i_BoardSize == 8)
            {
                buildSoldiersListForSizeBoardEight8();
            }
            else
               if (i_BoardSize == 10)
            {
                buildSoldiersListForSizeBoardTen10();
            }
        }

        // For Board Size 6
        private void buildSoldiersListForSizeBoardSix6()
        {
            for (int i = 0; i < 6; i++)
            {
                m_SoldiersList.Add(new Soldier(this));
            }

            if (m_PlayerType == PlayerType.MainRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(0, 5);
                m_SoldiersList[1].SetCoordination(2, 5);
                m_SoldiersList[2].SetCoordination(4, 5);
                m_SoldiersList[3].SetCoordination(1, 4);
                m_SoldiersList[4].SetCoordination(3, 4);
                m_SoldiersList[5].SetCoordination(5, 4);
            }
            else if (m_PlayerType == PlayerType.RivalComputerPlayer || m_PlayerType == PlayerType.RivalRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(1, 0);
                m_SoldiersList[1].SetCoordination(3, 0);
                m_SoldiersList[2].SetCoordination(5, 0);
                m_SoldiersList[3].SetCoordination(0, 1);
                m_SoldiersList[4].SetCoordination(2, 1);
                m_SoldiersList[5].SetCoordination(4, 1);
            }
        }

        // For Board Size 8
        private void buildSoldiersListForSizeBoardEight8()
        {
            for (int i = 0; i < 12; i++)
            {
                m_SoldiersList.Add(new Soldier(this));
            }

            if (m_PlayerType == PlayerType.MainRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(0, 7);
                m_SoldiersList[1].SetCoordination(2, 7);
                m_SoldiersList[2].SetCoordination(4, 7);
                m_SoldiersList[3].SetCoordination(6, 7);
                m_SoldiersList[4].SetCoordination(1, 6);
                m_SoldiersList[5].SetCoordination(3, 6);
                m_SoldiersList[6].SetCoordination(5, 6);
                m_SoldiersList[7].SetCoordination(7, 6);
                m_SoldiersList[8].SetCoordination(0, 5);
                m_SoldiersList[9].SetCoordination(2, 5);
                m_SoldiersList[10].SetCoordination(4, 5);
                m_SoldiersList[11].SetCoordination(6, 5);
            }
            else
            if (m_PlayerType == PlayerType.RivalComputerPlayer || m_PlayerType == PlayerType.RivalRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(1, 0);
                m_SoldiersList[1].SetCoordination(3, 0);
                m_SoldiersList[2].SetCoordination(5, 0);
                m_SoldiersList[3].SetCoordination(7, 0);
                m_SoldiersList[4].SetCoordination(0, 1);
                m_SoldiersList[5].SetCoordination(2, 1);
                m_SoldiersList[6].SetCoordination(4, 1);
                m_SoldiersList[7].SetCoordination(6, 1);
                m_SoldiersList[8].SetCoordination(1, 2);
                m_SoldiersList[9].SetCoordination(3, 2);
                m_SoldiersList[10].SetCoordination(5, 2);
                m_SoldiersList[11].SetCoordination(7, 2);
            }
        }

        // For Board Size 10
        private void buildSoldiersListForSizeBoardTen10()
        {
            for (int i = 0; i < 20; i++)
            {
                m_SoldiersList.Add(new Soldier(this));
            }

            if (m_PlayerType == PlayerType.MainRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(0, 9);
                m_SoldiersList[1].SetCoordination(2, 9);
                m_SoldiersList[2].SetCoordination(4, 9);
                m_SoldiersList[3].SetCoordination(6, 9);
                m_SoldiersList[4].SetCoordination(8, 9);
                m_SoldiersList[5].SetCoordination(1, 8);
                m_SoldiersList[6].SetCoordination(3, 8);
                m_SoldiersList[7].SetCoordination(5, 8);
                m_SoldiersList[8].SetCoordination(7, 8);
                m_SoldiersList[9].SetCoordination(9, 8);
                m_SoldiersList[10].SetCoordination(0, 7);
                m_SoldiersList[11].SetCoordination(2, 7);
                m_SoldiersList[12].SetCoordination(4, 7);
                m_SoldiersList[13].SetCoordination(6, 7);
                m_SoldiersList[14].SetCoordination(8, 7);
                m_SoldiersList[15].SetCoordination(1, 6);
                m_SoldiersList[16].SetCoordination(3, 6);
                m_SoldiersList[17].SetCoordination(5, 6);
                m_SoldiersList[18].SetCoordination(7, 6);
                m_SoldiersList[19].SetCoordination(9, 6);
            }
            else
            if (m_PlayerType == PlayerType.RivalComputerPlayer || m_PlayerType == PlayerType.RivalRealPlayer)
            {
                m_SoldiersList[0].SetCoordination(1, 0);
                m_SoldiersList[1].SetCoordination(3, 0);
                m_SoldiersList[2].SetCoordination(5, 0);
                m_SoldiersList[3].SetCoordination(7, 0);
                m_SoldiersList[4].SetCoordination(9, 0);
                m_SoldiersList[5].SetCoordination(0, 1);
                m_SoldiersList[6].SetCoordination(2, 1);
                m_SoldiersList[7].SetCoordination(4, 1);
                m_SoldiersList[8].SetCoordination(6, 1);
                m_SoldiersList[9].SetCoordination(8, 1);
                m_SoldiersList[10].SetCoordination(1, 2);
                m_SoldiersList[11].SetCoordination(3, 2);
                m_SoldiersList[12].SetCoordination(5, 2);
                m_SoldiersList[13].SetCoordination(7, 2);
                m_SoldiersList[14].SetCoordination(9, 2);
                m_SoldiersList[15].SetCoordination(0, 3);
                m_SoldiersList[16].SetCoordination(2, 3);
                m_SoldiersList[17].SetCoordination(4, 3);
                m_SoldiersList[18].SetCoordination(6, 3);
                m_SoldiersList[19].SetCoordination(8, 3);
            }
        }

        public void InitializeSoldiersList(int i_SizeBoard)
        {
            SoldiersList.Clear();
            SoldiersList.TrimExcess();
            this.BuildSoldiersList(i_SizeBoard);
        }

        public int ConvertSoldiersToPoints()
        {
            int totalPoints = 0;
            foreach (Soldier currentSoldier in this.SoldiersList)
            {
                if (currentSoldier.TypeOfSoldier == SoldierType.RegularSoldier)
                {
                    totalPoints += 1;
                }
                else
                 if (currentSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                {
                    totalPoints += 4;
                }
            }

            return totalPoints;
        }

        public bool CheckIfHaveValidMove(Board i_TheBoard)
        {
            List<BaseAndDestinationCoordinations> EatMoves = new List<BaseAndDestinationCoordinations>();
            bool                                  canEat = false;
            bool                                  haveValidMove = false;

            canEat = this.CheckIfPlayerIsNecesseryToEat(EatMoves, i_TheBoard);
            if (canEat == true)
            {
                haveValidMove = true;
            }
            else
            {
                foreach (Soldier currentSoldier in this.SoldiersList)
                {
                    if (this.TypePlayer == PlayerType.MainRealPlayer)
                    {
                        if (currentSoldier.CheckMoveRightUp(i_TheBoard) == true || currentSoldier.CheckMoveLeftUp(i_TheBoard) == true)
                        {
                            haveValidMove = true;
                            break;
                        }
                        else
                             if (currentSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                        {
                            if (currentSoldier.CheckMoveRightDown(i_TheBoard) == true || currentSoldier.CheckMoveLeftDown(i_TheBoard) == true)
                            {
                                haveValidMove = true;
                                break;
                            }
                        }
                    }
                    else
                    if (this.TypePlayer == PlayerType.RivalComputerPlayer || this.TypePlayer == PlayerType.RivalRealPlayer)
                    {
                        if (currentSoldier.CheckMoveLeftDown(i_TheBoard) == true || currentSoldier.CheckMoveRightDown(i_TheBoard) == true)
                        {
                            haveValidMove = true;
                            break;
                        }
                        else
                            if (currentSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                        {
                            if (currentSoldier.CheckMoveRightUp(i_TheBoard) == true || currentSoldier.CheckMoveLeftUp(i_TheBoard) == true)
                            {
                                haveValidMove = true;
                                break;
                            }
                        }
                    }
                }
            }

            return haveValidMove;
        }

        public bool CheckIfPlayerIsNecesseryToEat(List<BaseAndDestinationCoordinations> i_PossibleEatCoordinations, Board i_Board)
        {
            int  necesseryEatCounter = 0;
            bool needEat;

            foreach (Soldier currentSoldier in this.SoldiersList)
            {
                needEat = currentSoldier.CheckIfSoldierNeedEat(i_PossibleEatCoordinations, i_Board);
                if (needEat == true)
                {
                    necesseryEatCounter++;
                }
            }

            return necesseryEatCounter > 0 ? true : false;
        }

        public bool CheckIfThePlayerCanRetire(Player i_RivalPlayer)
        {
            bool canRetire = false;
            int  turnPlayerPoints = this.ConvertSoldiersToPoints();
            int  rivalPlayerPoints = i_RivalPlayer.ConvertSoldiersToPoints();

            if (turnPlayerPoints < rivalPlayerPoints)
            {
                canRetire = true;
            }

            return canRetire;
        }
    }
}
