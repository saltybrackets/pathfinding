using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media.Imaging;
using PathFinding.Map;


namespace PathFinding
{
    public class Maze
    {
        #region Constants
        public const PixelFormat DefaultPixelFormat = PixelFormat.Format24bppRgb;
        #endregion


        #region Fields
        private MazeData data;
        #endregion


        #region Constructors
        public Maze(MazeData data)
        {
            this.data = data;
        }


        public Maze(Bitmap bitmap)
        {
            this.data = ParseBitmap(bitmap);
        }
        #endregion


        #region Properties
        public MazeData Data
        {
            get { return this.data; }
        }
        #endregion


        public int GetEmptyPixelsAmount()
        {
            MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
            return Array.FindAll(mapFeatures, x => x == MapFeature.Empty).Length;
        }


        public int GetEndPixelsAmount()
        {
            MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
            return Array.FindAll(mapFeatures, x => x == MapFeature.End).Length;
        }


        public int GetStartPixelsAmount()
        {
            MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
            return Array.FindAll(mapFeatures, x => x == MapFeature.Start).Length;
        }


        public int GetWallPixelsAmount()
        {
            MapFeature[] mapFeatures = this.data.Features.Cast<MapFeature>().ToArray();
            return Array.FindAll(mapFeatures, x => x == MapFeature.Wall).Length;
        }


        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            BitmapImage image = new BitmapImage();
            using (MemoryStream mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("Maze:")
                .Append("\nEmptyPixels: ").Append(GetEmptyPixelsAmount())
                .Append("\nWallPixels: ").Append(GetWallPixelsAmount())
                .Append("\nStartPixels: ").Append(GetStartPixelsAmount())
                .Append("\nEndPixels: ").Append(GetEndPixelsAmount())
                .ToString();
        }


        /// <summary>
        /// Parse a bitmap into map structures by analyzing pixel color.
        /// <see href="http://stackoverflow.com/questions/6020406/travel-through-pixels-in-bmp">
        /// Derived from a bitmap pixel analysis algorithm found here.
        /// </see>
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="pixelFormat">Color depth to process at.</param>
        public static MazeData ParseBitmap(Bitmap bitmap, PixelFormat pixelFormat = DefaultPixelFormat)
        {
            Rectangle dimensions = new Rectangle(
                0, 0,
                bitmap.Width, bitmap.Height);
            bitmap = bitmap.Clone(dimensions, DefaultPixelFormat);
            BitmapData data = bitmap.LockBits(
                dimensions,
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            int scanWidth = Math.Abs(data.Stride);
            int byteLength = scanWidth*data.Height;
            byte[] rgbValues = new byte[byteLength];
            int r, g, b, pixelBytes;
            Marshal.Copy(data.Scan0, rgbValues, 0, byteLength);

            int black = Color.Black.ToArgb();
            int blue = Color.Blue.ToArgb();
            int red = Color.Red.ToArgb();
            int pixelColor;

            MapFeature[,] features = new MapFeature[bitmap.Width, bitmap.Height];
            Point start = new Point();
            Point end = new Point();
            int rgbValuesIndex = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    try
                    {
                        pixelBytes = x*3;
                        r = rgbValues[(y*scanWidth) + pixelBytes + 2];
                        g = rgbValues[(y*scanWidth) + pixelBytes + 1];
                        b = rgbValues[(y*scanWidth) + pixelBytes + 0];
                        pixelColor = Color.FromArgb(255, r, g, b).ToArgb();

                        if (pixelColor == black)
                            features[x, y] = MapFeature.Wall;
                        else if (pixelColor == red)
                        {
                            features[x, y] = MapFeature.Start;
                            start = new Point(x, y);
                        }

                        else if (pixelColor == blue)
                        {
                            features[x, y] = MapFeature.End;
                            end = new Point(x, y);
                        }
                            
                        // Else, it's empty, which is the default enum value.
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Failed with: column:" + x + " y:" + x + "\n" +
                                            "Dimensions:" + data.Width + "x" + data.Height, e);
                    }

                    rgbValuesIndex++;
                }
            }

            return new MazeData(bitmap, features, start, end);
        }

    }
}