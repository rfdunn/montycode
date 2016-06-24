using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MatchingGame.Properties;

namespace MatchingGame
{
    public class TileManager
    {
        private List<TileImage> pics = new List<TileImage>()
        {
            new TileImage(Resources._mozart, "mozart"),
            new TileImage(Resources._551px_SuperHornMK8, "superhorn"),
            new TileImage(Resources.dasistkitteh, "kitteh"),
            new TileImage(Resources._464029759_bca1c5711d_o, "46402"),
            new TileImage(Resources.arr2d2, "arr"),
            new TileImage(Resources.cat_beard, "catbeard"),
            new TileImage(Resources.dogmakingsausages, "dog"),
            new TileImage(Resources.mockingbird, "mockingbird"),
        };

        private Dictionary<string, TileImage> assignedImages = new Dictionary<string, TileImage>();
        private Dictionary<string, bool> visibleStatus = new Dictionary<string, bool>();

        // Use this Random object to choose random icons for the squares.
        private readonly Random random = new Random();

        public TileManager(TableLayoutControlCollection controlCollection)
        {
            AssignPicsToSquares(controlCollection);
        }

        private void AssignImageToTile(string pictureBoxName)
        {
            int randomNumber = random.Next(pics.Count);
            TileImage img = pics[randomNumber];
            img.AssignedTo++;
            if (img.AssignedTo == 2)
            {
                pics.RemoveAt(randomNumber);
            }
            assignedImages.Add(pictureBoxName, img);
            visibleStatus.Add(pictureBoxName, false);
        }

        public Image GetImageForPictureBox(string pictureBoxName)
        {
            return assignedImages[pictureBoxName].Image;
        }

        public bool AreTileImagesTheSame(PictureBox firstPictureBox, PictureBox secondPictureBox)
        {
            return assignedImages[firstPictureBox.Name].Tag == assignedImages[secondPictureBox.Name].Tag;
        }

        public void ShowTile(PictureBox pictureBox)
        {
            visibleStatus[pictureBox.Name] = true;
            pictureBox.Image = GetImageForPictureBox(pictureBox.Name);
        }

        public void HideTile(PictureBox pictureBox)
        {
            visibleStatus[pictureBox.Name] = false;
        }

        public int VisibleTiles
        {
            get { return visibleStatus.Count(x => x.Value); }
        }

        public bool GameWon
        {
            get { return visibleStatus.All(x => x.Value); }
        }

        public void UpdateControls(TableLayoutControlCollection controlCollection)
        {
            // Hide both icons.
            foreach (var valuePair in visibleStatus)
            {
                PictureBox pictureBox = controlCollection[valuePair.Key] as PictureBox;
                if (pictureBox != null && valuePair.Value == false)
                {
                    pictureBox.Image = null;
                }
            }
        }

        private void AssignPicsToSquares(TableLayoutControlCollection controlCollection)
        {
            foreach (var control in controlCollection)
            {
                PictureBox box = control as PictureBox;
                if (box != null)
                {
                    AssignImageToTile(box.Name);
                }
            }
        }
    }
}