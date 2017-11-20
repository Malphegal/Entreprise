using System;
using System.Linq;
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
            string mode;
            string type;
            string res = "";

            Console.WriteLine(" 1. Je veux entrer des verbes à l'infinitif\n 2. Je veux entrer des verbes conjugués");
            if (Console.ReadLine() == "1")
                type = "verbe infinitif";
            else
                type = "verbe conjugué";

            Console.WriteLine();

            while (true)
            {
                temps = "";
                mode = "indicatif";
                res = "";

                Console.WriteLine("Verbe ?\t");
                if (type == "verbe infinitif")
                    verbeInfinitif = new InfinitiveVerb(Console.ReadLine());
                else
                    verbeConjugue = new ConjugatedVerb(Console.ReadLine());

                while (res != "autre")
                {

                    Console.WriteLine("\nMode et temps ?\t");
                    res = Console.ReadLine();

                    if (res == "autre")
                        break;

                    // On cherche le mode
                    if (res.Contains("conditionnel"))
                        mode = "conditionnel";
                    else if (res.Contains("subjonctif"))
                        mode = "subjonctif";
                    else if (res.Contains("impératif"))
                        mode = "impératif";
                    else if (res.Contains("participe"))
                        mode = "participe";
                    else
                        mode = "indicatif";

                    // On cherche le temps
                    if (res.Contains("présent"))
                        temps = "présent";

                    else if (mode == "indicatif")
                    {
                        if (res.Contains("imparfait"))
                            temps = "imparfait";
                        else if (res.Contains("passé simple"))
                            temps = "passé simple";
                        else if (res.Contains("futur simple"))
                            temps = "futur simple";
                        else if (res.Contains("composé"))
                            temps = "passé composé";
                        else if (res.Contains("parfait"))
                            temps = "plus-que-parfait";
                        else if (res.Contains("passé antérieur"))
                            temps = "passé antérieur";
                        else if (res.Contains("futur antérieur"))
                            temps = "futur antérieur";
                        else if (res.Contains("passé"))
                            temps = "passé simple";
                        else if (res.Contains("futur"))
                            temps = "futur simple";
                        else
                            temps = "présent";
                    }
                    else if (mode == "subjonctif")
                    {
                        if (res.Contains("imparfait"))
                            temps = "imparfait";
                        else if (res.Contains("passé"))
                            temps = "passé";
                        else if (res.Contains("parfait"))
                            temps = "plus-que-parfait";
                        else
                            temps = "présent";
                    }
                    else
                    {
                        if (res.Contains("passé"))
                            temps = "passé";
                        else
                            temps = "présent";
                    }

                    Console.WriteLine();
                    if (type == "verbe infinitif")
                    {
                        if (mode == "impératif")
                        {
                            Console.WriteLine(verbeInfinitif.Conjugate("2", temps, mode).Verb + " !");
                            Console.WriteLine(verbeInfinitif.Conjugate("4", temps, mode).Verb + " !");
                            Console.WriteLine(verbeInfinitif.Conjugate("5", temps, mode).Verb + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            ConjugatedVerb verbe = verbeInfinitif.Conjugate("1", temps, mode);
                            if (verbe.Verb != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verb[0]))
                                Console.WriteLine("que j'" + verbeInfinitif.Conjugate("1", temps, mode).Verb);
                            else
                                Console.WriteLine("que je " + verbeInfinitif.Conjugate("1", temps, mode).Verb);
                            Console.WriteLine("que tu " + verbeInfinitif.Conjugate("2", temps, mode).Verb);
                            Console.WriteLine("qu'il " + verbeInfinitif.Conjugate("3", temps, mode).Verb);
                            Console.WriteLine("que nous " + verbeInfinitif.Conjugate("4", temps, mode).Verb);
                            Console.WriteLine("que vous " + verbeInfinitif.Conjugate("5", temps, mode).Verb);
                            Console.WriteLine("qu'ils " + verbeInfinitif.Conjugate("6", temps, mode).Verb);
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(verbeInfinitif.Conjugate("1", temps, mode).Verb);
                        }
                        else
                        {
                            ConjugatedVerb verbe = verbeInfinitif.Conjugate("1", temps, mode);
                            if (verbe.Verb != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verb[0]))
                                Console.WriteLine("J'" + verbeInfinitif.Conjugate("1", temps, mode).Verb);
                            else
                                Console.WriteLine("Je " + verbeInfinitif.Conjugate("1", temps, mode).Verb);
                            Console.WriteLine("Tu " + verbeInfinitif.Conjugate("2", temps, mode).Verb);
                            Console.WriteLine("Il " + verbeInfinitif.Conjugate("3", temps, mode).Verb);
                            Console.WriteLine("Nous " + verbeInfinitif.Conjugate("4", temps, mode).Verb);
                            Console.WriteLine("Vous " + verbeInfinitif.Conjugate("5", temps, mode).Verb);
                            Console.WriteLine("Ils " + verbeInfinitif.Conjugate("6", temps, mode).Verb);
                        }
                    }
                    else
                    {
                        if (mode == "impératif")
                        {
                            Console.WriteLine(verbeConjugue.Conjugate("2", temps, mode).Verb + " !");
                            Console.WriteLine(verbeConjugue.Conjugate("4", temps, mode).Verb + " !");
                            Console.WriteLine(verbeConjugue.Conjugate("5", temps, mode).Verb + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            ConjugatedVerb verbe = verbeInfinitif.Conjugate("1", temps, mode);
                            if (verbe.Verb != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verb[0]))
                                Console.WriteLine("que j'" + verbeConjugue.Conjugate("1", temps, mode).Verb);
                            else
                                Console.WriteLine("que je " + verbeConjugue.Conjugate("1", temps, mode).Verb);
                            Console.WriteLine("que tu " + verbeConjugue.Conjugate("2", temps, mode).Verb);
                            Console.WriteLine("qu'il " + verbeConjugue.Conjugate("3", temps, mode).Verb);
                            Console.WriteLine("que nous " + verbeConjugue.Conjugate("4", temps, mode).Verb);
                            Console.WriteLine("que vous " + verbeConjugue.Conjugate("5", temps, mode).Verb);
                            Console.WriteLine("qu'ils " + verbeConjugue.Conjugate("6", temps, mode).Verb);
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(verbeConjugue.Conjugate("1", temps, mode).Verb);
                        }
                        else
                        {
                            ConjugatedVerb verbe = verbeInfinitif.Conjugate("1", temps, mode);
                            if (verbe.Verb != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verb[0]))
                                Console.WriteLine("J'" + verbeConjugue.Conjugate("1", temps, mode).Verb);
                            else
                                Console.WriteLine("Je " + verbeConjugue.Conjugate("1", temps, mode).Verb);
                            Console.WriteLine("Tu " + verbeConjugue.Conjugate("2", temps, mode).Verb);
                            Console.WriteLine("Il " + verbeConjugue.Conjugate("3", temps, mode).Verb);
                            Console.WriteLine("Nous " + verbeConjugue.Conjugate("4", temps, mode).Verb);
                            Console.WriteLine("Vous " + verbeConjugue.Conjugate("5", temps, mode).Verb);
                            Console.WriteLine("Ils " + verbeConjugue.Conjugate("6", temps, mode).Verb);
                        }
                    }
                    Console.WriteLine();
                }
                Console.Clear();
            }


            // POUR CRÉER UN FICHIER TEXTE CONTENANT LA LISTE DES VERBES À AJOUTER
            // Conjugaison.AjoutAutomatiqueVerbe.triVerbes();
        }
    }
}
