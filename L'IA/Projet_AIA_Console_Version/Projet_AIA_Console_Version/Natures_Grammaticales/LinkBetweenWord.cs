using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    public class LinkBetweenWord : Word
    {
        // CHAMP

        private string _mot;

        // CONSTRUCTEUR

        public LinkBetweenWord(string mot)
        {
            this._mot = mot;
            this.Nature = "link";
        }

        // METHODES

        public override string ToString()
        {
            return this._mot;
        }
    }
}
