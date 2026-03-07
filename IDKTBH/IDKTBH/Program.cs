using System;
using System.Linq;

namespace MyApplication
{
    class Weapons
    {
        public string damage = "10";
        public string distance = "5ft";
        public string TOD = "peircing";

        static void Main(string[] args)
        {
            Weapons Sword = new Weapons();
            Weapons Javelin = new Weapons();
            Console.WriteLine(Sword.damage + " " + Sword.distance + " " + Sword.TOD);
            Console.WriteLine(Javelin.distance);
        }
    }
}