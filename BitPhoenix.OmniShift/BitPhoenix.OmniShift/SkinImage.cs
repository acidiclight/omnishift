using System;
using System.Collections.Generic;
using System.Text;

namespace BitPhoenix.OmniShift
{
    public class SkinImage
    {
        private byte[] _data = null;
        private int _width = 0;
        private int _height = 0;

        public SkinImage(int w, int h)
        {
            if (w * h <= 0) throw new InvalidOperationException("Image width and height must be above zero.");

            _data = new byte[(w * 4) * h];
            _width = w;
            _height = h;
        }

        public int Width => _width;
        public int Height => _height;
        public byte[] PixelData => _data;
    }
}
