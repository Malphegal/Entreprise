using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    public class ConjugatedVerb : Verb
    {
            // PROPRIETES

        public string Person    { get; protected set; } // La personne à laquelle est conjugué le verbe
        public string Time      { get; protected set; } // Le temps auquel est conjugué le verbe
        public string Mode      { get; protected set; } // Le mode auquel le verbe est conjugué (ex : l'indicatif)


            // CONSTRUCTEUR

        // On appelle ce constructeur si on a aucun moyen dans la phrase de connaître la personne
        // du verbe.
        public ConjugatedVerb(string verb)
        {
            infoVerbe = null;
            this._verbe = verb;
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
                this.AuxAvoir = infoVerbe[5] == null ? (byte)1 : Convert.ToByte(infoVerbe[5]);
                this.AuxEtre = infoVerbe[6] == null ? (byte)0 : Convert.ToByte(infoVerbe[6]);
                this.NonPronominale = infoVerbe[7] == null ? (byte)1 : Convert.ToByte(infoVerbe[7]);
                this.Pronominale = infoVerbe[8] == null ? (byte)1 : Convert.ToByte(infoVerbe[8]);
                this.Transitif = infoVerbe[9];
                this.Intransitif = infoVerbe[10] == null ? (byte)1 : Convert.ToByte(infoVerbe[10]);
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
                this.AuxAvoir = 1;
                this.AuxEtre = 0;
                this.NonPronominale = 1;
                this.Pronominale = 1;
                this.Transitif = null;
                this.Intransitif = 1;
            }
            if (new[] { "1", "2", "3" }.Contains(this.Person))
                this.Number = "S";
            else
                this.Number = "P";
            this.Gender = "N";
    }
        // On appelle ce constructeur si on peut connaître dans la phrase la personne du verbe, grâce à un
        // nom ou un pronom personnel le précédant.
        public ConjugatedVerb(string verb, string person)
        {
            infoVerbe = null;
            this._verbe = verb;
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
                this.AuxAvoir = infoVerbe[1] == null ? (byte)1 : Convert.ToByte(infoVerbe[1]);
                this.AuxEtre = infoVerbe[2] == null ? (byte)0 : Convert.ToByte(infoVerbe[2]);
                this.NonPronominale = infoVerbe[3] == null ? (byte)1 : Convert.ToByte(infoVerbe[3]);
                this.Pronominale = infoVerbe[4] == null ? (byte)1 : Convert.ToByte(infoVerbe[4]);
                this.Transitif = infoVerbe[5];
                this.Intransitif = infoVerbe[6] == null ? (byte)1 : Convert.ToByte(infoVerbe[6]);
            }
            else
            {
                this.Group = groupOf(verb, Nature);
                this.Time = timeOf(verb);
                this.Mode = modeOf(verb);
                this.Action = infinitiveOf(verb);
                // Puisqu'on ne connait pas le verbe, on a aucune info sur lui.
                // On attribut donc des valeurs par défaut qui correspondent aux cas les plus courants.
                this.AuxAvoir = 1;
                this.AuxEtre = 0;
                this.NonPronominale = 1;
                this.Pronominale = 1;
                this.Transitif = null;
                this.Intransitif = 1;
            }
            if (new[] { "1", "2", "3" }.Contains(this.Person))
                this.Number = "S";
            else
                this.Number = "P";
            this.Gender = "N";
        }
        // On appelle ce constructeur dans la méthode qui conjugue des verbes à l'infinitif.
        // En effet, on possède déjà toutes les informations sur le verbe conjugué.
        public ConjugatedVerb(string verb, string person, string group, string time, string mode, string action,
                byte auxAvoir, byte auxEtre, byte nonPronominale, byte pronominale, string transitif, byte intransitif)
        {
            this._verbe = verb;
            this.Nature = "verbe conjugué";
            this.Person = person;
            this.Group = group;
            this.Time = time;
            this.Mode = mode;
            this.Action = action;
            this.AuxAvoir = auxAvoir;
            this.AuxEtre = auxEtre;
            this.NonPronominale = nonPronominale;
            this.Pronominale = pronominale;
            this.Transitif = transitif;
            this.Intransitif = intransitif;
            if (new[] { "1", "2", "3" }.Contains(this.Person))
                this.Number = "S";
            else
                this.Number = "P";
            this.Gender = "N";
        }


            // METHODES

        // Renvoie le participe présent du verbe.
        public override string GetParticipePresent()
        {
            return ConjugationOf(this, "1", "présent", "participe");
        }

        // Renvoie le participe passé du verbe.
        public override string GetParticipePasse()
        {
            return ConjugationOf(this, "1", "passé", "participe");
        }

        public override string ToString()
        {
            return this._verbe;
        }


            // METHODES DE CLASSE

        // Transforme le verbe conjugué en un pluriel.
        public static void SingularToPlurial(ref ConjugatedVerb verbe)
        {
            Verb v = verbe;
            if (verbe.Nature == "verbe conjugué")
            {
                switch (verbe.Person)
                {
                    case "1":
                        Conjugate(ref v, "4", verbe.Time, verbe.Mode);
                        break;
                    case "2":
                        Conjugate(ref v, "5", verbe.Time, verbe.Mode);
                        break;
                    case "3":
                        Conjugate(ref v, "6", verbe.Time, verbe.Mode);
                        break;
                }
                // Si le verbe est déjà au pluriel, on ne fait rien.
            }
            // Si le verbe est un infinitif, on ne fait rien.
        }

        // Transforme le verbe conjugué en un singulier.
        public static void PlurialToSingular(ref ConjugatedVerb verbe)
        {
            Verb v = verbe;
            if (verbe.Nature == "verbe conjugué")
            {
                switch (verbe.Person)
                {
                    case "4":
                        Conjugate(ref v, "1", verbe.Time, verbe.Mode);
                        break;
                    case "5":
                        Conjugate(ref v, "2", verbe.Time, verbe.Mode);
                        break;
                    case "6":
                        Conjugate(ref v, "3", verbe.Time, verbe.Mode);
                        break;
                }
                // Si le verbe est déjà au singulier, on ne fait rien.
            }
            // Si le verbe est un infinitif, on ne fait rien.
        }
    }
}
