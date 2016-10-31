using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace test_project
{
    class Generator
    {
        static public List<Person> GeneratePeople()
        {
            string firstNamePath = Path.Combine(Directory.GetCurrentDirectory(), "firstName.txt");
            string lastNamePath = Path.Combine(Directory.GetCurrentDirectory(), "lastName.txt");
            while (true)
            {
                //Console.Write("Number of people to generate: ");
                string userInput = "25";//Console.ReadLine();
                int numPeople = 0;
                if (int.TryParse(userInput, out numPeople) && numPeople > 0)
                {
                    List<Person> people = new List<Person>();
                    Console.Write("Generating people...");
                    for (int i = 0; i < numPeople; i++)
                    {
                        people.Add(new Person(RandomLine(firstNamePath), RandomLine(lastNamePath)));
                        people[i].computer.owner = people[i].firstName + " " + people[i].lastName;
                        double percentage = (i * 100) / numPeople;
                        Console.Write("\rGenerating people..." + percentage + "%");
                    }
                    Console.WriteLine("\rComplete!                   ");
                    return people;
                }
                else
                {
                    Console.WriteLine("0 or NAN detected!");
                }
            }
        }

        static public string GeneratePassword(int level)
        {
            int length = 5;
            for(int i = 0; i < level; i++)
            {
                length++;
            }
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            while (0 < length--)
            {
                res.Append(valid[Program.rng.Next(valid.Length)]);
            }
            return res.ToString();
        }

        static public int[] GeneratePorts(int level)
        {
            if (level == 1)
            {
                int[] ports = new int[1];
                ports[0] = 23;
                return ports;
            }
            else if (level == 2)
            {
                int[] ports = new int[1];
                ports[0] = 23;
                return ports;
            }
            else
            {
                int[] ports = new int[1];
                ports[0] = 0;
                return ports;
            }
        }

        static public string RandomLine(string filepath)
        {
            int totalLines = File.ReadAllLines(filepath).Length;
            int lineNum = Program.rng.Next(1, totalLines);

            using (StreamReader reader = new StreamReader(filepath))
            {
                string line = null;
                for (int i = 0; i < lineNum; i++)
                {
                    line = reader.ReadLine();
                }

                return line;
            }
        }
    }
}
