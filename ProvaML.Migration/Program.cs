using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using DbUp;

namespace ProvaML.Migration
{
    class Program
    {
        static int Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));
            
            var connectionString =
                args.FirstOrDefault()
                ?? Environment.GetEnvironmentVariable("ConnectionString");
            
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif                
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            
            return 0;
        }
    }
}