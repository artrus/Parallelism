using Parallelism;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        //Чтение 3х файлов в раздельных задачах
        stopwatch.Start();
        Task<int> task1 =  Task.Run(() =>
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

        while (!(task1.IsCompleted && task2.IsCompleted && task3.IsCompleted)) { }  //ожидание завершения всех задач

        stopwatch.Stop();
        Console.WriteLine($"Spaces in file1 = {task1.Result}");
        Console.WriteLine($"Spaces in file2 = {task2.Result}");
        Console.WriteLine($"Spaces in file3 = {task3.Result}");
        Console.WriteLine($"3 files time={stopwatch.ElapsedMilliseconds}");
        Console.WriteLine();

        //чтение каталога
        SpaceFinder spaceFinder = new SpaceFinder();
        spaceFinder.FileResult += DisplaySpaces;    //подписка на событие после выполнения каждой задачи

        stopwatch.Restart();
        stopwatch.Start();
        spaceFinder.FindInPath(@"d:\Projects\OTUS\DZ6\TestCatalog\");

        while (!spaceFinder.isComplete()) { }   //ожидание выполнения всех задач 

        stopwatch.Stop();
        Console.WriteLine($"Path time={stopwatch.ElapsedMilliseconds}");
    }

    private static void DisplaySpaces(string message)
    {
        Console.WriteLine(message);
    }
}