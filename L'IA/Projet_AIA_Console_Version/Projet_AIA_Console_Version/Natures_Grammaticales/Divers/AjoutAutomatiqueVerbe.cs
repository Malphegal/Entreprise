using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conjugaison_verbes.Conjugaison
{
    class AjoutAutomatiqueVerbe
    {
        public static void triVerbes()
        {
            string[] lesVerbes = System.IO.File.ReadAllLines(@"..\..\..\lesVerbes.txt");
            List<string[]> verbesSplitSautsLigne = new List<string[]>();
            List<string[]> verbesSplitTires = new List<string[]>();
            List<string> verbesTrim = new List<string>();
            foreach (string item in lesVerbes)
            {
                Console.WriteLine(item);
                Console.ReadKey(true);
            }
            foreach (string item in lesVerbes)
            {
                verbesSplitSautsLigne.Add(item.Split('\n'));
            }
            foreach (string[] item in verbesSplitSautsLigne)
            {
                foreach (string item2 in item)
                    verbesSplitTires.Add(item2.Split('-'));
            }
            foreach (string[] item in verbesSplitTires)
            {
                for (int j = 0; j < item.Length; j++)
                    item[j] = item[j].Trim(' ');
            }
            int i = 0;
            foreach (string[] item in verbesSplitTires)
            {
                System.IO.File.WriteAllLines(@"..\..\..\lesVerbesTries" + i + ".txt", item.ToArray(), Encoding.UTF8);
                i++;
            }
        }
    }
}
