using Projet_AIA_Console_Version.Natures_Grammaticales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.FonctionMask;

namespace Projet_AIA_Console_Version.Fonctions_Grammaticales
{
    public static class Fonction
    {
        // Toutes les fonctions grammaticales possibles pour un mot
        public enum Function
        {
            verbe,
            sujetDe,
            déterminentDe,
            codDe,                  // COD
            coiDe,                  // COI
            cosDe,                  // COS
            attributDe,             // attribut (du sujet ou du cod)
            épithèteDe,
            adverbeDe,
            cclieuDe,               // complément circonstanciel de lieu
            cctempsDe,              // complément circonstanciel de temps
            ccmaniereDe,            // complément circonstanciel de manière
            ccmoyenDe,              // complément circonstanciel de moyen
            ccbutDe,                // complément circonstanciel de but
            cccauseDe,              // complément circonstanciel de cause
            ccconséquenceDe,        // complément circonstanciel de conséquence
            ccconditionDe,          // complément circonstanciel de condition
            ccconcessionDe,         // complément circonstanciel de concession
            cccomparaisonDe,        // complément circonstanciel de comparaison
            ccquantitéDe            // complément circonstanciel de quantité

            #region Documentation
            // Compléments circonstanciels :
            // http://la-conjugaison.nouvelobs.com/regles/grammaire/le-complement-circonstanciel-80.php 
            #endregion
        }

        // Le triplet mot - type de lien - cible du lien. Ex : mot chat - sujetDe - mot mange
        public struct MotFonction
        {
            public Word mot;
            public Function lien;
            public Word cibleLien;

            public MotFonction(Word mot, Function lien, Word cibleLien)
            {
                this.mot = mot;
                this.lien = lien;
                this.cibleLien = cibleLien;
            }

            public string DescriptifFonction()
            {
                if (this.mot.Nature == "GN")
                    return ((GroupeNominal)this.mot).DescriptifFonction();
                return this.mot.ToString() + " " + ToStringFunction(this.lien) + " " + this.cibleLien.ToString();
            }
        }

        // Attribue une fonction grammaticale à chaque mot de la phrase.
        public static List<MotFonction> CreateMotFonction(List<Word> phrase, FonctionMaskNugget[] listFonction)
        {
            List<MotFonction> resFonction = new List<MotFonction>();

            for (int i = 0; i < phrase.Count(); i++)
            {
                if (phrase[i].Nature == "GN")
                {
                    GroupeNominal gn = (GroupeNominal)phrase[i];
                    gn.ModifierLienChefGN(listFonction[i].fonction, phrase[i + listFonction[i].cibleLien]);
                }

                resFonction.Add(new MotFonction(phrase[i], listFonction[i].fonction, phrase[i + listFonction[i].cibleLien]));
            }

            return resFonction;
        }

        // Renvoie le meilleur mask de fonctions pour la phrase entrée en paramètres
        private static Mask DeterminerMask(List<Word> phrase)
        {
            int nbCorrespondanceOld = 0;
            Mask bestMask = new Mask(); // Si on n'a trouvé aucun mask, on renvoie un mask vide

            foreach (Mask mask in allMask)
            {
                // On ne retient que les mask dont la taille est identique à celle de la phrase
                if (mask.size == phrase.Count())
                {
                    // On parcourt la liste des natures des mask; on compare avec la phrase, et on compte les correspondances
                    int nbCorrespondance = 0;
                    for (int i = 0; i < phrase.Count(); i++)
                    {
                        if (phrase[i].Nature == mask.natureMask[i].nature && EvaluerCondition(mask.natureMask[i].condition, phrase[i]))
                            nbCorrespondance++;
                    }

                    // Si le mask courant possède davantage de correspondance que le précédent, on le retient
                    if (nbCorrespondance > nbCorrespondanceOld)
                    {
                        bestMask = mask;
                        nbCorrespondanceOld = nbCorrespondance;
                    }
                }
            }

            // On renvoie le mask retenu (avec le plus haut nombre de correspondances, ou un mask vide si aucun mask n'a été trouvé)
            return bestMask;
        }

        public static List<MotFonction> DeterminerLesFonctions(List<Word> phrase)
        {
            // Si la phrase ne contient qu'un seul mot, on ne cherche pas à déterminer sa fonction : on renvoie null
            if (phrase.Count < 2)
                return null;

            // Si null, c'est qu'on n'a pas trouvé de mask... et donc qu'on n'a pas compris la phrase (format inconnu)
            if (DeterminerMask(phrase).fonctionMask == null)
                throw new FunctionsNotFound("Error : Mask not found\n");

            return CreateMotFonction(phrase, DeterminerMask(phrase).fonctionMask);
        }

        public static string ToStringFunction(Function fonction)
        {
            switch (fonction)
            {
                case Function.verbe:
                    return "verbe";
                case Function.sujetDe:
                    return "sujet de";
                case Function.déterminentDe:
                    return "déterminent de";
                case Function.codDe:
                    return "COD de";
                case Function.coiDe:
                    return "COI de";
                case Function.cosDe:
                    return "COS de";
                case Function.attributDe:
                    return "attribut de";
                case Function.épithèteDe:
                    return "épithète de";
                case Function.adverbeDe:
                    return "adverbe de";
                case Function.cclieuDe:
                    return "CC Lieu de";
                case Function.cctempsDe:
                    return "CC Temps de";
                case Function.ccmaniereDe:
                    return "CC Manière de";
                case Function.ccmoyenDe:
                    return "CC Moyen de";
                case Function.ccbutDe:
                    return "CC But de";
                case Function.cccauseDe:
                    return "CC Cause de";
                case Function.ccconséquenceDe:
                    return "CC Conséquence de";
                case Function.ccconditionDe:
                    return "CC Condition de";
                case Function.ccconcessionDe:
                    return "CC Concession de";
                case Function.cccomparaisonDe:
                    return "CC Comparaison de";
                case Function.ccquantitéDe:
                    return "CC Quantité de";
                default:
                    return "";
            }
        }
    }
}
