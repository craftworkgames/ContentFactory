using System;
using System.Collections.Generic;

namespace ContentFactory.Features.TexturePacker
{
    public class TexturePackerData
    {
        public TexturePackerData(int maxWidth, int maxHeight, int count)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Count = count;
            Frames = new List<TexturePackerFrame>();
        }

        public int MaxWidth { get; }
        public int MaxHeight { get; }
        public int Count { get; }
        public int Columns => (int)Math.Sqrt(Count) + 1;
        public int Rows => Count / Columns + 1;
        public int PackedWidth => MaxWidth * Columns;
        public int PackedHeight => MaxHeight * Rows;
        public List<TexturePackerFrame> Frames { get; }
    }
}