using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conjugaison_verbes.Verb;

namespace Conjugaison_verbes
{
    class Program
    {
        static void Main()
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
