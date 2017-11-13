using System;
using System.Net;
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
        #region COMMENTAIRE
        /* 
         * Indicatif                présent             [1 - 6]         185
         * Indicatif                imparfait           [1 - 6]         185
         * indicatif                passé simple        [1 - 6]         185
         * Indicatif                futur Simple        [1 - 6]         196
         * 
         * Conditionnel             présent             [1 - 6]         196
         * 
         * Subjonctif               présent             [1 - 6]         207
         * Subjonctif               imparfait           [1 - 6]         207
         * 
         * Impératif                présent             [2 4 5]         208
         * 
         * Participe                présent             [1]             210
         * Participe                passé               [1]             210
         */
        #endregion

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

            using (OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =..\..\..\IA.MDB")) {
                con.Open();
                string[] lesVerbesFormat = File.ReadAllLines(outtxt);
                foreach (string ligneValues in lesVerbesFormat)
                {
                    Console.WriteLine(ligneValues);
                    new OleDbCommand(@"INSERT INTO VerbesConjugues (Groupe, Temps, Mode, Personne, Infinitif, nbrUtilisation)
                    VALUES (" + ligneValues + ")", con).ExecuteNonQuery();
                }
            }
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

        public static void VerbesToURL()
        {
            string[] tousLesVerbes = File.ReadAllLines("verbes.txt");

            string res = "";
            foreach (string s in tousLesVerbes)
                res += "http://la-conjugaison.nouvelobs.com/du/verbe/" + s + ".php\n";

            File.AppendAllText("out.txt", res);
        }

        public static void URLToFichier()
        {
            string[] aa = File.ReadAllLines("out.txt").Skip(2).ToArray();
            foreach (string url in aa)
            {
                WebRequest objRequest = WebRequest.Create(new Uri(url));

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

                string Charset = objResponse.CharacterSet;
                Encoding encoding = Encoding.GetEncoding(Charset);

                StreamReader sr =
                       new StreamReader(objResponse.GetResponseStream(), encoding);
                string body = sr.ReadToEnd();
                File.AppendAllText("test/" + url.Split('/')[5] + ".txt", body, Encoding.UTF8);
            }
        }

        public static void HTMLToFormatHTML()
        {
            string[] outtxt = File.ReadAllLines("out.txt");
            string[] fichierCourant;
            string fichierNouveau;

            string verbeActuel;

            for (int i = 2; i < outtxt.Length; i++)
            {
                fichierCourant = File.ReadAllLines("test/" + (outtxt[i].Split('/')[5] + ".txt"));

                verbeActuel = fichierCourant[5].Split(' ')[4];
                verbeActuel += verbeActuel == "se" ? fichierCourant[5].Split(' ')[5] : "";

                fichierNouveau = fichierCourant[184] + fichierCourant[195] + fichierCourant[206] + fichierCourant[207] + fichierCourant[209];

                File.AppendAllText("test_format/" + verbeActuel + ".txt", fichierNouveau, Encoding.UTF8);
            }
        }
    }
}