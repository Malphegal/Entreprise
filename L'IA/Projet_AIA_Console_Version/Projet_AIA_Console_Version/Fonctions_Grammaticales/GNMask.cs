using Projet_AIA_Console_Version.Natures_Grammaticales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.Fonction;
using static Projet_AIA_Console_Version.Fonctions_Grammaticales.FonctionMask;

namespace Projet_AIA_Console_Version.Fonctions_Grammaticales
{
    public static class GNMask
    {
        public static Mask[] allGNMask = new Mask[6];
        public static int TailleMaxMask { get; } = 4;

        public static void CreateGNMaskList()
        {
            // le gentil chat blanc
            allGNMask[0] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminant", Condition.None),
                                            MyTuple.New("adjectif", Condition.None),
                                            MyTuple.New("nom", Condition.None),
                                            MyTuple.New("adjectif", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 2),
                                                MyTuple.New(Function.épithèteDe, 1),
                                                MyTuple.New(Function.sujetDe, 0),
                                                MyTuple.New(Function.épithèteDe, -1)));

            // le gentil chat 
            allGNMask[1] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminant", Condition.None),
                                            MyTuple.New("adjectif", Condition.None),
                                            MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 2),
                                                MyTuple.New(Function.épithèteDe, 1),
                                                MyTuple.New(Function.sujetDe, 0)));

            // le chat blanc
            allGNMask[2] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminant", Condition.None),
                                            MyTuple.New("nom", Condition.None),
                                            MyTuple.New("adjectif", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 1),
                                                MyTuple.New(Function.sujetDe, 0),
                                                MyTuple.New(Function.épithèteDe, -1)));


            // le chat 
            allGNMask[3] = new Mask(
                CreateListNatureMaskNugget( MyTuple.New("déterminant", Condition.None),
                                            MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(   MyTuple.New(Function.déterminentDe, 1),
                                                MyTuple.New(Function.sujetDe, 0)));

            // chat 
            allGNMask[4] = new Mask(
                CreateListNatureMaskNugget(MyTuple.New("nom", Condition.None)),
                CreateListFonctionMaskNugget(MyTuple.New(Function.sujetDe, 0)));

            // il 
            allGNMask[5] = new Mask(
                CreateListNatureMaskNugget(MyTuple.New("pronom", Condition.PronPers)),
                CreateListFonctionMaskNugget(MyTuple.New(Function.sujetDe, 0)));
        }

    }
}
