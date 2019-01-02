using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public class TexturePackerFrame
    {
        public TexturePackerFrame(string name, Rectangle sourceRectangle)
        {
            Name = name;
            SourceRectangle = sourceRectangle;
        }

        public string Name { get; }
        public Rectangle SourceRectangle { get; }
    }
}
