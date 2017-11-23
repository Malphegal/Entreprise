using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class InfinitiveVerb : Verb
    {
        // CONSTRUCTEUR
        public InfinitiveVerb(string verb)
        {
            infoVerbe = null;
            this.Verbe = verb;
            this.Nature = "verbe infinitif";
            if (estConnu(verb, "VerbesInfinitifs"))
            {
                // Si la case est renseignée, le champ prend la valeur de la case.
                // Sinon, le champ prend la valeur par défaut spécifiée.
                this.Group = infoVerbe[0] == null ? groupOf(verb, Nature) : infoVerbe[0];
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
        public InfinitiveVerb(string verb, string group, byte auxAvoir, byte auxEtre, byte nonPronominale,
                                byte pronominale, string transitif, byte intransitif)
        {
            this.Verbe = verb;
            this.Nature = "verbe infinitif";
            this.Group = group;
            this.auxAvoir = auxAvoir;
            this.auxEtre = auxEtre;
            this.nonPronominale = nonPronominale;
            this.pronominale = pronominale;
            this.transitif = transitif;
            this.intransitif = intransitif;
        }
        
        // ------ INSTANCE METHODS ------

        // Conjugue le verbe à la personne et au temps donnés en paramètre.
        public override Verb Conjugate(string person, string time, string mode)
        {
            return (ConjugatedVerb)Conjugate(this, person, time, mode);
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

        // Transforme le verbe entré en paramètre en un verbe à l'infinitif.
        public override Verb ToInfinitive(Verb verbe)
        {
            return this;
        }

        public override string ToString()
        {
            return this.Verbe;
        }
    }
}
