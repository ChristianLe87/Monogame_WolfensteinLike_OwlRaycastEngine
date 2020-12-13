using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Monogame_WolfensteinLike
{
    public class Tools
    {
        /// <summary>
        /// Returns texture by texture name string
        /// </summary>
        public static Texture2D GetTexture(string imageName, string folder)
        {
            string absolutePath = new DirectoryInfo(Path.Combine(Path.Combine(Game1.contentManager.RootDirectory, folder), $"{imageName}.png")).ToString();

            FileStream fileStream = new FileStream(absolutePath, FileMode.Open);

            var result = Texture2D.FromStream(Game1.graphicsDeviceManager.GraphicsDevice, fileStream);
            fileStream.Dispose();

            return result;
        }

        /// <summary>
        /// Creates rectangle slices for each x in width.
        /// </summary>
        public static Rectangle[] SliceView(int width, int height)
        {
            Rectangle[] rectangles = new Rectangle[width];

            for (int i = 0; i < width; i++)
            {
                rectangles[i] = new Rectangle(i, 0, 1, height);
            }

            return rectangles;
        }

        //returns an initialised Level struct
        public static Level[] CreateLevels(int numLevels, int width, int height)
        {
            Level[] arr = new Level[numLevels];

            for (int i = 0; i < numLevels; i++)
            {
                arr[i] = new Level(width, height);
            }

            return arr;
        }
    }
}
