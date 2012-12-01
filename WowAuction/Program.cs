using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Data.SqlClient;


namespace WowAuction
{
    class Program
    {
        static void Main(string[] args)
        {
            menu programMenu = new menu();



            bool isNum = false;

            while (true)
            {
                programMenu.MainMenu();

                while (!isNum)
                {
                    string consoleInput = Console.ReadLine();

                    isNum = int.TryParse(consoleInput, out programMenu.menuChoice);

                    if (!isNum)
                    {
                        Console.WriteLine("Please choose an activity by writing a number from the list.");
                    }
                }

                programMenu.subMenu();

                isNum = false;
            }

        }
    }
}
