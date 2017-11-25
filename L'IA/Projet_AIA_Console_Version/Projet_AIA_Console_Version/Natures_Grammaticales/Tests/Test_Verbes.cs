using Projet_AIA_Console_Version.Natures_Grammaticales;
using System;
using System.Linq;
using static Projet_AIA_Console_Version.Verb;

namespace Projet_AIA_Console_Version
{
    class Test_Verbes
    {
        public static void Test_Verbe()
        {
            // MINI PROGRAMME DE TEST POUR LA CONJUGAISON DES VERBES
            Verb verbe;
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
                    verbe = new InfinitiveVerb(Console.ReadLine());
                else
                    verbe = new ConjugatedVerb(Console.ReadLine());

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
                            Console.WriteLine(ConjugationOf(verbe, "2", temps, mode) + " !");
                            Console.WriteLine(ConjugationOf(verbe, "4", temps, mode) + " !");
                            Console.WriteLine(ConjugationOf(verbe, "5", temps, mode) + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            string leVerbe = ConjugationOf(verbe, "1", temps, mode);
                            if (leVerbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(leVerbe[0]))
                                Console.WriteLine("que j'" + leVerbe);
                            else
                                Console.WriteLine("que je " + ConjugationOf(verbe, "1", temps, mode));
                            Console.WriteLine("que tu " + ConjugationOf(verbe, "2", temps, mode));
                            Console.WriteLine("qu'il " + ConjugationOf(verbe, "3", temps, mode));
                            Console.WriteLine("que nous " + ConjugationOf(verbe, "4", temps, mode));
                            Console.WriteLine("que vous " + ConjugationOf(verbe, "5", temps, mode));
                            Console.WriteLine("qu'ils " + ConjugationOf(verbe, "6", temps, mode));
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(ConjugationOf(verbe, "1", temps, mode));
                        }
                        else
                        {
                            string leVerbe = ConjugationOf(verbe, "1", temps, mode);
                            if (leVerbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(leVerbe[0]))
                                Console.WriteLine("J'" + leVerbe);
                            else
                                Console.WriteLine("Je " + ConjugationOf(verbe, "1", temps, mode));
                            Console.WriteLine("Tu " + ConjugationOf(verbe, "2", temps, mode));
                            Console.WriteLine("Il " + ConjugationOf(verbe, "3", temps, mode));
                            Console.WriteLine("Nous " + ConjugationOf(verbe, "4", temps, mode));
                            Console.WriteLine("Vous " + ConjugationOf(verbe, "5", temps, mode));
                            Console.WriteLine("Ils " + ConjugationOf(verbe, "6", temps, mode));
                        }
                    }
                    else
                    {
                        if (mode == "impératif")
                        {
                            Console.WriteLine(ConjugationOf(verbe, "2", temps, mode) + " !");
                            Console.WriteLine(ConjugationOf(verbe, "4", temps, mode) + " !");
                            Console.WriteLine(ConjugationOf(verbe, "5", temps, mode) + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            string leVerbe = ConjugationOf(verbe, "1", temps, mode);
                            if (leVerbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(leVerbe[0]))
                                Console.WriteLine("que j'" + leVerbe);
                            else
                                Console.WriteLine("que je " + ConjugationOf(verbe, "1", temps, mode));
                            Console.WriteLine("que tu " + ConjugationOf(verbe, "2", temps, mode));
                            Console.WriteLine("qu'il " + ConjugationOf(verbe, "3", temps, mode));
                            Console.WriteLine("que nous " + ConjugationOf(verbe, "4", temps, mode));
                            Console.WriteLine("que vous " + ConjugationOf(verbe, "5", temps, mode));
                            Console.WriteLine("qu'ils " + ConjugationOf(verbe, "6", temps, mode));
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(ConjugationOf(verbe, "1", temps, mode));
                        }
                        else
                        {
                            string leVerbe = ConjugationOf(verbe, "1", temps, mode);
                            if (leVerbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(leVerbe[0]))
                                Console.WriteLine("J'" + leVerbe);
                            else
                                Console.WriteLine("Je " + ConjugationOf(verbe, "1", temps, mode));
                            Console.WriteLine("Tu " + ConjugationOf(verbe, "2", temps, mode));
                            Console.WriteLine("Il " + ConjugationOf(verbe, "3", temps, mode));
                            Console.WriteLine("Nous " + ConjugationOf(verbe, "4", temps, mode));
                            Console.WriteLine("Vous " + ConjugationOf(verbe, "5", temps, mode));
                            Console.WriteLine("Ils " + ConjugationOf(verbe, "6", temps, mode));
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
