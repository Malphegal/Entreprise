using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Noms_2017_07_15.Noms.Names;

namespace Noms_2017_07_15
{
    class Program
    {
        static void Main(string[] args)
        {
            string resGenre;
            string resNombre;
            while (true)
            {
                Console.Write("Nom ?\t");
                Name nom = new Name(Console.ReadLine());
                if (nom.gender == "M")
                    resGenre = "masculin";
                else
                    resGenre = "féminin";
                if (nom.number == "S")
                    resNombre = "singulier";
                else
                    resNombre = "pluriel";
                Console.WriteLine(nom + " est un " + resGenre + " " + resNombre);

                if (nom.number == "S")
                    Console.WriteLine("Au pluriel, il donne : " + nom.singularToPlurial());
                else
                    Console.WriteLine("Au singulier, il donne : " + nom.plurialToSingular());
                Console.WriteLine();
            }
        }
    }
}
