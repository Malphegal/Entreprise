using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_AIA_Console_Version.Natures_Grammaticales.Determiner;

namespace Projet_AIA_Console_Version.Natures_Grammaticales.Tests
{
    class Test_Determinants
    {
        public static void Test_Determinant()
        {
            while (true)
            {
                string resGenre = "";
                string resNombre = "";

                Console.Write("Déterminant ?\t");
                Determiner determiner = new Determiner(Console.ReadLine());
                if (determiner.Gender == "M")
                    resGenre = "masculin";
                else if (determiner.Gender == "F")
                    resGenre = "féminin";
                else if (determiner.Gender == "N")
                    resGenre = "neutre";
                else
                    resGenre = "inconnu";
                if (determiner.Number == "S")
                    resNombre = "singulier";
                else if (determiner.Number == "P")
                    resNombre = "pluriel";
                else
                    resNombre = "inconnu";
                Console.WriteLine(determiner + " est un " + determiner.Type1 + " " + determiner.Type2 + " " + resGenre + " " + resNombre);
                Console.WriteLine("\t" + MaleSingularOf(determiner));
                Console.WriteLine("\t" + FemaleSingularOf(determiner));
                Console.WriteLine("\t" + MalePlurialOf(determiner));
                Console.WriteLine("\t" + FemalePlurialOf(determiner));
                Console.WriteLine();
            }
        }
    }
}
