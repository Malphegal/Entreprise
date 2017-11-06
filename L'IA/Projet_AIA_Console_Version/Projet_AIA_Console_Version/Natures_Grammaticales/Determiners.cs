using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class Determiners
    {
        static private string[] infoDeterminant = null;    // Tableau contenu les informations du déterminant en cours de traitement.

        // ------ STRUCTURES ------

        // Determiner
        public struct Determiner
        {
            public const string nature = "déterminant";
            public string determiner { get; private set; }
            public string gender { get; private set; }
            public readonly string genderBase;
            public string number { get; private set; }
            public readonly string numberBase;
            public string type1 { get; private set; }
            public string type2 { get; private set; }

            public Determiner(string determiner)
            {
                infoDeterminant = null;

                if (determiner == "de")
                    determiner = "du";

                this.determiner = determiner;

                if(estConnu(determiner))
                {
                    this.gender = infoDeterminant[0];
                    this.number = infoDeterminant[1];
                    this.type1 = infoDeterminant[2];
                    this.type2 = infoDeterminant[3];
                }
                else
                {
                    this.gender = null;
                    this.number = null;
                    this.type1 = null;
                    this.type2 = null;
                }
                this.genderBase = this.gender;
                this.numberBase = this.number;

                if (determiner == "l'")
                    determiner = "le";
            }

            // ------ INSTANCE METHODS ------

            public bool isSingular()
            {
                return this.number == "S";
            }

            public bool isPlurial()
            {
                return this.number == "P";
            }

            public bool isMale()
            {
                return this.gender == "M";
            }

            public bool isFemale()
            {
                return this.gender == "F";
            }

            public string toMaleSingular()
            {
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "F";
                else
                    genderTempo = this.gender;

                if (estConnu(this.determiner))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                    {
                        if (this.determiner == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.number])
                        {
                            this.determiner = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["MS"];
                            break;
                        }
                    }
                }

                this.gender = "M";
                this.number = "S";
                return this.determiner;
            }

            public string toMalePlurial()
            {
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "F";
                else
                    genderTempo = this.gender;

                if (estConnu(this.determiner))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                    {
                        if (this.determiner == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.number])
                        {
                            this.determiner = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["MP"];
                            break;
                        }
                    }
                }

                this.gender = "M";
                this.number = "P";
                return this.determiner;
            }

            public string toFemaleSingular()
            {
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "M";
                else
                    genderTempo = this.gender;

                if (estConnu(this.determiner))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                    {
                        if (this.determiner == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.number])
                        {
                            this.determiner = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["FS"];
                            break;
                        }
                    }
                }

                this.gender = "F";
                this.number = "S";
                return this.determiner;
            }

            public string toFemalePlurial()
            {
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "M";
                else
                    genderTempo = this.gender;

                if (estConnu(this.determiner))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["DeterminantsAccords"].Rows.Count; i++)
                    {
                        if (this.determiner == (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i][genderTempo + this.number])
                        {
                            this.determiner = (string)Phrase.lesData.Tables["DeterminantsAccords"].Rows[i]["FP"];
                            break;
                        }
                    }
                }

                this.gender = "F";
                this.number = "P";
                return this.determiner;
            }

            public string toSingular()
            {
                if (this.number == "P")
                {
                    if (this.gender == "M")
                        return this.toMaleSingular();
                    else
                        return this.toFemaleSingular();
                }
                else
                    return this.determiner;
            }

            public string toPlurial()
            {
                if (this.number == "S")
                {
                    if (this.gender == "M")
                        return this.toMalePlurial();
                    else
                        return this.toFemalePlurial();
                }
                else
                    return this.determiner;
            }

            public string toMale()
            {
                if (this.gender == "F")
                {
                    if (this.number == "S")
                        return this.toMaleSingular();
                    else
                        return this.toMalePlurial();
                }
                else
                    return this.determiner;
            }

            public string toFemale()
            {
                if (this.gender == "M")
                {
                    if (this.number == "S")
                        return this.toFemaleSingular();
                    else
                        return this.toFemalePlurial();
                }
                else
                    return this.determiner;
            }

            public override string ToString()
            {
                return this.determiner;
            }
        }

        // ------ CLASS METHODS ------

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
