using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BitPhoenix.OmniShift
{
    public static class OmniShift
    {
        private static List<ISkinLoader> _loaders = new List<ISkinLoader>();

        public static void RegisterLoader<T>() where T : ISkinLoader, new()
        {
            var instance = new T();
            _loaders.Add(instance);
        }

        public static SkinData LoadSkin(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Skin file \"{path}\" does not exist.");

            if (_loaders.Count == 0)
                throw new InvalidOperationException("No skin loaders have been registered yet.");

            using(var stream = File.OpenRead(path))
            {
                foreach(var loader in _loaders)
                {
                    stream.Position = 0;
                    if(loader.IsFormatValid(stream))
                    {
                        return loader.ExtractSkinData(stream);
                    }
                }
            }

            throw new InvalidOperationException("No skin loaders were found that could accept the given skin file format.");
        }
    }
}
