using Projet_AIA_Console_Version.Natures_Grammaticales.Invariables;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    static class Nature
    {
        // Détermine la nature de chaque mot d'une phrase entrée en paramètre, en déterminant
        // dans un premier temps toutes les natures possibles pour ce mot, puis la nature
        // la plus probable en fonction du nbrUtilisation de chaque nature pour ce mot
        // et du contexte dans lequel est le mot.
        public static List<Word> DeterminerLesNatures(List<List<object[]>> laPhrase)
        {
            int i;
            string motCourant;
            int tailleMotCourant;
            bool precDeterOuPronom = false;


            //  =======================================================================================================
            //  ETAPE 1
            // ========================================================================================================
            // On cherche à attribuer une nature unique à chaque mot de la phrase. Pour cela, on parcourt les mots de la
            // phrase. On note dans un premier temps toutes les natures possibles pour chaque mot. Puis, on trie les natures
            // possibles en fonction du nombre d'utilisation de ce mot pour cette nature, et on ne conserve que la nature
            // ayant le nombre d'utilisation le plus élevé.

            #region Etape1 : Recherche de toutes les natures possibles
            for (i = 0; i < laPhrase.Count; i++) // on parcourt chaque mot de la phrase
            {

                // On traite le cas du mot du qui peut être un déterminant partitif (ex : du beurre),
                // ou une contraction d'une préposition et d'un déterminant (de le → du).
                #region Cas de "du" → "de le"
                if (laPhrase[i][0][0] as string == "du" && i < laPhrase.Count - 1)
                {
                    laPhrase.RemoveAt(i);
                    laPhrase.Insert(i, new List<object[]> { new object[] { "de", 0 } });
                    laPhrase.Insert(i + 1, new List<object[]> { new object[] { "le", 0 } });
                }
                #endregion

                #region Les cas où le mot courant est connu dans la base de données
                // On parcourt la liste de chaque natures grammaticales suivantes.
                // Si le mot courant est contenu dans une de ces listes, alors on a trouvé sa nature.
                motCourant = (string)laPhrase[i][0][0];
                tailleMotCourant = motCourant.Length;

                // PREPOSITIONS
                rechercheDansTable("Prepositions", i, motCourant, laPhrase, false);

                if (tailleMotCourant > 1)   // car un mot d'une seule lettre ne peut pas être des natures suivantes
                {
                    // CONJONCTIONS DE COORDINATION
                    rechercheDansTable("ConjDeCoords", i, motCourant, laPhrase, false);

                    // CONJONCTIONS DE SUBORDINATION
                    rechercheDansTable("ConjDeSubs", i, motCourant, laPhrase, false);

                    // DETERMINANTS
                    // On on a trouvé que le mot courant pouvait être un déterminant, mais que le mot précédent est un nom
                    // ou un adjectif avec un nombre d'utilisation faible, on supprime de la liste le fait que le mot
                    // courant puissent être un déterminant, car en général il n'y a pas de déterminant après un nom ou
                    // après un adjectif.
                    if (rechercheDansTable("Determinants", i, motCourant, laPhrase, false)
                        && i > 0 && laPhrase[i - 1].Count > 0 && new string[] { "Noms", "Adjectifs" }.Contains(laPhrase[i - 1][1][0])
                        && (int)laPhrase[i - 1][1][1] < 5)
                    {
                        laPhrase[i].RemoveAt(laPhrase[i].Count - 1);
                    }

                    // PRONOMS
                    rechercheDansTable("Pronoms", i, motCourant, laPhrase, false);

                    // VERBES INFINITIFS
                    rechercheDansTable("VerbesInfinitifs", i, motCourant, laPhrase, false);

                    // ADVERBES CONNUS
                    rechercheDansTable("Adverbes", i, motCourant, laPhrase, false);
                }

                // VERBES CONJUGUÉS et NOMS et ADJECTIFS
                if (laPhrase[i].Count == 1) // car si le mot est déjà d'une autre nature, il ne peut pas être un verbe, un nom ou un adjectif
                {
                    // Verbe
                    rechercheDansTable("VerbesConjugues", i, motCourant, laPhrase, precDeterOuPronom);

                    // Nom
                    // Si on a trouvé que le mot courant est un nom, mais que le mot précédent est également
                    // spécifié comme pouvant être un nom avec un faible nombre d'utilisation,
                    // alors on supprime l'option "Noms" pour le mot précédent et on lui met l'option
                    // "Adjectifs" à la place.
                    if (rechercheDansTable("Noms", i, motCourant, laPhrase, precDeterOuPronom)
                        && i > 0 && laPhrase[i - 1].Count > 0 && laPhrase[i - 1][1][0] as string == "Noms"
                        && (int)laPhrase[i - 1][1][1] < 5)
                    {
                        laPhrase[i - 1].RemoveAt(laPhrase[i].Count - 1);
                        laPhrase[i - 1].Add(new object[] { "Adjectifs", "2" });
                    }

                    // Adjectif
                    rechercheDansTable("Adjectifs", i, motCourant, laPhrase, precDeterOuPronom);
                }
                #endregion

                // Les mots comme "le", "la", "les", "leur" (etc) peuvent à la fois être des déterminants ou des pronoms.
                // Ils sont des déterminants s'ils sont devant un nom (il peut y avoir un adjectif entre le déterminant et le nom),
                // et sont des pronoms s'ils sont devant un verbe.
                #region Traitement des hésitations entre déterminant et pronom
                if (new string[] { "le", "la", "les", "leur" }.Contains(laPhrase[i][0][0]))
                {
                    // S'il y a encore un mot après...
                    if (laPhrase.Count > i + 1)
                        // On passe le booléen à true pour effectuer des tests au prochain tour.
                        precDeterOuPronom = true;
                    // Sinon, s'il n'y a plus de mot après...
                    else
                        // Le mot est certainement un pronom.
                        laPhrase[i].Add(new object[] { "Pronoms", 3 });
                }
                else
                    precDeterOuPronom = false;
                #endregion

                // Le mot "que" peut à la fois être une conjonction de coordination ou un pronom relatif
                // selon s'il suit un verbe ou un nom.
                // On traite cette hésitation.
                #region Traitement des hésitations entre conjDeSub et pronom relatif pour "que"
                if (laPhrase[i][0][0] as string == "que" && i > 0)
                {
                    string nature = "";
                    for (int indice = i - 1; i >= 0; indice--)
                    {
                        if (laPhrase[indice].Count > 1 && laPhrase[indice][1][0] as string == "Noms")
                        {
                            nature = "Pronoms";
                            break;
                        }
                        else if (laPhrase[indice].Count > 1 && laPhrase[indice][1][0] as string == "VerbesConjugues")
                        {
                            nature = "ConjDeCoords";
                            break;
                        }
                    }

                    // On vide la liste des natures possibles pour le "que" et on y met
                    // qu'une seule nature en fonction des résultats précédents.
                    if (nature != "")
                    {
                        object[] mot = laPhrase[i][0];
                        laPhrase[i].Clear();
                        laPhrase[i].Add(mot);
                        laPhrase[i].Add(new object[] { nature, 3 });
                    }
                }
                #endregion

                // Si on a rien trouvé, et que le mot courant n'a toujours pas de nature attribuée
                #region Les cas où le mot courant n'est pas connu dans la base de données
                if (laPhrase[i].Count == 1 && i > 0)    // on teste i > 0 pour éviter les IndexOutOfRangeException avec laPhrase[i - 1]
                {
                    // Verbe infinitif
                    // Si le mot courant termine par "er" ou "ir", il a de grandes chances d'être un verbe à l'infinitif.
                    if (motCourant.EndsWith("er") || motCourant.EndsWith("ir"))
                        laPhrase[i].Add(new object[] { "VerbesInfinitifs", 3 });

                    // Verbe conjugué
                    // Si le mot précédent est un pronom ou un nom, alors le mot courant peut être un verbe
                    if (laPhrase[i - 1].Count > 1 && new string[] { "Pronoms", "Noms" }.Contains(laPhrase[i - 1][1][0] as string))
                        laPhrase[i].Add(new object[] { "VerbesConjugues", 2 });

                    // Nom
                    // Si le mot précédent est un déterminant, alors le mot courant peut être un nom
                    //var testMotPrecedent = (from item in laPhrase[i - 1] where (string)item[0] == "Determinants" select item);
                    if (laPhrase[i - 1].Exists(item => (string)item[0] == "Determinants"))
                        laPhrase[i].Add(new object[] { "Noms", 2 });

                    // Adjectif
                    // Si le mot précédent est un nom
                    // OU si le mot précédent est une des conjugaisons du verbe être
                    // ALORS le mot courant peut être un adjectif.

                    int j, compteur;
                    for (j = i - 1, compteur = 0; j >= 0; j--)
                        if (laPhrase[j].Exists(a => a[0] as string == "Adverbes"))
                            ++compteur;
                        else
                            break;

                    if (laPhrase[i - 1 - compteur].Exists(item => (string)item[0] == "Noms")
                        || (RecupBDD.lesData.Tables["VerbesConjugues"].AsEnumerable()
                        .Where(x => x["Infinitif"] as string == "être").AsEnumerable()
                        .Any(c => c["Verbe"].ToString() == laPhrase[i - 1 - compteur][0][0].ToString())))
                    {
                        laPhrase[i].Add(new object[] { "Adjectifs", 2 });
                    }

                    // Participe présent
                    // Si le mot courant termine par "-ant", alors il peut être un participe présent.
                    if (motCourant.Length > 3 && motCourant.EndsWith("ant"))
                        laPhrase[i].Add(new object[] { "VerbesConjugues", 1 });

                    // Participe passé
                    // Si le mot courant termine par "-é", "-i" ou "u", alors il peut être un participe passé.
                    if (motCourant.Length > 1 && (motCourant.EndsWith("é") || motCourant.EndsWith("i") || motCourant.EndsWith("u")))
                    {
                        laPhrase[i].Add(new object[] { "VerbesConjugues", 1 });
                    }

                    else if (motCourant.Length > 2 && (motCourant.EndsWith("és") || motCourant.EndsWith("ée")
                            || motCourant.EndsWith("is") || motCourant.EndsWith("ie")
                            || motCourant.EndsWith("us") || motCourant.EndsWith("ue")))
                    {
                        if (rechercheDansTable("VerbesConjugues", i, motCourant.Substring(0, motCourant.Length - 1), laPhrase, false))
                            laPhrase[i].Add(new object[] { "VerbesConjugues", 5 });
                        else
                            laPhrase[i].Add(new object[] { "VerbesConjugues", 1 });
                    }

                    else if (motCourant.Length > 3 && (motCourant.EndsWith("ées") || motCourant.EndsWith("ies") || motCourant.EndsWith("ues")))
                    {
                        if (rechercheDansTable("VerbesInfinitifs", i, motCourant.Substring(0, motCourant.Length - 2), laPhrase, false))
                            laPhrase[i].Add(new object[] { "VerbesConjugues", 5 });
                        else
                            laPhrase[i].Add(new object[] { "VerbesConjugues", 1 });
                    }


                    // ADVERBES INCONNUS EN -ment
                    // Comme la table Adverbes ne contient pas tous les adverbes existants
                    // (car il en existe beaucoup trop), on repere aussi les adverbes par leur
                    // terminaison en -ment

                    if (motCourant.Length > 4 && motCourant.EndsWith("ment"))
                        laPhrase[i].Add(new object[] { "Adverbes", 5 });

                }
                #endregion


                // Une fois que l'on a trouvé toutes les natures possibles pour le mot courant,
                // on les trie de la nature la plus probable à la nature la moins probable,
                // cela en utilisant le nbrUtilisation associé à chacune des natures
                // pour le mot en question.

                // On trie les natures du mot courant ( laPhrase[i] ) de la plus probable à la moins probable 
                for (int a = 2; a < laPhrase[i].Count; a++)
                {
                    if ((int)laPhrase[i][a][1] <= (int)laPhrase[i][a - 1][1])
                        laPhrase[i].RemoveAt(a);
                    else
                        laPhrase[i].RemoveAt(a - 1);
                }
            }

            #endregion

            //  =======================================================================================================
            //  ETAPE 2
            // ========================================================================================================
            // Maintenant qu'on a attribué une unique nature à chaque mot, on cherche à déterminer le genre et le nombre
            // des mots variables et la personne (+ nombre) et le temps des verbes. On stocke ces informations en transformant
            // chaque string contenant un mot de la phrase en type correspondant à sa nature (structure)
            #region Etape2 : Attribution d'une nature unique à chaque mot, selon la plus probable

            List<Word> phrase = new List<Word>();

            for (i = 0; i < laPhrase.Count; i++) // Pour chaque mot de la phrase...
            {
                // Afin que le programme ne crash pas si le mot testé n'a pas de nature attribuée
                // (et donc que la case laPhrase[i][1] n'existe pas, ce qui entraîne une exception).
                if (laPhrase[i].Count > 1)
                {
                    switch (laPhrase[i][1][0] as string)   // laPhrase[i][1][0] → correspond à la nature
                    {
                        case "Noms":
                            phrase.Add(new Name((string)laPhrase[i][0][0]));
                            break;
                        case "Adjectifs":
                            phrase.Add(new Adjective((string)laPhrase[i][0][0]));
                            break;
                        case "VerbesConjugues":
                            // On traite les verbes conjugués à des temps composés
                            if (phrase.Count > 0 && phrase.Last().Nature == "verbe conjugué")
                            {
                                ConjugatedVerb auxiliaire = (ConjugatedVerb)phrase.Last();

                                if (new string[] { "être", "avoir" }.Contains(auxiliaire.Action))
                                {
                                    phrase.RemoveAt(phrase.Count - 1);
                                    ConjugatedVerb participe = new ConjugatedVerb((string)laPhrase[i][0][0]);

                                    phrase.Add(new ConjugatedVerb(auxiliaire.ToString() + " " + participe.ToString(), auxiliaire.Person, participe.Group,
                                        ConjugatedVerb.TimeOfVerbeCompose(auxiliaire), auxiliaire.Mode, participe.Action, participe.AuxAvoir, participe.AuxEtre,
                                        participe.NonPronominale, auxiliaire.Pronominale, auxiliaire.Transitif, auxiliaire.Intransitif));
                                    break;
                                }
                            }
                            phrase.Add(new ConjugatedVerb((string)laPhrase[i][0][0]));
                            break;
                        case "VerbesInfinitifs":
                            phrase.Add(new InfinitiveVerb((string)laPhrase[i][0][0]));
                            break;
                        case "Determinants":
                            phrase.Add(new Determiner((string)laPhrase[i][0][0]));
                            break;
                        case "Pronoms":
                            phrase.Add(new Pronoun((string)laPhrase[i][0][0]));
                            break;
                        case "Adverbes":
                            phrase.Add(new Adverbe((string)laPhrase[i][0][0]));
                            break;
                        case "ConjDeCoords":
                            phrase.Add(new ConjDeCoord((string)laPhrase[i][0][0]));
                            break;
                        case "ConjDeSubs":
                            phrase.Add(new ConjDeSub((string)laPhrase[i][0][0]));
                            break;
                        case "Prepositions":
                            phrase.Add(new Preposition((string)laPhrase[i][0][0]));
                            break;
                        default:
                            phrase.Add(new UnknowWord((string)laPhrase[i][0][0]));
                            break;
                    }
                }
                else
                    phrase.Add(new UnknowWord((string)laPhrase[i][0][0]));
            }
            #endregion

            return phrase;
        }


        // Fonction utilisée par la fonction DeterminerLesNatures (ci-dessus)
        // Effectue la recherche d'un mot de la phrase dans une table spécifiée et note sa nature (qui est le nom de la table) s'il est trouvé.
        // Renvoie true si une nature a été ajoutée dans la liste.
        private static bool rechercheDansTable(string nomDeLaTable, int indexDuMotDeLaPhrase, string motCourant, List<List<object[]>> laPhrase, bool precDeterOuPronom)
        {
            for (int j = 0; j < RecupBDD.lesData.Tables[nomDeLaTable].Rows.Count; j++)   // On parcourt la table
            {
                if ((string)RecupBDD.lesData.Tables[nomDeLaTable].Rows[j][0] == motCourant)  // Si on trouve le mot courant dans la table
                {
                    laPhrase[indexDuMotDeLaPhrase].Add(new object[] { nomDeLaTable, RecupBDD.lesData.Tables[nomDeLaTable].Rows[j]["nbrUtilisation"] });

                    // Traiter les hésitations pour les mots pouvant être à la fois déterminants ou pronoms :
                    #region Traitement des hésitations entre déterminant ou pronom
                    // Si le mot précédent pouvait être un déterminant ou un pronom et que le mot actuel est un nom ou un adjectif...
                    if (precDeterOuPronom && (nomDeLaTable == "Noms" || nomDeLaTable == "Adjectifs"))
                    {
                        // On récupère le mot pouvant être un déterminant ou un pronom.
                        object[] leDeterOuPronom = laPhrase[indexDuMotDeLaPhrase - 1][0];
                        // On vide la liste contenant les natures possibles du mot.
                        laPhrase[indexDuMotDeLaPhrase - 1].Clear();
                        // On remet le mot dans la liste qu'on a vidée.
                        laPhrase[indexDuMotDeLaPhrase - 1].Add(leDeterOuPronom);
                        // On ajoute la bonne nature : ici déterminant.
                        laPhrase[indexDuMotDeLaPhrase - 1].Add(new object[] { "Determinants", "99" });
                    }
                    // Sinon, si le mot précédent pouvait être un déterminant ou un pronom et que le mot actuel est un verbe conjugué...
                    else if (precDeterOuPronom && nomDeLaTable == "VerbesConjugues")
                    {
                        // On récupère le mot pouvant être un déterminant ou un pronom.
                        object[] leDeterOuPronom = laPhrase[indexDuMotDeLaPhrase - 1][0];
                        // On vide la liste contenant les natures possibles du mot.
                        laPhrase[indexDuMotDeLaPhrase - 1].Clear();
                        // On remet le mot dans la liste qu'on a vidée.
                        laPhrase[indexDuMotDeLaPhrase - 1].Add(leDeterOuPronom);
                        // On ajoute la bonne nature : ici pronom.
                        laPhrase[indexDuMotDeLaPhrase - 1].Add(new object[] { "Pronoms", "99" });
                    }
                    #endregion

                    // On renvoie true car une nature a été ajoutée à la liste.
                    return true;
                }
            }
            // On renvoie false car aucune nature n'a été ajoutée à la liste.
            return false;
        }

    }
}
