using System.Data;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class Determiner : VariableWord
    {
            // CHAMP

        static private string[] infoDeterminant = null;    // Tableau contenu les informations du déterminant en cours de traitement.
        private string _determinant;

            // PROPRIETES

        public string Type1         { get; private set; }
        public string Type2         { get; private set; }

            // CONSTRUCTEUR

        // Constructeur de base.
        public Determiner(string determiner)
        {
            infoDeterminant = null;

            if (determiner == "de")
                determiner = "du";

            this._determinant = determiner;
            this.Nature = "déterminant";

            if(estConnu(determiner))
            {
                this.Gender = infoDeterminant[0];
                this.Number = infoDeterminant[1];
                this.Type1 = infoDeterminant[2];
                this.Type2 = infoDeterminant[3];
            }
            else
            {
                this.Gender = null;
                this.Number = null;
                this.Type1 = null;
                this.Type2 = null;
            }
            this.GenderBase = this.Gender;
            this.NumberBase = this.Number;

            if (determiner == "l'")
                determiner = "le";
        }

        // Constructeur copiant le déterminant passé en paramètre.
        public Determiner(Determiner determiner)
        {
            infoDeterminant = null;
            this.Nature = "déterminant";
            this._determinant = determiner._determinant;
            this.Gender = determiner.Gender;
            this.GenderBase = determiner.GenderBase;
            this.Number = determiner.Number;
            this.NumberBase = determiner.NumberBase;
            this.Type1 = determiner.Type1;
            this.Type2 = determiner.Type2;
        }

            // METHODES

        // Transforme le déterminant en un masculin singulier.
        public void ToMaleSingular()
        {
            // On déclare une variable genderTempo afin de gérer les adjectifs de genre neutre.
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "F";
            else
                genderTempo = this.Gender;

            if (estConnu(this._determinant))
            {
                for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                {
                    if (this._determinant == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        this._determinant = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["MS"];
                        break;
                    }
                }
            }

            this.Gender = "M";
            this.Number = "S";
        }

        // Transforme le déterminant en un masculin pluriel.
        public void ToMalePlurial()
        {
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "F";
            else
                genderTempo = this.Gender;

            if (estConnu(this._determinant))
            {
                for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                {
                    if (this._determinant == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        this._determinant = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["MP"];
                        break;
                    }
                }
            }

            this.Gender = "M";
            this.Number = "P";
        }

        // Transforme le déterminant en un féminin singulier.
        public void ToFemaleSingular()
        {
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "M";
            else
                genderTempo = this.Gender;

            if (estConnu(this._determinant))
            {
                for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                {
                    if (this._determinant == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        this._determinant = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["FS"];
                        break;
                    }
                }
            }

            this.Gender = "F";
            this.Number = "S";
        }

        // Transforme le déterminant en un féminin pluriel.
        public void ToFemalePlurial()
        {
            string genderTempo;
            if (this.Gender == "N")
                genderTempo = "M";
            else
                genderTempo = this.Gender;

            if (estConnu(this._determinant))
            {
                for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                {
                    if (this._determinant == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.Number])
                    {
                        this._determinant = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["FP"];
                        break;
                    }
                }
            }

            this.Gender = "F";
            this.Number = "P";
        }

        // Transforme le déterminant en un singulier du même genre.
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

        // Transforme le déterminant en un pluriel du même genre.
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

        // Transforme le déterminant en un masculin du même nombre.
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

        // Transforme le déterminant en un féminin du même nombre.
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
            return this._determinant;
        }


            // METHODES STATIC

        // PUBLIC

        // Renvoie une string correspondant au masculin singulier du déterminant passé en paramètre.
        public static string MaleSingularOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToMaleSingular();
            return d._determinant;
        }

        // Renvoie une string correspondant au masculin pluriel du déterminant passé en paramètre.
        public static string MalePlurialOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToMalePlurial();
            return d._determinant;
        }

        // Renvoie une string correspondant au féminin singulier du déterminant passé en paramètre.
        public static string FemaleSingularOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToFemaleSingular();
            return d._determinant;
        }

        // Renvoie une string correspondant au féminin pluriel du déterminant passé en paramètre.
        public static string FemalePlurialOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToFemalePlurial();
            return d._determinant;
        }

        // Renvoie une string correspondant au singulier du déterminant passé en paramètre.
        public static string SingularOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToSingular();
            return d._determinant;
        }

        // Renvoie une string correspondant au pluriel du déterminant passé en paramètre.
        public static string PlurialOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToPlurial();
            return d._determinant;
        }

        // Renvoie une string correspondant au masculin du déterminant passé en paramètre.
        public static string MaleOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToMale();
            return d._determinant;
        }

        // Renvoie une string correspondant au féminin du déterminant passé en paramètre.
        public static string FemaleOf(Determiner determiner)
        {
            Determiner d = new Determiner(determiner);
            d.ToFemale();
            return d._determinant;
        }

        // PRIVATE

        // Renvoie true si le déterminant est connu dans la table, sinon false
        private static bool estConnu(string determiner)
        {
            // Si le champ static infoDeterminant est null, c'est qu'on ne sait pas encore si le déterminant est connu.
            if (infoDeterminant == null)
            {
                DataRow row = null;
                for (int i = 0; i < Phrase.lesData.Tables["Determinants"].Rows.Count; i++)
                {
                    if (determiner == (string)Phrase.lesData.Tables["Determinants"].Rows[i]["Determinant"])
                    {
                        row = Phrase.lesData.Tables["Determinants"].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé le déterminant dans la table, alors il existe et on peut remplir infoDeterminant avec ses informations.
                if (row != null)
                {
                    infoDeterminant = new string[] { (string)(row)["Genre"],
                                                     (string)(row)["Nombre"],
                                                     (string)(row)["Type1"],
                                                     (string)(row)["Type2"],};
                    return true;
                }
                // Sinon, le déterminant n'existe pas. On remplit la première case du tableau avec "-1" pour montrer que le déterminant a été cherché.
                else
                {
                    infoDeterminant = new string[] { "-1" };
                    return false;
                }
            }
            // Si le premier élément du tableau infoDeterminant vaut "-1", c'est que le déterminant n'est pas connu.
            else if (infoDeterminant[0] == "-1")
                return false;
            // Autrement, c'est que le tableau infoDeterminant est correctement rempli avec les informations de le déterminant : il est donc connu.
            else
                return true;
        }
    }
}
