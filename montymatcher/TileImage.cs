using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingGame
{
    public class TileImage
    {
        public Image Image { get; private set; }
        public string Tag { get; private set; }
        public int AssignedTo { get; set; }

        public TileImage(Image image, string tag)
        {
            Image = image;
            Tag = tag;
            AssignedTo = 0;
        }
    }
}
