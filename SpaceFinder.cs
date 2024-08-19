using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallelism
{
    public class SpaceFinder
    {
        public static int FindSpacesInFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var file = sr.ReadToEnd();
                var count = file.Count(Char.IsWhiteSpace);
                return count;
            }
        }
    }
}
