using System.IO;
using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public class TexturePackerFrame
    {
        public TexturePackerFrame(string fullPath, Rectangle sourceRectangle, Size sourceSize, bool trimmed)
        {
            Name = Path.GetFileName(fullPath);
            SourceRectangle = sourceRectangle;
            SourceSize = sourceSize;
            Trimmed = trimmed;
        }

        public string Name { get; }
        public Rectangle SourceRectangle { get; }
        public Size SourceSize { get; }
        public bool Trimmed { get; }
    }
}
