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

                fichierNouveau = fichierCourant[184] + "\n" + fichierCourant[195] + "\n" + fichierCourant[206] +
                     "\n" + fichierCourant[207] + "\n" + fichierCourant[209];

                File.AppendAllText("test_format/" + verbeActuel + ".txt", fichierNouveau, Encoding.UTF8);
            }
        }

        public static void FormatHTMLToLigne()
        {
            string[] lesVerbesTXT = Directory.GetFiles(@"test_format/");
            string[] lesVerbes = new string[lesVerbesTXT.Length];
            for (int i = 0; i < lesVerbesTXT.Length; i++)
                lesVerbes[i] = lesVerbesTXT[i].Split('/')[1].Split('.')[0];

            string stringDeficherDeUnVerbe;
            string[] lesTempsDuVerbeCourant = new string[10]; // Une case par temps

            for (int i = 0; i < lesVerbesTXT.Length; i++) // Pour chaque verbe dans le dossier 'test_format/'
            {
                stringDeficherDeUnVerbe = File.ReadAllText(lesVerbesTXT[i]);

                /*splitDuFichierSource = stringDeficherDeUnVerbe.Split(new string[] { "tempscorps" }, StringSplitOptions.None);

                lesTempsDuVerbeCourant = new string[] { splitDuFichierSource[1], splitDuFichierSource[3], splitDuFichierSource[5] };

                lesTempsDuVerbeCourantFormat = new string[][] { new string[] { } };

                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[0].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[1].Split('>').First().Split('<')[0]);
                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[1].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[2].Split('>').First().Split('<')[0]);
                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[2].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[3].Split('>').First().Split('<')[0]);
                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[3].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[4].Split('>').First().Split('<')[0]);
                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[4].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[5].Split('>').First().Split('<')[0]);
                Console.WriteLine(lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[5].Split('>').Last() +
                    lesTempsDuVerbeCourant[0].Split(new string[] { "<b>" }, StringSplitOptions.None)[6].Split('>').First().Split('<')[0]);
                    */
                lesTempsDuVerbeCourant[0] = stringDeficherDeUnVerbe.Split(new string[] { "Présent" }, StringSplitOptions.None)[2].Substring(60); // Indicatif Présent
                //Console.WriteLine(lesTempsDuVerbeCourant[0]);
                lesTempsDuVerbeCourant[1] = stringDeficherDeUnVerbe.Split(new string[] { "Imparfait" }, StringSplitOptions.None)[2].Substring(60); // Indicatif Imparfait
                //Console.WriteLine(lesTempsDuVerbeCourant[1]);
                lesTempsDuVerbeCourant[2] = stringDeficherDeUnVerbe.Split(new string[] { "Passé simple" }, StringSplitOptions.None)[2].Substring(60); // Indicatif Passé simple
                //Console.WriteLine(lesTempsDuVerbeCourant[2]);
                lesTempsDuVerbeCourant[3] = stringDeficherDeUnVerbe.Split(new string[] { "Futur simple" }, StringSplitOptions.None)[2].Substring(60); // Indicatif Futur Simple
                //Console.WriteLine(lesTempsDuVerbeCourant[3]);
                lesTempsDuVerbeCourant[4] = stringDeficherDeUnVerbe.Split(new string[] { "Présent" }, StringSplitOptions.None)[4].Substring(60); // Conditionnel Présent
                //Console.WriteLine(lesTempsDuVerbeCourant[4]);
                lesTempsDuVerbeCourant[5] = stringDeficherDeUnVerbe.Split(new string[] { "Présent" }, StringSplitOptions.None)[6].Substring(60); // Subjonctif Présent
                //Console.WriteLine(lesTempsDuVerbeCourant[5]);
                lesTempsDuVerbeCourant[6] = stringDeficherDeUnVerbe.Split(new string[] { "Imparfait" }, StringSplitOptions.None)[4].Substring(60); // Subjonctif Imparfait
                //Console.WriteLine(lesTempsDuVerbeCourant[6]);
                lesTempsDuVerbeCourant[7] = stringDeficherDeUnVerbe.Split(new string[] { "Présent" }, StringSplitOptions.None)[8].Substring(60); // Impératif Présent
                //Console.WriteLine(lesTempsDuVerbeCourant[7]);
                lesTempsDuVerbeCourant[8] = stringDeficherDeUnVerbe.Split(new string[] { "Présent" }, StringSplitOptions.None)[10].Substring(60); // Participe Présent
                //Console.WriteLine(lesTempsDuVerbeCourant[8]);
                lesTempsDuVerbeCourant[9] = stringDeficherDeUnVerbe.Split(new string[] { "Passé" }, StringSplitOptions.None)[14].Substring(60); // Participe Passé
                //Console.WriteLine(lesTempsDuVerbeCourant[9]);

                string resUnVerbe = "";

                for (int j = 0; j < 10; j++)
                {
                    lesTempsDuVerbeCourant[j] = lesTempsDuVerbeCourant[j].Replace("<b>", "");
                    lesTempsDuVerbeCourant[j] = lesTempsDuVerbeCourant[j].Replace("</b>", "");
                    lesTempsDuVerbeCourant[j] = lesTempsDuVerbeCourant[j].Replace("<br />", "\n");
                    lesTempsDuVerbeCourant[j] = lesTempsDuVerbeCourant[j].Split('\n').ArrayStringVersString(j < 7 ? 6 : j < 8 ? 3 : 1, '\n');
                    resUnVerbe += lesTempsDuVerbeCourant[j] + "\n";
                    //Console.WriteLine("\t" + ActuelTemps(j + 1) + "\n\n" + lesTempsDuVerbeCourant[j] + "\n\n");
                    //Console.ReadKey(true);
                }

                File.AppendAllText("test_toutesConjug/" + lesVerbes[i] + ".txt", resUnVerbe, Encoding.UTF8);

                //Console.WriteLine(resUnVerbe);
                //Console.WriteLine();
                //Console.ReadKey(true);
            }
        }

        static string ArrayStringVersString(this string[] tab, int nb, char separateur)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in tab.Take(nb))
                stringBuilder.Append(s + separateur);
            return stringBuilder.ToString();
        }

        // pour le CW uniquement
        static string ActuelTemps(int n)
        {
            switch (n)
            {
                case 1: return "Indicatif présent";
                case 2: return "Indicatif imparfait";
                case 3: return "Indicatif passé simple";
                case 4: return "Indicatif futur simple";
                case 5: return "Conditionnel présent";
                case 6: return "subjontif présent";
                case 7: return "subjonctif imparfait";
                case 8: return "impératif présent";
                case 9: return "Participe présent";
                default: return "Participe passé";
            }
        }

        public static void LigneToInsertFile()
        {
            string[] lesVerbesTXT = Directory.GetFiles(@"test_toutesConjug/");
            string[] lesVerbes = new string[lesVerbesTXT.Length];
            for (int i = 0; i < lesVerbesTXT.Length; i++)
                lesVerbes[i] = lesVerbesTXT[i].Split('/')[1].Split('.')[0];
            
            for (int i = 0; i < lesVerbesTXT.Length; i++) // Pour chaque verbe
            {
                string[] lesLignesDuVerbe = File.ReadAllLines(lesVerbesTXT[i]);

                string res = string.Empty;

                for (int j = 0; j < lesLignesDuVerbe.Length; j++)
                {
                    if (j < 6)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 6)
                        continue;

                    else if (j < 13)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 13)
                        continue;

                    else if (j < 20)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 20)
                        continue;

                    else if (j < 27)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 27)
                        continue;

                    else if (j < 34)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 34)
                        continue;

                    else if (j < 41)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 41)
                        continue;

                    else if (j < 48)
                        res += lesLignesDuVerbe[j].Split('\'', ' ').Last().RetirerLeTiret() + "\n";
                    else if (j == 48)
                        continue;

                    else if (j < 52)
                        res += lesLignesDuVerbe[j].RetirerLeTiret() + "\n";
                    else if (j == 52)
                        continue;

                    else if (j == 53)
                        res += lesLignesDuVerbe[j].RetirerLeTiret() + "\n";
                    else if (j == 54)
                        continue;

                    else if (j == 55)
                        res += lesLignesDuVerbe[j].RetirerLeTiret().TrimStart(' ');
                    else
                        continue;
                }
                File.AppendAllText("test_toutesInsert/" + lesVerbes[i] + ".txt", res, Encoding.UTF8);
            }
        }

        static string RetirerLeTiret(this string s)
        {
            return s == "-" ? "RIENRIEN" : s;
        }

        public static void InsertFileToInsertFormatFile()
        {
            string[] lesVerbesTXT = Directory.GetFiles(@"test_toutesInsert/");
            string[] lesVerbes = new string[lesVerbesTXT.Length];
            for (int i = 0; i < lesVerbesTXT.Length; i++)
                lesVerbes[i] = lesVerbesTXT[i].Split('/')[1].Split('.')[0];

            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB"))
                for (int i = 0; i < lesVerbesTXT.Length; i++)
                {
                    string resDeUnVerbe = "";
                    string[] conjugDuVerbeCourant = File.ReadAllLines(lesVerbesTXT[i]);

                    for (int j = 0; j < 47; j++)
                        resDeUnVerbe += "UPDATE VerbesConjuges SET Verbe = '" + conjugDuVerbeCourant[j] + "' WHERE Mode = '"
                            + SetDuVerbe(j)[0] + "' AND Temps = '" + SetDuVerbe(j)[1] + "' AND Infinitif = '" + lesVerbes[i] + "'" +
                            " AND Personne = '" + SetDePersonne(j) + "'\n";

                    File.AppendAllText("test_WhereInsert/" + lesVerbes[i] + ".txt", resDeUnVerbe, Encoding.UTF8);
                    /* OleDbCommand cmd = new OleDbCommand(@"UPDATE VerbesConjugues SET
                     Verbe = " + "" + @"
                     WHERE Infinitif = " + lesVerbes[i], con);*/
                }
        }

        static string[] SetDuVerbe(int i) {
            string[] lesModes = { "indicatif", "conditionnel", "subjonctif", "impératif", "participe" };
            string[] lesTemps = { "présent", "imparfait", "passé simple", "futur simple" };
            string[] res = new string[2];

            if (i < 6)
                res[1] = lesTemps[0];
            else if (i < 12)
                res[1] = lesTemps[1];
            else if (i < 18)
                res[1] = lesTemps[2];
            else if (i < 24)
                res[1] = lesTemps[3];
            else if (i < 30)
                res[1] = lesTemps[0];
            else if (i < 36)
                res[1] = lesTemps[0];
            else if (i < 42)
                res[1] = lesTemps[1];
            else if (i < 46)
                res[1] = lesTemps[0];
            else
                res[1] = lesTemps[1];

            res[0] = lesModes[i < 24 ? 0 : i < 30 ? 1 : i < 42 ? 2 : i < 45 ? 3 : 4];

            return res;
        }

        static int SetDePersonne(int i)
        {
            if (i < 6)
                return i + 1;
            else if (i < 12)
                return i - 6 + 1;
            else if (i < 18)
                return i - 12 + 1;
            else if (i < 24)
                return i - 18 + 1;
            else if (i < 30)
                return i - 24 + 1;
            else if (i < 36)
                return i - 30 + 1;
            else if (i < 42)
                return i - 36 + 1;
            else if (i < 43)
                return 2;
            else if (i < 44)
                return 4;
            else if (i < 45)
                return 5;
            else
                return 1;
        }

        public static void InsertFormatFileToDb()
        {
            string[] lesVerbesTXT = Directory.GetFiles(@"test_WhereInsert/");

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB");
            OleDbCommand cmd = new OleDbCommand() { Connection = con };

            for (int i = 0; i < lesVerbesTXT.Length; i++)
            {
                string[] unVerbe = File.ReadLines(lesVerbesTXT[i]).ToArray();

                for (int j = 0; j < 47; j++)
                {
                    cmd.CommandText = unVerbe[j];
                    if (cmd.ExecuteNonQuery() != 0)
                        Console.WriteLine(cmd.CommandText);
                }
            }
        }
    }
}