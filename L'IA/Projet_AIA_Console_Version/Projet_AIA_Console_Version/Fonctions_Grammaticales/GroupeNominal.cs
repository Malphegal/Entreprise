using Projet_AIA_Console_Version.Natures_Grammaticales;
using System.Collections.Generic;
using System.Linq;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.Fonction;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.FonctionMask;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.GNMask;

namespace Projet_AIA_Console_Version.Fonctions_Grammaticales
{
    public class GroupeNominal : Word
    {
        readonly MotFonction[] composants;

        public GroupeNominal(params MotFonction[] mots)
        {
            this.Nature = "GN";
            this.composants = mots;
        }

        public string DescriptifFonction()
        {
            string lesMots = "";
            for (int i = 0; i < composants.Length; i++)
                lesMots += composants[i].mot.ToString() + " " + ToStringFunction(composants[i].lien) + " " + composants[i].cibleLien.ToString() + "  ";
            return lesMots;
        }

        public void ModifierLienChefGN(Function lien, Word cibleLien)
        {
            for (int i = 0; i < composants.Length; i++)
            {
                if (composants[i].mot.Nature == "nom" || composants[i].mot.Nature == "pronom")
                {
                    composants[i].lien = lien;
                    composants[i].cibleLien = cibleLien;
                }
            }
        }

        public static List<Word> RassemblerGN(List<Word> phraseArg)
        {
            List<MyTuple<Mask, int, int>> tousLesMaskTrouves = TrouverGroupesNominaux(phraseArg);
            List<Word> phrase = new List<Word>(phraseArg);
            GroupeNominal gn = new GroupeNominal();

            int reducTaillePhraseOld = 0;
            int reducTaillePhraseNew = 0;

            // Pour chaque GN trouvé, on rassemble les mots qu'il comprend
            for (int i = 0; i < tousLesMaskTrouves.Count; i++)
            {
                reducTaillePhraseNew += tousLesMaskTrouves[i].Third - 1;

                // On crée un groupe nominal qui contient les mots pourvus de leur fonction
                gn = new GroupeNominal(CreateMotFonction(phraseArg.GetRange(tousLesMaskTrouves[i].Second, tousLesMaskTrouves[i].Third), tousLesMaskTrouves[i].First.fonctionMask).ToArray());

                // On supprime ces mots de la phrase et on y insert le gn à la place
                phrase.RemoveRange(tousLesMaskTrouves[i].Second - reducTaillePhraseOld, tousLesMaskTrouves[i].Third);
                phrase.Insert(tousLesMaskTrouves[i].Second - reducTaillePhraseOld, gn);

                reducTaillePhraseOld = reducTaillePhraseNew;
            }

            return phrase;
        }

        private static List<MyTuple<Mask, int, int>> TrouverGroupesNominaux(List<Word> phrase)
        {
            List<MyTuple<Mask, int, int>> res = new List<MyTuple<Mask, int, int>>(phrase.Count / 2);

            // Si la taille de la phrase est inférieure à la taille du plus grand des mask, on commence à partir des mask de la taille de la phrase.
            int tailleMaxMask = phrase.Count < TailleMaxMask ? phrase.Count : TailleMaxMask;

            List<int> indicesDejaTraites = new List<int>(phrase.Count());

            // On parcourt tous les mask (qui sont classés du plus grand au plus petit)
            for (int indMask = 0; indMask < allGNMask.Length; indMask++)
            {
                int maskLength = allGNMask[indMask].size;   // On récupère la taille du mask courant

                // On fait "glisser" le mask sur la phrase, jusqu'à ce que les natures entre le mask et la phrase correspondent
                for (int startIndex = 0; startIndex + maskLength <= phrase.Count; startIndex++)
                {
                    int compteurSimilitudes = 0;
                    for (int j = 0; j < maskLength; j++)
                    {
                        if (phrase[startIndex + j].Nature == allGNMask[indMask].natureMask[j].nature)
                            compteurSimilitudes++;
                    }

                    if (compteurSimilitudes == maskLength)
                    {
                        // Si le début du mask est compris dans une séquence de la phrase appartenant déjà à un mask, on n'ajoute pas le nouveau mask
                        if (!indicesDejaTraites.Contains(startIndex))
                        {
                            res.Add(new MyTuple<Mask, int, int>(allGNMask[indMask], startIndex, maskLength));

                            for (int indiceMotTraite = startIndex; indiceMotTraite < startIndex + maskLength; indiceMotTraite++)
                                indicesDejaTraites.Add(indiceMotTraite);
                        }
                    }
                }
            }

            // Si on n'a trouvé aucun mask de GN pour cette phrase...
            if (res.Count == 0)
                throw new GroupeNominalNotFound("Error : GN Mask not found\n");
            else
                return res;
        }
    }
}
