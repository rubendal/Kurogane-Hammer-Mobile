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

namespace Kurogane_Hammer.Droid
{
    public class Storage : PlatformDependent.Storage
    {
        private Context _context;

        public Storage(Context context)
        {
            _context = context;
        }

        private string ResolvePath(string path)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(ResolvePath(path));
        }

        public string Read(string path)
        {
            string content = File.ReadAllText(ResolvePath(path));
            //string content = null;
            //try
            //{
            //    using (StreamReader reader = new StreamReader(ResolvePath(path)))
            //    {
            //        content = reader.ReadToEnd();
            //    }
            //}catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            return content;
        }

        public void Write(string path, string content)
        {
            string t = ResolvePath(path).Replace(Path.GetFileName(path),"");
            if (!Directory.Exists(t))
            {
                Directory.CreateDirectory(t);
            }
            File.WriteAllText(ResolvePath(path), content);
            //try
            //{
            //    using (StreamWriter writer = new StreamWriter(ResolvePath(path), true))
            //    {
            //        writer.Write(content);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
    }
}