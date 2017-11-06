using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_AIA_Console_Version.Natures_Grammaticales.Determiners;

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
                if (determiner.gender == "M")
                    resGenre = "masculin";
                else if (determiner.gender == "F")
                    resGenre = "féminin";
                else if (determiner.gender == "N")
                    resGenre = "neutre";
                else
                    resGenre = "inconnu";
                if (determiner.number == "S")
                    resNombre = "singulier";
                else if (determiner.number == "P")
                    resNombre = "pluriel";
                else
                    resNombre = "inconnu";
                Console.WriteLine(determiner + " est un " + determiner.type1 + " " + determiner.type2 + " " + resGenre + " " + resNombre);
                Console.WriteLine("\t" + determiner.toMaleSingular());
                Console.WriteLine("\t" + determiner.toFemaleSingular());
                Console.WriteLine("\t" + determiner.toMalePlurial());
                Console.WriteLine("\t" + determiner.toFemalePlurial());
                Console.WriteLine();
            }
        }
    }
}
