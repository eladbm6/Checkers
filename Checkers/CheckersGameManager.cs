using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05
{
    public class CheckersGameManager
    {
        public  const    int         k_NumberPlayers = 2;
        private readonly Player[]    m_ThePlayers = new Player[k_NumberPlayers];
        private          Board       m_TheBoard;
        private          Player      m_CurrentTurnPlayer;
        private          Player      m_LastGameWinner = null;

        public CheckersGameManager(int i_BoardSize, string i_MainPlayerName, string i_SecondPlayerName, PlayerType i_SecondPlayerType)
        {
            m_TheBoard = new Board(i_BoardSize);
            m_ThePlayers[0] = new Player(i_MainPlayerName, PlayerType.MainRealPlayer, i_BoardSize);
            m_ThePlayers[1] = new Player(i_SecondPlayerName, i_SecondPlayerType, i_BoardSize);
            m_CurrentTurnPlayer = m_ThePlayers[0]; // The default is the 1st player start the first turn.
            m_TheBoard.UpdateAllBoard(m_ThePlayers);
        }

        public bool ExecuteTheMove(Coordination i_Source, Coordination i_Destantion, out bool o_GameOver, out bool o_DrawGame)
        {
            bool needEatAgain = false;
            bool nextTurnHaveValidMoves = true;

            GameStep CurrentStep = new GameStep(m_CurrentTurnPlayer, m_TheBoard, i_Source, i_Destantion);
            needEatAgain = CurrentStep.StartStep();
            if (needEatAgain == false)
            {
                changeNextPlayerTurn();
            }

            nextTurnHaveValidMoves = m_CurrentTurnPlayer.CheckIfHaveValidMove(m_TheBoard);
            if (nextTurnHaveValidMoves == false)
            {
                changeNextPlayerTurn();
            }

            o_DrawGame = checkIfIsDraw();
            o_GameOver = isGameOver(o_DrawGame);

            return needEatAgain;
        }

        private void changeNextPlayerTurn()
        {
            if (m_CurrentTurnPlayer == m_ThePlayers[0])
            {
                m_CurrentTurnPlayer = m_ThePlayers[1];
            }
            else
            {
                m_CurrentTurnPlayer = m_ThePlayers[0];
            }
        }

        public void SetDetailsForNewGame()
        {
            buildSoldiersListsAgain();
            m_TheBoard.CleanBoardMatrix();
            m_TheBoard.UpdateAllBoard(m_ThePlayers);
            m_CurrentTurnPlayer = m_ThePlayers[0];
        }

        private bool isGameOver(bool i_DrawGame)
        {
            bool flag = false;

            if (i_DrawGame == true)
            {
                flag = true;
            }
            else
            if (m_ThePlayers[0].SoldiersList.Count == 0 || m_ThePlayers[1].SoldiersList.Count == 0)
            {
                flag = true;
                if (m_ThePlayers[0].SoldiersList.Count == 0)
                {
                    m_LastGameWinner = m_ThePlayers[1];
                }
                else
                {
                    m_LastGameWinner = m_ThePlayers[0];
                }
            }

            if (flag == true)
            {
                updateWinPoints();
            }

            return flag;
        }

        private void updateWinPoints()
        {
            int[] winPoints = new int[k_NumberPlayers];

            for (int i = 0; i < 2; i++)
            {
                winPoints[i] = m_ThePlayers[i].ConvertSoldiersToPoints();
            }

            if (winPoints[0] > winPoints[1])
            {
                m_ThePlayers[0].WinPoints += winPoints[0] - winPoints[1];
            }
            else
            {
                m_ThePlayers[1].WinPoints += winPoints[1] - winPoints[0];
            }
        }

        private void buildSoldiersListsAgain()
        {
            for (int i = 0; i < 2; i++)
            {
                m_ThePlayers[i].InitializeSoldiersList(m_TheBoard.BoardSize);
            }
        }

        private bool checkIfIsDraw()
        {
            bool isDraw = false;
            bool validMovesFor1stPlayer = m_ThePlayers[0].CheckIfHaveValidMove(m_TheBoard);
            bool validMovesFor2ndPlayer = m_ThePlayers[1].CheckIfHaveValidMove(m_TheBoard);

            if (validMovesFor1stPlayer == false && validMovesFor2ndPlayer == false)
            {
                isDraw = true;
            }

            return isDraw;
        }

        public Board TheBoard
        {
            get { return m_TheBoard; }
        }

        public Player[] ThePlayers
        {
            get { return m_ThePlayers; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentTurnPlayer; }
        }

        public int WinnerNumberPlayer()
        {
            int winnerNumber = 0;

            if (m_LastGameWinner == m_ThePlayers[0])
            {
                winnerNumber = 1;
            }
            else
            {
                winnerNumber = 2;
            }

            return winnerNumber;
        }
    }
}