using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem.Console
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Garage mainGarage = new Garage();
            mainGarage.Run();
        }
    }
}
