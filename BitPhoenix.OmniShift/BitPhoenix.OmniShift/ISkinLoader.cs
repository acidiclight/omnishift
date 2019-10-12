using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BitPhoenix.OmniShift
{
    public interface ISkinLoader
    {
        public bool IsFormatValid(Stream skinStream);
        public SkinData ExtractSkinData(Stream skinStream);
        public void WriteSkinData(Stream skinStream, SkinData data);
    }
}
