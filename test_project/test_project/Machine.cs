using System;
using System.IO;
using System.Text;
using System.Data;

namespace test_project
{
    class Machine
    {
        public string ipaddress;
        public string hostname;
        public string opsys;
        public string owner;
        private string password;
        public int level;
        public int[] ports;

        public Machine()
        {
            int[] num = new int[4];
            Random rng = Program.rng;
            for (int i = 0; i < 4; i++)
            {
                num[i] = rng.Next() % 255;
            }
            ipaddress = num[0].ToString() + "." + num[1].ToString() + "." + num[2].ToString() + "." + num[3].ToString();

            hostname = "Machine" + rng.Next();
            
            opsys = "Linux";
            
            owner = "";
            
            int ranNum = Program.rng.Next();
            if (ranNum % 100 < 51)
                level = 1;
            else if (ranNum % 100 > 50 && ranNum < 81)
                level = 2;
            else if (ranNum % 100 > 80 && ranNum < 91)
                level = 3;
            else if (ranNum % 100 > 90 && ranNum < 100)
                level = 4;
            else
                level = 5;
            level = 1;

            ports = Generator.GeneratePorts(level);
            password = Generator.GeneratePassword(level);
        }

        public string GetMachineInfo()
        {
            return ipaddress + " " + hostname + " " + " " + opsys + " " + owner;
        }

        public string GetPassword()
        {
            return password;
        }
    }
}
