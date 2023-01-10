using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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

    public int CountPlayerOne = 2;
    public int CountPlayerTwo = 2;

    public int CurrentPlayer = 1;
    public int EnemyPlayer = 2;

    public bool HelpTurnOn = false;

    public string beurtstring;
    //  player 1 -> colour : red // false is rood
    //  player 2 -> colour : blue // true is blauw


    public Speelbord()
    {
        #region
        Controls.Add(afbeelding);
        afbeelding.Location = new Point(100, 100);
        afbeelding.Size = new Size(BoardX, BoardY); // voor pixels dat alles er op komt
        afbeelding.BackColor = Color.White;

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
        count1.Text = $"Rood heeft {CountPlayerOne} stenen";

        Controls.Add(count2);
        count2.Location = new Point(450, 10);
        count2.Size = new Size(100, 50);
        count2.BackColor = Color.LightGray;
        count2.Text = $"Blauw heeft {CountPlayerTwo} stenen";

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
        beurt.TextChanged += VeranderBeurtLabel;
        vier.Click += ButtonVier;
        zes.Click += ButtonZes;
        acht.Click += ButtonAcht;
        tien.Click += ButtonTien;
        nieuw_spel.Click += ButtonNieuwSpel;
        help.Click += ButtonHelp;
        afbeelding.MouseClick += BoardPosition;
        afbeelding.Paint += TekenSpeelbord;

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
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonNieuwSpel(object o, EventArgs ea)
    {
        n = 6;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 150;
        Midden_y = 150;
        CurrentPlayer = 1;
        EnemyPlayer = 2;
        SetArray();
        // afbeelding.Invalidate();

    }

    void ButtonHelp(object o, EventArgs ea)
    {
        HelpTurnOn = !HelpTurnOn;
        afbeelding.Invalidate();
    }

    string CurrentTurn()
    {
        if (CurrentPlayer == 1)
        {
            beurtstring = "Rood";
            beurt.Invalidate();
            return beurtstring;
        }

        else if (CurrentPlayer == 2)
        {
            beurtstring = "Blauw";
            beurt.Invalidate();
            return beurtstring;
        }

        else
        {
            beurtstring = "Niemand";
            return beurtstring;
        }

    }

    void VeranderBeurtLabel(object o, EventArgs ea)
    {
        CurrentTurn();
        beurt.Invalidate();
    }

    public void SetArray() // n x n array
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


        afbeelding.Invalidate();
    }

    // Swaps who is currently playing. (review this laterrr)
    public void ChangePlayer(int CurrentPlayerT, int EnemyPlayerT)
    {
        if (CurrentPlayer == 1)
        {
            CurrentPlayer = EnemyPlayer;

        }
        else if (CurrentPlayer == 2)
        {
            CurrentPlayer = 1;
        }
    }

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

        // region just prints the values to console. 
        #region
        // prints the value of x and y on console to see if they are correct for the array.
        Debug.WriteLine($"Value of spelArray[x,y]: {spelArray[x, y]}");
        Debug.WriteLine("Wie is er aan de beurt:" + beurtstring);
        Debug.WriteLine("Count player 1:" + CountPlayerOne);
        Debug.WriteLine("Count player 2:" + CountPlayerTwo);
        Debug.WriteLine("Wat is winnaar" + Winner());
           
     
        #endregion

        if (ValidMovesLeft() == true)
        {
            if (isLegalMove(x, y))
            {
                spelArray[x, y] = CurrentPlayer;
                EntrapmentFinder(x, y);
                Counter();
                ChangePlayer(CurrentPlayer, EnemyPlayer);
                afbeelding.Invalidate();
            }
        }
        else
        {
            MessageBox.Show(Winner());
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

        for (int i = 0; i < n; i++) // places the stones in position
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

                else if (spelArray[i, j] == 0 && isLegalMove(i, j) == true && HelpTurnOn == true)
                {
                    pea.Graphics.FillEllipse(Brushes.Green, i * 50 + (5 / 2 * i), j * 50 + (3 * j), 25, 25);
                }

            }
        }
    }

    bool isLegalMove(int x, int y) // needs reviewing 
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
                if (BadStone == true) // 
                    return false;
            }

            if (PlayStone) // There is an enemy stone there.
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

    public bool ShowMeHelp()
    {
        for (int r = 0; r < n; r++)
            for (int c = 0; c < n; c++)
                if (spelArray[r, c] == 0)
                {
                    isLegalMove(r, c);
                    return true;
                }

        return false;
    }

    public void Counter()
    {
        for (int i = 0; i < n; i++) // places the stones in position
            for (int j = 0; j < n; j++)
            {
                if (spelArray[i, j] == 1)
                {
                    CountPlayerOne++;

                }

                else if (spelArray[i, j] == 2)
                {
                    CountPlayerTwo++;
                }
            }
    }

    public bool ValidMovesLeft() 
    {
        if (ShowMeHelp() == false) // Checks if player has possible moves, if not, turn passes.
        {
            {
             ChangePlayer(CurrentPlayer, EnemyPlayer);
            }
            if (ShowMeHelp() == false)  // If both players cannot move, game ends and shows result.
            {
                return false;
            }
            return true;
        }
        
        else
        {
            return true;
        }
       
    }

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









