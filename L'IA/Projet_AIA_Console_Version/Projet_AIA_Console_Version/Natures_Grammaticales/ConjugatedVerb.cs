using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class ConjugatedVerb : Verb
    {
            // PROPRIETES

        public string Person { get; private set; }  // La personne à laquelle est conjugué le verbe
        public string Time { get; private set; }    // Le temps auquel est conjugué le verbe
        public string Mode { get; private set; }    // Le mode auquel le verbe est conjugué (ex : l'indicatif)


            // CONSTRUCTEUR

        // On appelle ce constructeur si on a aucun moyen dans la phrase de connaître la personne
        // du verbe.
        public ConjugatedVerb(string verb)
        {
            infoVerbe = null;
            this.Verbe = verb;
            this.Nature = "verbe conjugué";
            if (estConnu(verb, "VerbesConjugues"))
            {
                // Si la case est renseignée, le champ prend la valeur de la case.
                // Sinon, le champ prend la valeur par défaut spécifiée.
                this.Group = infoVerbe[0] == null ? groupOf(verb, Nature) : infoVerbe[0];
                this.Time = infoVerbe[1] == null ? timeOf(verb) : infoVerbe[1];
                this.Mode = infoVerbe[2] == null ? modeOf(verb) : infoVerbe[2];
                this.Person = infoVerbe[3] == null ? personOf(verb) : infoVerbe[3];
                this.Action = infoVerbe[4] == null ? infinitiveOf(verb) : infoVerbe[4];
                this.auxAvoir = infoVerbe[5] == null ? (byte)1 : Convert.ToByte(infoVerbe[5]);
                this.auxEtre = infoVerbe[6] == null ? (byte)0 : Convert.ToByte(infoVerbe[6]);
                this.nonPronominale = infoVerbe[7] == null ? (byte)1 : Convert.ToByte(infoVerbe[7]);
                this.pronominale = infoVerbe[8] == null ? (byte)1 : Convert.ToByte(infoVerbe[8]);
                this.transitif = infoVerbe[9];
                this.intransitif = infoVerbe[10] == null ? (byte)1 : Convert.ToByte(infoVerbe[10]);
            }
            else
            {
                this.Group = groupOf(verb, Nature);
                this.Time = timeOf(verb);
                this.Mode = modeOf(verb);
                this.Person = personOf(verb);
                this.Action = infinitiveOf(verb);
                // Puisqu'on ne connait pas le verbe, on a aucune info sur lui.
                // On attribut donc des valeurs par défaut qui correspondent aux cas les plus courants.
                this.auxAvoir = 1;
                this.auxEtre = 0;
                this.nonPronominale = 1;
                this.pronominale = 1;
                this.transitif = null;
                this.intransitif = 1;
            }

        }
        // On appelle ce constructeur si on peut connaître dans la phrase la personne du verbe, grâce à un
        // nom ou un pronom personnel le précédant.
        public ConjugatedVerb(string verb, string person)
        {
            infoVerbe = null;
            this.Verbe = verb;
            this.Nature = "verbe conjugué";
            this.Person = person;
            if (estConnu(verb, "VerbesConjugues"))
            {
                // Si la case est renseignée, le champ prend la valeur de la case.
                // Sinon, le champ prend la valeur par défaut spécifiée.
                this.Group = infoVerbe[0] == null ? groupOf(verb, Nature) : infoVerbe[0];
                this.Time = infoVerbe[1] == null ? timeOf(verb) : infoVerbe[1];
                this.Mode = infoVerbe[2] == null ? modeOf(verb) : infoVerbe[2];
                this.Action = infoVerbe[4] == null ? infinitiveOf(verb) : infoVerbe[4];
                this.auxAvoir = infoVerbe[1] == null ? (byte)1 : Convert.ToByte(infoVerbe[1]);
                this.auxEtre = infoVerbe[2] == null ? (byte)0 : Convert.ToByte(infoVerbe[2]);
                this.nonPronominale = infoVerbe[3] == null ? (byte)1 : Convert.ToByte(infoVerbe[3]);
                this.pronominale = infoVerbe[4] == null ? (byte)1 : Convert.ToByte(infoVerbe[4]);
                this.transitif = infoVerbe[5];
                this.intransitif = infoVerbe[6] == null ? (byte)1 : Convert.ToByte(infoVerbe[6]);
            }
            else
            {
                this.Group = groupOf(verb, Nature);
                this.Time = timeOf(verb);
                this.Mode = modeOf(verb);
                this.Action = infinitiveOf(verb);
                // Puisqu'on ne connait pas le verbe, on a aucune info sur lui.
                // On attribut donc des valeurs par défaut qui correspondent aux cas les plus courants.
                this.auxAvoir = 1;
                this.auxEtre = 0;
                this.nonPronominale = 1;
                this.pronominale = 1;
                this.transitif = null;
                this.intransitif = 1;
            }
        }
        // On appelle ce constructeur dans la méthode qui conjugue des verbes à l'infinitif.
        // En effet, on possède déjà toutes les informations sur le verbe conjugué.
        public ConjugatedVerb(string verb, string person, string group, string time, string mode, string action,
                byte auxAvoir, byte auxEtre, byte nonPronominale, byte pronominale, string transitif, byte intransitif)
        {
            this.Verbe = verb;
            this.Nature = "verbe conjugué";
            this.Person = person;
            this.Group = group;
            this.Time = time;
            this.Mode = mode;
            this.Action = action;
            this.auxAvoir = auxAvoir;
            this.auxEtre = auxEtre;
            this.nonPronominale = nonPronominale;
            this.pronominale = pronominale;
            this.transitif = transitif;
            this.intransitif = intransitif;
        }


            // METHODS

        public bool isSingular()
        {
            return new string[] { "1", "2", "3" }.Contains(this.Person);
        }

        public bool isPlurial()
        {
            return new string[] { "4", "5", "6" }.Contains(this.Person);
        }

        // Conjugue le verbe à la personne et au temps donnés en paramètre.
        public override Verb Conjugate(string person, string time, string mode)
        {
            ConjugatedVerb newVerbe = (ConjugatedVerb)Conjugate(this, person, time, mode);
            /*this.Verbe = newVerbe.Verbe;
            this.Person = person;
            this.Time = time;
            this.Mode = mode;*/
            return newVerbe;
        }

        // Renvoie le participe présent du verbe.
        public override string GetParticipePresent()
        {
            return Conjugate(this, "1", "présent", "participe").Verbe;
        }

        // Renvoie le participe passé du verbe.
        public override string GetParticipePasse()
        {
            return Conjugate(this, "1", "passé", "participe").Verbe;
        }

        // Transforme un verbe conjugué en verbe infinitif à partir de l'infinitif contenu dans le verbe conjugué et du groupe.
        public override Verb ToInfinitive(Verb verbe)
        {
            return new InfinitiveVerb(verbe.Action, verbe.Group, verbe.auxAvoir, verbe.auxEtre, verbe.nonPronominale, verbe.pronominale, verbe.transitif, verbe.intransitif);
        }

        public override string ToString()
        {
            return this.Verbe;
        }
    }
}
