using Parallelism;
using System.Diagnostics;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var stopwatch = new Stopwatch();

        //Чтение 3х файлов в раздельных задачах
        stopwatch.Start();
        Task<int> task1 = Task.Run(() =>
        {
            int count = SpaceFinder.FindSpacesInFile(@"d:\Projects\OTUS\DZ6\TestFiles\File1.ini");
            return count;
        });
        Task<int> task2 = Task.Run(() =>
        {
            int count = SpaceFinder.FindSpacesInFile(@"d:\Projects\OTUS\DZ6\TestFiles\File2.ini");
            return count;
        });
        Task<int> task3 = Task.Run(() =>
        {
            int count = SpaceFinder.FindSpacesInFile(@"d:\Projects\OTUS\DZ6\TestFiles\File3.ini");
            return count;
        });

        await Task.WhenAll(task1, task2, task3);

        stopwatch.Stop();

        Console.WriteLine($"Spaces in file1 = {task1.Result}");
        Console.WriteLine($"Spaces in file2 = {task2.Result}");
        Console.WriteLine($"Spaces in file3 = {task3.Result}");
        Console.WriteLine($"3 files time={stopwatch.ElapsedMilliseconds}");
        Console.WriteLine();

        //чтение каталога
        stopwatch.Restart();
        stopwatch.Start();

        var tasks = Finder.FindInPath(@"d:\Projects\OTUS\DZ6\TestCatalog\", SpaceFinder.FindSpacesInFile);  //передача делегата который возвращает FileInfoSize
        FileInfoSize[] results = await Task.WhenAll(tasks);

        stopwatch.Stop();

        foreach (var result in results)
            Console.WriteLine($"File:{result.Name} size={result.Size}");

        Console.WriteLine($"Path time={stopwatch.ElapsedMilliseconds}");
    }
}