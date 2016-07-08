using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Montymatcher.Properties;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // firstClicked points to the first Label control  that the player clicks, but it will be null if the player hasn't clicked a label yet.

        // secondClicked points to the second Label control that the player clicks.

        PictureBox firstPictureBoxClicked = null;
        PictureBox secondPictureBoxClicked = null;

        public TileManager TileManager;

        public Form1()
        {
            InitializeComponent();
            TileManager = new TileManager(tableLayoutPanel1.Controls);
        }

        private void picturebox_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching icons have been shown to the player, so ignore any clicks if the timer is running 
            if (timer1.Enabled == true)
                return;

            PictureBox clickedBox = sender as PictureBox;

            if (clickedBox != null)
            {
                // If the clicked label is black, the player clicked an icon that's already been revealed -- ignore the click.
                if (clickedBox.Image != null)
                    // All done - leave the if statements.
                    return;

                // If firstClicked is null, this is the first icon in the pair that the player clicked, so set firstClicked to the label that the player clicked, change its color to black, and return. 
                if (TileManager.VisibleTiles % 2 == 0)
                {
                    firstPictureBoxClicked = clickedBox;
                    TileManager.ShowTile(clickedBox);
                    // All done - leave the if statements.
                    return;
                }

                // If the player gets this far, the timer isn't running and firstClicked isn't null, so this must be the second icon the player clicked. Set its color to black.
                //secondPictureBoxClicked = clickedBox;
                secondPictureBoxClicked = clickedBox;
                TileManager.ShowTile(clickedBox);
                // Check to see if the player won.
                CheckForPictureBoxWinner();

                // If the player clicked two matching icons, keep them black and reset firstClicked and secondClicked so the player can click another icon. 
                if (TileManager.AreTileImagesTheSame(firstPictureBoxClicked, secondPictureBoxClicked))
                {
                    TileManager.ShowTile(firstPictureBoxClicked);
                    TileManager.ShowTile(secondPictureBoxClicked);
                    firstPictureBoxClicked = null;
                    secondPictureBoxClicked = null;
                    return;
                }

                // If the player gets this far, the player clicked two different icons, so start the timer (which will wait three quarters of  a second, and then hide the icons).
                timer1.Start();
            }
        }

        private void timer1PictureBox_Tick(object sender, EventArgs e)
        {
            // Stop the timer.
            timer1.Stop();

            TileManager.HideTile(firstPictureBoxClicked);
            TileManager.HideTile(secondPictureBoxClicked);


            TileManager.UpdateControls(tableLayoutPanel1.Controls);

            // Reset firstClicked and secondClicked so the next time a label is clicked, the program knows it's the first click.
            firstPictureBoxClicked = null;
            secondPictureBoxClicked = null;
        }

        private void CheckForPictureBoxWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, checking each one to see if its icon is matched.
            if (TileManager.GameWon)
            {
                MessageBox.Show("You matched all the icons!", "Well done!");
                Close();
            }
        }
    }
}