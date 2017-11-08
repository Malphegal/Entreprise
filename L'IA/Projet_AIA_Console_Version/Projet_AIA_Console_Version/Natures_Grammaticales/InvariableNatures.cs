using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    static class InvariableNatures
    {

        // ========== Adverbe ==========

        // ----- Structure -----

        public struct Adverbe
        {
            public readonly string adverbe;
            public const string nature = "adverbe";
            public string type { get; private set; }

            public Adverbe(string adverbe)
            {
                this.adverbe = adverbe;
                this.type = typeOf(adverbe);
            }

            public override string ToString()
            {
                return this.adverbe;
            }
        }

        // ----- Methods -----

        // Renvoie le type
        private static string typeOf(string adverbe)
        {
            for (int i = 0; i < Phrase.lesData.Tables["Adverbes"].Rows.Count; i++)
            {
                if (adverbe == (string)Phrase.lesData.Tables["Adverbes"].Rows[i]["Adverbe"])
                    return (string)Phrase.lesData.Tables["Adverbes"].Rows[i]["Type"];
            }

            // Vu que la table des adverbes ne contient pas tous les adverbes, si l'adverbe n'a pas été
            // trouvé dans la table, on renvoie le type "manière" par défaut, car la plupart des adverbes
            // finissant par "-ement" sont de type manière.
            return "manière";
        }


        // ========== Conjonction de coordination ==========

        public struct ConjDeCoord
        {
            public readonly string conjDeCoord;
            public const string nature = "conjDeCoord";

            public ConjDeCoord(string conjDeCoord)
            {
                this.conjDeCoord = conjDeCoord;
            }

            public override string ToString()
            {
                return this.conjDeCoord;
            }
        }

        // ========== Conjonction de subordination ==========

        public struct ConjDeSub
        {
            public readonly string conjDeSub;
            public const string nature = "conjDeSub";

            public ConjDeSub(string conjDeSub)
            {
                this.conjDeSub = conjDeSub;
            }

            public override string ToString()
            {
                return this.conjDeSub;
            }
        }

        // ========== Préposition ==========

        public struct Preposition
        {
            public readonly string preposition;
            public const string nature = "preposition";

            public Preposition(string preposition)
            {
                this.preposition = preposition;
            }

            public override string ToString()
            {
                return this.preposition;
            }
        }
    }
}
