using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public static class ImageExtensions
    {
        private static int FindLeftEdge(Image<Rgba32> image)
        {
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    if (image[x, y].A != 0)
                        return x;
                }
            }

            return -1;
        }

        private static int FindRightEdge(Image<Rgba32> image)
        {
            for (var x = image.Width - 1; x >= 0; x--)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    if (image[x, y].A != 0)
                        return x;
                }
            }

            return -1;
        }

        private static int FindTopEdge(Image<Rgba32> image)
        {
            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[x, y].A != 0)
                        return y;
                }
            }

            return -1;
        }

        private static int FindBottomEdge(Image<Rgba32> image)
        {
            for (var y = image.Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[x, y].A != 0)
                        return y;
                }
            }

            return -1;
        }

        public static Rectangle Trim(this Image<Rgba32> image)
        {
            var left = FindLeftEdge(image);
            var top = FindTopEdge(image);
            var right = FindRightEdge(image);
            var bottom = FindBottomEdge(image);
            return new Rectangle(left, top, right - left + 1, bottom - top + 1);
        }
    }
}