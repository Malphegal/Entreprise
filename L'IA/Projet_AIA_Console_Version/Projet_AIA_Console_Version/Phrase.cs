using System;
using System.Collections.Generic;
using System.Linq;
using Projet_AIA_Console_Version.Natures_Grammaticales;

namespace Projet_AIA_Console_Version
{
    static class Phrase
    {

        // Découpe la réponse de l'utilisateur en List de phrases, qui sont des List de mots qui sont
        // eux mêmes des List de natures possibles.
        public static List<List<List<object[]>>> DecouperPhrase(string reponseUtilisateur)
        {
            List<List<List<object[]>>> lesPhrases = new List<List<List<object[]>>>();
            List<List<object[]>> unePhrase = new List<List<object[]>>();

            int positionDuPoint = 0;
            int positionDuSeparateur = 0;
            int startPhrase = 0;
            int startMot = 0;
            string phrase;
            string mot;
            int compteurMot = 0;

            // Extraction des phrases depuis la reponseUtilisateur
            do
            {
                positionDuPoint = reponseUtilisateur.IndexOf('.', startPhrase);
                if (positionDuPoint >= -1)
                {
                    if (positionDuPoint == -1)
                        positionDuPoint = reponseUtilisateur.Length - 1;
                    phrase = reponseUtilisateur.Substring(startPhrase, positionDuPoint - startPhrase + 1).Trim();

                    // Extraction des mots depuis la phrase
                    do
                    {
                        positionDuSeparateur = phrase.IndexOfAny(new char[] { ' ', '\'' }, startMot);
                        if (positionDuSeparateur >= 0)
                        {
                            // On récupère le mot.
                            mot = phrase.Substring(startMot, positionDuSeparateur - startMot + 1).Trim();

                            // On traite le cas où le mot contient un apostrophe.
                            mot = TraiterMotsComprenantApostropheOuEspace(mot, ref unePhrase);

                            // On ajoute le mot dans le tableau des mots.
                            unePhrase.Add(new List<object[]> { new object[] { mot, 0 } });
                            // On redéfinit les variables pour passer au mot suivant.
                            startMot = positionDuSeparateur + 1;
                            compteurMot++;
                        }
                        // Sinon, s'il n'y a plus de points...
                        else if (positionDuSeparateur == -1)
                        {
                            // On récupère le mot.
                            mot = phrase.Substring(startMot).Trim('.');

                            // On traite le cas où le mot contient un apostrophe.
                            mot = TraiterMotsComprenantApostropheOuEspace(mot, ref unePhrase);

                            // On ajoute le dernier mot dans le tableau des mots.
                            unePhrase.Add(new List<object[]> { new object[] { mot, 0 } });
                        }
                    } while (positionDuSeparateur > 0);

                    // On ajoute la phrase dans la liste des phrases.
                    lesPhrases.Add(unePhrase);

                    // On redéfinit les variables avant de passer à la phrase suivante.
                    positionDuSeparateur = 0;
                    startMot = 0;
                    compteurMot = 0;
                    startPhrase = positionDuPoint + 1;
                }
                unePhrase = new List<List<object[]>>(16);
            } while (positionDuPoint > 0 && positionDuPoint < reponseUtilisateur.Length - 1);  // si <=0, c'est qu'il n'y a plus de point

            return lesPhrases;
        }

        // Attribue une fonction grammaticale à chaque mot de la phrase.
        public static List<Word[]> DeterminerLesFonctions(List<Word> phraseArg)
        {
            // On effectue une copie de la phrase reçue afin de ne pas modifier la List
            // de base, car on va effectuer des suppressions de mots.
            List<Word> phrase = new List<Word>(phraseArg);
            List<Word[]> phraseFonction = new List<Word[]>();

            //  =======================================================================================================
            //  ========== ETAPE 1
            // ========================================================================================================
            // On commence par former les groupes nominaux :
            // - liaisons Adjectifs Adverbes
            // - liaisons Determinant ou Adjectif Nom
            // - liaisons Complémnt du nom Nom
            #region Etape1 : Groupes Nominaux


            // ---------- ADVERBES et ADJECTIFS ----------
            LierAdverbesAdjectifs(phrase, phraseFonction);

            // ---------- DETERMINANTS, ADJECTIFS et NOMS ----------
            // On parcourt les mots de la phrase à la recherche de noms.
            LierDeterminantsNomsAdjectifs(phrase, phraseFonction);

            // ---------- COMPLEMENTS DU NOM et NOMS ----------
            LierNomsComplNom(phrase, phraseFonction);

            #endregion

            //  =======================================================================================================
            //  ========== ETAPE 2
            // ========================================================================================================
            // On compte d'abord le nombre de verbes conjugués dans la phrase,
            // car une phrase se contruit la plupart du temps autour d'un verbe conjugué.
            // On récupère dans une List les indices auxquels se trouvent les verbes dans la List phrase.
            #region Etape2 : Groupes Verbaux

            List<int> lesVerbesConjugues = GetListIndexOf("verbe conjugué", phrase);

            // Si aucun verbe conjugué n'a été trouvé dans la phrase,
            // alors on est face à une phrase non verbale.
            // On traite les phrases non verbale :
            if (lesVerbesConjugues.Count == 0)
            {
                // TODO: Traiter les phrases non verbales
            }

            // Sinon, c'est qu'il s'agit d'une phrase verbale.
            else
            {
                // On décompose la phrase en propositions subordonnées et coordonnées.
                List<List<Word>> lesPropositions = DecomposerPhraseEnPropositions(phrase, lesVerbesConjugues);
            }

            #endregion

            // _________

            foreach(Word mot in phrase)
                Console.WriteLine(mot.ToString());

            return phraseFonction;
        }


        // Renvoie true si les deux mots entrés en paramètre sont accordés, sinon false.
        public static bool SontAccordes(VariableWord motVariable, ConjugatedVerb verbeConjugue)
        {
            if (verbeConjugue.GetGender() == motVariable.Gender && verbeConjugue.Number == motVariable.Number)
                return true;
            else
                return false;
        }
        public static bool SontAccordes(VariableWord motVariable1, VariableWord motVariable2)
        {
            if (motVariable1.Gender == motVariable2.Gender && motVariable1.Number == motVariable2.Number)
                return true;
            else
                return false;
        }

        // PRIVEES

        // Lie les adverbes aux adjectifs de phrase et place le résultat dans phraseFonction, en supprimant les adverbes.
        private static void LierAdverbesAdjectifs(List<Word> phrase, List<Word[]> phraseFonction)
        {
            // On parcourt les mots de la phrase à la recherche d'adjectifs.
            for (byte indiceMotCourant = 0; indiceMotCourant < phrase.Count; indiceMotCourant++)
            {
                Word mot = phrase[indiceMotCourant];
                // Si on tombe sur un nom ou nom propre...
                if (mot.Nature == "adjectif")
                {
                    // S'il y a des mots devant...
                    if (indiceMotCourant > 0)
                    {
                        // On cherche ensuite les adverbes avant l'adjectif.
                        // Tant qu'on ne dépasse pas de la phrase vers l'avant et que le mot précédent est un adverbe...
                        byte indiceMotTeste = (byte)(indiceMotCourant - 1);
                        while (phrase[indiceMotTeste].Nature == "adverbe")
                        {
                            phraseFonction.Add(new Word[] { phrase[indiceMotTeste], new LinkBetweenWord("ADV"), mot });
                            phrase.RemoveAt(indiceMotTeste);
                            indiceMotCourant--; // Car la suppression à décalé les éléments

                            if (indiceMotTeste != 0)
                                indiceMotTeste--;
                            else
                                break;
                        }
                    }
                }
            }
        }

        // Lie les déterminants, les noms et les adjectifs et place le résultat dans phraseFonction, en supprimant les déterminants et les adjectifs.
        private static void LierDeterminantsNomsAdjectifs(List<Word> phrase, List<Word[]> phraseFonction)
        {
            for (byte indiceMotCourant = 0; indiceMotCourant < phrase.Count; indiceMotCourant++)
            {
                Word mot = phrase[indiceMotCourant];
                // Si on tombe sur un nom ou nom propre...
                if (mot.Nature == "nom" || mot.Nature == "nom propre")
                {
                    // S'il y a des mots devant...
                    #region Avant le nom
                    if (indiceMotCourant > 0)
                    {
                        // On cherche d'abord les adjectifs et les déterminants devant le nom.
                        // Tant qu'on ne dépasse pas de la phrase vers l'avant et que le mot précédant est un déterminant ou un adjectif...
                        byte indiceMotTeste = (byte)(indiceMotCourant - 1);
                        while (phrase[indiceMotTeste].Nature == "déterminant" || phrase[indiceMotTeste].Nature == "adjectif")
                        {
                            Word motPrec = phrase[indiceMotTeste];
                            switch (motPrec.Nature)
                            {
                                case "déterminant":
                                    phraseFonction.Add(new Word[] { motPrec, new LinkBetweenWord("DTMN"), mot });
                                    phrase.RemoveAt(indiceMotTeste);
                                    indiceMotCourant--; // Car la suppression à tout décalé
                                    break;
                                case "adjectif":
                                    phraseFonction.Add(new Word[] { motPrec, new LinkBetweenWord("ADJ"), mot });
                                    phrase.RemoveAt(indiceMotTeste);
                                    indiceMotCourant--; // Car la suppression à tout décalé
                                    break;
                            }

                            if (indiceMotTeste != 0)
                            {
                                indiceMotTeste--;

                                // Traiter les "et" ou virgules entre les adjectifs.
                                // Si on rencontre un "et" ou une virgule, on les supprime.
                                if (phrase[indiceMotTeste].ToString() == "et" || phrase[indiceMotTeste].ToString() == ",")
                                {
                                    phrase.RemoveAt(indiceMotTeste);
                                    indiceMotCourant--; // Car la suppression a tout décalé
                                    if (indiceMotTeste != 0)
                                        indiceMotTeste--;
                                }
                            }
                            else
                                break;
                        }
                    }
                    #endregion

                    // S'il y a des mots après...
                    #region Après le nom
                    if (indiceMotCourant < phrase.Count - 1)
                    {
                        byte indiceMotTeste = (byte)(indiceMotCourant + 1);
                        // On cherche ensuite les adjectifs après le nom.
                        // Tant qu'on ne dépasse pas de la phrase vers l'arrière et que le mot suivant est un adjectif...
                        while (phrase[indiceMotTeste].Nature == "adjectif")
                        {
                            phraseFonction.Add(new Word[] { phrase[indiceMotTeste], new LinkBetweenWord("ADJ"), mot });
                            phrase.RemoveAt(indiceMotTeste);

                            if (indiceMotTeste < phrase.Count - 1)
                            {
                                // Pas de indiceMotTeste++ car la suppression a déjà décalé les éléments.

                                // Traiter les "et" ou virgules entre les adjectifs.
                                // Si on rencontre un "et" ou une virgule, on les supprime.
                                if (phrase[indiceMotTeste].ToString() == "et" || phrase[indiceMotTeste].ToString() == ",")
                                {
                                    // Si c'était le dernier élément de la phrase, on sort.
                                    if (indiceMotTeste >= phrase.Count - 1)
                                        break;
                                }
                            }
                            else
                                break;
                        }
                    }
                    #endregion
                }
            }
        }

        // Lie les noms et les compléments du nom et place le résultat dans phraseFonction.
        private static void LierNomsComplNom(List<Word> phrase, List<Word[]> phraseFonction)
        {
            for (ushort indiceMotCourant = 0; indiceMotCourant < phrase.Count; indiceMotCourant++)
            {
                Word mot = phrase[indiceMotCourant];
                bool quiSujet = true;
                // Si on tombe sur un nom ou nom propre...
                if ((mot.Nature == "nom" || mot.Nature == "nom propre") && indiceMotCourant < phrase.Count - 1)
                {
                    VariableWord nomComplete = (VariableWord)mot;
                    ushort indiceMotTeste = (ushort)(indiceMotCourant + 1);
                    // On cherche un éventuel complément du nom, qui se situe toujours après le nom.
                    // Le complément du nom est la plupart du temps précédé d'une préposition.
                    // Ainsi, pour savoir si le nom possède un complément du nom, on regarde si une
                    // préposition se trouve après lui.
                    if (phrase[indiceMotTeste].Nature == "préposition")
                    {
                        Word laPreposition = phrase[indiceMotTeste];
                        if (indiceMotTeste < phrase.Count - 1)
                        {
                            indiceMotTeste++;
                            // Si le mot suivant la préposition est et nom, un nom propre, un pronom, un verbe infinitif, ou un adverbe,
                            // la préposition se rattache à ce mot suivant.
                            if (new string[] { "nom", "nom propre", "pronom", "verbe infinitif", "adverbe" }.Contains(phrase[indiceMotTeste].Nature))
                                phraseFonction.Add(new Word[] { laPreposition, new LinkBetweenWord("PREP"), phrase[indiceMotTeste] });

                            // On supprime la préposition de la phrase et vu que cela décale les mots vers la gauche, on fait indiceMotTeste-=2
                            // pour retomber sur le nom complément.
                            phrase.RemoveAt(indiceMotTeste - 1);
                            indiceMotTeste--;
                            if (phrase[indiceMotTeste].ToString() == "qui")
                            {
                                indiceMotTeste--;
                                quiSujet = false;
                            }

                            // On traite le cas où le nom complément peut être suivi d'un pronom relatif (qui que quoi dont où -quel)
                            // qui se rattache au nom complément (et non pas au nom complété). La seule façon de savoir si le pronom
                            // relatif "QUI" (sujet) se rattache au nom complément ou au nom complété est de regarder les accords avec
                            // le verbe dans la suite de la phrase, pour voir à quel nom entre le nom complément et le nom complété
                            // conjugue le verbe. Si les deux noms sont du même genre/nombre... on ne peut devenir que grâce au sens de la phrase !
                            // Pour les autres pronoms relatifs (que COD, quoi, dont (= de qui) COI, auquel (= de qui) COI, où CCL),
                            // ils se rattachent la plupart du temps au nom qui se trouve juste avant (donc le nom complément) s'il
                            // n'y a pas de virgule pour les séparer.

                            // Le pronom relatif ne peut se rattacher qu'à un nom, on vérifie donc que le mot soit un nom.
                            // On vérifie également qu'on ne se situe pas au dernier mot, et donc qu'il y a encore des mots après...
                            // Enfin, on regarde si le mot suivant est un pronom.
                            if ((phrase[indiceMotTeste].Nature == "nom" || phrase[indiceMotTeste].Nature == "nom propre")
                                && indiceMotTeste < phrase.Count - 1 && phrase[indiceMotTeste + 1].Nature == "pronom")
                            {
                                Name nomComplement = (Name)phrase[indiceMotTeste];
                                Pronoun pronom = (Pronoun)phrase[indiceMotTeste + 1];
                                // On vérifie que le pronom soit de type relatif.
                                if (pronom.Type.Contains("relatif"))
                                {
                                    // Si le pronom relatif est "qui"...
                                    #region Cas où le pronom relatif est "qui"
                                    if (pronom.ToString() == "qui")
                                    {
                                        // Tant qu'il y a encore un encore un mot après...
                                        for (ushort i = (ushort)(indiceMotTeste + 2); i < phrase.Count; i++)
                                        {
                                            // Si on a trouvé un verbe conjugué...
                                            if (phrase[i].Nature == "verbe conjugué")
                                            {
                                                ConjugatedVerb verbeConjugue = (ConjugatedVerb)phrase[i];

                                                // Si le verbe est conjugué à un temps composé (et donc qu'il possède un participe passé accordable)...
                                                #region Cas où le verbe est à un temps composé
                                                if (verbeConjugue.IsTempsCompose())
                                                {
                                                    // On récupère l'auxiliaire.
                                                    Verb auxConjugue = new ConjugatedVerb(verbeConjugue.GetAuxiliaireConjugue(), verbeConjugue.Person);
                                                    // On le passe à l'infinitif.
                                                    Verb.ToInfinitive(ref auxConjugue);

                                                    // Si le verbe utilise l'auxiliaire être, et que le nom complément est accordé avec le verbe,
                                                    // le pronom relatif remplace le nom complément.
                                                    if (quiSujet && auxConjugue.ToString() == "être" && SontAccordes(nomComplement, verbeConjugue))
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });

                                                    // Si le verbe utilise l'auxiliaire être, et que le nom complément est accordé avec le verbe,
                                                    // le pronom relatif remplace le nom complété.
                                                    else if (quiSujet && auxConjugue.ToString() == "être" && SontAccordes(nomComplete, verbeConjugue))
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplete });

                                                    // Autrement, le cas par défaut est que le pronom relatif remplace le nom précédent, donc nom complément.
                                                    else
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });
                                                }
                                                #endregion

                                                // Sinon, si le verbe n'est pas à un temps composé, on regarde s'il s'agit du verbe être et
                                                // on cherche s'il est suivi d'un adjectif, accordable...
                                                #region Cas où le verbe n'est pas à un temps composé, on cherche un adjectif à la suite.
                                                else if (verbeConjugue.Action == "être" && i < phrase.Count - 1)
                                                {
                                                    // On ne cherche l'adjectif que 4 positions plus loin dans la phrase.
                                                    for (int j = i + 1; j < i + 5; j++)
                                                    {
                                                        // Si on trouve un adjectif...
                                                        if (phrase[j].Nature == "adjectif")
                                                        {
                                                            // Si cet adjectif est accordé avec le nom complément...
                                                            if (SontAccordes(nomComplement, (VariableWord)phrase[j]))
                                                                phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });

                                                            // Sinon, si cet adjectif est accordé avec le nom complété...
                                                            else if (SontAccordes(nomComplete, (VariableWord)phrase[j]))
                                                                phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplete });

                                                            // Autrement, le cas par défaut est que le pronom relatif remplace le nom précédent, donc nom complément.
                                                            else
                                                                phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });

                                                            // On sort du for après le premier adjectif trouvé.
                                                            break;
                                                        }
                                                    }
                                                }
                                                #endregion

                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    // Sinon, si le pronom relatif est "que"...
                                    #region Cas où le pronom relatif est "que"
                                    else if (pronom.ToString() == "que")
                                    {
                                        // Tant qu'il y a encore un encore un mot après...
                                        for (ushort i = (ushort)(indiceMotTeste + 2); i < phrase.Count; i++)
                                        {
                                            // Si on a trouvé un verbe conjugué...
                                            if (phrase[i].Nature == "verbe conjugué")
                                            {
                                                ConjugatedVerb verbeConjugue = (ConjugatedVerb)phrase[i];

                                                // Si le verbe est conjugué à un temps composé (et donc qu'il possède un participe passé accordable)...
                                                #region Cas où le verbe est à un temps composé
                                                if (verbeConjugue.IsTempsCompose())
                                                {
                                                    // On récupère l'auxiliaire.
                                                    Verb auxConjugue = new ConjugatedVerb(verbeConjugue.GetAuxiliaireConjugue(), verbeConjugue.Person);
                                                    // On le passe à l'infinitif.
                                                    Verb.ToInfinitive(ref auxConjugue);

                                                    // Si le verbe utilise l'auxiliaire avoir, et que le nom complément est accordé avec le participe passé,
                                                    // le pronom relatif remplace le nom complément (car avec avoir, le participe passé s'accorde avec le COD
                                                    // si ce dernier se situe avant le verbe).
                                                    if (auxConjugue.ToString() == "avoir" && SontAccordes(nomComplement, verbeConjugue))
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });

                                                    // Si le verbe utilise l'auxiliaire être, et que le nom complément est accordé avec le verbe,
                                                    // le pronom relatif remplace le nom complété.
                                                    else if (auxConjugue.ToString() == "avoir" && SontAccordes(nomComplete, verbeConjugue))
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplete });

                                                    // Autrement, le cas par défaut est que le pronom relatif remplace le nom précédent, donc nom complément.
                                                    else
                                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });
                                                }
                                                #endregion

                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    // Sinon, si le pronom relatif est "dont", "auquel", "où"...
                                    #region Cas où le pronom relatif est "dont", "auquel", "où"
                                    else if (new string[] { "dont", "auquel", "où" }.Contains(pronom.ToString()))
                                    {
                                        phraseFonction.Add(new Word[] { pronom, new LinkBetweenWord("REMPLACE"), nomComplement });
                                    }
                                    #endregion

                                }
                            }
                        }
                    }

                    // TODO: Traiter les rares cas où le complément du nom n'est pas séparé du nom par une préposition.

                }
            }
        }

        // Renvoie une liste de byte contenant les indices de tous les mots de la nature
        // spécifiée en paramètre.
        private static List<int> GetListIndexOf(string nature, List<Word> phrase)
        {
            List<int> lesIndex = new List<int>();

            for (int i = 0; i < phrase.Count; i++)
            {
                if (phrase[i].Nature == nature)
                {
                    lesIndex.Add(i);
                    
                }
            }

            return lesIndex;
        }
        private static List<int> GetListIndexOf(string nature1, string nature2, List<Word> phrase)
        {
            List<int> lesIndex = new List<int>();

            for (int i = 0; i < phrase.Count; i++)
            {
                if (phrase[i].Nature == nature1 || phrase[i].Nature == nature2)
                {
                    lesIndex.Add(i);

                }
            }

            return lesIndex;
        }

        private static List<List<Word>> DecomposerPhraseEnPropositions(List<Word> phrase, List<int> lesVerbesConjugues)
        {
            // On parcourt la phrase à la recherche de conjonctions de subordination ou de coordination
            // dans le but de découper la phrase en différentes propositions.
            List<int> lesConjonctions = GetListIndexOf("conjDeSub", "conjDeCoord", phrase);

            // On crée une liste qui va contenir les différentes propositions de la phrase en cours de traitement.
            List<List<Word>> lesPropositions = new List<List<Word>>();

            // Si la phrase possède des conjonctions...
            if (lesConjonctions.Count() > 0)
            {
                int indexInf = 0;
                int indexSup = lesConjonctions[0];
                int taille = indexSup - indexInf;
                lesPropositions.Add(phrase.GetRange(indexInf, indexSup));
                for (int i = 1; i < lesConjonctions.Count(); i++)
                {
                    indexInf = indexSup;
                    indexSup = lesConjonctions[i];
                    taille = indexSup - indexInf;
                    lesPropositions.Add(phrase.GetRange(indexInf, taille));
                }
                lesPropositions.Add(phrase.GetRange(indexSup, phrase.Count() - indexSup));
            }
            // Sinon si la phrase ne contient pas de conjonction, la proposition est l'intégralité de la phrase
            else
                lesPropositions.Add(phrase);

            // Affichage
            Console.WriteLine(" ----- Affichage découp propositions -----");
            foreach (List<Word> proposition in lesPropositions)
            {
                foreach(Word mot in proposition)
                {
                    Console.Write(mot.ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" ---------------------------------------- \n");

            return lesPropositions;
        }


       
        // Procédure utilisée par la fonction DecouperPhrase (ci-dessus)
        private static string TraiterMotsComprenantApostropheOuEspace(string mot, ref List<List<object[]>> unePhrase)
        {
            string[] exceptionsApostrophe = new string[] { "aujourd'hui", "d'accord", "d'abord", "quelqu'un", "presqu'île" };

            // On ne peut tester le mot précédent dans la liste que si la liste contient au moins
            // un élément.
            if (unePhrase.Count > 0)
            {
                string dernierMot = (string)unePhrase.Last()[0][0];
                // On traite les mots comprenant des apostrophes afin qu'ils soient considérés
                // comme un seul et et non comme deux.
                if (exceptionsApostrophe.Contains(dernierMot + mot))
                {
                    unePhrase.RemoveAt(unePhrase.Count - 1);
                    mot = dernierMot + mot;
                }
            }

            return mot;
        }
    }
}