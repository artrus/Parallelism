using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallelism
{
    public class Finder
    {
        public static List<Task<FileInfoSize>> FindInPath(string path, Func<string, int> finder)     //поиск в папке
        {
            var tasks = new List<Task<FileInfoSize>>();
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                Task<FileInfoSize> task = Task.Run(() =>
                {
                    var count = finder(path + "/" + file.Name);
                    return new FileInfoSize(file.Name, count);
                });
                tasks.Add(task);
            }
            return tasks;
        }
    }
}
