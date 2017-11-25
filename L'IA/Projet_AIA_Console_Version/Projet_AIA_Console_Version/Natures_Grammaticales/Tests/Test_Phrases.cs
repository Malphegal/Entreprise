using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_AIA_Console_Version;
using Projet_AIA_Console_Version.Natures_Grammaticales;

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
                List<Word> phraseAvecNature;
                foreach (List<List<object[]>> phrase in lesPhrases)
                {
                    phraseAvecNature = Phrase.DeterminerLesNatures(phrase);
                    Console.WriteLine("\nVoici la phrase et la nature pour chacun de ses mots : ");
                    foreach (Word mot in phraseAvecNature)
                    {
                        Console.WriteLine(mot.ToString() + " : " + mot.Nature);
                    }
                }
                Console.WriteLine("\n");
            }
        }
    }
}
