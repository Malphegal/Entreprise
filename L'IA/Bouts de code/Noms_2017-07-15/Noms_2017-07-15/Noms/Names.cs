using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Noms_2017_07_15.Noms
{
    class Names
    {
        static private CON con = new CON(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB");

        // ------ STRUCTURES ------

        // Name
        public struct Name
        {
            public string name { get; private set; }
            public string gender { get; private set; }
            public string group { get; private set; }   // Le groupe des exceptions du pluriel
            /* group 1 : "" → "s" (la règle de base du pluriel)
             * group 2 : "ail" → "aux" (le reste fait "ail" → "ails")
             * group 3 : "ou" → "oux" (le reste fait "ou" → "ous") et règle normale "au/eau/eu" → "aux/eaux/eux" (exceptions "au/eau/eu" → "aus/eaus/eus" placées dans groupe 1)
             * group 4 : règle normale "al" → "aux" (exceptions "al" → "als" placées dans groupe 1)
             * group 5 : mots finissant par "s" au singulier : "s" → "s"
             * http://www.francaisfacile.com/exercices/exercice-francais-2/exercice-francais-18015.php */
            public string number { get; private set; }
            public string numberBase { get; private set; }

            public Name(string name)
            {
                this.name = name;
                this.number = numberOf(name);
                this.numberBase = number;
                this.gender = genderOf(name, number);
                this.group = groupOf(name, number);
            }
            public Name(string name, string number, string gender, string group)
            {
                this.name = name;
                this.gender = gender;
                this.group = group;
                this.number = number;
                this.numberBase = number;
            }

            // ------ INSTANCE METHODS ------

            public bool isSingular()
            {
                return this.number == "S";
            }

            public bool isPlurial()
            {
                return this.number == "P";
            }

            public string singularToPlurial()
            {
                this.name = Names.singularToPlurial(this.name, this.group);
                this.number = "P";
                return this.name;
            }

            public string plurialToSingular()
            {
                this.name = Names.plurialToSingular(this.name, this.group);
                this.number = "S";
                return this.name;
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        // ------ CLASS METHODS ------

        // Return the name's gender
        public static string genderOf(string nameArg, string number)
        {
            string name = nameArg;
            string gender;
            int male = 0;
            int female = 0;
            // If the name is plurial, we pass it to singular
            if (number == "P")
                name = plurialToSingular(name);
            // If the name existes in the data base, we search its gender in the data base
            if (existe(name))
            {
                con.Open();
                gender = new CMD(@"SELECT Gender FROM Noms WHERE Nom = '" + name + "'", con).ExecuteScalar().ToString();
                con.Close();
            }
            // else, we estimate its gender with its ending
            else
            {
                int length = name.Length;
                if (name[length - 1] == 'e')
                    female++;
                else
                    male++;

                if (female > male)
                    gender = "F";
                else
                    gender = "M";

            }   // TODO : ask to sentence ?
            return gender;
        }
        public static string genderOf(string name)
        {
            return genderOf(name, numberOf(name));
        }

        // Return the name's exception group
        public static string groupOf(string name, string number)
        {
            string group;
            // If the name is plurial, we pass it to singular
            if (number == "P")
                name = plurialToSingular(name);
            // If the name existes in the data base, we search its group in the data base
            if (existe(name))
            {
                con.Open();
                group = new CMD(@"SELECT Group FROM Noms WHERE Nom = '" + name + "'", con).ExecuteScalar().ToString();
                con.Close();
            }
            // else, we estimate its group with its ending
            else
            {
                int length = name.Length;
                if (length > 1 && name[length - 1] == 's')
                    group = "5";
                else if (length > 3 && name.Substring(length - 3) == "ail")
                    group = "2";
                else if (length > 2 && name.Substring(length - 2) == "ou")
                    group = "3";
                else if (length > 2 && name.Substring(length - 2) == "eu")
                    group = "6";
                else if (length > 1 && name.Substring(length - 1) == "z")
                    group = "7";
                else
                    group = "1";
            }
            return group;
        }
        public static string groupOf(string name)
        {
            return groupOf(name, numberOf(name));
        }

        // Return the name's number (S for singular or P for plurial)
        public static string numberOf(string nameArg)
        {
            string name = nameArg;
            int length = name.Length;
            int singular = 0;  // number of time the program find the name is singular
            int plurial = 0;   // number of time the program find the name is plurial
            string res = "";

            // if the name is in the data base, that means the name is singular
            if (existe(name))
                singular++;
            else if ((name[length - 1] == 's' || name[length - 1] == 'x') && existe(name.Substring(0, length - 1)))
                plurial++;

            // ending by s or x → plurial, else → singular
            if (name[length - 1] == 's' || name[length - 1] == 'x')
                plurial++;
            else
                singular++;

            // if there is an hesitation (can be singular or plurial at the same time) or if we didn't find anything
            if (singular == plurial)
            {
                // TODO : ask to sentence : on regarde dans la phrase si l'adjectif est accordé à un nom singulier ou pluriel
                res = "";   // res = askToSentence(adjectiveArg);
                Console.WriteLine("Ask to sentence");
            }

            if (plurial > singular)
                res = "P";
            else
                res = "S";
            return res;
        }

        // Return true if the name is on plurial, else false
        public static bool isPlurial(string name)
        {
            return numberOf(name) == "P";
        }

        // Return the plurial of a name given as a parameter
        public static string singularToPlurial(string name, string group)
        {
            con.Open();
            string singularEnding = new CMD(@"SELECT Singular FROM PlurielNoms WHERE Groupe = '" + group + "'", con).ExecuteScalar().ToString();
            string plurialEnding = new CMD(@"SELECT Plurial FROM PlurielNoms WHERE Groupe = '" + group + "'", con).ExecuteScalar().ToString();
            con.Close();
            return name.Substring(0, name.Length - singularEnding.Length) + plurialEnding;
        }

        // Return the singular of a name given as a parameter
        public static string plurialToSingular(string name)
        {
            int length = name.Length;
            if (length > 1 && name[length - 1] == 's')
                name = name.Substring(0, length - 1);
            else if (length > 3 && name.Substring(length - 3) == "oux")
                name = name.Substring(0, length - 2) + "ou";
            else if (length > 3 && name.Substring(length - 3) == "eux")
                name = name.Substring(0, length - 2) + "eu";
            else if (length > 3 && name.Substring(length - 3) == "aux")
            {
                string nameTest = name.Substring(0, length - 3) + "ail";
                if (existe(nameTest))
                    name = nameTest;
                else
                    name = name.Substring(0, length - 3) + "al";
            }
            return name;
        }
        public static string plurialToSingular(string name, string group)
        {
            con.Open();
            string singularEnding = new CMD(@"SELECT Singular FROM PlurielNoms WHERE Groupe = '" + group + "'", con).ExecuteScalar().ToString();
            string plurialEnding = new CMD(@"SELECT Plurial FROM PlurielNoms WHERE Groupe = '" + group + "'", con).ExecuteScalar().ToString();
            con.Close();
            return name.Substring(0, name.Length - plurialEnding.Length) + singularEnding;
        }

        // Return true if the name is in the data table, else false
        public static bool existe(string name)
        {
            con.Open();
            bool res = new CMD(@"SELECT Nom FROM Noms WHERE Nom = '" + name + "'", con).ExecuteScalar() != null;
            con.Close();
            return res;
        }
    }
}