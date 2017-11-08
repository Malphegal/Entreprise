﻿using System;
using static Projet_AIA_Console_Version.Verbs;

namespace Projet_AIA_Console_Version
{
    class Test_Verbes
    {
        public static void Test_Verbe()
        {
            // MINI PROGRAMME DE TEST POUR LA CONJUGAISON DES VERBES
            InfinitiveVerb verbeInfinitif = new InfinitiveVerb();
            ConjugatedVerb verbeConjugue = new ConjugatedVerb();
            string temps;
            string type;

            Console.WriteLine(" 1. Je veux entrer des verbes à l'infinitif\n 2. Je veux entrer des verbes conjugués");
            if (Console.ReadLine() == "1")
                type = "verbe infinitif";
            else
                type = "verbe conjugué";

            Console.WriteLine();

            while (true)
            {
                temps = "";

                Console.WriteLine("Verbe ?\t");
                if (type == "verbe infinitif")
                    verbeInfinitif = new InfinitiveVerb(Console.ReadLine());
                else
                    verbeConjugue = new ConjugatedVerb(Console.ReadLine());

                Console.WriteLine("\nTemps ?\t 1. Présent\t 2. Futur simple\t 3. Imparfait\t 4. Passé");
                switch (Console.ReadLine())
                {
                    case "1":
                        temps = "présent indicatif";
                        break;
                    case "2":
                        temps = "futur_simple indicatif";
                        break;
                    case "3":
                        temps = "imparfait indicatif";
                        break;
                    case "4":
                        temps = "passé_simple indicatif";
                        break;
                }

                if (type == "verbe infinitif")
                {
                    Console.WriteLine("Je " + verbeInfinitif.Conjugate("1", temps).verb);
                    Console.WriteLine("Tu " + verbeInfinitif.Conjugate("2", temps).verb);
                    Console.WriteLine("Il " + verbeInfinitif.Conjugate("3", temps).verb);
                    Console.WriteLine("Nous " + verbeInfinitif.Conjugate("4", temps).verb);
                    Console.WriteLine("Vous " + verbeInfinitif.Conjugate("5", temps).verb);
                    Console.WriteLine("Ils " + verbeInfinitif.Conjugate("6", temps).verb);
                }
                else
                {
                    Console.WriteLine("Je " + verbeConjugue.Conjugate("1", temps).verb);
                    Console.WriteLine("Tu " + verbeConjugue.Conjugate("2", temps).verb);
                    Console.WriteLine("Il " + verbeConjugue.Conjugate("3", temps).verb);
                    Console.WriteLine("Nous " + verbeConjugue.Conjugate("4", temps).verb);
                    Console.WriteLine("Vous " + verbeConjugue.Conjugate("5", temps).verb);
                    Console.WriteLine("Ils " + verbeConjugue.Conjugate("6", temps).verb);
                }
                Console.WriteLine();
            }


            // POUR CRÉER UN FICHIER TEXTE CONTENANT LA LISTE DES VERBES À AJOUTER
            // Conjugaison.AjoutAutomatiqueVerbe.triVerbes();
        }
    }
}
