using Parallelism;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
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

        //while (!(task1.IsCompleted && task2.IsCompleted && task3.IsCompleted)) { }  //ожидание завершения всех задач
        var results1 = Task.WhenAll(task1, task2, task3);

        while (!results1.IsCompleted) { }

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
        var results = Task.WhenAll(tasks);

        while (!results.IsCompleted) { }    //ожидание завершения тасок

        stopwatch.Stop();

        foreach (var result in results.Result)
            Console.WriteLine($"File:{result.Name} size={result.Size}");

        Console.WriteLine($"Path time={stopwatch.ElapsedMilliseconds}");
    }
}