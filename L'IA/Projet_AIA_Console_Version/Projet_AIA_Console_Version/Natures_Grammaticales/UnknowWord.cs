using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    public class UnknowWord : Word
    {
            // CHAMP

        private string _mot;

            // CONSTRUCTEUR

        public UnknowWord(string mot)
        {
            this._mot = mot;
            this.Nature = "inconnue";
        }

            // METHODES

        public override string ToString()
        {
            return this._mot;
        }
    }
}
