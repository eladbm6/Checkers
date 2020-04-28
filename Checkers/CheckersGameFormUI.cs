using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace B18_Ex05
{
    public class CheckersGameFormUI : Form
    {
        // Interior Enums
        private enum eGameOverTypes
        {
            DrawGame,
            EndGame,
        }

        // Data Members.
        private Button[,]           m_CoordinationButtons;
        private int                 m_BoardSizeUI;
        private bool                m_isButtonwasChoosen = false;
        private CheckersGameManager m_CheckersGameLogic;
        private Coordination        m_CurrentSource;
        private Coordination        m_CurrentDestination;

        // C'tor
        public CheckersGameFormUI(CheckersGameManager i_GameLogic)
        {
            m_CheckersGameLogic = i_GameLogic;
            m_BoardSizeUI = m_CheckersGameLogic.TheBoard.BoardSize;
            m_CoordinationButtons = new Button[m_BoardSizeUI, m_BoardSizeUI];
            InitializeComponent();
        }

        // Methods
        private void InitializeComponent()
        {
            Label labelFirstPlayerName = new Label();
            Label labelSecondPlayerName = new Label();

            this.StartPosition = FormStartPosition.CenterScreen;
            labelFirstPlayerName.AutoSize = true;
            labelFirstPlayerName.Location = new System.Drawing.Point(10, 10);
            labelFirstPlayerName.Name = "LabelFirstPlayerName";
            labelFirstPlayerName.Size = new System.Drawing.Size(100, 100);
            labelFirstPlayerName.TabIndex = 7;
            labelFirstPlayerName.Text = string.Format("Player 1: {0}", m_CheckersGameLogic.ThePlayers[0].PlayerName);
            labelSecondPlayerName.AutoSize = true;
            labelSecondPlayerName.Location = new System.Drawing.Point(120, 10);
            labelSecondPlayerName.Name = "LabelSecondPlayerName";
            labelSecondPlayerName.Size = new System.Drawing.Size(100, 100);
            labelSecondPlayerName.TabIndex = 7;
            labelSecondPlayerName.Text = string.Format("Player 2: {0}", m_CheckersGameLogic.ThePlayers[1].PlayerName);
            setFormWidthAndHeight(m_BoardSizeUI);
            this.Text = "Checkers Game";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            Controls.Add(labelFirstPlayerName);
            Controls.Add(labelSecondPlayerName);
            initializeButton();
        }

        private void initializeButton()
        {
            for (int x = 0; x < m_BoardSizeUI; x++)
            {
                for (int y = 0; y < m_BoardSizeUI; y++)
                {
                    if (((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0)))
                    {
                        addDisableButton(x, y);
                    }
                    else
                    {
                        addEnableButton(x, y);
                    }
                }
            }
        }

        private void setFormWidthAndHeight(int i_BoardSize)
        {
            this.Height = 310 + (40 * (i_BoardSize - 6));
            this.Width = 245 + (40 * (i_BoardSize - 6));
        }

        private void addEnableButton(int i_X, int i_Y)
        {
            Button coordinationButton = new Button();

            coordinationButton.Location = new System.Drawing.Point(i_X * 40, (i_Y + 1) * 40);
            coordinationButton.Name = "Coordination Button";
            coordinationButton.Size = new System.Drawing.Size(40, 40);
            coordinationButton.UseVisualStyleBackColor = true;
            m_CoordinationButtons[i_X, i_Y] = coordinationButton;
            coordinationButton.Click += buttonWas_Click;
            Controls.Add(m_CoordinationButtons[i_X, i_Y]);
        }

        private void addDisableButton(int i_X, int i_Y)
        {
            Button blockButton = new Button();

            blockButton.Location = new System.Drawing.Point(i_X * 40, (i_Y + 1) * 40);
            blockButton.Name = "Block Button";
            blockButton.Size = new System.Drawing.Size(40, 40);
            blockButton.UseVisualStyleBackColor = true;
            m_CoordinationButtons[i_X, i_Y] = blockButton;
            Controls.Add(m_CoordinationButtons[i_X, i_Y]);
            blockButton.Enabled = false;
            blockButton.BackColor = System.Drawing.Color.Gray;
        }

        private void buttonWas_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (!m_isButtonwasChoosen)
            {
                pressedButton.BackColor = System.Drawing.Color.LightBlue;
                pressedButton.Click -= buttonWas_Click;
                pressedButton.Click += buttonWas_ClickAgain;
                m_isButtonwasChoosen = true;
                m_CurrentSource = new Coordination();
                getCoordinationFromClick(ref m_CurrentSource, pressedButton);
            }
            else if (m_isButtonwasChoosen)
            {
                m_CurrentDestination = new Coordination();
                getCoordinationFromClick(ref m_CurrentDestination, pressedButton);
                transferToLogicTheMoveChoosed(m_CurrentSource, m_CurrentDestination);
                m_isButtonwasChoosen = false;
                buttonWas_ClickAgain(m_CoordinationButtons[m_CurrentSource.GetX(), m_CurrentSource.GetY()], null);
            }
        }

        private void transferToLogicTheMoveChoosed(Coordination i_Source, Coordination i_Destination)
        {
            bool isGameOver;
            bool isDrawGame;
            bool isOneMoreTurn = false;

            try
            {
                isOneMoreTurn = m_CheckersGameLogic.ExecuteTheMove(i_Source, i_Destination, out isGameOver, out isDrawGame);
                updateStatusGameOnForm(isGameOver, isDrawGame);
                if (isGameOver == false)
                {
                    if (isOneMoreTurn == false)
                    {
                        if (m_CheckersGameLogic.CurrentPlayer.TypePlayer == PlayerType.RivalComputerPlayer)
                        {
                            m_CheckersGameLogic.ExecuteTheMove(i_Source, i_Destination, out isGameOver, out isDrawGame);
                            updateStatusGameOnForm(isGameOver, isDrawGame);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Note: Because the rule of must eat when you can, so you have one more turn!");
                    }
                }
            }
            catch (Exception generalException)
            {
                MessageBox.Show(generalException.Message);
            }
        }

        private void updateStatusGameOnForm(bool i_GameOver, bool i_DrawGame)
        {
            cleanBoardOnForm();
            if (i_DrawGame == true)
            {
                showMessageBoxOfGameOver(eGameOverTypes.DrawGame);
            }
            else
            if (i_GameOver == true)
            {
                showMessageBoxOfGameOver(eGameOverTypes.EndGame);
            }

            drawBoardOnForm();
        }

        private void showMessageBoxOfGameOver(eGameOverTypes i_GameOverType)
        {
            string noteOfGameOver = null;
            string gameOverTitle = "Game over!";

            if (i_GameOverType == eGameOverTypes.DrawGame)
            {
                noteOfGameOver = "Tie";
            }
            else
            if (i_GameOverType == eGameOverTypes.EndGame)
            {
                noteOfGameOver = string.Format("Player {0} Won!", m_CheckersGameLogic.WinnerNumberPlayer());
            }

            noteOfGameOver += string.Format("{0}Another Round?", Environment.NewLine);
            DialogResult gameOverDialog = MessageBox.Show(noteOfGameOver, gameOverTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (gameOverDialog == DialogResult.No)
            {
                this.Close();
            }
            else
                if (gameOverDialog == DialogResult.Yes)
            {
                m_CheckersGameLogic.SetDetailsForNewGame();
            }
        }

        public void buttonWas_ClickAgain(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            pressedButton.BackColor = System.Drawing.Color.White;
            pressedButton.Click -= buttonWas_ClickAgain;
            pressedButton.Click += buttonWas_Click;
            m_isButtonwasChoosen = false;
        }

        private void getCoordinationFromClick(ref Coordination i_Coordination, Button i_PressedButton)
        {
            int widthBoard = m_CoordinationButtons.GetLength(0);
            int heightBoard = m_CoordinationButtons.GetLength(1);

            for (int x = 0; x < widthBoard; ++x)
            {
                for (int y = 0; y < heightBoard; ++y)
                {
                    if (m_CoordinationButtons[x, y].Equals(i_PressedButton))
                    {
                        i_Coordination.SetCoordination(x, y);
                    }
                }
            }
        }

        private void cleanBoardOnForm()
        {
            foreach (Button theButton in m_CoordinationButtons)
            {
                theButton.Text = string.Empty;
            }
        }

        private void drawBoardOnForm()
        {
            Board theBoard = m_CheckersGameLogic.TheBoard;
            int   sizeBoard = theBoard.BoardSize;

            for (int i = 0; i < sizeBoard; i++)
            {
                for (int j = 0; j < sizeBoard; j++)
                {
                    if (theBoard.GetSoliderFromCoordination(i, j) != null)
                    {
                        if (theBoard.GetSoliderFromCoordination(i, j).SoldierPlayerType == PlayerType.MainRealPlayer)
                        {
                            if (theBoard.GetSoliderFromCoordination(i, j).TypeOfSoldier == SoldierType.RegularSoldier)
                            {
                                m_CoordinationButtons[i, j].Text = "X";
                            }
                            else
                            {
                                m_CoordinationButtons[i, j].Text = "K";
                            }
                        }
                        else
                        {
                            if (theBoard.GetSoliderFromCoordination(i, j).TypeOfSoldier == SoldierType.RegularSoldier)
                            {
                                m_CoordinationButtons[i, j].Text = "O";
                            }
                            else
                            {
                                m_CoordinationButtons[i, j].Text = "U";
                            }
                        }
                    }
                    else
                    {
                        m_CoordinationButtons[i, j].Text = string.Empty;
                    }
                }
            }
        }

        public void StartGame()
        {
            drawBoardOnForm();
            this.ShowDialog();
        }
    }
}
