using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallelism
{
    public class SpaceFinder
    {
        public delegate void EventHandler(string message);
        public event EventHandler? FileResult;

        List<Task>? tasks;

        public static int FindSpacesInFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string file = sr.ReadToEnd();
                int count = file.Count(Char.IsWhiteSpace);
                return count;
            }
        }

        public void FindInPath(string path)     //поиск в папке
        {
            tasks = new List<Task>();   
            DirectoryInfo dir = new DirectoryInfo(path);
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                var task = Task.Run(() =>
                {
                    var count = FindSpacesInFile(path + "/" + file.Name);
                    FileResult?.Invoke($"{file.Name} spaces count={count}");
                });
                tasks.Add(task);
            }
        }

        public bool isComplete()   
        {
            bool result = true;
            foreach(var task in tasks)
            {
                if (!task.IsCompleted)
                    result = false;
            }
            return result;
        }
    }
}
