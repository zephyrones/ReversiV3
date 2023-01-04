using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

Application.Run(new Speelbord());

class Speelbord : Form
{
    public int Midden_x = 150; // midden van het label (voor 6x6 is dat 300/2)
    public int Midden_y = 150;
    public int n = 6; // sets the size of the field (make this variable later!!)
    public int[,] spelArray;
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
        afbeelding.MouseClick += BoardPosition;
        afbeelding.Paint += TekenSpeelbord;
   

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

    // Swaps who is currently playing.
    public void ChangePlayer(int CurrentPlayer, int WaitingPlayer)
    {
        TempWaitingPlayer = CurrentPlayer;
        TempCurrentPlayer = WaitingPlayer;
    }

    // Sets the position to the place the CurrentPlayer selected.
   // public void PlaceStones(int x, int y, int Player)
   // {
      //  spelArray[x, y] = Player; // set it to two to see if it even works LOL
        
   // }

    // Gets the position of the mouse to know where to place a stone.
    public void BoardPosition(object o, MouseEventArgs mea)
    {
        // divided by pixels because each square is 50 by 50, so then you get 1 until n again.
        // gets the location of where the mouse is, which we combine with a MouseClick to place the stone
        int x = mea.X / 50;
        int y = mea.Y / 50;

        afbeelding.Invalidate();
        spelArray[x, y] = 2;
        afbeelding.Invalidate();

        // prints the value of x and y on console to see if they are correct for the array.
        Debug.WriteLine(x);
        Debug.WriteLine(y);
      
        // PlaceStones(x, y, CurrentPlayer);

        

        ChangePlayer(CurrentPlayer, WaitingPlayer);
      
    }

 

    // Puts down a stone for the player who's turn it is.


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
                            pea.Graphics.FillEllipse(Brushes.Red, i*50, j*50, 50, 50);
                        }
                        else if (spelArray[i, j] == 2)
                        {
                            pea.Graphics.FillEllipse(Brushes.Blue, i*50, j*50, 50, 50);
                        }
                    }
                }
            }
        }




