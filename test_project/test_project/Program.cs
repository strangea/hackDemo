using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace test_project
{
    class Program
    {
        public static readonly Random rng = new Random();
       
        static void Main(string[] args)
        {
            int time = Program.rng.Next() % 100;
            for (int i = 0; i < time; i++)
            {
                int percentage = (100 * i) / time;
                Console.Write("\rBooting PC..." + percentage + "%");
                System.Threading.Tasks.Task.Delay(5).Wait();
            }
            Console.WriteLine("\rBooting PC...OK!");
            for (int i = 0; i < time; i++)
            {
                int percentage = (100 * i) / time;
                Console.Write("\rLoading OS..." + percentage + "%");
                System.Threading.Tasks.Task.Delay(10).Wait();
            }
            Console.WriteLine("\rLoading OS...OK!");
            //Console.SetWindowSize(100, 50);
            List<Person> people = Generator.GeneratePeople();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Welcome to IcarusOS"));
            Console.ForegroundColor = ConsoleColor.White;
            Player player = new Player();
            CmdLine.Start(people, player);
        }
    }
}
