using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    class Pronouns
    {
        static private string[] infoPronom = null;    // Tableau contenu les informations du pronom en cours de traitement.

        // ------ STRUCTURES ------

        // Pronoun
        public struct Pronoun
        {
            public const string nature = "pronom";
            public string pronoun { get; private set; }
            public string gender { get; private set; }
            public readonly string genderBase;
            public string number { get; private set; }
            public readonly string numberBase;
            public string type { get; private set; }

            public Pronoun(string pronoun)
            {
                infoPronom = null;
                this.pronoun = pronoun;

                if (estConnu(pronoun))
                {
                    this.gender = infoPronom[0];
                    this.number = infoPronom[1];
                    this.type = infoPronom[2];
                }
                else
                {
                    this.gender = null;
                    this.number = null;
                    this.type = null;
                }
                this.genderBase = this.gender;
                this.numberBase = this.number;

                if (pronoun == "l'")
                    pronoun = "le";
                else if (pronoun == "m'")
                    pronoun = "me";
                else if (pronoun == "t'")
                    pronoun = "te";
                else if (pronoun == "s'")
                    pronoun = "se";
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

                if (estConnu(this.pronoun))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                    {
                        if (this.pronoun == (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.number])
                        {
                            this.pronoun = (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i]["MS"];
                            break;
                        }
                    }
                }

                this.gender = "M";
                this.number = "S";
                return this.pronoun;
            }

            public string toMalePlurial()
            {
                string pronom = null;
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "F";
                else
                    genderTempo = this.gender;

                if (estConnu(this.pronoun))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                    {
                        if (this.pronoun == (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.number])
                        {
                            pronom = (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i]["MP"];
                            break;
                        }
                    }
                }

                if (pronom != null)
                {
                    this.pronoun = pronom;
                    this.gender = "M";
                    this.number = "P";
                }

                return this.pronoun;
            }

            public string toFemaleSingular()
            {
                string pronom = null;
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "M";
                else
                    genderTempo = this.gender;

                if (estConnu(this.pronoun))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                    {
                        if (this.pronoun == (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.number])
                        {
                            pronom = (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i]["FS"];
                            break;
                        }
                    }
                }

                if (pronom != null)
                {
                    this.pronoun = pronom;
                    this.gender = "F";
                    this.number = "S";
                }

                return this.pronoun;
            }

            public string toFemalePlurial()
            {
                string pronom = null;
                string genderTempo;
                if (this.gender == "N")
                    genderTempo = "M";
                else
                    genderTempo = this.gender;

                if (estConnu(this.pronoun))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["PronomsAccords"].Rows.Count; i++)
                    {
                        if (this.pronoun == (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i][genderTempo + this.number])
                        {
                            pronom = (string)Phrase.lesData.Tables["PronomsAccords"].Rows[i]["FP"];
                            break;
                        }
                    }
                }

                if (pronom != null)
                {
                    this.pronoun = pronom;
                    this.gender = "F";
                    this.number = "P";
                }

                return this.pronoun;
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
                    return this.pronoun;
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
                    return this.pronoun;
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
                    return this.pronoun;
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
                    return this.pronoun;
            }

            public override string ToString()
            {
                return this.pronoun;
            }
        }

        // ------ CLASS METHODS ------

        // Renvoie true si le pronom est connu dans la table, sinon false
        private static bool estConnu(string pronoun)
        {
            // Si le champ static infoPronom est null, c'est qu'on ne sait pas encore si le pronom est connu.
            if (infoPronom == null)
            {
                DataRow row = null;
                for (int i = 0; i < Phrase.lesData.Tables["Pronoms"].Rows.Count; i++)
                {
                    if (pronoun == (string)Phrase.lesData.Tables["Pronoms"].Rows[i]["Pronom"])
                    {
                        row = Phrase.lesData.Tables["Pronoms"].Rows[i];
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
