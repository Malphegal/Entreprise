using Projet_AIA_Console_Version.Natures_Grammaticales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version
{
    public abstract class Verb : VariableWord
    {
            // CHAMPS

        static protected string[] infoVerbe = null;    // Tableau contenant les informations du verbe en cours de traitement.
        protected string _verbe;

            // PROPRIETES

        public string Action        { get; protected set; } // L'infinitif du verbe et l'action effectuée
        public string Group         { get; protected set; } // Le groupe du verbe
        public byte AuxAvoir        { get; protected set; }
        public byte AuxEtre         { get; protected set; }
        public byte NonPronominale  { get; protected set; }
        public byte Pronominale     { get; protected set; }
        public string Transitif     { get; protected set; }
        public byte Intransitif     { get; protected set; }


            // CONSTRUCTEUR

        public Verb()
        { }


            // METHODES

        // Renvoie true si peut utiliser l'auxiliaire avoir.
        public bool CanUseAvoir()
        {
            return this.AuxAvoir == 1;
        }

        // Renvoie true si peut utiliser l'auxiliaire être.
        public bool CanUseEtre()
        {
            return this.AuxEtre == 1;
        }

        // Renvoie true si peut être de forme non pronominale.
        public bool isNonPronominale()
        {
            return this.NonPronominale == 1;
        }

        // Renvoie true si peut être de forme pronominale.
        public bool isPronominale()
        {
            return this.Pronominale == 1;
        }

        // Renvoie true si peut être transitif.
        public bool isTransitif()
        {
            return this.Transitif != "null";
        }

        // Renvoie true si peut être intransitif.
        public bool isIntransitif()
        {
            return this.Intransitif == 1;
        }

        // Renvoie true si peut être transitif direct.
        public bool isTransitifDirect()
        {
            return this.Transitif == "direct" || this.Transitif == "both";
        }

        // Renvoie true si peut être transitif indirect.
        public bool isTransitifIndirect()
        {
            return this.Transitif == "indirect" || this.Transitif == "both";
        }

        // Renvoie le participe présent du verbe.
        public abstract string GetParticipePresent();

        // Renvoie le participe passé du verbe.
        public abstract string GetParticipePasse();

        // Renvoie l'auxiliaire utilisé dans la conjugaison du verbe aux temps composés (avoir ou être).
        public string GetAuxiliaire()
        {
            if (this.AuxAvoir == 1)
                return "avoir";
            else if (this.AuxEtre == 1)
                return "être";
            else
                return "";
        }

        public override string ToString()
        {
            return this._verbe;
        }


            // METHODES DE CLASSE

        // Conjugue le verbe conjugué à la personne et au temps entrés en paramètre.
        public static string ConjugationOf(Verb verbe, string person, string time, string mode)
        {
            // On déclare un objet InfinitiveVerb sur lequel on effectuera les modifications afin de ne pas modifier le verbe entré en paramètre.
            InfinitiveVerb verbeInfinitif;
            // Cas où le verbe entré en paramètre est un verbe conjugué.
            if (verbe.Nature == "verbe conjugué")
                verbeInfinitif = new InfinitiveVerb(verbe.Action, verbe.Group, verbe.AuxAvoir, verbe.AuxEtre, verbe.NonPronominale,
                    verbe.Pronominale, verbe.Transitif, verbe.Intransitif);
            // Cas où le verbe entré en paramètre est un infinitif.

            else
                verbeInfinitif = (InfinitiveVerb)verbe;
            // ----- Cas des temps non composés -----
            if (new string[] { "présent", "imparfait", "passé simple", "futur simple" }.Contains(time) || mode + time == "participepassé")
            {
                string nomTable = verbeInfinitif.Nature == "verbeInfinitif conjugué" ? "VerbesConjugues" : "VerbesInfinitifs";
                // Si le verbeInfinitif est connu, on récupère directement sa conjugaison dans la base de données.
                if (estConnu(verbeInfinitif._verbe, nomTable))
                {
                    for (int idRow = 0; idRow < Phrase.lesData.Tables["VerbesConjugues"].Rows.Count; idRow++)
                    {
                        DataRow row = Phrase.lesData.Tables["VerbesConjugues"].Rows[idRow];
                        // Si on a trouvé la ligne pour l'infinitif où le groupe est le même pour le temps et la personne que l'on souhaite,
                        // en renvoie le verbeInfinitif conjugué de cette même ligne.
                        if (row["Infinitif"] as string == verbeInfinitif._verbe && row["Groupe"] as string == verbeInfinitif.Group && row["Temps"] as string == time
                                && row["Mode"] as string == mode && row["Personne"] as string == person && row["Verbe"] as string != null)
                            // On ne renvoie le verbeInfinitif conjugué à cet endroit là que si le verbeInfinitif trouvé n'est pas null (c'est à dire, qu'il est
                            // renseigné dans la table. Sinon, on passe à la suite afin de le construire manuellement.
                            return new ConjugatedVerb(row["Verbe"] as string, person, verbeInfinitif.Group, time, mode, verbeInfinitif._verbe,
                                        verbeInfinitif.AuxAvoir, verbeInfinitif.AuxEtre, verbeInfinitif.NonPronominale, verbeInfinitif.Pronominale,
                                        verbeInfinitif.Transitif, verbeInfinitif.Intransitif)._verbe;
                    }
                }
                // Si on arrive à ce stade du programme, c'est que le verbeInfinitif n'est pas connu ou que la forme voulue (temps / personne)
                // n'est pas connue. Il faut donc construire la conjugaison manuellement.

                // On commence par récupérer le radical du verbeInfinitif.
                string stem = stemOf(verbeInfinitif._verbe, verbeInfinitif.Nature);
                string ending = "";

                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    DataRow row = Phrase.lesData.Tables["Conjugaison"].Rows[idRow];
                    // Si on a trouvé la ligne où le groupe est le même pour le temps et la personne que l'on souhaite,
                    // on récupère la terminaison correspondante à cette ligne.
                    if (row["VerbGroup"] as string == verbeInfinitif.Group && row["Time"] as string == time
                            && row["Mode"] as string == mode && row["Person"] as string == person)
                    {
                        ending = row["Ending"] as string;
                        break;
                    }
                }

                // Si ending est null, ending vaut la chaîne vide.
                ending = ending ?? "";

                // Si à ce stade, la terminaison est vide, c'est que la combinaison personne/temps/mode n'existe pas
                // (exemple : à l'impératif, seules les personnes 2, 4 et 5 existent).
                // On n'entre alors pas dans le traitement des exceptions car il n'y a rien à traiter et
                // cela va donc générer des erreurs.
                if (ending != "")
                {
                    // On traite les exceptions :

                    // Premier groupe :
                    if (verbeInfinitif.Group == "1")
                    {
                        // -eler, -eter → -elle, -ette si "appeler" ou "jeter"
                        if (ending[0] == 'e' && ending != "ez" && (verbeInfinitif._verbe.Contains("appeler") || verbeInfinitif._verbe.Contains("jeter")))
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
                    else if (verbeInfinitif.Group == "2")
                    {
                        // Les verbes du deuxième groupe sont régulier, sauf le verbeInfinitif haïr.
                        if (verbeInfinitif._verbe == "ha" && !(new string[] { "1", "2", "3" }.Contains(person)
                                && new string[] { "présent indicatif", "présent impératif" }.Contains(time + " " + mode)))
                            ending = "ï" + ending.Substring(1);
                    }

                    // Troisième groupe :
                    else if (verbeInfinitif.Group == "3")
                    {
                        // Si le dernier caractère du radical est un 't' et que la terminaison commence par un 's',
                        // on supprime le 't'. Exemple : sortir → sort- → sors 
                        if (stem[stem.Length - 1] == 't' && ending[0] == 's')
                            stem = stem.Substring(0, stem.Length - 1);
                    }

                    // Général :
                    // c → ç
                    if (stem[stem.Length - 1] == 'c' && new char[] { 'a', 'o', 'â' }.Contains(ending[0]))
                        stem = stem.Substring(0, stem.Length - 1) + "ç";
                    // g → ge
                    else if (stem[stem.Length - 1] == 'g' && new char[] { 'a', 'o', 'â' }.Contains(ending[0]))
                        stem += "e";
                }

                return new ConjugatedVerb(stem + ending, person, verbeInfinitif.Group, time, mode, verbeInfinitif._verbe, verbeInfinitif.AuxAvoir,
                    verbeInfinitif.AuxEtre, verbeInfinitif.NonPronominale, verbeInfinitif.Pronominale, verbeInfinitif.Transitif,
                    verbeInfinitif.Intransitif)._verbe;
            }


            // ----- Cas des temps composés -----
            // Si le temps souhaité est un temps composé (ex : passé composé), on le construit de la façon suivante :
            // auxiliaire conjugué au temps correspondant (passé composé → présent) + participe passé du verbeInfinitif.
            else
            {
                string verb = "";
                // Indicatif
                if (time == "passé composé")
                    verb = ConjugationOf(new InfinitiveVerb(verbeInfinitif.GetAuxiliaire()), person, "présent", mode) + " " + verbeInfinitif.GetParticipePasse();
                else if (time == "plus-que-parfait")
                    verb = ConjugationOf(new InfinitiveVerb(verbeInfinitif.GetAuxiliaire()), person, "imparfait", mode) + " " + verbeInfinitif.GetParticipePasse();
                else if (time == "passé antérieur")
                    verb = ConjugationOf(new InfinitiveVerb(verbeInfinitif.GetAuxiliaire()), person, "passé simple", mode) + " " + verbeInfinitif.GetParticipePasse();
                else if (time == "futur antérieur")
                    verb = ConjugationOf(new InfinitiveVerb(verbeInfinitif.GetAuxiliaire()), person, "futur simple", mode) + " " + verbeInfinitif.GetParticipePasse();
                // Autre
                else if (time == "passé")
                    verb = ConjugationOf(new InfinitiveVerb(verbeInfinitif.GetAuxiliaire()), person, "présent", mode) + " " + verbeInfinitif.GetParticipePasse();

                return new ConjugatedVerb(verb, person, verbeInfinitif.Group, time, mode, verbeInfinitif._verbe, verbeInfinitif.AuxAvoir,
                    verbeInfinitif.AuxEtre, verbeInfinitif.NonPronominale, verbeInfinitif.Pronominale, verbeInfinitif.Transitif,
                    verbeInfinitif.Intransitif)._verbe;
            }
        }

        // Conjugue le verbe à la personne et au temps donnés en paramètre.
        public static void Conjugate(ref Verb verbe, string person, string time, string mode)
        {
            string action;
            if (verbe.Nature == "verbe conjugué")
                action = verbe.Action;
            else
                action = verbe._verbe;
            verbe = new ConjugatedVerb(ConjugationOf(verbe, person, time, mode), person, verbe.Group, time, mode, action, verbe.AuxAvoir,
                verbe.AuxEtre, verbe.NonPronominale, verbe.Pronominale, verbe.Transitif, verbe.Intransitif);
        }


        // Renvoie le groupe du verbe.
        protected static string groupOf(string verb, string nature)
        {
            // Cas où le verbe est un infinitif :
            if (nature == "verbe infinitif")
            {
                // Si le verbe est "aller" ou termine par "re" ou "oir", on renvoie le groupe "3".
                if (verb == "aller" || verb.Substring(verb.Length - 2) == "re" || verb.Substring(verb.Length - 3) == "oir")
                    return "3";
                // Si le verbe est haïr ou se termine par "ir", on renvoie le groupe "2".
                else if (verb == "haïr" || verb.Substring(verb.Length - 2) == "ir")
                    return "2";
                // Si le verbe termine par "er", on renvoie le groupe "1".
                else if (verb.Substring(verb.Length - 2) == "er")
                    return "1";
                // Si on ne sait pas, dans les cas restant, on renvoie le groupe "3".
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
        protected static string stemOf(string verb, string nature)
        {
            verb = verb ?? "";
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

            // Si on a rien trouvé et qu'on ne sait pas, on renvoie le verbe sans les 2 derniers caractères si le verbe
            // faut au moins trois caractères de taille, sinon on renvoie la chaîne vide.
            if (verb.Length >= 3)
                return verb.Substring(0, verb.Length - 2);
            else
                return "";
        }

        // Renvoie la terminaison du verbe.
        protected static string endingOf(string verb, string nature)
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
                        if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Ending"])
                            return verb.Substring(i);
                    }
                }
            }

            // Si on a rien trouvé et qu'on ne sait pas, on renvoie les 2 derniers caractères du verbe.
            return verb.Substring(verb.Length - 2);
        }

        // Renvoie le temps auquel est conjugué le verbe.
        protected static string timeOf(string verb)
        {
            // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
            // à quel temps appartient cette terminaison.
            for (int i = 0; i < verb.Length; i++)
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie le temps correspondant.
                    if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Ending"])
                        return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Time"];
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie "présent indicatif" par défaut.
            return "présent";
        }

        // Renvoie le mode auquel est conjugué le verbe.
        protected static string modeOf(string verb)
        {
            // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
            // à quel mode appartient cette terminaison.
            for (int i = 0; i < verb.Length; i++)
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie le mode correspondant.
                    if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Ending"])
                        return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Mode"];
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie "indicatif" par défaut.
            return "indicatif";
        }

        // Renvoie la personne à laquelle est conjuguée le verbe.
        protected static string personOf(string verb)
        {
            // On récupère la terminaison du verbe conjugué et on cherche dans la table Conjugaison
            // pour quelle personne elle existe..
            for (int i = 0; i < verb.Length; i++)
            {
                for (int idRow = 0; idRow < Phrase.lesData.Tables["Conjugaison"].Rows.Count; idRow++)
                {
                    // Si on a trouvé la terminaison du verbe dans la table Conjugaison, on renvoie la personne correspondante.
                    if (verb.Substring(i) == (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Ending"])
                        return (string)Phrase.lesData.Tables["Conjugaison"].Rows[idRow]["Person"];
                }
            }

            // Si on n'a rien trouvé et qu'on ne sait pas, on renvoie la personne "3" par défaut.
            return "3";
        }

        // Renvoie l'infinitif (l'action) d'un verbe conjugué.
        protected static string infinitiveOf(string verb)
        {
            return infinitiveOf(stemOf(verb, "verbe conjugué"), groupOf(verb, "verbe conjugué"));
        }
        // Renvoie l'infinitif (l'action) d'un verbe conjugué connaissant son radical et son groupe.
        protected static string infinitiveOf(string stem, string group)
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
                    else if (stem[stem.Length-1] == 'l')
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

        // Transforme le verbe en un verbe à l'infinitif.
        public static void ToInfinitive(ref Verb verbe)
        {
            if (verbe.Nature == "verbe conjugué")
                verbe = new InfinitiveVerb(verbe.Action, verbe.Group, verbe.AuxAvoir, verbe.AuxEtre, verbe.NonPronominale,
                    verbe.Pronominale, verbe.Transitif, verbe.Intransitif);
            // Sinon, on ne fait rien car le verbe est déjà à l'infinitif.
        }

        // Renvoie true si le verbe est connu dans la table, sinon false
        // L'argument typeVerbe vaut VerbesInfinitifs si le verbe est un infinitif,
        // et vaut VerbesConjugues si le verbe est conjugué.
        protected static bool estConnu(string verb, string typeVerbe)
        {
            // Si le champ static infoVerbe est null, c'est qu'on ne sait pas encore si le verbe est connu.
            if (infoVerbe == null)
            {
                DataRow row = null;
                DataRow rowInfo = null;
                for (int i = 0; i < Phrase.lesData.Tables[typeVerbe].Rows.Count; i++)
                {
                    if (verb == Phrase.lesData.Tables[typeVerbe].Rows[i]["Verbe"] as string)
                    {
                        row = Phrase.lesData.Tables[typeVerbe].Rows[i];
                        break;
                    }
                }

                // Si on a trouvé le verbe dans la table, alors il existe et on peut remplir infoVerbe avec ses informations.
                if (row != null)
                {
                    // On n'a pas les mêmes infos sur le verbe qu'il soit conjugué ou non.
                    if (typeVerbe == "VerbesConjugues")
                    {
                        // On cherche les informations du verbes dans la table VerbesInfinitifs (auxiliaire, transitif...).
                        for (int j = 0; j < Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows.Count; j++)
                        {
                            // On fait pour cela une recherche par l'infinitif, puisque la table ne contient que des infinitifs.
                            if (row["Infinitif"] as string == Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows[j]["Verbe"] as string)
                            {
                                rowInfo = Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows[j];
                                break;
                            }
                        }
                        infoVerbe = new string[] { row["Groupe"] as string,
                                                    row["Temps"] as string,
                                                    row["Mode"] as string,
                                                    row["Personne"] as string,
                                                    row["Infinitif"] as string,
                                                    rowInfo["AuxAvoir"] as string,
                                                    rowInfo["AuxEtre"] as string,
                                                    rowInfo["NonPronominale"] as string,
                                                    rowInfo["Pronominale"] as string,
                                                    rowInfo["Transitif"] as string,
                                                    rowInfo["Intransitif"] as string };
                    }
                    else
                    {
                        // On cherche les informations du verbes dans la table VerbesInfinitifs (auxiliaire, transitif...).
                        for (int j = 0; j < Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows.Count; j++)
                        {
                            // On fait pour cela une recherche par l'infinitif, puisque la table ne contient que des infinitifs.
                            if (verb == Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows[j]["Verbe"] as string)
                            {
                                rowInfo = Phrase.lesData.Tables["InfosVerbesInfinitifs"].Rows[j];
                                break;
                            }
                        }
                        infoVerbe = new string[] { row["Groupe"] as string,
                                                    rowInfo["AuxAvoir"] as string,
                                                    rowInfo["AuxEtre"]  as string,
                                                    rowInfo["NonPronominale"]  as string,
                                                    rowInfo["Pronominale"]  as string,
                                                    rowInfo["Transitif"] as string,
                                                    rowInfo["Intransitif"] as string };
                    }
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
