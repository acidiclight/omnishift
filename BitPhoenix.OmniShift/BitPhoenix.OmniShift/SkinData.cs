using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace BitPhoenix.OmniShift
{
    public class SkinData
    {
        private Dictionary<string, SkinImage> _images = null;
        private List<ISkinProperty> _properties = null;

        public SkinData()
        {
            _images = new Dictionary<string, SkinImage>();
            _properties = new List<ISkinProperty>();
        }

        public T GetValue<T>(string name)
        {
            var prop = _properties.FirstOrDefault(x => x.Name == name && x is SkinProperty<T>) as SkinProperty<T>;

            if (prop != null)
                return prop.Value;

            SetValue(name, default(T));
            return default(T);
        }

        public void SetValue<T>(string name, T value)
        {
            var prop = _properties.FirstOrDefault(x => x.Name == name && x is SkinProperty<T>) as SkinProperty<T>;

            if(prop != null)
            {
                prop.Value = value;
            }
            else
            {
                prop = new SkinProperty<T>(name, value);
                _properties.Add(prop);
            }
        }

        public void SetImage(string name, Image image)
        {
            using(var bitmap = new Bitmap(image))
            {
                var width = bitmap.Width;
                var height = bitmap.Height;

                var lck = bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                SkinImage img = null;
                if (_images.ContainsKey(name))
                    img = _images[name];
                else
                    img = new SkinImage(width, height);

                Marshal.Copy(lck.Scan0, img.PixelData, 0, img.PixelData.Length);

                bitmap.UnlockBits(lck);
            }
        }

        public Image GetImage(string name)
        {
            if(_images.ContainsKey(name))
            {
                var sImage = _images[name];

                var bitmap = new Bitmap(sImage.Width, sImage.Height);

                var lck = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Marshal.Copy(sImage.PixelData, 0, lck.Scan0, sImage.PixelData.Length);

                bitmap.UnlockBits(lck);

                return bitmap;
            }
            throw new ArgumentException("Image name not found.", nameof(name));
        }
    }
}
