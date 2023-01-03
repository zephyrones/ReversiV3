using System;
using System.Drawing;
using System.Windows.Forms;

Application.Run(new Speelbord());

class Speelbord : Form 
{
    public int Midden_x = 150; // midden van het label (voor 6x6 is dat 300/2)
    public int Midden_y = 150;
    public int n = 6; //veldsize
    public int[,] spelArray;
    public Label afbeelding = new Label();


   
    public Speelbord()
    { // label maken
      // this.Size = new Size(500, 500);
      //this.BackColor = Color.White;
      //this.Paint += this.tekenSpeelbord;
      //this.Paint += this.tekenSteenB;
      // this.Paint += this.tekenSteenR;

        Controls.Add(afbeelding);
        afbeelding.Location = new Point(100, 100);
        afbeelding.Size = new Size(301, 301); // voor pixels dat alles er op komt
        afbeelding.BackColor = Color.White;

        afbeelding.Paint += tekenSpeelbord;
        afbeelding.Paint += tekenSteenB;
        afbeelding.Paint += tekenSteenR;



    }

    public void tekenSpeelbord(object o, PaintEventArgs pea) // vakje is 50 x 50
    {
        Graphics gr = pea.Graphics;
        for (int i = 0; i < n; i++)
        {
            int Begin_x = Midden_x - ((n / 2) * 50);
            for (int j = 0; j < n; j++)
            {
                int Begin_y = Midden_y - ((n / 2) * 50);
                gr.DrawRectangle(Pens.Black, Begin_x + (i * 50), Begin_y + (j * 50), 50, 50);
            }
        }
    }


    /* Hey girlies, jullie waren bezig met het op de goede plaats krijgen van de steentjes op het spelbord.
     * Een globaal idee is dat we variabelen berekenen afhankelijk van waar je klikt.
     * Dan kijken we hoe vaak het in 50 past (aka onze vakjes) en dan max n*50 (en niet in de min) 
     */

    public void tekenSteenB(object o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.FillEllipse(Brushes.Blue, 60, 60, 50, 50);
    }

    public void tekenSteenR(object o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.FillEllipse(Brushes.Red, 150, 150, 50, 50);
    }


    public void Array() // n x n array
    { 
    spelArray = new int[n, n];  

    for (int i = 0; i<n; i++) // maakt een nul-array
        {
          for(int j = 0; j<n; j++)
            {
                spelArray[i, j] = 0;
            }
        }

    spelArray[3, 3] = 1; // overschrijft waarde van de array!
    spelArray[4, 4] = 1;
    spelArray[3, 4] = 2;
    spelArray[4, 3] = 2;
    }


    // mouse click event maken, als je klikt dan leest ie x en y coordinaten af, en dan met de afronding iets leuks doen
    // 40 en 35 bijv is dan [1,1] 
    // i en j x 50 (grootte van vakje)

}

class Stenen
{
    public void tekenSteen(object o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.FillEllipse(Brushes.Indigo, 20, 20, 50, 50);
    }
}