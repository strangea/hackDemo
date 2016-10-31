using System;

namespace test_project
{
    class Player : Person
    {
        public string alias;

        public Player()
        {
            Console.Write("First name: ");
            firstName = Console.ReadLine();
            Console.Write("Last name: ");
            lastName = Console.ReadLine();
            Console.Write("Alias: ");
            alias = Console.ReadLine();
            computer = new Machine();
            computer.owner = alias;
            computer.level = 1;
        }
    }
}
