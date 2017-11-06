using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Adjectifs_2017_07_28.Adjectifs
{
    class Adjectives
    {
        static private CON con = new CON(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB");

        // ------ STRUCTURES ------

        // Adjective
        public struct Adjective
        {
            public string adjective { get; private set; }
            public string gender { get; private set; }
            public string genderBase { get; private set; }
            public string groupM2F { get; private set; }
            public string groupS2P { get; private set; }
            public string number { get; private set; }
            public string numberBase { get; private set; }

            public Adjective(string adjective)
            {
                this.adjective = adjective;
                this.gender = genderOf(adjective);
                this.genderBase = this.gender;
                this.number = numberOf(adjective);
                this.numberBase = this.number;
                this.groupM2F = groupM2FOf(adjective, this.gender, this.number);
                this.groupS2P = groupS2POf(adjective, this.gender, this.number);
            }
            public Adjective(string adjective, string number, string numberBase, string gender, string genderBase, string groupM2F, string groupS2P)
            {
                this.adjective = adjective;
                this.genderBase = gender;
                this.gender = gender;
                this.groupM2F = groupM2F;
                this.groupS2P = groupS2P;
                this.numberBase = number;
                this.number = number;
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

            public bool isMale()
            {
                return this.gender == "M";
            }

            public bool isFemale()
            {
                return this.gender == "F";
            }

            public string singularMaleToPlurialMale()
            {
                this.adjective = Adjectives.singularToPlurial(this.adjective, this.groupS2P);
                this.number = "P";
                return adjective;
            }

            public string plurialMaleToSingularMale()
            {
                this.adjective = Adjectives.plurialToSingular(this.adjective, this.groupS2P);
                this.number = "S";
                return adjective;
            }

            public string maleSingularToFemaleSingular()
            {
                this.adjective = Adjectives.maleToFemale(this.adjective, this.groupM2F);
                this.gender = "F";
                return adjective;
            }

            public string femaleSingularToMaleSingular()
            {
                this.adjective = Adjectives.femaleToMale(this.adjective, this.groupM2F);
                this.gender = "M";
                return adjective;
            }

            public string singularMaleToPlurialFemale()
            {
                this.adjective = Adjectives.maleToFemale(adjective, this.groupM2F);
                this.gender = "F";
                this.adjective = Adjectives.singularToPlurial(adjective, "10");
                this.number = "P";
                return adjective;
            }

            public string plurialFemaleToSingularMale()
            {
                this.adjective = Adjectives.plurialToSingular(adjective, "10");
                this.number = "S";
                this.adjective = Adjectives.femaleToMale(adjective, this.groupM2F);
                this.gender = "M";
                return adjective;
            }

            public override string ToString()
            {
                return this.adjective;
            }
        }

        // ------ CLASS METHODS ------

        // Return the adjective's gender (M for male or F for female)
        public static string genderOf(string adjectiveArg)
        {
            string adjective = adjectiveArg;
            int length = adjective.Length;
            string ending = "";
            int male = 0;  // number of time the program find the adjectif is male
            int female = 0;   // number of time the program find the adjectif is female
            bool findSomethingM;
            bool findSomethingF;
            string res = "";
            int again = 0;
            if (adjective != "frais" && adjective[length - 1] == 's')
            {
                adjective = adjective.Substring(0, length - 1); // we delete the "s"
                length--;                                       // we do this to transform some plurial to singular, and to avoid the case when we enter
                                                                // a plurial adjectif and the program thinks it's male because there is an exception group
                                                                // where the male singular ending by "s"
            }
            if (existe(adjective))
                male++;
            else if (adjective[length - 1] == 'e' && existe(adjective.Substring(0, length - 1)))
                female++;
            do
            {
                again--;
                con.Open();
                // research in male and female endings
                for (int i = length; i >= 1; i--)
                {
                    ending = adjective.Substring(length - i);
                    findSomethingM = new CMD("SELECT Male FROM ExceptionsAdjectifsM2F WHERE Male = '" + ending + "'", con).ExecuteScalar() != null;
                    findSomethingF = new CMD("SELECT Female FROM ExceptionsAdjectifsM2F WHERE Female = '" + ending + "'", con).ExecuteScalar() != null;
                    if (findSomethingM)
                        male++;
                    if (findSomethingF)
                        female++;
                }
                con.Close();
            
                if (male == 0 && female == 0 && again == -1)
                {
                    again=+2;
                    if (adjective.Substring(length - 4) == "eaux")
                        adjective = adjective.Substring(0, length - 4) + "eau";
                    else if (adjective.Substring(length - 3) == "aux")
                        adjective = adjective.Substring(0, length - 3) + "al";
                    length = adjective.Length;
                }
            } while (again == 1);
            // if there is an hesitation (can be male or female at the same time) or if we didn't find anything
            if (male == female)
            {
                // TODO : ask to sentence : on regarde dans la phrase si l'adjectif est accordé à un nom masculin ou féminin
                res = "";   // res = askToSentence(adjectiveArg);
                Console.WriteLine("Ask to sentence");
                if (res == "")  // if the sentence don't know
                {
                    if (adjective[length - 1] == 'e')
                        female++;
                    else
                        male++;
                }
            }
            if (male>female)
                res = "M";
            else if (female>male)
                res = "F";
            return res;
        }

        // Return the adjective's number (S for singular or P for plurial)
        public static string numberOf(string adjectiveArg)
        {
            string adjective = adjectiveArg;
            int length = adjective.Length;
            string ending = "";
            int singular = 0;  // number of time the program find the adjectif is singular
            int plurial = 0;   // number of time the program find the adjectif is plurial
            bool findSomethingS;
            bool findSomethingP;
            string res = "";
            if (existe(adjective))
                singular++;
            else if ((adjective[length - 1] == 's' && existe(adjective.Substring(0, length - 1))) || (adjective.Substring(length - 2) == "es" && existe(adjective.Substring(0, length - 2))))
                plurial++;

            con.Open();
            // research in singular and plurial endings
            for (int i = length; i >= 1; i--)
            {
                ending = adjective.Substring(length - i);
                findSomethingS = new CMD("SELECT Singular FROM ExceptionsAdjectifsS2P WHERE Singular = '" + ending + "'", con).ExecuteScalar() != null;
                findSomethingP = new CMD("SELECT Plurial FROM ExceptionsAdjectifsS2P WHERE Plurial = '" + ending + "'", con).ExecuteScalar() != null;
                if (findSomethingS)
                    singular++;
                if (findSomethingP)
                    plurial++;
            }
            con.Close();

            // if there is an hesitation (can be singular or plurial at the same time) or if we didn't find anything
            if (singular == plurial)
            {
                // TODO : ask to sentence : on regarde dans la phrase si l'adjectif est accordé à un nom singulier ou pluriel
                res = "";   // res = askToSentence(adjectiveArg);
                Console.WriteLine("Ask to sentence");
                if (res == "")  // if the sentence don't know
                {
                    if (adjective[length - 1] == 's' || adjective.Substring(length - 1) == "x")
                        plurial++;
                    else
                        singular++;
                }
            }
            if (singular > plurial)
                res = "S";
            else if (plurial > singular)
                res = "P";
            return res;
        }

        // Return the adjective's exception singular to plurial group
        public static string groupS2POf(string adjective, string gender, string number)
        {
            string res = "";
            // we transform the adjective to male singular if it is not
            if (number == "P")
                adjective = plurialToSingular(adjective);
            if (gender == "F")
                adjective = femaleToMale(adjective);
            // if the IA knows the adjective, we only have to make a research on the data base
            if (existe(adjective))
            {
                con.Open();
                res = new CMD("SELECT GroupS2P FROM Adjectifs WHERE Adjective = '" + adjective + "'", con).ExecuteScalar().ToString();
                con.Close();
            }
            // else, we had to estimate it
            else
            {
                int length = adjective.Length;
                string ending = "";
                object resEnding;
                con.Open();
                for (int i = length; res == "" && i >= 1; i--)
                {
                    ending = adjective.Substring(length - i);
                    resEnding = new CMD("SELECT GroupS2P FROM ExceptionsAdjectifsS2P WHERE Singular = '" + ending + "' ORDER BY GroupS2P ", con).ExecuteScalar();
                    if (resEnding != null)
                        res = resEnding.ToString();
                }
            }
            con.Close();
            if (res == "")
                res = "10";
            return res;
        }
        public static string groupS2POf(string adjective)
        {
            string gender = genderOf(adjective);
            string number = numberOf(adjective);
            return groupS2POf(adjective, gender, number);
        }

        // Return the adjective's exception male to female group
        public static string groupM2FOf(string adjective, string gender, string number)
        {
            string res = "";
            // we transform the adjective to male singular if it is not
            if (number == "P")
                adjective = plurialToSingular(adjective);
            if (gender == "F")
                adjective = femaleToMale(adjective);
            // if the IA knows the adjective, we only have to make a research on the data base
            if (existe(adjective))
            {
                con.Open();
                res = new CMD("SELECT GroupM2F FROM Adjectifs WHERE Adjective = '" + adjective + "'", con).ExecuteScalar().ToString();
                con.Close();
            }
            else
            {
                int length = adjective.Length;
                string ending = "";
                object resEnding;
                con.Open();
                for (int i = length; res == "" && i >= 1; i--)
                {
                    ending = adjective.Substring(length - i);
                    resEnding = new CMD("SELECT GroupM2F FROM ExceptionsAdjectifsM2F WHERE Male = '" + ending + "' ORDER BY GroupM2F", con).ExecuteScalar();
                    if (resEnding != null)
                        res = resEnding.ToString();
                }
            }
            con.Close();
            if (res == "")
                res = "10";
            return res;
        }
        public static string groupM2FOf(string adjective)
        {
            return groupM2FOf(adjective, genderOf(adjective), numberOf(adjective));
        }

        // Return true if the adjective is on plurial, else false
        public static bool isPlurial(string adjective)
        {
            if (numberOf(adjective) == "P")
                return true;
            else
                return false;
        }

        // Return true if the adjective is on singular, else false
        public static bool isSingular(string adjective)
        {
            if (numberOf(adjective) == "S")
                return true;
            else
                return false;
        }

        // Return true if the adjective is male, else false
        public static bool isMale(string adjective)
        {
            if (genderOf(adjective) == "M")
                return true;
            else
                return false;
        }

        // Return true if the adjective is female, else false
        public static bool isFemale(string adjective)
        {
            if (genderOf(adjective) == "F")
                return true;
            else
                return false;
        }

        // Return the plurial of a adjective given as a parameter
        public static string singularToPlurial(string adjective, string groupS2P)
        {
            con.Open();
            string singularEnding = new CMD(@"SELECT Singular FROM ExceptionsAdjectifsS2P WHERE GroupS2P = '" + groupS2P + "'", con).ExecuteScalar().ToString();
            string plurialEnding = new CMD(@"SELECT Plurial FROM ExceptionsAdjectifsS2P WHERE GroupS2P = '" + groupS2P + "'", con).ExecuteScalar().ToString();
            con.Close();
            return adjective.Substring(0, adjective.Length - singularEnding.Length) + plurialEnding;
        }
        public static string singularToPlurial(string adjective)
        {
            int length = adjective.Length;
            string resEnding = "";
            string ending = "";
            object resReq;
            con.Open();
            for (int i = length; resEnding == "" && i >= 1; i--)
            {
                ending = adjective.Substring(length - i);
                resReq = new CMD("SELECT Plurial FROM ExceptionsAdjectifsS2P WHERE Singular = '" + ending + "'", con).ExecuteScalar();
                // we collect the first result only
                if (resReq != null)
                    resEnding = resReq.ToString();
            }
            con.Close();
            return adjective.Substring(0, length - ending.Length) + resEnding;
        }

        // Return the plurial of a adjective given as a parameter
        public static string plurialToSingular(string adjective, string groupS2P)
        {
            con.Open();
            string singularEnding = new CMD(@"SELECT Singular FROM ExceptionsAdjectifsS2P WHERE GroupS2P = '" + groupS2P + "'", con).ExecuteScalar().ToString();
            string plurialEnding = new CMD(@"SELECT Plurial FROM ExceptionsAdjectifsS2P WHERE GroupS2P = '" + groupS2P + "'", con).ExecuteScalar().ToString();
            con.Close();
            return adjective.Substring(0, adjective.Length - plurialEnding.Length) + singularEnding;
        }
        public static string plurialToSingular(string adjective)
        {
            int length = adjective.Length;
            string resEnding = "";
            string ending = "";
            object resReq;
            con.Open();
            for (int i = length; resEnding == "" && i>=1; i--)
            {
                ending = adjective.Substring(length - i);
                resReq = new CMD("SELECT Singular FROM ExceptionsAdjectifsS2P WHERE Plurial = '" + ending + "'", con).ExecuteScalar();
                // we collect the first result only
                if (resReq != null)
                    resEnding = resReq.ToString();
            }
            con.Close();
            return adjective.Substring(0, length - ending.Length) + resEnding;
        }

        // Return the female version of a adjective given as a parameter
        public static string maleToFemale(string adjective, string groupM2F)
        {
            con.Open();
            string maleEnding = new CMD(@"SELECT Male FROM ExceptionsAdjectifsM2F WHERE GroupM2F = '" + groupM2F + "'", con).ExecuteScalar().ToString();
            string femaleEnding = new CMD(@"SELECT Female FROM ExceptionsAdjectifsM2F WHERE GroupM2F = '" + groupM2F + "'", con).ExecuteScalar().ToString();
            con.Close();
            return adjective.Substring(0, adjective.Length - maleEnding.Length) + femaleEnding;
        }
        public static string maleToFemale(string adjective)
        {
            int length = adjective.Length;
            string resEnding = "";
            string ending = "";
            object resReq;
            con.Open();
            for (int i = length; resEnding == "" && i >= 1; i--)
            {
                ending = adjective.Substring(length - i);
                resReq = new CMD("SELECT Female FROM ExceptionsAdjectifsM2F WHERE Male = '" + ending + "'", con).ExecuteScalar();
                // we collect the first result only
                if (resReq != null)
                    resEnding = resReq.ToString();
            }
            con.Close();
            return adjective.Substring(0, length - ending.Length) + resEnding;
        }

        // Return the male version of a adjective given as a parameter
        public static string femaleToMale(string adjective, string groupM2F)
        {
            con.Open();
            string maleEnding = new CMD(@"SELECT Male FROM ExceptionsAdjectifsM2F WHERE GroupM2F = '" + groupM2F + "'", con).ExecuteScalar().ToString();
            string femaleEnding = new CMD(@"SELECT Female FROM ExceptionsAdjectifsM2F WHERE GroupM2F = '" + groupM2F + "'", con).ExecuteScalar().ToString();
            con.Close();
            return adjective.Substring(0, adjective.Length - femaleEnding.Length) + maleEnding;
        }
        public static string femaleToMale(string adjective)
        {
            int length = adjective.Length;
            string resEnding = "";
            string ending = "";
            object resReq;
            con.Open();
            for (int i = length; resEnding == "" && i >= 1; i--)
            {
                ending = adjective.Substring(length - i);
                resReq = new CMD("SELECT Male FROM ExceptionsAdjectifsM2F WHERE Female = '" + ending + "'", con).ExecuteScalar();
                // we collect the first result only
                if (resReq != null)
                    resEnding = resReq.ToString();
            }
            con.Close();
            return adjective.Substring(0, length - ending.Length) + resEnding;
        }

        // Return true if the adjective is in the data table, else false
        public static bool existe(string adjective)
        {
            con.Open();
            bool res = new CMD(@"SELECT Adjective FROM Adjectifs WHERE Adjective = '" + adjective + "'", con).ExecuteScalar() != null;
            con.Close();
            return res;
        }
    }
}
