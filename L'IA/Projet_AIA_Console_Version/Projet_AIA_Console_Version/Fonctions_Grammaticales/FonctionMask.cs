using Projet_AIA_Console_Version.Natures_Grammaticales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.Fonction;

namespace Projet_AIA_Console_Version.Fonctions_Grammaticales
{
    public static class FonctionMask
    {
        public static Mask[] allMask = new Mask[5];

        public enum Condition
        {
            None,
            PronPers,
            VerbEtat,
            PasVerbEtat
        }

        public struct NatureMaskNugget
        {
            public string nature;
            public Condition condition;

            public NatureMaskNugget(string nature, Condition condition)
            {
                this.nature = nature;
                this.condition = condition;
            }
        }

        public struct FonctionMaskNugget
        {
            public Function fonction;
            public int cibleLien;  // 0 : origine, mot courant. Si -1 : mot précédent. Si 1 : mot suivant. Etc.

            public FonctionMaskNugget(Function fonction, int cibleLien)
            {
                this.fonction = fonction;
                this.cibleLien = cibleLien;
            }
        }

        public struct Mask
        {
            public NatureMaskNugget[] natureMask;
            public FonctionMaskNugget[] fonctionMask;
            public int size;

            public Mask(NatureMaskNugget[] natureMask, FonctionMaskNugget[] fonctionMask)
            {
                this.natureMask = natureMask;
                this.fonctionMask = fonctionMask;
                this.size = natureMask.Length;
            }
        }

        public static void CreateMaskList()
        {
            /*
            // il mange
            allMask[0] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("pronom", Condition.PronPers),
                                            MyTuple.New("verbe conjugué", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0)));

            // il mange bien
            allMask[1] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("pronom", Condition.PronPers),
                                            MyTuple.New("verbe conjugué", Condition.None),
                                            MyTuple.New("adverbe", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0),
                                                MyTuple.New(Function.adverbeDe, -1)));

            // il mange la pomme
            allMask[2] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("pronom", Condition.PronPers),
                                            MyTuple.New("verbe conjugué", Condition.PasVerbEtat),
                                            MyTuple.New("déterminent", Condition.None),
                                            MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0),
                                                MyTuple.New(Function.déterminentDe, 1),
                                                MyTuple.New(Function.codDe, -2)));

            // le chat mange la pomme
            allMask[3] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminent", Condition.None),
                                            MyTuple.New("nom", Condition.None),
                                            MyTuple.New("verbe conjugué", Condition.PasVerbEtat),
                                            MyTuple.New("déterminent", Condition.None),
                                            MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 1),
                                                MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0),
                                                MyTuple.New(Function.déterminentDe, 1),
                                                MyTuple.New(Function.codDe, -2)));

            // le gentil chat 
            allMask[4] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminent", Condition.None),
                                            MyTuple.New("adjectif", Condition.None),
                                            MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 2),
                                                MyTuple.New(Function.épithèteDe, 1),
                                                MyTuple.New(Function.sujetDe, 0)));
            */

            allMask[0] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("GN", Condition.None),
                                            MyTuple.New("verbe conjugué", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0)));

            allMask[1] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("GN", Condition.None),
                                            MyTuple.New("verbe conjugué", Condition.None),
                                            MyTuple.New("GN", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.sujetDe, 1),
                                                MyTuple.New(Function.verbe, 0),
                                                MyTuple.New(Function.codDe, -1)));
        }

        public static NatureMaskNugget[] CreateListNatureMaskNugget(params MyTuple<string, Condition>[] natureMaskNugget)
        {
            NatureMaskNugget[] res = new NatureMaskNugget[natureMaskNugget.Length];
            for (int i = 0; i < natureMaskNugget.Length; i++)
                res[i] = new NatureMaskNugget(natureMaskNugget[i].First, natureMaskNugget[i].Second);
            return res;
        }

        public static FonctionMaskNugget[] CreateListFonctionMaskNugget(params MyTuple<Function, int>[] fonctionMaskNugget)
        {
            FonctionMaskNugget[] res = new FonctionMaskNugget[fonctionMaskNugget.Length];
            for (int i = 0; i < fonctionMaskNugget.Length; i++)
                res[i] = new FonctionMaskNugget(fonctionMaskNugget[i].First, fonctionMaskNugget[i].Second);
            return res;
        }

        // Renvoie True si la condition passée en paramètre est vraie, sinon False
        public static bool EvaluerCondition(Condition condition, Word mot)
        {
            switch (condition)
            {
                case Condition.None:
                    return true;
                case Condition.PronPers:
                    Pronoun pronom = (Pronoun)mot;
                    return pronom.NatureDetaillee == "pronompersonnel";
                case Condition.VerbEtat:
                    ConjugatedVerb verbe = (ConjugatedVerb)mot;
                    return ConjugatedVerb.IsAStateVerb(verbe);
                case Condition.PasVerbEtat:
                    ConjugatedVerb verbe2 = (ConjugatedVerb)mot;
                    return !ConjugatedVerb.IsAStateVerb(verbe2);
            }
            return false;
        }
    }
}
