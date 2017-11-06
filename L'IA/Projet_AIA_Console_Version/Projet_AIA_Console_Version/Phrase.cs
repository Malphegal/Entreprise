﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Projet_AIA_Console_Version
{
    static class Phrase
    {
        static private CON con = new CON(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\IA.MDB");
        static CMD cmd = new CMD("SELECT * FROM Pronoms ORDER BY nbrUtilisation DESC", con);
        static OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        public static DataSet lesData = new DataSet(); // DataSet contenant toutes les DataTables nécessaires


        public static void CreationDataSets()
        {
            da.Fill(lesData, "Pronoms");
            cmd.CommandText = "SELECT * FROM Determinants ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "Determinants");
            cmd.CommandText = "SELECT * FROM DeterminantsAccords";
            da.Fill(lesData, "DeterminantsAccords");
            cmd.CommandText = "SELECT * FROM ConjDeCoords ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "ConjDeCoords");
            cmd.CommandText = "SELECT * FROM Prepositions ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "Prepositions");
            cmd.CommandText = "SELECT * FROM ConjDeSubs ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "ConjDeSubs");
            cmd.CommandText = "SELECT * FROM Adverbes ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "Adverbes");
            cmd.CommandText = "SELECT * FROM VerbesConjugues ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "VerbesConjugues");
            cmd.CommandText = "SELECT * FROM Noms ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "Noms");
            cmd.CommandText = "SELECT * FROM NomsExceptions";
            da.Fill(lesData, "NomsExceptions");
            cmd.CommandText = "SELECT * FROM NomsAccords";
            da.Fill(lesData, "NomsAccords");
            cmd.CommandText = "SELECT * FROM Adjectifs ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "Adjectifs");
            cmd.CommandText = "SELECT * FROM AdjectifsAccords";
            da.Fill(lesData, "AdjectifsAccords");
            cmd.CommandText = "SELECT Infinitif, sum(nbrUtilisation) AS nbrUtilisation FROM VerbesConjugues GROUP BY Infinitif, nbrUtilisation ORDER BY nbrUtilisation DESC ";
            da.Fill(lesData, "VerbesInfinitifs");
            cmd.CommandText = "SELECT * FROM AdjectifsExceptionsS2P ORDER BY GroupS2P ASC";
            da.Fill(lesData, "AdjectifsExceptionsS2P");
            cmd.CommandText = "SELECT * FROM AdjectifsExceptionsM2F ORDER BY GroupM2F ASC";
            da.Fill(lesData, "AdjectifsExceptionsM2F");
        }

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
                        positionDuPoint = reponseUtilisateur.Length-1;
                    phrase = reponseUtilisateur.Substring(startPhrase, positionDuPoint - startPhrase + 1).Trim();
                    // Extraction des mots depuis la phrase
                    do
                    {
                        positionDuSeparateur = phrase.IndexOfAny(new char[] { ' ', '\'' }, startMot);
                        if (positionDuSeparateur >= 0)
                        {
                            mot = phrase.Substring(startMot, positionDuSeparateur - startMot + 1).Trim();
                            unePhrase.Add(new List<object[]> { new object[] { mot, 0 } });    // on ajoute le mot dans le tableau des mots
                            startMot = positionDuSeparateur + 1;
                            compteurMot++;
                        }
                        else if (positionDuSeparateur == -1)
                            unePhrase.Add(new List<object[]> { new object[] { phrase.Substring(startMot).Trim('.') } });    // on ajoute le dernier mot dans le tableau des mots
                    } while (positionDuSeparateur > 0);
                    positionDuSeparateur = 0;
                    startMot = 0;
                    compteurMot = 0;
                    lesPhrases.Add(unePhrase);  // on ajoute la phrase dans la liste des phrases
                    startPhrase = positionDuPoint + 1;
                }
                unePhrase = new List<List<object[]>>(16);
            } while (positionDuPoint > 0 && positionDuPoint < reponseUtilisateur.Length-1);  // si <=0, c'est qu'il n'y a plus de point
            return lesPhrases;
        }

        public static void DeterminerLesNatures(List<List<object[]>> laPhrase)
        {
            int i;
            string motCourant;
            int tailleMotCourant;


            //  =======================================================================================================
            //  ETAPE 1
            // ========================================================================================================
            // On cherche à attribuer une nature unique à chaque mot de la phrase. Pour cela, on parcourt les mots de la
            // phrase. On note dans un premier temps toutes les natures possibles pour chaque mot. Puis, on trie les natures
            // possibles en fonction du nombre d'utilisation de ce mot pour cette nature, et on ne conserve que la nature
            // ayant le nombre d'utilisation le plus élevé.

            for (i = 0; i < laPhrase.Count; i++) // on parcourt chaque mot de la phrase
            {
                // On parcourt la liste de chaque natures grammaticales suivantes.
                // Si le mot courant est contenu dans une de ces listes, alors on a trouvé sa nature.
                motCourant = (string)laPhrase[i][0][0];
                tailleMotCourant = motCourant.Length;

                // PREPOSITIONS
                rechercheDansTable("Prepositions", i, motCourant, laPhrase);

                if (tailleMotCourant > 1)   // car un mot d'une seule lettre ne peut pas être des natures suivantes
                {
                    // CONJONCTIONS DE COORDINATION
                    rechercheDansTable("ConjDeCoords", i, motCourant, laPhrase);

                    // CONJONCTIONS DE SUBORDINATION
                    rechercheDansTable("ConjDeSubs", i, motCourant, laPhrase);

                    // DETERMINANTS
                    rechercheDansTable("Determinants", i, motCourant, laPhrase);

                    // PRONOMS
                    rechercheDansTable("Pronoms", i, motCourant, laPhrase);

                    // VERBES INFINITIFS
                    rechercheDansTable("VerbesInfinitifs", i, motCourant, laPhrase);

                    // VERBES CONJUGUÉS et NOMS et ADJECTIFS
                    if (laPhrase[i].Count == 1) // car si le mot est déjà d'une autre nature, il ne peut pas être un verbe, un nom ou un adjectif
                    {
                        // Verbe
                        rechercheDansTable("VerbesConjugues", i, motCourant, laPhrase);
                        // Nom
                        rechercheDansTable("Noms", i, motCourant, laPhrase);
                        // Adjectif
                        rechercheDansTable("Adjectifs", i, motCourant, laPhrase);
                    }
                    // Si on a rien trouvé, et que le mot courant n'a toujours pas de nature attribuée
                    if (laPhrase[i].Count == 1 && i > 0)    // on teste i > 0 pour éviter les IndexOutOfRangeException avec laPhrase[i - 1]
                    {
                        // Verbe
                        // Si le mot précédent est un pronom ou un nom, alors le mot courant peut être un verbe
                        if (new string[] { "Pronoms", "Noms" }.Contains((string)laPhrase[i - 1][1][0]))
                            laPhrase[i].Add(new object[] { "VerbesConjugues", 0 });
                        // Nom
                        // Si le mot précédent est un déterminant, alors le mot courant peut être un nom
                        var testMotPrecedent = (from item in laPhrase[i - 1] where (string)item[0] == "Determinants" select item);
                        if (testMotPrecedent != null && testMotPrecedent.Count() != 0)
                            laPhrase[i].Add(new object[] { "Noms", 0 });
                        // Adjectif
                        // Si le mot précédent est un nom, alors le mot courant peut être un adjectif
                        testMotPrecedent = (from item in laPhrase[i - 1] where (string)item[0] == "Noms" select item);
                        // Si le mot précédent est le verbe être, alors le mot courant peut être un adjectif
                        var testVerbeEtre = (from str in lesData.Tables["VerbesConjugues"].AsEnumerable()
                                     where (string)str["Infinitif"] == "être"
                                     select str).ToList();

                        // Afin de convertir la List de DataRow en List de String
                        List<string> ListString = new List<string>();
                        foreach (var row in testVerbeEtre)
                            ListString.Add(row[0].ToString());
                        // ------------------

                        if ((testMotPrecedent != null && testMotPrecedent.Count() != 0) || ListString.Contains(laPhrase[i - 1][0][0]))
                        {
                            laPhrase[i].Add(new object[] { "Adjectif", 0});
                        }
                    }

                    // ADVERBES
                    // Comme la table Adverbes ne contient pas tous les adverbes existants
                    // (car il en existe beaucoup trop), on repere aussi les adverbes par leur
                    // terminaison en -ment

                    if (motCourant.Length > 4 && motCourant.Contains("ment"))
                        laPhrase[i].Add(new object[] { "Adverbes", 0 });
                    rechercheDansTable("Adverbes", i, motCourant, laPhrase);

                }


                // Une fois que l'on a trouvé toutes les natures possibles pour le mot courant,
                // on les trie de la nature la plus probable à la nature la moins probable,
                // cela en utilisant le nbrUtilisation associé à chacune des natures
                // pour le mot en question.

                // On trie les natures du mot courant ( laPhrase[i] ) de la plus probable à la moins probable 
                laPhrase[i].Skip(1).OrderBy(imp => (int)imp[1]);
            }


            //  =======================================================================================================
            //  ETAPE 2
            // ========================================================================================================
            // Maintenant qu'on a attribué une unique nature à chaque mot, on cherche à déterminer le genre et le nombre
            // des mots variables et la personne (+ nombre) et le temps des verbes. On stocke ces informations en transformant
            // chaque string contenant un mot de la phrase en type correspondant à sa nature (structure)

            List<object> phrase = new List<object>();

            for (i = 0; i < laPhrase.Count; i++) // Pour chaque mot de la phrase...
            {
                switch((string)laPhrase[i][1][0])   // laPhrase[i][1][0] → correspond à la nature
                {
                    case "Noms":
                        phrase.Add(new Names.Name((string)laPhrase[i][0][0]));
                        break;
                    case "Adjectifs":
                        phrase.Add(new Adjectives.Adjective((string)laPhrase[i][0][0]));
                        break;
                }
            }

        }

        // Procédure utilisée par la fonction DeterminerLesNatures (ci-dessus)
        // Effectue la recherche d'un mot de la phrase dans une table spécifiée et note sa nature (qui est le nom de la table) s'il est trouvé
        private static void rechercheDansTable(string nomDeLaTable, int indexDuMotDeLaPhrase, string motCourant, List<List<object[]>> laPhrase)
        {
            for (int j = 0; j < lesData.Tables[nomDeLaTable].Rows.Count; j++)   // On parcourt la table
            {
                if ((string)lesData.Tables[nomDeLaTable].Rows[j][0] == motCourant)  // Si on trouve le mot courant dans la table
                {
                    laPhrase[indexDuMotDeLaPhrase].Add(new object[] { nomDeLaTable, lesData.Tables[nomDeLaTable].Rows[j]["nbrUtilisation"] });
                    break;
                }
            }
        }
    }
}
