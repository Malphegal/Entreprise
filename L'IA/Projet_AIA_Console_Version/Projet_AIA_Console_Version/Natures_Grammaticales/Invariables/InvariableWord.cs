namespace Projet_AIA_Console_Version.Natures_Grammaticales.Invariables
{
    // ========== ADVERBE ==========
    public class Adverbe : Word
    {
            // CHAMP

        private readonly string _adverbe;

            // PROPRIETES

        public string Type  { get; private set; }

            // CONSTRUCTEUR

        public Adverbe(string adverbe)
        {
            this._adverbe = adverbe;
            this.Nature = "adverbe";
            this.Type = typeOf(adverbe);
        }

            // METHODES

        public override string ToString()
        {
            return this._adverbe;
        }

            // METHODES STATIC

        // Renvoie le type du l'adverbe.
        private static string typeOf(string adverbe)
        {
            for (int i = 0; i < RecupBDD.lesData.Tables["Adverbes"].Rows.Count; i++)
            {
                if (adverbe == (string)RecupBDD.lesData.Tables["Adverbes"].Rows[i]["Adverbe"])
                    return (string)RecupBDD.lesData.Tables["Adverbes"].Rows[i]["Type"];
            }

            // Vu que la table des adverbes ne contient pas tous les adverbes, si l'adverbe n'a pas été
            // trouvé dans la table, on renvoie le type "manière" par défaut, car la plupart des adverbes
            // finissant par "-ement" sont de type manière.
            return "manière";
        }
    }


    // ========== CONJONCTION DE COORDINATION ==========
    public class ConjDeCoord : Word
    {
            // CHAMP

        private readonly string _conjDeCoord;

            // CONSTRUCTEUR

        public ConjDeCoord(string conjDeCoord)
        {
            this._conjDeCoord = conjDeCoord;
            this.Nature = "conjDeCoord";
        }

            // METHODES

        public override string ToString()
        {
            return this._conjDeCoord;
        }
    }

    // ========== CONJONCTION DE SUBORDINATION ==========
    public class ConjDeSub : Word
    {
            // CHAMP

        private readonly string _conjDeSub;

            // CONSTRUCTEUR

        public ConjDeSub(string conjDeSub)
        {
            this._conjDeSub = conjDeSub;
            this.Nature = "conjDeSub";
        }

            // METHODES

        public override string ToString()
        {
            return this._conjDeSub;
        }
    }

    // ========== PREPOSITION ==========
    public class Preposition : Word
    {
            // CHAMP

        private readonly string _preposition;

            // CONSTRUCTEUR

        public Preposition(string preposition)
        {
            this._preposition = preposition;
            this.Nature = "préposition";
        }

            // METHODES

        public override string ToString()
        {
            return this._preposition;
        }
    }
}
