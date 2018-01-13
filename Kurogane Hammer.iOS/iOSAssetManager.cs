using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Foundation;

namespace Kurogane_Hammer.iOS
{
    public class iOSAssetManager : PlatformDependent.AssetResourceManager
    {
        public string ReadAsset(string path)
        {
            string file = Path.Combine(NSBundle.MainBundle.BundlePath, "Content", path);
            string content = File.ReadAllText(file);

            return content;
        }

        public byte[] ReadAssetData(string path)
        {
            string file = Path.Combine(NSBundle.MainBundle.BundlePath, "Content", path);
            return File.ReadAllBytes(file);
        }
    }
}
