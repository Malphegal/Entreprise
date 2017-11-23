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
                            Console.WriteLine(verbe.Conjugate("2", temps, mode).Verbe + " !");
                            Console.WriteLine(verbe.Conjugate("4", temps, mode).Verbe + " !");
                            Console.WriteLine(verbe.Conjugate("5", temps, mode).Verbe + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            verbe = verbe.Conjugate("1", temps, mode);
                            if (verbe.Verbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verbe[0]))
                                Console.WriteLine("que j'" + verbe.Conjugate("1", temps, mode).Verbe);
                            else
                                Console.WriteLine("que je " + verbe.Conjugate("1", temps, mode).Verbe);
                            Console.WriteLine("que tu " + verbe.Conjugate("2", temps, mode).Verbe);
                            Console.WriteLine("qu'il " + verbe.Conjugate("3", temps, mode).Verbe);
                            Console.WriteLine("que nous " + verbe.Conjugate("4", temps, mode).Verbe);
                            Console.WriteLine("que vous " + verbe.Conjugate("5", temps, mode).Verbe);
                            Console.WriteLine("qu'ils " + verbe.Conjugate("6", temps, mode).Verbe);
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(verbe.Conjugate("1", temps, mode).Verbe);
                        }
                        else
                        {
                            verbe = verbe.Conjugate("1", temps, mode);
                            if (verbe.Verbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verbe[0]))
                                Console.WriteLine("J'" + verbe.Conjugate("1", temps, mode).Verbe);
                            else
                                Console.WriteLine("Je " + verbe.Conjugate("1", temps, mode).Verbe);
                            Console.WriteLine("Tu " + verbe.Conjugate("2", temps, mode).Verbe);
                            Console.WriteLine("Il " + verbe.Conjugate("3", temps, mode).Verbe);
                            Console.WriteLine("Nous " + verbe.Conjugate("4", temps, mode).Verbe);
                            Console.WriteLine("Vous " + verbe.Conjugate("5", temps, mode).Verbe);
                            Console.WriteLine("Ils " + verbe.Conjugate("6", temps, mode).Verbe);
                        }
                    }
                    else
                    {
                        if (mode == "impératif")
                        {
                            Console.WriteLine(verbe.Conjugate("2", temps, mode).Verbe + " !");
                            Console.WriteLine(verbe.Conjugate("4", temps, mode).Verbe + " !");
                            Console.WriteLine(verbe.Conjugate("5", temps, mode).Verbe + " !");
                        }
                        else if (mode == "subjonctif")
                        {
                            verbe = verbe.Conjugate("1", temps, mode);
                            if (verbe.Verbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verbe[0]))
                                Console.WriteLine("que j'" + verbe.Conjugate("1", temps, mode).Verbe);
                            else
                                Console.WriteLine("que je " + verbe.Conjugate("1", temps, mode).Verbe);
                            Console.WriteLine("que tu " + verbe.Conjugate("2", temps, mode).Verbe);
                            Console.WriteLine("qu'il " + verbe.Conjugate("3", temps, mode).Verbe);
                            Console.WriteLine("que nous " + verbe.Conjugate("4", temps, mode).Verbe);
                            Console.WriteLine("que vous " + verbe.Conjugate("5", temps, mode).Verbe);
                            Console.WriteLine("qu'ils " + verbe.Conjugate("6", temps, mode).Verbe);
                        }
                        else if (mode == "participe")
                        {
                            Console.WriteLine(verbe.Conjugate("1", temps, mode).Verbe);
                        }
                        else
                        {
                            verbe = verbe.Conjugate("1", temps, mode);
                            if (verbe.Verbe != "" && new char[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(verbe.Verbe[0]))
                                Console.WriteLine("J'" + verbe.Conjugate("1", temps, mode).Verbe);
                            else
                                Console.WriteLine("Je " + verbe.Conjugate("1", temps, mode).Verbe);
                            Console.WriteLine("Tu " + verbe.Conjugate("2", temps, mode).Verbe);
                            Console.WriteLine("Il " + verbe.Conjugate("3", temps, mode).Verbe);
                            Console.WriteLine("Nous " + verbe.Conjugate("4", temps, mode).Verbe);
                            Console.WriteLine("Vous " + verbe.Conjugate("5", temps, mode).Verbe);
                            Console.WriteLine("Ils " + verbe.Conjugate("6", temps, mode).Verbe);
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
