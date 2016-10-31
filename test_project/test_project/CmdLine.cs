using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace test_project
{
    class CmdLine
    {
        static public string currentUser = "user";
        static public string currentConn = "localhost";
        static public Machine connectedMachine = null;
        static public string connectionType = null;

        static public void Start(List<Person> people, Player player) 
        {
            currentUser = player.alias;
            string cmd = null;
            string mod = null;
            bool modFlag = false;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "               Welcome to IcarusOS"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "                  Type help for command list.\n\n"));
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.Write(currentUser + "@" + currentConn + "> ");
                cmd = Console.ReadLine();
                //splits string into cmd and modifier
                if (CheckCommand(cmd) != false)
                {
                    char[] delimiterChars = { ' ' };
                    string[] splitter = cmd.Split(delimiterChars);
                    cmd = splitter[0];
                    mod = splitter[1];
                    modFlag = true;
                }
                else
                {
                    modFlag = false;
                }
                    
                switch(cmd)
                {
                    case "connect":
                        string ip = null;
                        bool foundFlag = false;
                        if (modFlag != true)
                        {
                            Console.Write("Enter IP of machine to connect to: ");
                            ip = Console.ReadLine();
                        }
                        else
                        {
                            ip = mod;
                        }
                        for(int i=0; i<people.Count; i++)
                        {
                            if (people[i].computer.ipaddress == ip)
                            {
                                foundFlag = true;
                                Connect(people[i].computer);
                            }
                        }
                        if (foundFlag != true)
                        {
                            Console.WriteLine("No host found at that IP. It may be down or does not exist!");
                        }
                        break;

                    case "scan":
                        int numScan = 0;
                        if (modFlag != true)
                            Scan(people, numScan);
                        else
                        {
                            int.TryParse(mod, out numScan);
                            Scan(people, numScan);
                        }
                        break;

                    case "vulnscan":
                        ip = null;
                        if (modFlag != true)
                        {
                            Console.Write("Enter IP to scan: ");
                            ip = Console.ReadLine();
                        }
                        else
                        {
                            ip = mod;
                        }
                        Scan(people, ip);
                        break;

                    case "disconnect":
                        Disconnect(player);
                        break;

                    case "info":
                        if (connectedMachine == null)
                        {
                            Console.WriteLine("IP Address: " + player.computer.ipaddress);
                            Console.WriteLine("Hostname:   " + player.computer.hostname);
                            Console.WriteLine("PC Level:   " + player.computer.level);
                            Console.WriteLine("Owner:      " + player.computer.owner);
                        }
                        else
                        {
                            Console.WriteLine("IP Address: " + connectedMachine.ipaddress);
                            Console.WriteLine("Hostname:   " + connectedMachine.hostname);
                            Console.WriteLine("PC Level:   " + connectedMachine.level);
                            Console.WriteLine("Owner:      " + connectedMachine.owner);
                        }
                        break;

                    case "exploit":
                        ip = null;
                        string portstr = null;
                        int port = 0;
                        foundFlag = false;
                        if (modFlag != true)
                        {
                            Console.Write("Enter IP of machine to connect to: ");
                            ip = Console.ReadLine();
                            Console.Write("Enter port to exploit: ");
                            int.TryParse(mod, out port);
                        }
                        else
                        {
                            Console.Write("Enter port to exploit: ");
                            portstr = Console.ReadLine();
                            int.TryParse(portstr, out port);
                            ip = mod;
                        }
                        for(int i=0; i<people.Count; i++)
                        {
                            if (people[i].computer.ipaddress == ip)
                            {
                                foundFlag = true;
                                Exploit(people[i].computer, port);
                            }
                        }
                        if (foundFlag != true)
                        {
                            Console.WriteLine("No host found at that IP. It may be down or does not exist!");
                        }
                        break;

                    case "help":
                        Console.WriteLine("connect      Connect to another computer");
                        Console.WriteLine("disconnect   Disconnect from current session");
                        Console.WriteLine("scan         Scan all or number of IPs for responses");
                        Console.WriteLine("vulnscan     Scan specific IP for vulnerabilities");
                        Console.WriteLine("crack        Attempt to bruteforce when asked for password");
                        Console.WriteLine("exploit      Attempt to exploit a port");
                        Console.WriteLine("clear        Clear console screen");
                        Console.WriteLine("info         Display info on currently connected machine");
                        Console.WriteLine("help         Display this menu");
                        Console.WriteLine("quit         Exit IcarusOS");
                        break;

                    case "clear":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "               Welcome to IcarusOS"));
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "                  Type help for command list.\n\n"));
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "quit":
                        break;

                    default:
                        Console.WriteLine(cmd + " is not a recognized command");
                        break;

                }
            } while(cmd != "quit");            
        }

        static public bool Connect(Machine target)
        {
            if (connectedMachine == null)
            {
                string cmd = null;
                Console.WriteLine("Attempting to connect to: " + target.ipaddress);
                do
                {
                    Console.Write("Password: ");
                    cmd = Console.ReadLine();

                    if (cmd == "crack")
                    {
                        Cracker(target);
                        Console.WriteLine("Login authenticated!");
                        currentConn = target.hostname;
                        currentUser = "admin";
                        connectedMachine = target;
                        return true;
                    }
                    else if (cmd == target.GetPassword())
                    {
                        Console.WriteLine("Login authenticated!");
                        currentConn = target.hostname;
                        currentUser = "admin";
                        connectedMachine = target;
                        return true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid password!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (cmd != "quit");
                return false;
            }
            else
            {
                Console.WriteLine("Disconnect from " + connectedMachine.ipaddress + " before attempting another connection");
                return false;
            }
        }

        static public bool Disconnect(Player player)
        {
            Console.WriteLine("Disconnecting from " + connectedMachine.ipaddress);
            connectedMachine = null;
            currentConn = "localhost";
            currentUser = player.alias;
            return false;
        }

        static public bool Scan(List<Person> people, int numScan)
        {
            Console.WriteLine("Starting scan...");
            if (numScan <= 0)
            {
                Console.WriteLine("Only displaying first few results...");
                numScan = 5;
            }
            if (numScan > people.Count)
            {
                numScan = people.Count;
            }
            for(int i=0; i < numScan; i++) 
            {
                Console.WriteLine("Response from: " + people[i].computer.ipaddress);
                System.Threading.Tasks.Task.Delay(500).Wait();
            }
            Console.WriteLine("Scan complete!");
            return false;
        }

        static public bool Scan(List<Person> people, string ip)
        {
            bool foundFlag = false;
            bool vulnFlag = false;
            Console.WriteLine("Starting scan on " + ip);
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].computer.ipaddress == ip)
                {
                    Console.WriteLine("Response from: " + people[i].computer.ipaddress);
                    System.Threading.Tasks.Task.Delay(500).Wait();
                    for (int j = 0; j < people[i].computer.ports.Length; j++)
                    {
                        if (people[i].computer.ports[j] != 0)
                        {
                            Console.WriteLine("Port " + people[i].computer.ports[j] + " open!");
                            vulnFlag = true;
                        }
                    }
                    if (vulnFlag != true)
                        Console.WriteLine("No vulnerabilities found!");
                    foundFlag = true;
                    break;
                }
            }
            if(foundFlag != true)
                Console.WriteLine("No response from " + ip);
            Console.WriteLine("Scan complete!");
            return false;
        }

        //returns true if command has multiple pieces
        //(can be split based on delimiters)
        //else returns false
        static public bool CheckCommand(string cmd)
        {
            char[] delimiterChars = { ' ' };
            bool delimitFlag = false;
            for( int i = 0; i < delimiterChars.Length; i++ )
            {
                if (cmd.Contains(delimiterChars[i].ToString()) == true)
                {
                    delimitFlag = true;
                    break;
                }
                else
                {
                    delimitFlag = false;
                }
            }
            return delimitFlag;
        }

        static public string Cracker(Machine target)
        {
            int time = target.level * (Program.rng.Next() % 1000);
            for (int i = 0; i < time; i++)
            {
                int percentage = (100 * i) / time;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\r{0}      " + percentage + "/100%",Generator.GeneratePassword(target.level));
                System.Threading.Tasks.Task.Delay(50).Wait();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r{0}      Password found!", target.GetPassword());
            Console.ForegroundColor = ConsoleColor.White;
            return target.GetPassword();
        }

        static public bool Exploit(Machine target, int port)
        {
            if (port == 0)
            {
                Console.WriteLine(port + " is not valid!");
                return false;
            }
            bool exploited = false;
            int time = target.level * (Program.rng.Next() % 750);
            for (int i = 0; i < time/2; i++) 
            {
                int percentage = (100 * i) / time;
                Console.Write("\rPreparing exploit..." + percentage);
                System.Threading.Tasks.Task.Delay(50).Wait();
            }
            Console.WriteLine("\rPreparing exploit...OK!");
            for (int i = 0; i < time; i++)
            {
                int percentage = (100 * i) / time;
                Console.Write("\rRunning exploit on port " + port + "..." + percentage);
                System.Threading.Tasks.Task.Delay(50).Wait();
            }
            Console.WriteLine("\rRunning exploit on port " + port + "...OK!");

            for (int i = 0; i < target.ports.Length; i++)
            {
                if (target.ports[i] == port)
                {
                    exploited = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Port " + port + " exploited!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Login authenticated!");
                    currentConn = target.hostname;
                    currentUser = "admin";
                    connectedMachine = target;
                    return true;
                }
            }
            if (exploited != true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Failed to exploit " + port + "!");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return false;
        }

    }
}
