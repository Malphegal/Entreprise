using System;
using static Projet_AIA_Console_Version.Adjective;

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
                if (adjective.Gender == "M")
                    resGenre = "masculin";
                else
                    resGenre = "féminin";
                if (adjective.Number == "S")
                    resNombre = "singulier";
                else
                    resNombre = "pluriel";
                Console.WriteLine(adjective + " est un " + resGenre + " " + resNombre);

                while (!resQueFaire.Contains("changer") && !resQueFaire.Contains("autre"))
                {
                    Console.Write("Que voulez vous faire ?\t");
                    resQueFaire = Console.ReadLine();

                    if (resQueFaire.Contains("masculin singulier"))
                        Console.WriteLine("Masculin singulier : " + MaleSingularOf(adjective));
                    else if (resQueFaire.Contains("masculin pluriel"))
                        Console.WriteLine("Masculin pluriel : " + MalePlurialOf(adjective));
                    else if (resQueFaire.Contains("féminin singulier"))
                        Console.WriteLine("Féminin singulier : " + FemaleSingularOf(adjective));
                    else if (resQueFaire.Contains("féminin pluriel"))
                        Console.WriteLine("Féminin pluriel : " + FemalePlurialOf(adjective));
                    else if (resQueFaire.Contains("singulier"))
                        Console.WriteLine("Singulier : " + SingularOf(adjective));
                    else if (resQueFaire.Contains("pluriel"))
                        Console.WriteLine("Pluriel : " + PlurialOf(adjective));
                    else if (resQueFaire.Contains("masculin"))
                        Console.WriteLine("Masculin : " + MaleOf(adjective));
                    else if (resQueFaire.Contains("féminin"))
                        Console.WriteLine("Féminin : " + FemaleOf(adjective));

                    Console.WriteLine("");

                }
            }
        }
    }
}
