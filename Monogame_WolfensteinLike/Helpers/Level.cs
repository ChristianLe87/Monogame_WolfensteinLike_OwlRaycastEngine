using System.Linq;
using Microsoft.Xna.Framework;

namespace Monogame_WolfensteinLike
{
    //--struct to represent rects and tints of a level--//
    public class Level
    {
        public Rectangle[] sv { get; private set; }
        public Rectangle[] cts { get; private set; }

        //--current slice tint (for lighting)--//
        public Color[] st { get; private set; }
        public int[] currTexNum { get; private set; }

        public Level(int width, int height)
        {
            sv = Tools.SliceView(width, height);
            cts = new Rectangle[width];
            st = new Color[width];
            currTexNum = new int[width].Select(x => x = 1).ToArray();
        }
    }
}
