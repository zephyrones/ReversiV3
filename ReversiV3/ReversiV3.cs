using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

Application.Run(new Speelbord());

class Speelbord : Form
{
    public int Midden_x = 150; // midden van het label (voor 6x6 is dat 300/2)
    public int Midden_y = 150;
    public int n = 6; // sets the size of the field (make this variable later!!)
    public int[,] spelArray; // makes the array
    public int BoardX = 301;
    public int BoardY = 301;

    public Label afbeelding = new Label();
    public Label beurt = new Label();
    public Label count1 = new Label();
    public Label count2 = new Label();

    public Button help = new Button();
    public Button nieuw_spel = new Button();
    public Button vier = new Button();
    public Button zes = new Button();
    public Button acht = new Button();
    public Button tien = new Button();

    public int CountPlayerOne;
    public int CountPlayerTwo;

    public int CurrentPlayer = 1;
    public int EnemyPlayer = 2;

    public bool HelpTurnOn = false;
    public bool ValidMove;

    public string beurtstring;


    public Speelbord()
    {
        #region
        Controls.Add(afbeelding);
        afbeelding.Location = new Point(100, 100);
        afbeelding.Size = new Size(BoardX, BoardY); // voor pixels dat alles er op komt
        afbeelding.BackColor = Color.LightPink;

        Controls.Add(help);
        help.Location = new Point(10, 10);
        help.Size = new Size(50, 30);
        help.Text = "Help!";

        Controls.Add(nieuw_spel);
        nieuw_spel.Location = new Point(60, 10);
        nieuw_spel.Size = new Size(80, 30);
        nieuw_spel.Text = "Nieuw spel";

        Controls.Add(beurt);
        beurt.Location = new Point(200, 10);
        beurt.Size = new Size(100, 50);
        beurt.BackColor = Color.LightGray;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";

        Controls.Add(count1);
        count1.Location = new Point(350, 10);
        count1.Size = new Size(100, 50);
        count1.BackColor = Color.LightGray;
        count1.Text = $"Rood heeft 2 stenen";

        Controls.Add(count2);
        count2.Location = new Point(450, 10);
        count2.Size = new Size(100, 50);
        count2.BackColor = Color.LightGray;
        count2.Text = $"Blauw heeft 2 stenen";

        #endregion

        // region = buttons! 
        #region
        Controls.Add(vier);
        vier.Location = new Point(160, 50);
        vier.Size = new Size(50, 30);
        vier.Text = "4x4";

        Controls.Add(zes);
        zes.Location = new Point(10, 50);
        zes.Size = new Size(50, 30);
        zes.Text = "6x6";

        Controls.Add(acht);
        acht.Location = new Point(60, 50);
        acht.Size = new Size(50, 30);
        acht.Text = "8x8";

        Controls.Add(tien);
        tien.Location = new Point(110, 50);
        tien.Size = new Size(50, 30);
        tien.Text = "10x10";


        #endregion


        SetArray();
        vier.Click += ButtonVier;
        zes.Click += ButtonZes;
        acht.Click += ButtonAcht;
        tien.Click += ButtonTien;
        nieuw_spel.Click += ButtonNieuwSpel;
        help.Click += ButtonHelp;
        afbeelding.MouseClick += BoardPosition;
        afbeelding.Paint += TekenSpeelbord;
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
    }

    void ButtonVier(object o, EventArgs ea)
    {
        n = 4;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 100;
        Midden_y = 100;
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        count1.Text = $"Rood heeft 2 stenen";
        count2.Text = $"Blauw heeft 2 stenen";
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonZes(object o, EventArgs ea)
    {
        n = 6;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 150;
        Midden_y = 150;
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        count1.Text = $"Rood heeft 2 stenen";
        count2.Text = $"Blauw heeft 2 stenen";
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonAcht(object o, EventArgs ea)
    {
        n = 8;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 200;
        Midden_y = 200;
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        count1.Text = $"Rood heeft 2 stenen";
        count2.Text = $"Blauw heeft 2 stenen";
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonTien(object o, EventArgs ea)
    {
        n = 10;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 250;
        Midden_y = 250;
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        count1.Text = $"Rood heeft 2 stenen";
        count2.Text = $"Blauw heeft 2 stenen";
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonNieuwSpel(object o, EventArgs ea)
    {
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        count1.Text = $"Rood heeft 2 stenen";
        count2.Text = $"Blauw heeft 2 stenen";
        beurt.Invalidate();
        count1.Invalidate();
        count2.Invalidate();
        SetArray();

    }

    void ButtonHelp(object o, EventArgs ea)
    {
        HelpTurnOn = !HelpTurnOn;
        afbeelding.Invalidate();
    }

    string CurrentTurn() // Displays who's turn it is on a label.
    {
        if (CurrentPlayer == 1)
        {
            beurtstring = "Rood";
            beurt.Invalidate();
        }

        else if (CurrentPlayer == 2)
        {
            beurtstring = "Blauw";
            beurt.Invalidate();
        }

        return beurtstring;
    }

    public void SetArray()
    {
        spelArray = new int[n, n];

        // we make a zero-array as our starting point. 
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                spelArray[i, j] = 0;
            }
        }

        // places down the four starting stones in their respective positions.
        spelArray[n / 2, n / 2] = 1;
        spelArray[n / 2 - 1, n / 2 - 1] = 1;
        spelArray[n / 2 - 1, n / 2] = 2;
        spelArray[n / 2, n / 2 - 1] = 2;

        Counter();

        afbeelding.Invalidate();
    }

    // Swaps who is currently playing. 
    public void ChangePlayer(int CurrentPlayerT, int EnemyPlayerT)
    {
        Counter(); // updates the label that counts the stones on the board per player.
        count1.Text = $"Rood heeft {CountPlayerOne} stenen";
        count2.Text = $"Blauw heeft {CountPlayerTwo} stenen";
        count1.Invalidate();
        count2.Invalidate();

        // swaps players
        if (CurrentPlayer == 1)
        {
            CurrentPlayer = EnemyPlayer;

        }
        else if (CurrentPlayer == 2)
        {
            CurrentPlayer = 1;
        }

        // updates who's turn it is and displays it on the label.
        CurrentTurn();
        beurt.Text = $"{CurrentTurn()} is aan de beurt";
        beurt.Invalidate();

        ShowMeHelp();
    }

    // draws the stones on the game field
    public void PlaceStones(int x, int y, int row, int col)
    {

        if (spelArray[x, y] != CurrentPlayer) // Checks if the place isn't already occupied by the player.
        {
            spelArray[x, y] = CurrentPlayer;
            PlaceStones(x + row, y + col, row, col); // places stone
        }

    }

    // Gets the position of the mouse to know where to place a stone.
    public void BoardPosition(object sender, MouseEventArgs mea)
    {
        // divided by pixels because each square is 50 by 50, so then you get 1 until n again.
        // gets the location of where the mouse is, which we combine with a MouseClick to place the stone
        int x = mea.X / 50;
        int y = mea.Y / 50;


        // Checks if the player who's turn it is has any valid moves. And if so, places the stone.
        if (ShowMeHelp() == true)
        {
            if (isLegalMove(x, y))
            {
                spelArray[x, y] = CurrentPlayer;
                EntrapmentFinder(x, y);
                ChangePlayer(CurrentPlayer, EnemyPlayer);
                afbeelding.Invalidate();
                this.Invalidate();
            }
        }
        if (ShowMeHelp() == false) // If there are no possible moves, it shows the winner. 
        {
            MessageBox.Show($"{beurtstring} moet passen!");
            ChangePlayer(CurrentPlayer, EnemyPlayer);
            if (ShowMeHelp() == true)
            {

                if (isLegalMove(x, y))
                {
                    spelArray[x, y] = CurrentPlayer;
                    EntrapmentFinder(x, y);
                    ChangePlayer(CurrentPlayer, EnemyPlayer);
                    afbeelding.Invalidate();
                    this.Invalidate();
                }
            }
            else
            {
                MessageBox.Show(Winner());
            }
        }

        afbeelding.Invalidate();
    }

    // draws the game board and stones on the clicked positions
    public void TekenSpeelbord(object o, PaintEventArgs pea)
    {

        Graphics gr = pea.Graphics;
        for (int i = 0; i <= n; i++)
        {
            int Begin_x = Midden_x - ((n / 2) * 50);
            for (int j = 0; j <= n; j++)
            {
                int Begin_y = Midden_y - ((n / 2) * 50);
                gr.DrawRectangle(Pens.Black, Begin_x + (i * 50), Begin_y + (j * 50), 50, 50);
                // the size of a rectangle is 50 by 50.
            }
        }

        for (int i = 0; i < n; i++) // finds the location in the array
        {
            for (int j = 0; j < n; j++)
            {
                if (spelArray[i, j] == 1)
                {
                    pea.Graphics.FillEllipse(Brushes.Red, i * 50, j * 50, 50, 50);
                }
                else if (spelArray[i, j] == 2)
                {
                    pea.Graphics.FillEllipse(Brushes.Blue, i * 50, j * 50, 50, 50);
                }

                // draws the stones on the possible moves for the help function.
                else if (spelArray[i, j] == 0 && isLegalMove(i, j) == true && HelpTurnOn == true)
                {
                    pea.Graphics.FillEllipse(Brushes.Green, i * 50 + (5 / 2 * i), j * 50 + (3 * j), 25, 25);
                }

            }
        }
    }

    // checks if the player can make a move.
    bool isLegalMove(int x, int y)
    {

        if (spelArray[x, y] != 0) // if there is a stone on the board you cannot place another stone.
            return false;
        for (int row = -1; row <= 1; row++)
            for (int col = -1; col <= 1; col++)
                if (!(row == 0 && col == 0))
                    if (FindEnemy(x + row, y + col, row, col))
                        return true;
        return false;
    }

    // Finds the stones that are entrapped and flips them to the correct color.
    public void EntrapmentFinder(int x, int y)
    {
        for (int row = -1; row <= 1; row++)
            for (int col = -1; col <= 1; col++)
                if (!(row == 0 && col == 0))
                    FindEnemy(x + row, y + col, row, col, true);
    }

    bool FindEnemy(int x, int y, int row, int col, bool PlayStone = false, bool BadStone = true)
    {
        // Checks if the direction is still part of the game field.
        if (x < 0 || y < 0 || x >= n || y >= n)
        {
            return false;
        }

        if (spelArray[x, y] == CurrentPlayer) // Returns false if your own stone is right next to you.
        {
            {
                if (BadStone == true) // If there is no enemy stone next to you and returns false.
                    return false;
            }

            if (PlayStone) // If there is an enemy stone it can place it
            {
                PlaceStones(-1 * row + x, -1 * col + y, -1 * row, -1 * col);
            }
            return true;
        }
        else
        {
            if (spelArray[x, y] == 0) // If there's no stone next to it you cannot entrap a stone.
            {
                return false;
            }

            // Checks the next space if there is an enemy stone next to you.
            x = x + row;
            y = y + col;

            return FindEnemy(x, y, row, col, PlayStone, false);
        }
    }

    // Finds all legal moves so we can draw the help circles.
    public bool ShowMeHelp()
    {
        for (int r = 0; r < n; r++)
            for (int c = 0; c < n; c++)
                if (spelArray[r, c] == 0)
                {
                    if (isLegalMove(r, c) == true)
                        return true;
                }
        return false;
    }

    // checks most recent array and counts the stones
    public void Counter()
    {
        CountPlayerOne = 0;
        CountPlayerTwo = 0;

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (spelArray[i, j] == 1)
                {
                    CountPlayerOne = CountPlayerOne + 1;
                }

                else if (spelArray[i, j] == 2)
                {

                    CountPlayerTwo = CountPlayerTwo + 1;
                }
            }
    }


    /*
    public bool ValidMovesLeft()
    {
        if (ShowMeHelp() == false) // Checks if player has possible moves, if not, turn passes.
        {
            ChangePlayer(CurrentPlayer, EnemyPlayer);
                
                if (ShowMeHelp() == false)
            {
                return false;
            }
                
        }
        return true;

        else
            return true;
    }
    */

    // Compares the two stone counters and decides who won.
    public string Winner()
    {
        string winnaar;

        if (CountPlayerOne > CountPlayerTwo)
        {
            winnaar = "Rood heeft gewonnen!";
            return winnaar;
        }
        else if (CountPlayerOne < CountPlayerTwo)
        {
            winnaar = "Blauw heeft gewonnen!";
            return winnaar;
        }
        else
        {
            winnaar = "Remise!";
            return winnaar;
        }

    }




}








