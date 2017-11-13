using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Projet_AIA_Console_Version.Natures_Grammaticales.Divers
{
    public static class RemplissageAuto
    {
        public static void TousLesVerbes()
        {
            const string outtxt = "out.txt";
            string[] tousLesVerves = File.ReadAllLines("verbes.txt");
            string res = "";

            for (int i = 0; i < tousLesVerves.Length; i++)
            {
                int val = 0;

                    // Indicatif

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                    // Conditionnel

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                    // Subjonctif

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 3 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 6 + "', '" + tousLesVerves[i] + "', '0'\n";

                    // Impératif

                res += "'3', '" + Temps(val++) + 2 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 4 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 5 + "', '" + tousLesVerves[i] + "', '0'\n";

                    // Participe

                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
                res += "'3', '" + Temps(val++) + 1 + "', '" + tousLesVerves[i] + "', '0'\n";
            }

            File.AppendAllText(outtxt, res);

                // Maintenant, faire le insert dans la base de données

            System.Threading.Thread.Sleep(100);

            string[] lesVerbesFormat = File.ReadAllLines(outtxt);
            foreach (string ligneValues in lesVerbesFormat)
                new OleDbCommand(@"INSERT INTO VerbesConjugues ('Groupe', 'Temps', 'Mode', 'Personne', 'Infinitif', 'nbrUtilisation')
                    VALUES (" + ligneValues + ")", new OleDbConnection("")).ExecuteNonQuery();
        }

        private static string Temps(int valeur)
        {
            if (valeur < 6)
                return "présent', 'indicatif', '";
            if (valeur < 12)
                return "imparfait', 'indicatif', '";
            if (valeur < 18)
                return "passé simple', 'indicatif', '";
            if (valeur < 24)
                return "futur simple', 'indicatif', '";
            if (valeur < 30)
                return "présent', 'conditionnel', '";
            if (valeur < 36)
                return "présent', 'subjonctif', '";
            if (valeur < 42)
                return "imparfait', 'subjonctif', '";
            if (valeur < 45)
                return "présent', 'impératif', '";
            if (valeur < 46)
                return "présent', 'participe', '";
            return "passé', 'participe', '";
        }
    }
}
