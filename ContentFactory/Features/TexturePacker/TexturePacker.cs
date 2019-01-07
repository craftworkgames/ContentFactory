using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public class TexturePacker
    {
        private readonly string _sourceDirectory;
        private readonly string _targetImagePath;
        private readonly string _targetDataPath;

        public TexturePacker(string sourceDirectory, string targetImagePath, string targetDataPath)
        {
            _sourceDirectory = sourceDirectory;
            _targetImagePath = targetImagePath;
            _targetDataPath = targetDataPath;
        }

        private static void CopyImageToImage(Image<Rgba32> sourceImage, Rectangle sourceRectangle, Image<Rgba32> targetImage, Point targetOffset)
        {
            for (var x = sourceRectangle.Left; x < sourceRectangle.Right; x++)
            {
                for (var y = sourceRectangle.Top; y < sourceRectangle.Bottom; y++)
                {
                    var px = targetOffset.X + x - sourceRectangle.Left;
                    var py = targetOffset.Y + y - sourceRectangle.Top;
                    var color = sourceImage[x, y];

                    targetImage[px, py] = color;
                }
            }
        }

        public TexturePackerData Pack()
        {
            var data = CreatePackInfo(_sourceDirectory);
            var packedWidth = data.PackedWidth;
            var packedHeight = data.PackedHeight;
            //var maxWidth = data.MaxWidth;
            //var maxHeight = data.MaxHeight;
            var targetOffset = Point.Empty;

            using (var targetImage = new Image<Rgba32>(packedWidth, packedHeight))
            {
                foreach (var filePath in Directory.GetFiles(_sourceDirectory, "*.png"))
                {
                    using (var sourceImage = Image.Load(filePath))
                    {
                        var sourceRectangle = sourceImage.Trim();

                        CopyImageToImage(sourceImage, sourceRectangle, targetImage, targetOffset);

                        var frame = new TexturePackerFrame(filePath, sourceRectangle, new Size(sourceImage.Width, sourceImage.Height), trimmed: true);

                        data.Frames.Add(frame);

                        targetOffset.X += sourceRectangle.Width;

                        if (targetOffset.X > packedWidth - sourceRectangle.Width)
                        {
                            targetOffset.X = 0;
                            targetOffset.Y += sourceRectangle.Height;
                        }
                    }
                }

                var outputDirectory = Path.GetDirectoryName(_targetImagePath);
                EnsureDirectoryExists(outputDirectory);

                using (var fileStream = File.OpenWrite(_targetImagePath))
                    targetImage.SaveAsPng(fileStream);
            }

            SaveDataFile(data, _targetDataPath);
            return data;
        }

        private static void SaveDataFile(TexturePackerData data, string targetDataPath)
        {
            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                Converters =
                {
                    new RectangleJsonConverter(),
                    new SizeJsonConverter()
                }
            };
            using (var fileStream = new StreamWriter(targetDataPath))
            {
                serializer.Serialize(fileStream, data);
            }
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        private static TexturePackerData CreatePackInfo(string sourceDirectory)
        {
            var maxWidth = 0;
            var maxHeight = 0;
            var count = 0;

            foreach (var filePath in Directory.GetFiles(sourceDirectory, "*.png"))
            {
                using (var image = Image.Load(filePath))
                {
                    if (image.Width > maxWidth)
                        maxWidth = image.Width;

                    if (image.Height > maxHeight)
                        maxHeight = image.Height;
                }

                count++;
            }

            return new TexturePackerData(maxWidth, maxHeight, count);
        }
    }
}