using System.Data;

namespace Projet_AIA_Console_Version
{
    static class Adjectives
    {
        static private string[] infoAdjectif = null;    // Tableau contenant les informations de l'adjectif en cours de traitement.

        // ------ STRUCTURES ------

        // Adjective
        public struct Adjective
        {
            public const string nature = "adjectif";
            public string adjective { get; private set; }
            public string gender { get; private set; }
            public readonly string genderBase;
            public string number { get; private set; }
            public readonly string numberBase;

            // Constructeur appelé par la phrase lorsque celle-ci ne permet pas de déterminer le nombre et le genre
            // de l'adjectif.
            public Adjective(string adjective)
            {
                infoAdjectif = null;
                this.adjective = adjective;
                
                if (estConnu(adjective))
                {
                    this.gender = infoAdjectif[0];
                    this.number = infoAdjectif[1];
                }
                else
                {
                    this.gender = genderOf(adjective);
                    this.number = numberOf(adjective);
                }
                this.genderBase = this.gender;
                this.numberBase = this.number;
            }

            // Constructeur appelé par la phrase lorsque celle-ci permet déjà de déterminer le nombre et le genre
            // de l'adjectif.
            public Adjective(string adjective, string number, string gender)
            {
                infoAdjectif = null;
                this.adjective = adjective;
                this.gender = gender;
                this.genderBase = gender;
                this.number = number;
                this.numberBase = number;
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
                if (estConnu(this.adjective))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["AdjectifsAccords"].Rows.Count; i++)
                    {
                        if (this.adjective == (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i][this.gender + this.number])
                        {
                            this.adjective = (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i]["MS"];
                            break;
                        }
                    }
                }
                else
                {
                    if (this.gender == "M" && this.number == "P")
                        this.adjective = Adjectives.plurialToSingular(this.adjective);
                    else if (this.gender == "F" && this.number == "S")
                        this.adjective = Adjectives.femaleToMale(this.adjective);
                    else if (this.gender == "F" && this.number == "P")
                        this.adjective = Adjectives.femaleToMale(Adjectives.plurialToSingular(this.adjective));
                }

                this.gender = "M";
                this.number = "S";
                return this.adjective;
            }

            public string toMalePlurial()
            {
                if (estConnu(this.adjective))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["AdjectifsAccords"].Rows.Count; i++)
                    {
                        if (this.adjective == (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i][this.gender + this.number])
                        {
                            this.adjective = (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i]["MP"];
                            break;
                        }
                    }
                }
                else
                {
                    if (this.gender == "M" && this.number == "S")
                        this.adjective = Adjectives.singularToPlurial(this.adjective);
                    else if (this.gender == "F" && this.number == "P")
                        this.adjective = Adjectives.singularToPlurial(Adjectives.femaleToMale(Adjectives.plurialToSingular(this.adjective)));
                    else if (this.gender == "F" && this.number == "S")
                        this.adjective = Adjectives.singularToPlurial(Adjectives.femaleToMale(this.adjective));
                }

                this.gender = "M";
                this.number = "P";
                return this.adjective;
            }

            public string toFemaleSingular()
            {
                if (estConnu(this.adjective))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["AdjectifsAccords"].Rows.Count; i++)
                    {
                        if (this.adjective == (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i][this.gender + this.number])
                        {
                            this.adjective = (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i]["FS"];
                            break;
                        }
                    }
                }
                else
                {
                    if (this.gender == "F" && this.number == "P")
                        this.adjective = Adjectives.plurialToSingular(this.adjective);
                    else if (this.gender == "M" && this.number == "S")
                        this.adjective = Adjectives.maleToFemale(this.adjective);
                    else if (this.gender == "M" && this.number == "P")
                        this.adjective = Adjectives.maleToFemale(Adjectives.plurialToSingular(this.adjective));
                }

                this.gender = "F";
                this.number = "S";
                return this.adjective;
            }

            public string toFemalePlurial()
            {
                if (estConnu(this.adjective))
                {
                    for (int i = 0; i < Phrase.lesData.Tables["AdjectifsAccords"].Rows.Count; i++)
                    {
                        if (this.adjective == (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i][this.gender + this.number])
                        {
                            this.adjective = (string)Phrase.lesData.Tables["AdjectifsAccords"].Rows[i]["FP"];
                            break;
                        }
                    }
                }
                else
                {
                    if (this.gender == "F" && this.number == "S")
                        this.adjective = Adjectives.singularToPlurial(this.adjective);
                    else if (this.gender == "M" && this.number == "P")
                        this.adjective = Adjectives.singularToPlurial(Adjectives.maleToFemale(Adjectives.plurialToSingular(this.adjective)));
                    else if (this.gender == "M" && this.number == "S")
                        this.adjective = Adjectives.singularToPlurial(Adjectives.maleToFemale(this.adjective));
                }

                this.gender = "F";
                this.number = "P";
                return this.adjective;
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
                    return this.adjective;
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
                    return this.adjective;
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
                    return this.adjective;
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
                    return this.adjective;
            }

            public override string ToString()
            {
                return this.adjective;
            }
        }

        // ------ CLASS METHODS ------

        // Renvoie le genre de l'adjectif (M si masculin, F si féminin)
        private static string genderOf(string adjectiveArg)
        {
            string adjective = adjectiveArg;
            string ending = "";
            int male = 0;  // Le nombre de fois que le programme trouve que l'adjectif est un masculin
            int female = 0;   // Le nombre de fois que le programme trouve que l'adjectif est un féminin
            bool findSomethingM = false;
            bool findSomethingF = false;
            string res = null;
            int again = 0;

            // Si l'adjectif est connu, on récupère son genre dans la variable res
            if (estConnu(adjective))
                res = infoAdjectif[0];
            // Sinon, on entre dans le else
            else
            {
                // Si l'adjectif termine par "s" (sauf "frais), on supprime ce "s", afin de refaire la recherche dans la table,
                // dans le cas où l'IA ne connaissait pas le pluriel de l'adjectif mais connait son singulier.
                if (adjective != "frais" && adjective[adjective.Length - 1] == 's')
                    adjective = adjective.Substring(0, adjective.Length - 1);

                infoAdjectif = null;

                if (estConnu(adjective))
                    res = infoAdjectif[4];
                else
                {
                    do
                    {
                        again--;
                        // On recherche dans les terminaisons masculines et féminines.
                        // Si la terminaison de l'adjectif correspond à une terminaison au masculin, on incrémente male.
                        // Si la terminaison de l'adjectif correspond à une terminaison au féminin, on incrémente female.
                        for (int i = adjective.Length; i >= 1; i--)
                        {
                            ending = adjective.Substring(adjective.Length - i);
                            for (int j = 0; (!findSomethingM || !findSomethingF) && j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                            {
                                findSomethingM = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"] == ending;
                                findSomethingF = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"] == ending;
                            }
                            
                            if (findSomethingM)
                                male++;
                            if (findSomethingF)
                                female++;
                        }

                        // Si on a rien trouvé, c'est que l'adjectif est surement au pluriel.
                        // On essaye alors de le passer au singulier.
                        if (male == 0 && female == 0 && again == -1)
                        {
                            again = +2;
                            if (adjective.Length > 3 && adjective.Substring(adjective.Length - 4) == "eaux")
                                adjective = adjective.Substring(0, adjective.Length - 4) + "eau";
                            else if (adjective.Length > 2 && adjective.Substring(adjective.Length - 3) == "aux")
                                adjective = adjective.Substring(0, adjective.Length - 3) + "al";
                        }
                    } while (again == 1);
                    // S'il y a une hésitation (peut être masculin ou féminin en même temps), on tranche à partir de la terminaison
                    // de l'adjectif. S'il termine par 'e', c'est un féminin. Sinon, c'est un masculin.
                    if (male == female)
                    {
                        if (res == null)
                        {
                            if (adjective[adjective.Length - 1] == 'e')
                                female++;
                            else
                                male++;
                        }
                    }

                    // Si l'adjectif a plus de chance d'être masculin, res prend la valeur "M".
                    // Sinon, l'adjectif a plus de chance d'être féminin, donc res prend la valeur "F".
                    res = male > female ? "M" : "F";
                }
            }

            return res;
        }

        // Renvoie le nombre de l'adjectif (S pour singulier, P pour pluriel)
        private static string numberOf(string adjectiveArg)
        {
            string adjective = adjectiveArg;
            string ending = "";
            int singular = 0;  // Le nombre de fois que le programme trouve que l'adjectif est un singulier.
            int plurial = 0;   // Le nombre de fois que le programme trouve que l'adjectif est un pluriel.
            bool findSomethingS = false;
            bool findSomethingP = false;
            string res = "";

            // Si l'adjectif est connu, on récupère son nombre dans infoAdjectif.
            if (estConnu(adjective))
                res = infoAdjectif[1];

            // Sinon, l'adjectif n'est pas connu et on entre dans le else
            else
            {
                // On cherche dans les terminaisons singulières et plurielles.
                // Si la terminaison de l'adjectif correspond à une terminaison au sungilier, on incrémente singular.
                // Si la terminaison de l'adjectif correspond à une terminaison au pluriel, on incrémente plurial.
                for (int i = adjective.Length; i >= 1; i--)
                {
                    ending = adjective.Substring(adjective.Length - i);
                    for (int j = 0; (!findSomethingS || !findSomethingP) && j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                    {
                        findSomethingS = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"] == ending;
                        findSomethingP = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Plurial"] == ending;
                    }
                    
                    if (findSomethingS)
                        singular++;
                    if (findSomethingP)
                        plurial++;
                }

                // S'il y a une hésitation (l'adjectif peut être soit singulier, soit pluriel), on tranche à partir de la terminaison.
                if (singular == plurial)
                {
                    if (adjective[adjective.Length - 1] == 's' || adjective.Substring(adjective.Length - 1) == "x")
                        plurial++;
                    else
                        singular++;
                }

                // Si l'adjectif a plus de chance d'être singulier, res prend la valeur "S".
                // Sinon, l'adjectif a plus de chance d'être pluriel, donc res prend la valeur "P".
                res = singular > plurial ? "S" : "P";
            }

            return res;
        }

        // Renvoie le groupe d'exception S2P (singular to plurial) de l'adjectif.
        private static string groupS2POf(string adjective, string gender, string number)
        {
            string res = "";

                if (number == "P")
                {
                    adjective = plurialToSingular(adjective, ref res);
                    if (res != "")
                        return res;
                }

                string ending = "";

                for (int i = adjective.Length; res == "" && i >= 1; i--)
                {
                    ending = adjective.Substring(adjective.Length - i);
                    for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                    {
                        if ((string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"] == ending)
                            return (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["GroupS2P"];
                    }
                }

            // Si on n'a pas trouvé de groupe, on renvoie "10" par défaut.
            return "10";
        }
        private static string groupS2POf(string adjective)
        {
            string gender = genderOf(adjective);
            string number = numberOf(adjective);
            return groupS2POf(adjective, gender, number);
        }

        // Renvoie le groupe d'exception M2F (male to female) de l'adjectif.
        private static string groupM2FOf(string adjective, string gender, string number)
        {
            string res = "";
            // On transforme l'adjectif en masculin singulier s'il ne l'est pas.
            if (number == "P")
                adjective = plurialToSingular(adjective);
            if (gender == "F")
            {
                adjective = femaleToMale(adjective, ref res);
                return res;
            }

            string ending = "";
                
            for (int i = adjective.Length; res == "" && i >= 1; i--)
            {
                ending = adjective.Substring(adjective.Length - i);
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                {
                    if ((string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"] == ending)
                        return (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["GroupM2F"];
                }
            }

            // Si on n'a pas trouvé de groupe, on renvoie "10" par défaut.
            return "10";
        }
        public static string groupM2FOf(string adjective)
        {
            return groupM2FOf(adjective, genderOf(adjective), numberOf(adjective));
        }

        // Renvoie true si l'adjectif est un pluriel, sinon false.
        private static bool isPlurial(string adjective)
        {
            return numberOf(adjective) == "P";
        }

        // Renvoie true si l'adjectif est un singulier, sinon false.
        private static bool isSingular(string adjective)
        {
            return numberOf(adjective) == "S";
        }

        // Renvoie true si l'adjectif est au masculin, sinon false.
        private static bool isMale(string adjective)
        {
            return genderOf(adjective) == "M";
        }

        // Renvoie true si l'adjectif est un féminin, sinon false.
        private static bool isFemale(string adjective)
        {
            return genderOf(adjective) == "F";
        }

        // Renvoie le pluriel de l'adjectif entré en paramètre.
        private static string singularToPlurial(string adjective, string groupS2P)
        {
            string singularEnding = "";
            string plurialEnding = "s";

            // On parcourt la table des exceptions S2P.
            for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe S2P de l'adjectif...
                if (groupS2P == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["GroupS2P"])
                {
                    // On récupère la terminaison singulier et la terminaison pluriel correspondant à ce groupe S2P.
                    singularEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"];
                    plurialEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Plurial"];
                    break;
                }
            }

            return adjective.Substring(0, adjective.Length - singularEnding.Length) + plurialEnding;
        }
        private static string singularToPlurial(string adjective, ref string resGroupS2P)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions S2P.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un singulier...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Plurial"];    // resEnding prend pour valeur la terminaison au pluriel de cet adjectif.
                    resGroupS2P = (string)row["GroupS2P"];  // resGroupS2P prend pour valeur le groupe d'exception S2P de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
        }
        private static string singularToPlurial(string adjective)
        {
            string resEnding = "s";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "s" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions S2P.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un singulier...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Plurial"];    // resEnding prend pour valeur la terminaison au pluriel de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective + "s";
        }

        // Renvoie le pluriel d'un adjectif entré en paramètre.
        private static string plurialToSingular(string adjective, string groupS2P)
        {
            string singularEnding = "";
            string plurialEnding = "s";

            // On parcourt la table des exceptions S2P.
            for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe S2P de l'adjectif...
                if (groupS2P == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["GroupS2P"])
                {
                    // On récupère la terminaison singulier et la terminaison pluriel correspondant à ce groupe S2P.
                    singularEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Singular"];
                    plurialEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Plurial"];
                    break;
                }
            }

            return adjective.Substring(0, adjective.Length - plurialEnding.Length) + singularEnding;
        }
        private static string plurialToSingular(string adjective, ref string resGroupS2P)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i>=1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions S2P.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un pluriel...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Plurial"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Singular"];    // resEnding prend pour valeur la terminaison au singulier de cet adjectif.
                    resGroupS2P = (string)row["GroupS2P"];  // resGroupS2P prend pour valeur le groupe d'exception S2P de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
        }
        private static string plurialToSingular(string adjective)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions S2P.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un pluriel...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j]["Plurial"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsS2P"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Singular"];    // resEnding prend pour valeur la terminaison au singulier de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length);
        }

        // Renvoie le féminin d'un adjectif entré en paramètre.
        private static string maleToFemale(string adjective, string groupM2F)
        {
            string maleEnding = "";
            string femaleEnding = "e";

            // On parcourt la table des exceptions M2F.
            for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe M2F de l'adjectif...
                if (groupM2F == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["GroupM2F"])
                {
                    // On récupère la terminaison masculin et la terminaison féminin correspondant à ce groupe M2F.
                    maleEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"];
                    femaleEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"];
                    break;
                }
            }

            return adjective.Substring(0, adjective.Length - maleEnding.Length) + femaleEnding;
        }
        private static string maleToFemale(string adjective, ref string resGroupM2F)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions M2F.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un masculin...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Female"];    // resEnding prend pour valeur la terminaison au féminin de cet adjectif.
                    resGroupM2F = (string)row["GroupM2F"];  // resGroupM2F prend pour valeur le groupe d'exception M2F de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
        }
        private static string maleToFemale(string adjective)
        {
            string resEnding = "e";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "e" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions M2F.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un masculin...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Female"];    // resEnding prend pour valeur la terminaison au féminin de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective + resEnding;
        }

        // Renvoie le masculin d'un adjectif entré en paramètre.
        private static string femaleToMale(string adjective, string groupM2F)
        {
            string maleEnding = "";
            string femaleEnding = "e";

            // On parcourt la table des exceptions M2F.
            for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe M2F de l'adjectif...
                if (groupM2F == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["GroupM2F"])
                {
                    // On récupère la terminaison masculin et la terminaison féminin correspondant à ce groupe M2F.
                    maleEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Male"];
                    femaleEnding = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"];
                    break;
                }
            }
            return adjective.Substring(0, adjective.Length - femaleEnding.Length) + maleEnding;
        }
        private static string femaleToMale(string adjective, ref string resGroupM2F)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions M2F.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                {
                    // Si la terminaison ending de l'adjectif existe pour un féminin...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Male"];    // resEnding prend pour valeur la terminaison au masculin de cet adjectif.
                    resGroupM2F = (string)row["GroupM2F"];  // resGroupM2F prend pour valeur le groupe d'exception M2F de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
        }
        private static string femaleToMale(string adjective)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = adjective.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending de l'adjectif vers la droite
                ending = adjective.Substring(adjective.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions M2F.
                for (int j = 0; j < Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows.Count; j++)
                {
                    string s = (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"];
                    // Si la terminaison ending de l'adjectif existe pour un féminin...
                    if (ending == (string)Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j]["Female"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["AdjectifsExceptionsM2F"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Male"];    // resEnding prend pour valeur la terminaison au masculin de cet adjectif.
                    return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
                }
            }

            return adjective.Substring(0, adjective.Length - ending.Length) + resEnding;
        }

        // Renvoie true si l'adjectif est connu dans la table, sinon false
        private static bool estConnu(string adjective)
        {
            // Si le champ static infoAdjectif est null, c'est qu'on ne sait pas encore si l'adjectif est connu.
            if (infoAdjectif == null)
            {
                DataRow row = null;
                for (int i = 0; i < Phrase.lesData.Tables["Adjectifs"].Rows.Count; i++)
                {
                    if (adjective == (string)Phrase.lesData.Tables["Adjectifs"].Rows[i]["Adjective"])
                    {
                        row = Phrase.lesData.Tables["Adjectifs"].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé l'adjectif dans la table, alors il existe et on peut remplir infoAdjectif avec ses informations.
                if (row != null)
                {
                    infoAdjectif = new string[] { (string)(row)["Genre"],
                                                  (string)(row)["Nombre"]};
                    return true;
                }
                // Sinon, l'adjectif n'existe pas. On remplit la première case du tableau avec "-1" pour montrer que l'adjectif a été cherché.
                else
                {
                    infoAdjectif = new string[] { "-1" };
                    return false;
                }
            }
            // Si le premier élément du tableau infoAdjectif vaut "-1", c'est que l'adjectif n'est pas connu.
            else if (infoAdjectif[0] == "-1")
                return false;
            // Autrement, c'est que le tableau infoAdjectif est correctement rempli avec les informations de l'adjectif : il est donc connu.
            else
                return true;
        }
    }
}
