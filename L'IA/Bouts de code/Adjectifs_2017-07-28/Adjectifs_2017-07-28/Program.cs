using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adjectifs_2017_07_28.Adjectifs;
using static Adjectifs_2017_07_28.Adjectifs.Adjectives;

namespace Adjectifs_2017_07_28
{
    class Program
    {
        static void Main(string[] args)
        {
            string resGenre;
            string resNombre;
            while (true)
            {
                Console.Write("Adjectif ?\t");
                Adjective adjective = new Adjective(Console.ReadLine());
                if (adjective.gender == "M")
                    resGenre = "masculin";
                else
                    resGenre = "féminin";
                if (adjective.number == "S")
                    resNombre = "singulier";
                else
                    resNombre = "pluriel";
                Console.WriteLine(adjective + " est un " + resGenre + " " + resNombre);
                if (adjective.isMale() && adjective.isSingular())
                {
                    Console.WriteLine("Au féminin singulier, il donne : " + adjective.maleSingularToFemaleSingular());
                    adjective.femaleSingularToMaleSingular();
                    Console.WriteLine("Au masculin pluriel, il donne : " + adjective.singularMaleToPlurialMale());
                    adjective.plurialMaleToSingularMale();
                    Console.WriteLine("Au féminin pluriel, il donne : " + adjective.singularMaleToPlurialFemale());
                    adjective.plurialFemaleToSingularMale();
                }
                else if (adjective.isFemale() && adjective.isSingular())
                {
                    Console.WriteLine("Au masculin singulier, il donne : " + adjective.femaleSingularToMaleSingular());
                }
                else if (adjective.isMale() && adjective.isPlurial())
                {
                    Console.WriteLine("Au masculin singulier, il donne : " + adjective.plurialMaleToSingularMale());
                }
                else if (adjective.isFemale() && adjective.isPlurial())
                {
                    Console.WriteLine("Au masculin singulier, il donne : " + adjective.plurialFemaleToSingularMale());
                }
                Console.WriteLine("");
            }
        }
    }
}
