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
            this._verbe = verb;
            this.Nature = "verbe infinitif";
            if (estConnu(verb, "VerbesInfinitifs"))
            {
                // Si la case est renseignée, le champ prend la valeur de la case.
                // Sinon, le champ prend la valeur par défaut spécifiée.
                this.Group = infoVerbe[0] == null ? groupOf(verb, Nature) : infoVerbe[0];
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
                // Puisqu'on ne connait pas le verbe, on a aucune info sur lui.
                // On attribut donc des valeurs par défaut qui correspondent aux cas les plus courants.
                this.AuxAvoir = 1;
                this.AuxEtre = 0;
                this.NonPronominale = 1;
                this.Pronominale = 1;
                this.Transitif = null;
                this.Intransitif = 1;
            }
        }
        public InfinitiveVerb(string verb, string group, byte auxAvoir, byte auxEtre, byte nonPronominale,
                                byte pronominale, string transitif, byte intransitif)
        {
            this._verbe = verb;
            this.Nature = "verbe infinitif";
            this.Group = group;
            this.AuxAvoir = auxAvoir;
            this.AuxEtre = auxEtre;
            this.NonPronominale = nonPronominale;
            this.Pronominale = pronominale;
            this.Transitif = transitif;
            this.Intransitif = intransitif;
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
    }
}
