using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.PlatformDependent
{
    public interface AssetResourceManager
    {
        //For text, html, json files
        string ReadAsset(string path);

        //For images
        byte[] ReadAssetData(string path);
    }
}
