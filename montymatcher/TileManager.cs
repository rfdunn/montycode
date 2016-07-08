using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Montymatcher.Properties;

namespace MatchingGame
{
    public class TileManager
    {
        private List<TileImage> _pics = new List<TileImage>()
        {
            new TileImage(Resources.mozart, "mozart"),
            new TileImage(Resources._551px_SuperHornMK8, "superhorn"),
            new TileImage(Resources.dasistkitteh, "kitteh"),
            new TileImage(Resources._464029759_bca1c5711d_o, "46402"),
            new TileImage(Resources.arr2d2, "arr"),
            new TileImage(Resources.cat_beard, "catbeard"),
            new TileImage(Resources.dogmakingsausages, "dog"),
            new TileImage(Resources.mockingbird, "mockingbird"),
        };

        private Dictionary<string, TileImage> _assignedImages = new Dictionary<string, TileImage>();
        private Dictionary<string, bool> _visibleStatus = new Dictionary<string, bool>();

        // Use this Random object to choose random icons for the squares.
        private readonly Random _random = new Random();

        public TileManager(TableLayoutControlCollection controlCollection)
        {
            AssignPicsToSquares(controlCollection);
        }

        private void AssignImageToTile(string pictureBoxName)
        {
            int randomNumber = _random.Next(_pics.Count);
            TileImage img = _pics[randomNumber];
            img.AssignedTo++;
            if (img.AssignedTo == 2)
            {
                _pics.RemoveAt(randomNumber);
            }
            _assignedImages.Add(pictureBoxName, img);
            _visibleStatus.Add(pictureBoxName, false);
        }

        public Image GetImageForPictureBox(string pictureBoxName)
        {
            return _assignedImages[pictureBoxName].Image;
        }

        public bool AreTileImagesTheSame(PictureBox firstPictureBox, PictureBox secondPictureBox)
        {
            return _assignedImages[firstPictureBox.Name].Tag == _assignedImages[secondPictureBox.Name].Tag;
        }

        public void ShowTile(PictureBox pictureBox)
        {
            _visibleStatus[pictureBox.Name] = true;
            pictureBox.Image = GetImageForPictureBox(pictureBox.Name);
        }

        public void HideTile(PictureBox pictureBox)
        {
            _visibleStatus[pictureBox.Name] = false;
        }

        public int VisibleTiles
        {
            get { return _visibleStatus.Count(x => x.Value); }
        }

        public bool GameWon
        {
            get { return _visibleStatus.All(x => x.Value); }
        }

        public void UpdateControls(TableLayoutControlCollection controlCollection)
        {
            // Hide both icons.
            foreach (var valuePair in _visibleStatus)
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