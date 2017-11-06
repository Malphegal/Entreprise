using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conjugaison_verbes.Conjugaison
{
    class ConjugatedVerb
    {
        // ------ FIELDS ------
        private string verb;
        private string group;
        private string stem;    // radical
        private string ending;  // terminaison
        private string person;
        private string time;
        private string connecString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Documents\Projet AIA\IA.MDB";

        // ------ CONSTRUCTORS ------
        public ConjugatedVerb(string verb) : this(verb, groupOf(verb), stemOf(verb), endingOf(verb), personOf(verb), timeOf(verb))
        { }

        public ConjugatedVerb(string verb, string group, string stem, string ending, string person, string time)
        {
            this.verb = verb;
            this.group = group;
            this.stem = stem;
            this.ending = ending;
            this.person = person;
            this.time = time;
        }

        // ------ INSTANCE METHODS ------

        public string toString()
        {
            return this.verb;
        }
        public string getGroup()
        {
            return this.group;
        }
        public string getStem()
        {
            return this.stem;
        }
        public string getEnding()
        {
            return this.ending;
        }
        public string getPerson ()
        {
            return this.person;
        }
        public string getTime ()
        {
            return this.time;
        }

        // ------ CLASS METHODS ------

        // Return the verb's group
        public static string groupOf(string verb)
        {
            string group = "";



            return group;
        }

        // Return the verb's stem
        public static string stemOf(string verb)
        {
            string stem = "";

            return stem;
        }

        // Return the verb's ending
        public static string endingOf(string verb)
        {
            string ending = "";

            return ending;
        }

        // Return a verb object
        public static ConjugatedVerb ToVerb(string verb)
        {
            ConjugatedVerb verbObject = new ConjugatedVerb(verb);

            return verbObject;
        }

        // Return the verb's person
        public static string personOf(string verb)
        {
            string person = "";

            return person;
        }

        // Return the verb's time
        public static string timeOf(string verb)
        {
            string time = "";

            return time;
        }
    }
}
