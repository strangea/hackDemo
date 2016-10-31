using System;
using System.IO;
using System.Text;
using System.Data;

namespace test_project
{
    class Person
    {
        public string firstName;
        public string lastName;
        public Machine computer;

        public Person() 
        { 
            firstName = "John"; 
            lastName = "Doe";
            computer = new Machine();
        }

        public Person(string fn, string ln)
        {
            firstName = fn;
            lastName = ln;
            computer = new Machine();
        }
        
        public string GetFullName() { return firstName + " " + lastName; }
    }
}
