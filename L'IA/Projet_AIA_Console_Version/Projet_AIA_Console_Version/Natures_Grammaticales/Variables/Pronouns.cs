using System.Data;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class Pronoun : VariableWord
    {
            // CHAMP

        static private string[] infoPronom = null;    // Tableau contenu les informations du pronom en cours de traitement.
        private string _pronom;

            // PROPRIETES

        public string Type  { get; private set; }
        public string NatureDetaillee { get { return this.Nature + this.Type; } }

            // CONSTRUCTEUR

        // Constructeur de base.
        public Pronoun(string pronoun)
        {
            infoPronom = null;
            this._pronom = pronoun;
            this.Nature = "pronom";

            if (estConnu(pronoun))
            {
                this.Gender = infoPronom[0];
                this.Number = infoPronom[1];
                this.Type = infoPronom[2];
            }
            else
            {
                this.Gender = null;
                this.Number = null;
                this.Type = null;
            }
            this.GenderBase = this.Gender;
            this.NumberBase = this.Number;

            if (pronoun == "l'")
                pronoun = "le";
            else if (pronoun == "m'")
                pronoun = "me";
            else if (pronoun == "t'")
                pronoun = "te";
            else if (pronoun == "s'")
                pronoun = "se";
        }

        // Constructeur créant une copie du pronom passé en paramètre.
        public Pronoun(Pronoun pronom)
        {
            infoPronom = null;
            this._pronom = pronom._pronom;
            this.Nature = pronom.Nature;
            this.Number = pronom.Number;
            this.NumberBase = pronom.NumberBase;
            this.Gender = pronom.Gender;
            this.GenderBase = pronom.GenderBase;
            this.Type = pronom.Type;
        }

            // METHODES

        // Transforme le pronom en un masculin singulier.
        public void ToMaleSingular()
        {
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "F";
            else
                genderTempo = this.Gender;

            if (estConnu(this._pronom))
            {
                for (int i = 0; i < RecupBDD.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                {
                    if (this._pronom == (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        this._pronom = (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i]["MS"];
                        break;
                    }
                }
            }

            this.Gender = "M";
            this.Number = "S";
        }

        // Transforme le pronom en un masculin pluriel.
        public void ToMalePlurial()
        {
            string pronom = null;
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "F";
            else
                genderTempo = this.Gender;

            if (estConnu(this._pronom))
            {
                for (int i = 0; i < RecupBDD.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                {
                    if (this._pronom == (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        pronom = (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i]["MP"];
                        break;
                    }
                }
            }

            if (pronom != null)
            {
                this._pronom = pronom;
                this.Gender = "M";
                this.Number = "P";
            }
        }

        // Transforme le pronom en un féminin singulier.
        public void ToFemaleSingular()
        {
            string pronom = null;
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "M";
            else
                genderTempo = this.Gender;

            if (estConnu(this._pronom))
            {
                for (int i = 0; i < RecupBDD.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                {
                    if (this._pronom == (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        pronom = (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i]["FS"];
                        break;
                    }
                }
            }

            if (pronom != null)
            {
                this._pronom = pronom;
                this.Gender = "F";
                this.Number = "S";
            }
        }

        // Transforme le pronom en un féminin pluriel.
        public void ToFemalePlurial()
        {
            string pronom = null;
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "M";
            else
                genderTempo = this.Gender;

            if (estConnu(this._pronom))
            {
                for (int i = 0; i < RecupBDD.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                {
                    if (this._pronom == (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        pronom = (string)RecupBDD.lesData.Tables["PronomsAccords"].Rows[i]["FP"];
                        break;
                    }
                }
            }

            if (pronom != null)
            {
                this._pronom = pronom;
                this.Gender = "F";
                this.Number = "P";
            }
        }

        // Transforme le pronom en un singulier du même genre.
        public void ToSingular()
        {
            if (this.Number == "P")
            {
                if (this.Gender == "M")
                    this.ToMaleSingular();
                else
                    this.ToFemaleSingular();
            }
        }

        // Transforme le pronom en un pluriel du même genre.
        public void ToPlurial()
        {
            if (this.Number == "S")
            {
                if (this.Gender == "M")
                    this.ToMalePlurial();
                else
                    this.ToFemalePlurial();
            }
        }

        // Transforme le pronom en un masculin du même nombre.
        public void ToMale()
        {
            if (this.Gender == "F")
            {
                if (this.Number == "S")
                    this.ToMaleSingular();
                else
                    this.ToMalePlurial();
            }
        }

        // Transforme le pronom en un féminin du même nombre.
        public void ToFemale()
        {
            if (this.Gender == "M")
            {
                if (this.Number == "S")
                    this.ToFemaleSingular();
                else
                    this.ToFemalePlurial();
            }
        }

        public override string ToString()
        {
            return this._pronom;
        }


            // METHODES STATIC

        // PUBLIC

        // Renvoie une string correspondant au masculin singulier du pronom entré en paramètre.
        public static string MaleSingularOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToMaleSingular();
            return p._pronom;
        }

        // Renvoie une string correspondant au masculin pluriel du pronom entré en paramètre.
        public static string MalePlurialOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToMalePlurial();
            return p._pronom;
        }

        // Renvoie une string correspondant au féminin singulier du pronom entré en paramètre.
        public static string FemaleSingularOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToFemaleSingular();
            return p._pronom;
        }

        // Renvoie une string correspondant au féminin pluriel du pronom entré en paramètre.
        public static string FemalePlurialOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToFemalePlurial();
            return p._pronom;
        }

        // Renvoie une string correspondant au singulier du pronom entré en paramètre.
        public static string SingularOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToSingular();
            return p._pronom;
        }

        // Renvoie une string correspondant au pluriel du pronom entré en paramètre.
        public static string PlurialOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToPlurial();
            return p._pronom;
        }

        // Renvoie une string correspondant au masculin du pronom entré en paramètre.
        public static string MaleOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToMale();
            return p._pronom;
        }

        // Renvoie une string correspondant au féminin du pronom entré en paramètre.
        public static string FemaleOf(Pronoun pronom)
        {
            Pronoun p = new Pronoun(pronom);
            p.ToFemale();
            return p._pronom;
        }

        // PRIVATE

        // Renvoie true si le pronom est connu dans la table, sinon false
        private static bool estConnu(string pronoun)
        {
            // Si le champ static infoPronom est null, c'est qu'on ne sait pas encore si le pronom est connu.
            if (infoPronom == null)
            {
                DataRow row = null;
                for (int i = 0; i < RecupBDD.lesData.Tables["Pronoms"].Rows.Count; i++)
                {
                    if (pronoun == (string)RecupBDD.lesData.Tables["Pronoms"].Rows[i]["Pronom"])
                    {
                        row = RecupBDD.lesData.Tables["Pronoms"].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé le pronom dans la table, alors il existe et on peut remplir infoPronom avec ses informations.
                if (row != null)
                {
                    infoPronom = new string[] { (string)(row)["Genre"],
                                                     (string)(row)["Nombre"],
                                                     (string)(row)["Type"]};
                    return true;
                }
                // Sinon, le pronom n'existe pas. On remplit la première case du tableau avec "-1" pour montrer que le pronom a été cherché.
                else
                {
                    infoPronom = new string[] { "-1" };
                    return false;
                }
            }
            // Si le premier élément du tableau infoPronom vaut "-1", c'est que le pronom n'est pas connu.
            else if (infoPronom[0] == "-1")
                return false;
            // Autrement, c'est que le tableau infoPronom est correctement rempli avec les informations de le pronom : il est donc connu.
            else
                return true;
        }
    }
}
