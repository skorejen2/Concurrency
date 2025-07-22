



using System.Collections.Concurrent;

public class Counter
{
    public static BlockingCollection<int> totalValues = new();
    private static bool _IsFinished = false;

    static void Main(string[] args)
    {
        Thread t1 = new(() => Counter.InsertOne());
        t1.Name = "Thread 1";

        Thread t2 = new(() => Counter.TakeOne());
        t2.Name = "Thread 2";

        Thread t3 = new(() =>
        {
            for (var i = 0; ; i++)
            {
                System.Console.WriteLine("Total values count: " + totalValues.Count);
                Thread.Sleep(5000);
                if (_IsFinished) break;
            }
        });

        t1.Start();
        t2.Start();
        t3.Start();

        while (true)
        {
            var a = Console.ReadLine();
            System.Console.WriteLine("User typed: " + a + " \n");
            if (a!.Equals("c"))
            {
                System.Console.WriteLine(t3.IsAlive);
            }
            else if (a!.Equals("STOP"))
            {
                break;
            }
        }

        t1.Join();
        t2.Join();
        foreach (var item in totalValues)
        {
            System.Console.WriteLine(item + "'\n");
        }




    }

    public delegate void CountTo20();
    public static void InsertOne()
    {
        for (var a = 0; a < 50; a++)
        {
            var i = new Random().Next();
            totalValues.Add(i);
            System.Console.WriteLine(Thread.CurrentThread.Name + " added value " + i);
            Thread.Sleep(50);
        }
        _IsFinished = true;
        
    }
    public static void TakeOne()
    {
        
        for (int a = 0; ; a++)
        {
            if (_IsFinished) break;
            var i = totalValues.Take();
            System.Console.WriteLine(Thread.CurrentThread.Name + " took value " + i);
            Thread.Sleep(200);

        }
        
    }

}

public class Producer
{



}

public class Receiver
{

}