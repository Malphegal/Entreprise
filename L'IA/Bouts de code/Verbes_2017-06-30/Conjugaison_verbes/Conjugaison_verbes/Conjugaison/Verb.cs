using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Conjugaison_verbes
{
    public static class Verb
    {
        static private CON con = new CON(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB");

        // ------ STRUCTURES ------

        // Infinitive Verb
        public struct InfinitiveVerb
        {
            public string verb { get; private set; }    // the infinitive and the action
            public string group { get; private set; }
            public string stem { get; private set; }    // radical
            public string ending { get; private set; }  // terminaison

            // Structure's constructor
            public InfinitiveVerb(string verb) : this(verb, groupOf(verb), stemOf(verb)) { }
            public InfinitiveVerb(string verb, string group, string stem)
            {
                this.verb = verb;
                this.group = group;
                this.stem = stem;
                this.ending = endingOf(verb, stem);
            }

            // ------ INSTANCE METHODS ------

            // Conjuge the verb with the person and the time given in parameters
            public ConjugatedVerb Conjugate(string person, string time)
            {
                return Verb.Conjugate(this, person, time);
            }

            public override string ToString()
            {
                return this.verb;
            }
        }

        // Conjugated Verb
        public struct ConjugatedVerb
        {
            public string verb { get; private set; }        // the conjugated verb
            public string action { get; private set; }    // the infinitive and the action
            public string group { get; private set; }
            public string stem { get; private set; }        // radical
            public string ending { get; private set; }    // terminaison
            public string person { get; private set; }
            public string time { get; private set; }

            // Structure's constructors
            public ConjugatedVerb(string verb, string action, string group, string stem, string person, string time) : this()
            {
                this.verb = verb;
                this.action = action;
                this.group = group;
                this.stem = stem;
                this.ending = endingOf(verb, stem);
                this.person = person;
                this.time = time;
            }
            public ConjugatedVerb(string verb, string action, string person, string time) : this(verb, action, groupOf(verb), stemOf(verb), person, time)
            {
                this.ending = endingOf(verb, stem);
            }

            // ------ INSTANCE METHODS ------
            public override string ToString()
            {
                return this.verb;
            }
        }


        // ------ CLASS METHODS ------

        // Return the verb's group
        public static string groupOf(string verb)
        {
            con.Open();
            string group = new CMD(@"SELECT Group FROM Verbes WHERE Infinitive = '" + verb + "'", con).ExecuteScalar().ToString();
            con.Close();
            return group;
        }

        // Return the verb's stem
        public static string stemOf(string verb)
        {
            con.Open();
            string stem = new CMD(@"SELECT Stem FROM Verbes WHERE Infinitive = '" + verb + "'", con).ExecuteScalar().ToString();
            con.Close();
            return stem;
        }

        // Return the verb's ending
        public static string endingOf(string verb, string stem)
        {
            return verb.Substring(stem.Length);
        }

        // Conjuge the infinitive verb in parameters with the person and the time given in parameters
        public static ConjugatedVerb Conjugate(InfinitiveVerb verb, string person, string time)
        {
            con.Open();
            string conjugatedVerb = verb.stem + new CMD(@"SELECT ending FROM Conjugaison
                    WHERE verbGroup = '" + verb.group + "' AND time = '" + time + "' AND person = '" + person + "'", con).ExecuteScalar().ToString();
            con.Close();
            return new ConjugatedVerb(conjugatedVerb, verb.verb, verb.group, verb.stem, person, time);
        }
    }
}
