using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version.Natures_Grammaticales
{
    public abstract class VariableWord : Word
    {
            // PROPRIETES

        public string Gender        { get; protected set; }   // Le genre du mot : féminin ou masculin
        public string Number        { get; protected set; }   // Le nombre du mot : pluriel ou singulier
        public string NumberBase    { get; protected set; }
        public string GenderBase    { get; protected set; }

            // METHODS

        public bool IsSingular()
        {
            return this.Number == "S";
        }

        public bool IsPlurial()
        {
            return this.Number == "P";
        }

        public bool IsMale()
        {
            return this.Gender == "M";
        }

        public bool IsFemale()
        {
            return this.Gender == "F";
        }

    }
}
