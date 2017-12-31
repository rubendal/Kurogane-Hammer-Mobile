using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.PlatformDependent
{
    public interface Storage
    {
        bool FileExists(string path);
        string Read(string path);
        void Write(string path, string content);
    }
}
