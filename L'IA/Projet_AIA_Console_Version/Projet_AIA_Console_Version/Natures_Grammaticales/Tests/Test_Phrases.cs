using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_AIA_Console_Version;
using Projet_AIA_Console_Version.Natures_Grammaticales;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.Fonction;
using Projet_AIA_Console_Version.Fonctions_Grammaticales;

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
                // List<Word[]> phraseAvecFonction;
                List<MotFonction> phraseAvecFonction;
                foreach (List<List<object[]>> phrase in lesPhrases)
                {
                    phraseAvecNature = Nature.DeterminerLesNatures(phrase);
                    Console.WriteLine("\nVoici la phrase et la nature pour chacun de ses mots : ");
                    foreach (Word mot in phraseAvecNature)
                    {
                        Console.WriteLine(mot.ToString() + " : " + mot.Nature);
                    }

                    Console.WriteLine("\n");

                    /*
                    phraseAvecFonction = Phrase.DeterminerLesFonctions(phraseAvecNature);
                    Console.WriteLine("\nCompréhension : ");
                    foreach (Word[] idee in phraseAvecFonction)
                    {
                        Console.WriteLine(idee[0].ToString() + " " + idee[1].ToString() + " " + idee[2].ToString());
                    }
                    */

                    try
                    {
                        phraseAvecFonction = DeterminerLesFonctions(phraseAvecNature);

                        Console.WriteLine("\nCompréhension : ");
                        foreach (MotFonction idee in phraseAvecFonction)
                        {
                            Console.WriteLine(idee.mot.ToString() + " " + ToStringFunction(idee.lien) + " " + idee.cibleLien.ToString());
                        }
                    }
                    catch (FunctionsNotFound ex)
                    {
                        Console.WriteLine("\nCompréhension : ");
                        Console.WriteLine("\nJe n'ai pas compris la phrase. :(\n");
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.WriteLine("\n");
            }
        }
    }
}
