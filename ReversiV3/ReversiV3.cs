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
    public Label afbeelding = new Label();
    
    public int CountPlayerOne;
    public int CountPlayerTwo;
    int CurrentPlayer = 1; //  player 1 -> colour : red 
    int WaitingPlayer = 2; //  player 2 -> colour : blue
    int TempWaitingPlayer;
    int TempCurrentPlayer;

    public Speelbord()
    {
        Controls.Add(afbeelding);
        afbeelding.Location = new Point(100, 100);
        afbeelding.Size = new Size(301, 301); // voor pixels dat alles er op komt
        afbeelding.BackColor = Color.White;

        afbeelding.Paint += SetArray;
        afbeelding.Paint += TekenSpeelbord;
        afbeelding.MouseClick += BoardPosition;
        // afbeelding.Invalidate();
       
    }

  
    public void SetArray(object o, PaintEventArgs pea) // n x n array
    {
        spelArray = new int[n, n];
     
        for (int i = 0; i < n; i++) // maakt een nul-array
        {
            for (int j = 0; j < n; j++)
            {
                spelArray[i, j] = 0;
            }
        }

        // places down the 4 stones on the board.
        // arrays start from 0.
        spelArray[3, 3] = 1;
        spelArray[2, 2] = 1;
        spelArray[2, 3] = 2;
        spelArray[3, 2] = 2;
        

        // Counts the starting stones on the board.
        CountPlayerOne = 2;
        CountPlayerTwo = 2;
    }

    // Swaps who is currently playing. (review this laterrr)
    public void ChangePlayer(int CurrentPlayer, int WaitingPlayer)
    {
        TempWaitingPlayer = CurrentPlayer;
        TempCurrentPlayer = WaitingPlayer;
    }

    // Gets the position of the mouse to know where to place a stone.
    public void BoardPosition(object o, MouseEventArgs mea)
    {
        // divided by pixels because each square is 50 by 50, so then you get 1 until n again.
        // gets the location of where the mouse is, which we combine with a MouseClick to place the stone
         int x = mea.X / 50;
         int y = mea.Y / 50;
        spelArray[x, y] = 2;
        afbeelding.Invalidate();


        // prints the value of x and y on console to see if they are correct for the array.
        Debug.WriteLine($"Value of spelArray[x,y]: {spelArray[x,y]}");
        Debug.WriteLine($"This is x: {x}");
        Debug.WriteLine($"This is y: {y}");
        for (int i = 0; i<n; i++) // just prints every value in the array to check what happens
        {
            for (int j = 0; j<n; j++)
            {
                Debug.WriteLine($"This is je value for {i} and {j}: {spelArray[i, j]}");
            }
        }
        ChangePlayer(CurrentPlayer, WaitingPlayer);
      
    }

    public void PlaceStones(int x, int y, int Player)
    {
        spelArray[x, y] = 2; //Player; // set it to 2 to see if it even works LOL
        Debug.WriteLine(spelArray[x, y]);
    }

    // draws the game board and stones on the clicked positions
    public void TekenSpeelbord(object o, PaintEventArgs pea)
    {
       
        Graphics gr = pea.Graphics;
        for (int i = 0; i < n; i++)
        {
            int Begin_x = Midden_x - ((n / 2) * 50);
            for (int j = 0; j < n; j++)
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
}




