using Projet_AIA_Console_Version.Natures_Grammaticales.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version
{
    class Program
    {
        static void Main(string[] args)
        {
            // CREATION DATASETS
            Phrase.CreationDataSets();
            Console.WriteLine("DataSets créés avec succès !\n");


            Console.WriteLine();
            Console.WriteLine("\t\t\t========== PROJET AIA ==========\n\n");
            Console.WriteLine(" Quel programme de test souhaitez-vous lancer ?\n (Entrez le numéro correspondant)\n");
            Console.WriteLine("\t1 - Verbe \t2 - Nom \t3 - Adjectif \t4 - Phrase \t5 - Déterminant \t6 - TEST_VERBESn");
            switch (int.Parse(Console.ReadLine()))
            {
                case (1):
                    Console.Clear();
                    Test_Verbes.Test_Verbe();
                    break;
                case (2):
                    Console.Clear();
                    Test_Noms.Test_Nom();
                    break;
                case (3):
                    Console.Clear();
                    Test_Adjectifs.Test_Adjectif();
                    break;
                case (4):
                    Console.Clear();
                    Test_Phrases.Test_Phrase();
                    break;
                case (5):
                    Console.Clear();
                    Test_Determinants.Test_Determinant();
                    break;
                case (6):
                    Console.Clear();
                    Natures_Grammaticales.Divers.RemplissageAuto.TousLesVerbes();
                    break;
                default:
                    Console.WriteLine("Erreur : Veuillez entrer un numéro valide");
                    break;
            }
            Console.ReadLine();
        }
    }
}
