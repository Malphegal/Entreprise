using System;
using static Projet_AIA_Console_Version.Names;

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
