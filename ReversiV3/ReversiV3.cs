using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

Application.Run(new Speelbord());

class Speelbord : Form
{
    public int Midden_x = 200; // midden van het label (voor 6x6 is dat 300/2)
    public int Midden_y = 200;
    public int n = 8; // sets the size of the field (make this variable later!!)
    public int[,] spelArray; // makes the array
    public int BoardX = 401;
    public int BoardY = 401;

    public Label afbeelding = new Label();
    public Button help = new Button();
    public Button nieuw_spel = new Button();
    public Button zes = new Button();
    public Button acht = new Button();
    public Button tien = new Button();

    public int CountPlayerOne;
    public int CountPlayerTwo;
    bool CurrentPlayer = false; //  player 1 -> colour : red // false is rood
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
        #endregion

        // region = buttons! 
        #region
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
        zes.Click += ButtonZes;
        acht.Click += ButtonAcht;
        tien.Click += ButtonTien;
        nieuw_spel.Click += ButtonNieuwSpel;
        afbeelding.MouseClick += BoardPosition;
        afbeelding.Paint += TekenSpeelbord;

        // afbeelding.Invalidate();

    }

    void ButtonZes(object o, EventArgs ea)
    {
        n = 6;
        BoardX = n * 50 + 1;
        BoardY = n * 50 + 1;
        Midden_x = 150;
        Midden_y = 150;
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
        afbeelding.Size = new Size(BoardX, BoardY);
        SetArray();
    }

    void ButtonNieuwSpel(object o, EventArgs ea)
    {
        SetArray();
        // afbeelding.Invalidate();
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


        // Counts the starting stones on the board.
        CountPlayerOne = 2;
        CountPlayerTwo = 2;

        afbeelding.Invalidate();
    }

    // Swaps who is currently playing. (review this laterrr)
    public void ChangePlayer()
    {
        CurrentPlayer = !CurrentPlayer;
    }

    public void PlaceStones(int x, int y, bool CurrentPlayer)
    {
        spelArray[x, y] = CurrentPlayer ? 1 : 2; //Player; // set it to 2 to see if it even works LOL
    } // If current player == true, dan is het 1, en als false dan 2. 

    // Gets the position of the mouse to know where to place a stone.
    public void BoardPosition(object sender, MouseEventArgs mea)
    {
        // divided by pixels because each square is 50 by 50, so then you get 1 until n again.
        // gets the location of where the mouse is, which we combine with a MouseClick to place the stone
        int x = mea.X / 50;
        int y = mea.Y / 50;

       if (isLegalMove(x, y, CurrentPlayer) == true)
            PlaceStones(x, y, CurrentPlayer);
        else
            Debug.WriteLine("DIT GEEFT DUS FALSE AAN HAHA");
     

        // region just prints the values to console. 
        #region
        // prints the value of x and y on console to see if they are correct for the array.
        Debug.WriteLine($"Value of spelArray[x,y]: {spelArray[x, y]}");
        Debug.WriteLine($"This is x: {x}");
        Debug.WriteLine($"This is y: {y}");

        for (int i = 0; i < n; i++) // just prints every value in the array to check what happens
        {
            for (int j = 0; j < n; j++)
            {
                Debug.WriteLine($"This is je value for {i} and {j}: {spelArray[i, j]}");
            }
        }
        #endregion

        ChangePlayer();
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
            }
        }
    }
    bool isLegalMove(int x, int y, bool CurrentPlayer) // needs reviewing 
    {
        /* to see if a move is legal, it needs to block an enemy piece in, 
         * you need to check in all 3 directions: horizontal (x+-), vertical(y+-), diagonal(xy+-).
         * it needs to then have at least 1 enemy piece in the "middle" and 
         * one of your pieces next to it. 
         * we do this using if statements?
         */

        // determine if the field is empty
        if (spelArray[x, y] != 0)
        {
            return false;
        }

        // determine which 'number' is your enemy

        int EnemyPlayer = CurrentPlayer ? 2 : 1;

        Debug.WriteLine("This is current" + CurrentPlayer);
        Debug.WriteLine("This is enemy" + EnemyPlayer);

        // determine if you are placing your stone next to an enemy stone.
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (row == 0 && col == 0)
                    return false;
                else
                    FindEnemy(x + row, y + col, row, col, EnemyPlayer);

            }
        }

        // determine if there is an enemy stone in each direction


        // rn if u chose a field on the outer ring it says out of bounds so
        // fix that so it just says ok me no work then but no crash
        return true; // just so it dun crash
    }

    bool FindEnemy(int x, int y, int row, int col, int EnemyPlayer, bool EnemyStone = false, bool YourStone = true)
    {
        // searches inside of the field size, else it won't work.
        if (x < 0 || y < 0 || x >= n || y >= n)
        {
            return false;
        }

        if (spelArray[x,y] == EnemyPlayer)
        {
            x = x + row;
            y = y + col;
            return FindPlayer(x, y, row, col, EnemyPlayer);
        }
        else

        return true; // zodat ie niet piiept
    }

    bool FindPlayer(int x, int y, int row, int col, int EnemyPlayer)
    {
        if (spelArray[x, y] == EnemyPlayer)
        {
            x = x + row;
            y = y + col;
            FindPlayer(x, y, row, col, EnemyPlayer);
            return true;
        }
       
        if (spelArray[x, y] != EnemyPlayer) 
        {
            return true;
        }
      else
        {
            return false;
        }
      
    }


}
    



 





