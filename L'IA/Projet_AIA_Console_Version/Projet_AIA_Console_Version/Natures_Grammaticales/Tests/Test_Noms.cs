using System;
using static Projet_AIA_Console_Version.Name;

namespace Projet_AIA_Console_Version
{
    class Test_Noms
    {
        public static void Test_Nom()
        {
            string resGenre;
            string resNombre;
            while (true)
            {
                Console.Write("Nom ?\t");
                Name nom = new Name(Console.ReadLine());
                if (nom.Gender == "M")
                    resGenre = "masculin";
                else
                    resGenre = "féminin";
                if (nom.Number == "S")
                    resNombre = "singulier";
                else
                    resNombre = "pluriel";
                Console.WriteLine(nom + " est un " + resGenre + " " + resNombre);

                if (nom.Number == "S")
                    Console.WriteLine("Au pluriel, il donne : " + PlurialOf(nom));
                else
                    Console.WriteLine("Au singulier, il donne : " + SingularOf(nom));
                Console.WriteLine();
            }
        }
    }
}
