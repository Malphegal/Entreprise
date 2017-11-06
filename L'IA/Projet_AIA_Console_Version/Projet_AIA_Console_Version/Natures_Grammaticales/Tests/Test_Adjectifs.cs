using System;
using static Projet_AIA_Console_Version.Adjectives;

namespace Projet_AIA_Console_Version
{
    class Test_Adjectifs
    {
        public static void Test_Adjectif()
        {
            string resGenre;
            string resNombre;
            string resQueFaire = "";

            while (true)
            {
                resQueFaire = "";
                Console.Write("Adjectif ?\t");
                Adjective adjective = new Adjective(Console.ReadLine());
                if (adjective.gender == "M")
                    resGenre = "masculin";
                else
                    resGenre = "féminin";
                if (adjective.number == "S")
                    resNombre = "singulier";
                else
                    resNombre = "pluriel";
                Console.WriteLine(adjective + " est un " + resGenre + " " + resNombre);

                while (!resQueFaire.Contains("changer") && !resQueFaire.Contains("autre"))
                {
                    Console.Write("Que voulez vous faire ?\t");
                    resQueFaire = Console.ReadLine();

                    if (resQueFaire.Contains("masculin singulier"))
                        Console.WriteLine("Masculin singulier : " + adjective.toMaleSingular());
                    else if (resQueFaire.Contains("masculin pluriel"))
                        Console.WriteLine("Masculin pluriel : " + adjective.toMalePlurial());
                    else if (resQueFaire.Contains("féminin singulier"))
                        Console.WriteLine("Féminin singulier : " + adjective.toFemaleSingular());
                    else if (resQueFaire.Contains("féminin pluriel"))
                        Console.WriteLine("Féminin pluriel : " + adjective.toFemalePlurial());
                    else if (resQueFaire.Contains("singulier"))
                        Console.WriteLine("Singulier : " + adjective.toSingular());
                    else if (resQueFaire.Contains("pluriel"))
                        Console.WriteLine("Pluriel : " + adjective.toPlurial());
                    else if (resQueFaire.Contains("masculin"))
                        Console.WriteLine("Masculin : " + adjective.toMale());
                    else if (resQueFaire.Contains("féminin"))
                        Console.WriteLine("Féminin : " + adjective.toFemale());

                    Console.WriteLine("");

                }
            }
        }
    }
}
