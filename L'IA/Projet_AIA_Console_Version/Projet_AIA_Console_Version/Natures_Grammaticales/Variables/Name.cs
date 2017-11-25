using Projet_AIA_Console_Version.Natures_Grammaticales;
using System.Data;

namespace Projet_AIA_Console_Version
{
    class Name : VariableWord
    {
            // CHAMPS

        static private string[] infoNom = null;    // Tableau contenu les informations du nom en cours de traitement.
        private string _nom;

            // CONSTRUCTEUR

        // Constructeur appelé par la phrase lorsque celle-ci ne permet pas de déterminer le nombre et le genre
        // du nom.
        public Name(string name)
        {
            infoNom = null;
            this._nom = name;
            this.Nature = "nom";
                
            if (estConnu(name))
            {
                this.Number = infoNom[0];
                this.Gender = infoNom[1];
            }
            else
            {
                this.Number = numberOf(name);
                this.Gender = genderOf(name, Number);
            }
            this.NumberBase = Number;
            this.GenderBase = Gender;
        }

        // Constructeur appelé par la phrase lorsque celle-ci permet déjà de déterminer le nombre et le genre
        // du nom.
        public Name(string name, string number, string gender)
        {
            infoNom = null;
            this._nom = name;
            this.Gender = gender;
            this.Number = number;
            this.NumberBase = number;
            this.GenderBase = gender;
        }

        // Constructeur copiant le nom entré en paramètre.
        public Name(Name nom)
        {
            infoNom = null;
            this.Nature = nom.Nature;
            this._nom = nom._nom;
            this.Number = nom.Number;
            this.NumberBase = nom.NumberBase;
            this.Gender = nom.Gender;
            this.GenderBase = nom.GenderBase;
        }


            // METHODES

        // Transforme le nom en un pluriel.
        public void ToPlurial()
        {
            if (estConnu(this._nom))
            {
                for (int i = 0; i < Phrase.lesData.Tables["NomsAccords"].Rows.Count; i++)
                {
                    if (this._nom == (string)Phrase.lesData.Tables["NomsAccords"].Rows[i]["Singular"])
                    {
                        this._nom = (string)Phrase.lesData.Tables["NomsAccords"].Rows[i]["Plurial"];
                        break;
                    }
                }
            }
            else
                this._nom = singularToPlurial(this._nom);

            this.Number = "P";
        }

        // Transforme le nom en un singulier.
        public void ToSingular()
        {
            if (estConnu(this._nom))
            {
                for (int i = 0; i < Phrase.lesData.Tables["NomsAccords"].Rows.Count; i++)
                {
                    if (this._nom == (string)Phrase.lesData.Tables["NomsAccords"].Rows[i]["Plurial"])
                    {
                        this._nom = (string)Phrase.lesData.Tables["NomsAccords"].Rows[i]["Singular"];
                        break;
                    }
                }
            }
            else
                this._nom = plurialToSingular(this._nom);

            this.Number = "S";
        }

        public override string ToString()
        {
            return this._nom;
        }


            // METHODES STATIC

        // PUBLIC

        // Renvoie le singulier d'un nom entré en paramètre.
        public static string SingularOf(Name nom)
        {
            Name n = new Name(nom);
            n.ToSingular();
            return n._nom;
        }

        // Renvoie le pluriel d'un nom entré en paramètre.
        public static string PlurialOf(Name nom)
        {
            Name n = new Name(nom);
            n.ToPlurial();
            return n._nom;
        }

        // PRIVATE

        // Renvoie le genre du nom.
        private static string genderOf(string name, string number)
        {
            // Si le nom est connu, on récupère son genre dans la variable res
            if (estConnu(name))
                return infoNom[1];

            // Sinon, on entre dans le else
            else
            {
                // Si le nom est au pluriel, on le passe au singulier, afin de refaire la recherche dans la table,
                // dans le cas où l'IA ne connaissait pas le pluriel du nom mais connait son singulier.
                if (number == "P")
                {
                    infoNom = null;
                    // Si le nom termine par 's', on supprime ce 's'.
                    if (name[name.Length - 1] == 's')
                        name = name.Substring(0, name.Length - 1);
                    // Sinon, on appelle la fonction pour le transformer en singulier.
                    else
                        name = plurialToSingular(name);
                    // Si on le nom est connu, on renvoie son genre.
                    if (estConnu(name))
                        return infoNom[2];
                }

                // Si on arrive à ce stade du code, c'est que l'on a toujours pas trouvé le genre du nom.
                // On tranche de la manière suivante : si le nom termine par 'e', c'est un féminin.
                // Sinon, c'est un masculin.
                if (name[name.Length - 1] == 'e')
                    return "F";
                else
                    return "M";
            }
        }
        private static string genderOf(string name)
        {
            // Si le nom est connu, on récupère son genre dans la variable res
            if (estConnu(name))
                return infoNom[1];

            // Sinon, on entre dans le else
            else
            {
                // Si le nom termine par "s", on supprime ce "s", afin de refaire la recherche dans la table,
                // dans le cas où l'IA ne connaissait pas le pluriel du nom mais connait son singulier.
                if (name[name.Length - 1] == 's')
                {
                    infoNom = null;
                    name = name.Substring(0, name.Length - 1);
                    // Si on le nom est connu, on renvoie son genre.
                    if (estConnu(name))
                        return infoNom[2];
                }

                // Si on arrive à ce stade du code, c'est que l'on a toujours pas trouvé le genre du nom.
                // On tranche de la manière suivante : si le nom termine par 'e', c'est un féminin.
                // Sinon, c'est un masculin.
                if (name[name.Length - 1] == 'e')
                    return "F";
                else
                    return "M";
            }
        }

        // Renvoie le groupe d'exception du nom.
        private static string groupOf(string name, string number)
        {
            string res = "";

            if (number == "P")
            {
                name = plurialToSingular(name, ref res);
                if (res != "")
                    return res;
            }

            string ending = "";

            for (int i = name.Length; res == "" && i >= 1; i--)
            {
                ending = name.Substring(name.Length - i);
                for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                {
                    if ((string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"] == ending)
                        return (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Groupe"];
                }
            }

            // Si on n'a pas trouvé de groupe, on renvoie "1" par défaut.
            return "1";
        }
        private static string groupOf(string name)
        {
            return groupOf(name, numberOf(name));
        }

        // Renvoie le nombre du nom (S pour singulier, P pour pluriel)
        private static string numberOf(string name)
        {
            string ending = "";
            int singular = 0;  // Le nombre de fois que le programme trouve que le nom est un singulier.
            int plurial = 0;   // Le nombre de fois que le programme trouve que le nom est un pluriel.
            bool findSomethingS = false;
            bool findSomethingP = false;
            string res = "";

            // Si le nom est connu, on récupère son nombre dans infoNom.
            if (estConnu(name))
                res = infoNom[0];

            // Sinon, le nom n'est pas connu et on entre dans le else
            else
            {
                // On cherche dans les terminaisons singulières et plurielles.
                // Si la terminaison du nom correspond à une terminaison au singulier, on incrémente singular.
                // Si la terminaison du nom correspond à une terminaison au pluriel, on incrémente plurial.
                for (int i = name.Length; i >= 1; i--)
                {
                    ending = name.Substring(name.Length - i);
                    for (int j = 0; (!findSomethingS || !findSomethingP) && j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                    {
                        findSomethingS = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"] == ending;
                        findSomethingP = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Plurial"] == ending;
                    }

                    if (findSomethingS)
                        singular++;
                    if (findSomethingP)
                        plurial++;
                }

                // S'il y a une hésitation (le nom peut être soit singulier, soit pluriel), on tranche à partir de la terminaison.
                if (singular == plurial)
                {
                    if (name[name.Length - 1] == 's' || name.Substring(name.Length - 1) == "x")
                        plurial++;
                    else
                        singular++;
                }

                // Si le nom a plus de chance d'être singulier, res prend la valeur "S".
                // Sinon, le nom a plus de chance d'être pluriel, donc res prend la valeur "P".
                res = singular > plurial ? "S" : "P";
            }

            return res;
        }

        // Renvoie true si le nom est un singulier, sinon false.
        private static bool isSingular(string name)
        {
            return numberOf(name) == "S";
        }

        // Renvoie true si le nom est un pluriel, sinon false.
        private static bool isPlurial(string name)
        {
            return numberOf(name) == "P";
        }

        // Renvoie true si le nom est un masculin, sinon false.
        private static bool isMale(string name)
        {
            return genderOf(name) == "M";
        }

        // Renvoie true si le nom est un masculin, sinon false.
        private static bool isFemale(string name)
        {
            return genderOf(name) == "F";
        }

        // Renvoie le pluriel d'un nom singulier passé en paramètre.
        private static string singularToPlurial(string name, string group)
        {
            string singularEnding = "";
            string plurialEnding = "s";

            // On parcourt la table des exceptions pluriel.
            for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe du nom...
                if (group == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Groupe"])
                {
                    // On récupère la terminaison singulier et la terminaison pluriel correspondant à ce groupe.
                    singularEnding = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"];
                    plurialEnding = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Plurial"];
                    break;
                }
            }

            return name.Substring(0, name.Length - singularEnding.Length) + plurialEnding;
        }
        private static string singularToPlurial(string name, ref string group)
        {
            string resEnding = "s";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = name.Length; resEnding == "s" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending du nom vers la droite
                ending = name.Substring(name.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions.
                for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                {
                    // Si la terminaison ending du nom existe pour un singulier...
                    if (ending == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["NomsExceptions"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Plurial"];    // resEnding prend pour valeur la terminaison au pluriel de ce nom.
                    group = (string)row["Groupe"];  // group prend pour valeur le groupe d'exception de ce nom.
                    return name.Substring(0, name.Length - ending.Length + 1) + resEnding;
                }
            }

            return name.Substring(0, name.Length - ending.Length) + resEnding;
        }
        private static string singularToPlurial(string name)
        {
            string resEnding = "s";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = name.Length; resEnding == "s" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending du nom vers la droite
                ending = name.Substring(name.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions.
                for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                {
                    // Si la terminaison ending du nom existe pour un singulier...
                    if (ending == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["NomsExceptions"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Plurial"];    // resEnding prend pour valeur la terminaison au pluriel de ce nom.
                    return name.Substring(0, name.Length - ending.Length) + resEnding;
                }
            }

            return name.Substring(0, name.Length - ending.Length + 1) + resEnding;
        }

        // Rnvoie le singulier d'un nom pluriel passé en paramètre.
        private static string plurialToSingular(string name, string group)
        {
            string singularEnding = "";
            string plurialEnding = "s";

            // On parcourt la table des exceptions pluriel.
            for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
            {
                // Si on a trouvé dans la table le groupe du nom...
                if (group == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Groupe"])
                {
                    // On récupère la terminaison singulier et la terminaison pluriel correspondant à ce groupe.
                    singularEnding = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Singular"];
                    plurialEnding = (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Plurial"];
                    break;
                }
            }

            return name.Substring(0, name.Length - plurialEnding.Length) + singularEnding;
        }
        private static string plurialToSingular(string name, ref string group)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = name.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending du nom vers la droite
                ending = name.Substring(name.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions.
                for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                {
                    // Si la terminaison ending du nom existe pour un pluriel...
                    if (ending == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Plurial"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["NomsExceptions"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Singular"];    // resEnding prend pour valeur la terminaison au singulier de ce nom.
                    group = (string)row["Groupe"];  // group prend pour valeur le groupe d'exception de ce nom.
                    return name.Substring(0, name.Length - ending.Length) + resEnding;
                }
            }

            return name.Substring(0, name.Length - ending.Length) + resEnding;
        }
        private static string plurialToSingular(string name)
        {
            string resEnding = "";
            string ending = "";

            // On teste différentes terminaisons (de plus en plus petite) tant qu'on a rien trouvé.
            for (int i = name.Length; resEnding == "" && i >= 1; i--)
            {
                // On rétrécit de plus en plus la terminaison ending du nom vers la droite
                ending = name.Substring(name.Length - i);
                DataRow row = null;
                // On parcourt la table des exceptions.
                for (int j = 0; j < Phrase.lesData.Tables["NomsExceptions"].Rows.Count; j++)
                {
                    // Si la terminaison ending du nom existe pour un pluriel...
                    if (ending == (string)Phrase.lesData.Tables["NomsExceptions"].Rows[j]["Plurial"])
                    {
                        // On récupère la ligne correspondant à cette terminaison.
                        row = Phrase.lesData.Tables["NomsExceptions"].Rows[j];
                        break;
                    }
                }

                // Si on a trouvé une ligne...
                if (row != null)
                {
                    resEnding = (string)row["Singular"];    // resEnding prend pour valeur la terminaison au singulier de ce nom.
                    return name.Substring(0, name.Length - ending.Length) + resEnding;
                }
            }

            return name.Substring(0, name.Length - ending.Length) + resEnding;
        }

        // Renvoie true si le nom est connu par l'IA, sinon false.
        private static bool estConnu(string name)
        {
            // Si le champ static infoNom est null, c'est qu'on ne sait pas encore si le nom est connu.
            if (infoNom == null)
            {
                DataRow row = null;
                for (int i = 0; i < Phrase.lesData.Tables["Noms"].Rows.Count; i++)
                {
                    if (name == (string)Phrase.lesData.Tables["Noms"].Rows[i]["Nom"])
                    {
                        row = Phrase.lesData.Tables["Noms"].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé le nom dans la table, alors il existe et on peut remplir infoNom avec ses informations.
                if (row != null)
                {
                    infoNom = new string[] {  (string)(row)["Number"],
                                              (string)(row)["Gender"]};
                    return true;
                }
                // Sinon, le nom n'existe pas. On remplit la première case du tableau avec "-1" pour montrer que le nom a été cherché.
                else
                {
                    infoNom = new string[] { "-1" };
                    return false;
                }
            }
            // Si le premier élément du tableau infoNom vaut "-1", c'est que le nom n'est pas connu.
            else if (infoNom[0] == "-1")
                return false;
            // Autrement, c'est que le tableau infoNom est correctement rempli avec les informations du nom : il est donc connu.
            else
                return true;
        }
    }
}