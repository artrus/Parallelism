using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallelism
{
    public class FileInfoSize
    {
        public string Name;
        public int Size;

        public FileInfoSize(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
