using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class UI
    {
         public void GetDataForNewVehicle()
        {
            int licenseNumber;
            Console.Write("Please enter a license number: ");
            licenseNumber = int.Parse(Console.ReadLine());
        }
    }
}
