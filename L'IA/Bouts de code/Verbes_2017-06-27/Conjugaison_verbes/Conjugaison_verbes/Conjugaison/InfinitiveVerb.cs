using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Conjugaison_verbes
{
    public class InfinitiveVerb
    {
        // ------ FIELDS ------
        public string verb { get { return this.verb; } private set { } }
        protected string group;
        protected string stem;    // radical
        protected string ending;  // terminaison
        static private CON con = new CON(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Documents\Projet AIA\IA.MDB");

        // ------ CONSTRUCTORS ------
        public InfinitiveVerb (string verb) : this(verb, groupOf(verb), stemOf(verb), endingOf(verb))
        { }

        public InfinitiveVerb (string verb, string group, string stem, string ending)
        {
            this.verb = verb.ToLower();
            this.group = group;
            this.stem = stem;
            this.ending = ending;
        }

        // ------ INSTANCE METHODS ------
        
        public string getGroup ()
        {
            return this.group;
        }
        public string getStem ()
        {
            return this.stem;
        }
        public string getEnding ()
        {
            return this.ending;
        }


        // ------ CLASS METHODS ------

        // Return the verb's group
        public static string groupOf (string verb)
        {
            con.Open();
            string group = new CMD(@"SELECT Group FROM Verbes WHERE Infinitive IS '" + verb + "'", con).ExecuteScalar().ToString();
            con.Close();
            return group;
            
        }

        // Return the verb's stem
        public static string stemOf (string verb)
        {
            return verb.Substring(0, verb.Length - 3);  // return all except the two last letter
        }

        // Return the verb's ending
        public static string endingOf (string verb)
        {
            return verb.Substring(verb.Length - 3); // return the two last letter
        }

        // Return a verb object
        public static InfinitiveVerb ToVerb (string verb)
        {
            return new InfinitiveVerb(verb);
        }
    }
}
