using System;
using static Projet_AIA_Console_Version.Verbs;

namespace Projet_AIA_Console_Version
{
    class Test_Verbes
    {
        public static void Test_Verbe()
        {
            // MINI PROGRAMME DE TEST POUR LA CONJUGAISON DES VERBES
            string temps;
            string verbe = "aimer";
            InfinitiveVerb verbe1 = new InfinitiveVerb(verbe);
            Console.WriteLine("Le verbe choisi est : " + verbe1.ToString());
            Console.WriteLine();

            temps = "présent indicatif";
            Console.WriteLine("Voici sa conjugaison au : " + temps);
            for(int i=1; i<7; i++)
            {
                Console.WriteLine(verbe1.Conjugate(i.ToString(), temps));
            }
            Console.WriteLine();

            temps = "futur_simple indicatif";
            Console.WriteLine("Voici sa conjugaison au : " + temps);
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(verbe1.Conjugate(i.ToString(), temps));
            }
            Console.WriteLine();

            temps = "imparfait indicatif";
            Console.WriteLine("Voici sa conjugaison au : " + temps);
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(verbe1.Conjugate(i.ToString(), temps));
            }
            Console.WriteLine();

            temps = "passé_simple indicatif";
            Console.WriteLine("Voici sa conjugaison au : " + temps);
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(verbe1.Conjugate(i.ToString(), temps));
            }
            Console.WriteLine("\n");

            Console.ReadLine();

            // POUR CRÉER UN FICHIER TEXTE CONTENANT LA LISTE DES VERBES À AJOUTER
            // Conjugaison.AjoutAutomatiqueVerbe.triVerbes();
        }
    }
}
