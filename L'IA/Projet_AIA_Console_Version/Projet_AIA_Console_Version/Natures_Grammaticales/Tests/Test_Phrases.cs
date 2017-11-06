using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_AIA_Console_Version;

namespace Projet_AIA_Console_Version
{
    class Test_Phrases
    {
        public static void Test_Phrase()
        {

            while (true)
            {
                Console.WriteLine("Votre texte ?\t");
                string reponseUtilisateur = Console.ReadLine();

                // TEST DecouperPhrase
                List<List<List<object[]>>> lesPhrases = Phrase.DecouperPhrase(reponseUtilisateur.ToLower());
                /*foreach (List<List<string>> phrase in lesPhrases)
                {
                    Console.WriteLine("Voici la phrase : ");
                    foreach (List<string> mot in phrase)
                        Console.WriteLine(mot[0] + " -> " + mot[1]);
                }*/

                // TEST DeterminerLesNatures
                foreach (List<List<object[]>> phrase in lesPhrases)
                {
                    Phrase.DeterminerLesNatures(phrase);
                    Console.WriteLine("\nVoici la phrase et les natures possibles pour chacun de ses mots : ");
                    foreach (List<object[]> mot in phrase)
                    {
                        if (mot.Count > 1)
                        {
                            Console.Write(mot[0][0] + " -> ");
                            for (int i = 1; i < mot.Count; i++)
                                Console.Write(mot[i][0] + " ");
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine(mot[0][0] + " -> non renseigné");
                    }
                }
                Console.WriteLine("\n");
            }
        }
    }
}
