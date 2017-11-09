using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version
{
    public static class Verbs
    {
        static private string[] infoVerbe = null;    // Tableau contenant les informations du verbe en cours de traitement.

        // ------ STRUCTURES ------

        // Verbe infinitif
        public struct InfinitiveVerb
        {
            public readonly string nature;
            public string verb { get; private set; }    // L'infinitif du verbe et l'action effectuée
            public string group { get; private set; }   // Le groupe du verbe

            // Constructeur
            public InfinitiveVerb(string verb)
            {
                this.verb = verb;
                this.nature = "verbe infinitif";
                if (estConnu(verb, "VerbesInfinitifs"))
                    this.group = infoVerbe[0];
                else
                    this.group = groupOf(verb, nature);
            }
            public InfinitiveVerb(string verb, string group)
            {
                this.verb = verb;
                this.nature = "verbe infinitif";
                this.group = group;
            }

            // ------ INSTANCE METHODS ------

            // Conjugue le verbe à la personne et au temps donnés en paramètre.
            public ConjugatedVerb Conjugate(string person, string time)
            {
                return Verbs.Conjugate(this, person, time);
            }

            public override string ToString()
            {
                return this.verb;
            }
        }

        // Verbe conjugué
        public struct ConjugatedVerb
        {
            public readonly string nature;
            public string verb { get; private set; }    // Le verbe conjugué
            public readonly string action;              // L'infinitif du verbe et l'action effectuée
            public string group { get; private set; }   // Le groupe du verbe
            public string person { get; private set; }  // La personne à laquelle est conjugué le verbe
            public string time { get; private set; }    // Le temps auquel est conjugué le verbe

            // Constructeur
            // On appelle ce constructeur si on a aucun moyen dans la phrase de connaître la personne
            // du verbe.
            public ConjugatedVerb(string verb)
            {
                this.verb = verb;
                this.nature = "verbe conjugué";
                if (estConnu(verb, "VerbesConjugues"))
                {
                    this.group = infoVerbe[0];
                    this.time = infoVerbe[1];
                    this.person = infoVerbe[2];
                    this.action = infoVerbe[3];
                }
                else
                {
                    this.group = groupOf(verb, nature);
                    this.time = timeOf(verb);
                    this.person = personOf(verb);
                    this.action = infinitiveOf(verb);
                }

            }
            // On appelle ce constructeur si on peut connaître dans la phrase la personne du verbe, grâce à un
            // nom ou un pronom personnel le précédant.
            public ConjugatedVerb(string verb, string person)
            {
                this.verb = verb;
                this.nature = "verbe conjugué";
                this.person = person;
                if (estConnu(verb, "VerbesConjugues"))
                {
                    this.group = infoVerbe[0];
                    this.time = infoVerbe[1];
                    this.action = infoVerbe[3];
                }
                else
                {
                    this.group = groupOf(verb, nature);
                    this.time = timeOf(verb);
                    this.action = infinitiveOf(verb);
                }
            }
            // On appelle ce constructeur dans la méthode qui conjugue des verbes à l'infinitif.
            // En effet, on possède déjà toutes les informations sur le verbe conjugué.
            public ConjugatedVerb(string verb, string person, string group, string time, string action)
            {
                this.verb = verb;
                this.nature = "verbe conjugué";
                this.person = person;
                this.group = group;
                this.time = time;
                this.action = action;
            }


            // ------ INSTANCE METHODS ------
            public bool isSingular()
            {
                return new string[] { "1", "2", "3" }.Contains(this.person);
            }

            public bool isPlurial()
            {
                return new string[] { "4", "5", "6" }.Contains(this.person);
            }

            // Conjugue le verbe à la personne et au temps donnés en paramètre.
            public ConjugatedVerb Conjugate(string person, string time)
            {
                return Verbs.Conjugate(this, person, time);
            }

            public override string ToString()
            {
                return this.verb;
            }
        }


        // ------ CLASS METHODS ------

        // Renvoie le groupe du verbe.
        private static string groupOf(string verb, string nature)
        {
            // Cas où le verbe est un infinitif :
            if (nature == "verbe infinitif")
            {
                // Si le verbe est "aller", on renvoie le groupe "3".
                if (verb == "aller")
                    return "3";
                // Si le verbe est haïr, on renvoie le groupe "2".
                else if (verb == "haïr")
                    return "2";
                // Si le verbe termine par "er", on renvoie le groupe "1".
                else if (verb.Substring(verb.Length - 2) == "er")
                    return "1";
                // Dans les cas qu'il reste, si le verbe ne termine pas par "ir", on renvoie le groupe "3".
                else if (verb.Substring(verb.Length - 2) != "ir")
                    return "3";
                // Les cas restant sont les verbes terminant par "ir".
                // Si la forme conjuguée en -issons existe pour ce verbe, on renvoie le groupe "2".
                infoVerbe = null;
                if (estConnu(verb.Substring(0, verb.Length - 2) + "issons", "VerbesConjugues")/* && infoVerbe[2] == "4" && infoVerbe[1] == "présent indicatif"*/)
                    return "2";
                // Autrement, on renvoie le groupe "3".
                else
                    return "3";
            }
            // Cas où le verbe est conjugué :
            else
            {
                // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
                // à quel groupe appartient cette terminaison.
                for (int i = 0; i < verb.Length; i++)
                {
                    for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                    {
                        // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie le groupe correspondant.
                        if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"])
                            return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["verbGroup"];
                    }
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie le groupe "3" par défaut.
            return "3";
        }

        // Renvoie le radical du verbe.
        private static string stemOf(string verb, string nature)
        {
            // Cas où le verbe est un infinitif :
            if (nature == "verbe infinitif")
            {
                // Si le verbe se termine par "oir", on renvoie le reste du verbe sans cette terminaison.
                if (verb.Substring(verb.Length - 3) == "oir")
                    return verb.Substring(0, verb.Length - 3);
                // Si le verbe se termine par "er", "ir", ou "re", on renvoie le reste du verbe sans cette terminaison.
                else if (new string[] { "er", "ir", "re", "ïr" }.Contains(verb.Substring(verb.Length - 2)))
                    return verb.Substring(0, verb.Length - 2);
            }
            // Cas où le verbe est conjugué :
            else
            {
                // On rétrécit le verbe vers la droite jusqu'à trouver une terminaison qui existe dans la
                // table des conjugaisons.
                for (int i = 0; i < verb.Length; i++)
                {
                    for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                    {
                        // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie le reste du verbe
                        // sans cette terminaison.
                        if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"])
                            return verb.Substring(0, i);
                    }
                }
            }

            // Si on a rien trouvé et qu'on ne sait pas, on renvoie le verbe sans les 2 derniers caractères.
            return verb.Substring(0, verb.Length -2);
        }

        // Renvoie la terminaison du verbe.
        private static string endingOf(string verb, string nature)
        {
            // Cas où le verbe est un infinitif :
            if (nature == "verbe infinitif")
            {
                // Si le verbe se termine par "oir", on renvoie cette terminaison.
                if (verb.Substring(verb.Length - 3) == "oir")
                    return "oir";
                // Si le verbe se termine par "er", "ir", ou "re", on renvoie cette terminaison.
                else if (new string[] { "er", "ir", "re" }.Contains(verb.Substring(verb.Length - 2)))
                    return verb.Substring(verb.Length - 2);
            }
            // Cas où le verbe est conjugué :
            else
            {
                // On rétrécit le verbe vers la droite jusqu'à trouver une terminaison qui existe dans la
                // table des conjugaisons.
                for (int i = 0; i < verb.Length; i++)
                {
                    for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                    {
                        // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie cette terminaison.
                        if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"])
                            return verb.Substring(i);
                    }
                }
            }

            // Si on a rien trouvé et qu'on ne sait pas, on renvoie les 2 derniers caractères du verbe.
            return verb.Substring(verb.Length - 2);
        }

        // Renvoie le temps auquel est conjugué le verbe.
        private static string timeOf(string verb)
        {
            // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
            // à quel temps appartient cette terminaison.
            for (int i = 0; i < verb.Length; i++)
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie le temps correspondant.
                    if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"])
                        return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["time"];
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie "présent indicatif" par défaut.
            return "présent indicatif";
        }

        // Renvoie la personne à laquelle est conjuguée le verbe.
        private static string personOf(string verb)
        {
            // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
            // pour quelle personne elle existe..
            for (int i = 0; i < verb.Length; i++)
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie la personne correspondante.
                    if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"])
                        return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["person"];
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie la personne "3" par défaut.
            return "3";
        }

        // Renvoie l'infinitif (l'action) d'un verbe conjugué.
        private static string infinitiveOf(string verb)
        {
            return infinitiveOf(stemOf(verb, "verbe conjugué"), groupOf(verb, "verbe conjugué"));
        }
        // Renvoie l'infinitif (l'action) d'un verbe conjugué connaissant son radical et son groupe.
        private static string infinitiveOf(string stem, string group)
        {
            switch(group)
            {
                case "1":
                    if (stem.Contains("appell") || stem.Contains("jett"))
                        stem = stem.Substring(0, stem.Length - 1);
                    return stem + "er";
                case "2":
                    if (stem + "ir" == "hair")
                        return "haïr";
                    else
                        return stem + "ir";
                // Pour le troisième groupe, c'est très arbitraire car il n'y a pas vraiment de règle.
                // Le programme se trompera relativement souvent.
                case "3":
                    if (new char[] { 't', 'd', 'p' }.Contains(stem[stem.Length - 1]))
                        return stem + "re";
                    else if (stem[stem.Length] == 'l')
                        if (stem + "er" == "aller")
                            return "aller";
                        else
                            return stem + "oir";
                    else
                        return stem + "ir";
                    // S'il y a une erreur au niveau du groupe entré, on renvoie null.
                default:
                    return null;
            }
        }

        // Transforme un verbe conjugué en verbe infinitif à partir de l'infinitif contenu dans le verbe conjugué et du groupe.
        private static InfinitiveVerb ToInfinitive(ConjugatedVerb verbe)
        {
            return new InfinitiveVerb(verbe.action, verbe.group);
        }

        // Conjugue le verbe infinitif à la personne et au temps entrés en paramètre.
        private static ConjugatedVerb Conjugate(InfinitiveVerb verbe, string person, string time)
        {
            string nomTable = verbe.nature == "verbe conjugué" ? "VerbesConjugues":"VerbesInfinitifs";
            // Si le verbe est connu, on récupère directement sa conjugaison dans la base de données.
            if (estConnu(verbe.verb, nomTable))
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["VerbesConjugues"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la ligne pour l'infinitif où le groupe est le même pour le temps et la personne que l'on souhaite,
                    // en renvoie le verbe conjugué de cette même ligne.
                    if ((string)Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow]["Infinitif"] == verbe.verb
                            && (string)Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow]["Groupe"] == verbe.group
                            && (string)Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow]["Temps"] == time
                            && (string)Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow]["Personne"] == person)
                        return new ConjugatedVerb((string)Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow]["Verbe"], person, verbe.group, time, verbe.verb);
                }
            }
            // Si on arrive à ce stade du programme, c'est que le verbe n'est pas connu ou que la forme voulue (temps / personne)
            // n'est pas connue. Il faut donc construire la conjugaison manuellement.
            
            // On commence par récupérer le radical du verbe.
            string stem = stemOf(verbe.verb, verbe.nature);
            string ending = "";

            for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
            {
                // Si on a trouvé la ligne où le groupe est le même pour le temps et la personne que l'on souhaite,
                // on récupère la terminaison correspondante à cette ligne.
                if ((string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["verbGroup"] == verbe.group
                        && (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["time"] == time
                        && (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["person"] == person)
                    ending = (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["ending"];
            }

            // On traite les exceptions :

            // Premier groupe :
            if (verbe.group == "1")
            {
                // -eler, -eter → -elle, -ette si "appeler" ou "jeter"
                if (ending[0] == 'e' && ending != "ez" && (verbe.verb.Contains("appeler") || verbe.verb.Contains("jeter")))
                    stem += stem[stem.Length - 1];
                // -e*er → è
                else if (stem[stem.Length - 2] == 'e' && ending[0] == 'e' && ending != "ez")
                    stem = stem.Substring(0, stem.Length - 2) + "è" + stem[stem.Length - 1];
                // -é*er → è
                else if (stem[stem.Length - 2] == 'é' && new string[] { "e", "es", "ent" }.Contains(ending))
                    stem = stem.Substring(0, stem.Length - 2) + "è" + stem[stem.Length - 1];
                // -*yer → i
                else if (stem[stem.Length - 1] == 'y' && ending[0] == 'e' && ending != "ez")
                    stem = stem.Substring(0, stem.Length - 1) + "i";
            }

            // Deuxième groupe :
            else if (verbe.group == "2")
            {
                // Les verbes du deuxième groupe sont régulier, sauf le verbe haïr.
                if (verbe.verb == "ha" && !(new string[] { "1", "2", "3" }.Contains(person)
                        && new string[] { "présent indicatif", "présent impératif" }.Contains(time)))
                    ending = "ï" + ending.Substring(1);
            }

            // Général :
            // c → ç
            if (stem[stem.Length - 1] == 'c' && new char[] { 'a', 'o', 'â' }.Contains(ending[0]))
                stem = stem.Substring(0, stem.Length - 2) + "ç";
            // g → ge
            else if (stem[stem.Length - 1] == 'g' && new char[] { 'a', 'o', 'â' }.Contains(ending[0]))
                stem += "e";

            return new ConjugatedVerb(stem + ending, person, verbe.group, time, verbe.verb);
        }
        // Conjugue le verbe conjugué à la personne et au temps entrés en paramètre.
        private static ConjugatedVerb Conjugate(ConjugatedVerb verbe, string person, string time)
        {
            return Conjugate(ToInfinitive(verbe), person, time);
        }

        // Renvoie true si le verbe est connu dans la table, sinon false
        // L'argument typeVerbe vaut VerbesInfinitifs si le verbe est un infinitif,
        // et vaut VerbesConjugues si le verbe est conjugué.
        private static bool estConnu(string verb, string typeVerbe)
        {
            // Si le champ static infoVerbe est null, c'est qu'on ne sait pas encore si le verbe est connu.
            if (infoVerbe == null)
            {
                DataRow row = null;
                for (int i = 0; i < Phrase.lesData.Tables[typeVerbe].Rows.Count; i++)
                {
                    if (verb == (string)Phrase.lesData.Tables[typeVerbe].Rows[i]["Verbe"])
                    {
                        row = Phrase.lesData.Tables[typeVerbe].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé le verbe dans la table, alors il existe et on peut remplir infoVerbe avec ses informations.
                if (row != null)
                {
                    // On a pas les mêmes infos sur le verbe qu'il soit conjugué ou non.
                    if (typeVerbe == "VerbesConjugues")
                        infoVerbe = new string[] { (string)(row)["Groupe"],
                                                    (string)(row)["Temps"],
                                                    (string)(row)["Personne"],
                                                    (string)(row)["Infinitif"]};
                    else
                        infoVerbe = new string[] { (string)(row)["Groupe"] };
                    return true;
                }
                // Sinon, le verbe n'existe pas. On remplit la première case du tableau avec "-1" pour montrer que le verbe a été cherché.
                else
                {
                    infoVerbe = new string[] { "-1" };
                    return false;
                }
            }
            // Si le premier élément du tableau infoVerbe vaut "-1", c'est que le verbe n'est pas connu.
            else if (infoVerbe[0] == "-1")
                return false;
            // Autrement, c'est que le tableau infoVerbe est correctement rempli avec les informations du verbe : il est donc connu.
            else
                return true;
        }
    }
}
