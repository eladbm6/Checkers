using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace B18_Ex05
{
    internal class GameStep
    {
        private Player       m_CurrentTurnPlayer;
        private Board        m_TheBoard;
        private Coordination m_Source;
        private Coordination m_Destination;

        public GameStep(Player i_CurrentTurnPlayer, Board i_TheBoard, Coordination i_Source, Coordination i_Destantion) // C'TOR
        {
            m_CurrentTurnPlayer = i_CurrentTurnPlayer;
            m_TheBoard = i_TheBoard;
            m_Source = i_Source;
            m_Destination = i_Destantion;
        }

        public bool StartStep()
        {
            List<BaseAndDestinationCoordinations> possibleEatCoordinations = new List<BaseAndDestinationCoordinations>();
            List<BaseAndDestinationCoordinations> soldierNewDestantionsForEatAgain;
            Soldier                               soldierEatAgain = null;
            string                                stepString = null;
            bool                                  necesseryToEat = false;
            bool                                  necesseryToEatAgain = false;
            bool                                  haveValidMoves = m_CurrentTurnPlayer.CheckIfHaveValidMove(m_TheBoard);
            bool                                  playerThatNoComputerNeedEat = false;

            if (haveValidMoves == true)
            {
                necesseryToEat = m_CurrentTurnPlayer.CheckIfPlayerIsNecesseryToEat(possibleEatCoordinations, m_TheBoard);

                if (m_CurrentTurnPlayer.TypePlayer != PlayerType.RivalComputerPlayer)
                {
                    checkIfIsPossibleMoveAndExecuteIt(m_Source, m_Destination, possibleEatCoordinations);
                }
                else
                {
                    if (necesseryToEat == true)
                    {
                        soldierEatAgain = computerRandomEat(possibleEatCoordinations, ref stepString);
                    }
                    else
                    {
                        computerRandomWithoutEat();
                    }
                }

                if (necesseryToEat == true)
                {
                    do
                    {
                        if (m_CurrentTurnPlayer.TypePlayer != PlayerType.RivalComputerPlayer)
                        {
                            soldierEatAgain = m_TheBoard.GetSoliderFromCoordination(m_Destination.GetX(), m_Destination.GetY());
                        }

                        soldierNewDestantionsForEatAgain = new List<BaseAndDestinationCoordinations>();
                        necesseryToEatAgain = soldierEatAgain.CheckIfSoldierNeedEat(soldierNewDestantionsForEatAgain, m_TheBoard);
                        if (necesseryToEatAgain == true)
                        {
                            if (m_CurrentTurnPlayer.TypePlayer != PlayerType.RivalComputerPlayer)
                            {
                                playerThatNoComputerNeedEat = true;
                                necesseryToEatAgain = false; // Because if is not computer, we need get a input again so we end this loop.
                            }
                            else
                            {
                                soldierEatAgain = computerRandomEat(soldierNewDestantionsForEatAgain, ref stepString);
                            }
                        }
                    }
                    while (necesseryToEatAgain == true);
                }
            }

            return playerThatNoComputerNeedEat;
        }

        private void computerRandomWithoutEat()
        {
            Random       randomChooseSoldier = new Random();
            Random       leftOrRightOrUpOrDownRandom = new Random();
            Soldier      chooseSoldier;
            Coordination chooseSoldierCoordination = new Coordination();
            Coordination newDestantion = new Coordination();
            int          xCoordinationOfChooseSoldier;
            int          yCoordinationOfChooseSoldier;
            int          indexFromRandom;
            int          leftRightUpDownChoose;
            bool         validMove = false;

            while (validMove == false)
            {
                indexFromRandom = randomChooseSoldier.Next(m_CurrentTurnPlayer.SoldiersList.Count);
                chooseSoldier = m_CurrentTurnPlayer.SoldiersList[indexFromRandom];
                xCoordinationOfChooseSoldier = chooseSoldier.GetXCoordination();
                yCoordinationOfChooseSoldier = chooseSoldier.GetYCoordination();
                chooseSoldierCoordination.SetCoordination(xCoordinationOfChooseSoldier, yCoordinationOfChooseSoldier);
                if (chooseSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                {
                    leftRightUpDownChoose = leftOrRightOrUpOrDownRandom.Next(1, 5); // 4 moves can if king (so random from 4)
                }
                else
                {
                    leftRightUpDownChoose = leftOrRightOrUpOrDownRandom.Next(1, 3); // 2 moves can if king (so random from 2)
                }

                if (leftRightUpDownChoose == 1)
                {
                    validMove = chooseSoldier.CheckMoveLeftDown(m_TheBoard);
                    if (validMove == true)
                    {
                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier + 1);
                    }
                    else
                    {
                        validMove = chooseSoldier.CheckMoveRightDown(m_TheBoard);
                        if (validMove == true)
                        {
                            newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier + 1);
                        }
                        else
                        {
                            if (chooseSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                            {
                                validMove = chooseSoldier.CheckMoveRightUp(m_TheBoard);
                                if (validMove == true)
                                {
                                    newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier - 1);
                                }
                                else
                                {
                                    validMove = chooseSoldier.CheckMoveLeftUp(m_TheBoard);
                                    if (validMove == true)
                                    {
                                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier - 1);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                if (leftRightUpDownChoose == 2)
                {
                    validMove = chooseSoldier.CheckMoveRightDown(m_TheBoard);
                    if (validMove == true)
                    {
                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier + 1);
                    }
                    else
                    {
                        validMove = chooseSoldier.CheckMoveLeftDown(m_TheBoard);
                        if (validMove == true)
                        {
                            newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier + 1);
                        }
                        else
                        {
                            if (chooseSoldier.TypeOfSoldier == SoldierType.KingSoldier)
                            {
                                validMove = chooseSoldier.CheckMoveRightUp(m_TheBoard);
                                if (validMove == true)
                                {
                                    newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier - 1);
                                }
                                else
                                {
                                    validMove = chooseSoldier.CheckMoveLeftUp(m_TheBoard);
                                    if (validMove == true)
                                    {
                                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier - 1);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                if (leftRightUpDownChoose == 3)
                {
                    validMove = chooseSoldier.CheckMoveRightUp(m_TheBoard);
                    if (validMove == true)
                    {
                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier - 1);
                    }
                    else
                    {
                        validMove = chooseSoldier.CheckMoveLeftUp(m_TheBoard);
                        if (validMove == true)
                        {
                            newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier - 1);
                        }
                        else
                        {
                            validMove = chooseSoldier.CheckMoveLeftDown(m_TheBoard);
                            if (validMove == true)
                            {
                                newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier + 1);
                            }
                            else
                            {
                                validMove = chooseSoldier.CheckMoveRightDown(m_TheBoard);
                                if (validMove == true)
                                {
                                    newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier + 1);
                                }
                            }
                        }
                    }
                }
                else
                if (leftRightUpDownChoose == 4)
                {
                    validMove = chooseSoldier.CheckMoveLeftUp(m_TheBoard);
                    if (validMove == true)
                    {
                        newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier - 1);
                    }
                    else
                    {
                        validMove = chooseSoldier.CheckMoveRightUp(m_TheBoard);
                        if (validMove == true)
                        {
                            newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier - 1);
                        }
                        else
                        {
                            validMove = chooseSoldier.CheckMoveLeftDown(m_TheBoard);
                            if (validMove == true)
                            {
                                newDestantion.SetCoordination(xCoordinationOfChooseSoldier - 1, yCoordinationOfChooseSoldier + 1);
                            }
                            else
                            {
                                validMove = chooseSoldier.CheckMoveRightDown(m_TheBoard);
                                if (validMove == true)
                                {
                                    newDestantion.SetCoordination(xCoordinationOfChooseSoldier + 1, yCoordinationOfChooseSoldier + 1);
                                }
                            }
                        }
                    }
                }

                if (validMove == true)
                {
                    chooseSoldier.UpdateSoldierOnBoard(m_TheBoard, chooseSoldierCoordination, newDestantion);
                }
            }
        }

        private Soldier computerRandomEat(List<BaseAndDestinationCoordinations> i_EatCoordinations, ref string io_StepString)
        {
            Coordination baseCoordinationToEat;
            Coordination destantionCoordinationToEat;
            Soldier      soldierEat;
            Random       randomChooseSoldier = new Random();
            int          indexFromRandom = randomChooseSoldier.Next(0, i_EatCoordinations.Count);

            baseCoordinationToEat = i_EatCoordinations[indexFromRandom].BaseCoordination;
            destantionCoordinationToEat = i_EatCoordinations[indexFromRandom].DestantionCoordination;
            soldierEat = m_TheBoard.GetSoliderFromCoordination(baseCoordinationToEat.GetX(), baseCoordinationToEat.GetY());
            eatAndRemoveSoldier(baseCoordinationToEat, destantionCoordinationToEat);
            soldierEat.UpdateSoldierOnBoard(m_TheBoard, baseCoordinationToEat, destantionCoordinationToEat);
            io_StepString = buildStringOfStep(baseCoordinationToEat, destantionCoordinationToEat);

            return soldierEat;
        }

        private bool checkAndExecuteIfIsValidEatAgainMove(Soldier i_SoldierEatAgain, List<BaseAndDestinationCoordinations> i_SoldierNewDestantionsForEatAgain, Coordination i_PlayerBaseInput, Coordination i_PlayerDestantionInput)
        {
            bool validEatAgain = false;

            foreach (BaseAndDestinationCoordinations currentSoldierNewDestantionsForEatAgain in i_SoldierNewDestantionsForEatAgain)
            {
                if (currentSoldierNewDestantionsForEatAgain.DestantionCoordination.GetX() == i_PlayerDestantionInput.GetX() && currentSoldierNewDestantionsForEatAgain.DestantionCoordination.GetY() == i_PlayerDestantionInput.GetY() && i_SoldierEatAgain.GetXCoordination() == i_PlayerBaseInput.GetX() && i_SoldierEatAgain.GetYCoordination() == i_PlayerBaseInput.GetY())
                {
                    eatAndRemoveSoldier(i_PlayerBaseInput, i_PlayerDestantionInput);
                    i_SoldierEatAgain.UpdateSoldierOnBoard(m_TheBoard, i_PlayerBaseInput, i_PlayerDestantionInput);
                    validEatAgain = true;
                    break;
                }
            }

            return validEatAgain;
        }

        private void checkIfIsPossibleMoveAndExecuteIt(Coordination i_BaseCoordination, Coordination i_DestinationCoordination, List<BaseAndDestinationCoordinations> i_EatCoordinations)
        {
            Soldier baseSoldierToMove = m_TheBoard.GetSoliderFromCoordination(i_BaseCoordination.GetX(), i_BaseCoordination.GetY());

            if (baseSoldierToMove == null)
            {
                throw new Exception("Error: Invalid source point to move!");
            }
            else
                if (baseSoldierToMove.SoldierPlayerType != m_CurrentTurnPlayer.TypePlayer)
            {
                throw new Exception("Error: You cant move soldier of your rival! Please choose your soldier!");
            }
            else
            {
                if (isValidDestination(i_BaseCoordination, i_DestinationCoordination, baseSoldierToMove, i_EatCoordinations))
                {
                    baseSoldierToMove.UpdateSoldierOnBoard(m_TheBoard, i_BaseCoordination, i_DestinationCoordination);
                }
                else
                {
                    throw new Exception("Error: Invalid Move! Try Again!");
                }
            }
        }

        private bool isValidDestination(Coordination i_BaseCoordination, Coordination i_DestinationCoordination, Soldier i_BaseSoldier, List<BaseAndDestinationCoordinations> i_EatCoordinations)
        {
            bool isValid = false;
            bool soldierIsKing = false;

            if (i_BaseSoldier.TypeOfSoldier == SoldierType.KingSoldier)
            {
                soldierIsKing = true;
            }

            if (i_EatCoordinations.Count == 0)
            {
                if (m_CurrentTurnPlayer.TypePlayer == PlayerType.MainRealPlayer || soldierIsKing == true)
                {
                    if ((i_DestinationCoordination.GetX() == i_BaseCoordination.GetX() + 1 || i_DestinationCoordination.GetX() == i_BaseCoordination.GetX() - 1) && i_DestinationCoordination.GetY() == i_BaseCoordination.GetY() - 1 &&
                        i_DestinationCoordination.GetY() < m_TheBoard.BoardSize && i_DestinationCoordination.GetY() >= 0 && m_TheBoard.GetBoard()[i_DestinationCoordination.GetX(), i_DestinationCoordination.GetY()] == null)
                    {
                        isValid = true;
                    }
                }

                if (m_CurrentTurnPlayer.TypePlayer == PlayerType.RivalComputerPlayer || m_CurrentTurnPlayer.TypePlayer == PlayerType.RivalRealPlayer || soldierIsKing == true)
                {
                    if ((i_DestinationCoordination.GetX() == i_BaseCoordination.GetX() + 1 || i_DestinationCoordination.GetX() == i_BaseCoordination.GetX() - 1) && i_DestinationCoordination.GetY() == i_BaseCoordination.GetY() + 1 &&
                        i_DestinationCoordination.GetY() < m_TheBoard.BoardSize && i_DestinationCoordination.GetY() >= 0 && m_TheBoard.GetBoard()[i_DestinationCoordination.GetX(), i_DestinationCoordination.GetY()] == null)
                    {
                        isValid = true;
                    }
                }
            }
            else
            {
                if (checkIfValidEatMove(i_BaseCoordination, i_DestinationCoordination, i_EatCoordinations) == true)
                {
                    eatAndRemoveSoldier(i_BaseCoordination, i_DestinationCoordination);
                    isValid = true;
                }
                else
                {
                    throw new Exception("Error: If you have option to eat - so you must to eat!");
                }
            }

            return isValid;
        }

        private void eatAndRemoveSoldier(Coordination i_BaseCoordination, Coordination i_DestinationCoordination)
        {
            int          x, y;
            Soldier      willKillSoldier;
            Coordination deleteCoordination;

            if (i_BaseCoordination.GetX() < i_DestinationCoordination.GetX())
            {
                x = i_BaseCoordination.GetX() + 1;
            }
            else
            {
                x = i_BaseCoordination.GetX() - 1;
            }

            if (i_BaseCoordination.GetY() < i_DestinationCoordination.GetY())
            {
                y = i_BaseCoordination.GetY() + 1;
            }
            else
            {
                y = i_BaseCoordination.GetY() - 1;
            }

            deleteCoordination = new Coordination(x, y);
            willKillSoldier = m_TheBoard.GetSoliderFromCoordination(x, y);
            willKillSoldier.PlayerOfSoldier.SoldiersList.Remove(willKillSoldier);
            m_TheBoard.RemoveOneSoldierOnBoard(deleteCoordination);
        }

        private bool checkIfValidEatMove(Coordination i_BaseCoordination, Coordination i_DestinationCoordination, List<BaseAndDestinationCoordinations> i_EatCoordinations)
        {
            bool isCompatableCoordinations = false;

            foreach (BaseAndDestinationCoordinations currentEatCoordinations in i_EatCoordinations)
            {
                if (currentEatCoordinations.BaseCoordination.GetX() == i_BaseCoordination.GetX() && currentEatCoordinations.BaseCoordination.GetY() == i_BaseCoordination.GetY() && currentEatCoordinations.DestantionCoordination.GetX() == i_DestinationCoordination.GetX() && currentEatCoordinations.DestantionCoordination.GetY() == i_DestinationCoordination.GetY())
                {
                    isCompatableCoordinations = true;
                    break;
                }
            }

            return isCompatableCoordinations;
        }

        private string buildStringOfStep(Coordination i_BaseCoordination, Coordination i_DestantionCoordination)
        {
            char   xBase = (char)(i_BaseCoordination.GetX() + (int)'A');
            char   yBase = (char)(i_BaseCoordination.GetY() + (int)'a');
            char   xDestantion = (char)(i_DestantionCoordination.GetX() + (int)'A');
            char   yDestantion = (char)(i_DestantionCoordination.GetY() + (int)'a');
            string theStep = string.Format("{0}{1}>{2}{3}", xBase, yBase, xDestantion, yDestantion);

            return theStep;
        }
    }
}
