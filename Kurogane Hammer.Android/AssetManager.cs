using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using Android.Content.Res;

namespace Kurogane_Hammer.Droid
{
    public class AndroidAssetManager : PlatformDependent.AssetResourceManager
    {
        private Context _context;

        public AndroidAssetManager(Context context)
        {
            _context = context;
        }

        public string ReadAsset(string path)
        {
            AssetManager assets = _context.Assets;
            string content = null;
            try
            {
                using (StreamReader sr = new StreamReader(assets.Open(path)))
                {
                    content = sr.ReadToEnd();
                }
            }catch(Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            if (content == null)
                throw new IOException("Asset not found");
            return content;
        }

        public byte[] ReadAssetData(string path)
        {
            AssetManager assets = _context.Assets;
            byte[] content = null;
            try
            {
                using (StreamReader sr = new StreamReader(assets.Open(path)))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        sr.BaseStream.CopyTo(ms);
                        content = ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            if (content == null)
                throw new IOException("Asset not found");
            return content;
        }
    }
}