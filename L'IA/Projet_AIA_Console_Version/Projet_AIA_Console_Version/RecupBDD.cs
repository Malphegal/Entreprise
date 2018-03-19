using Projet_AIA_Console_Version.Fonctions_Grammaticales;
using System.Data;
using System.Data.OleDb;
using CMD = System.Data.OleDb.OleDbCommand;
using CON = System.Data.OleDb.OleDbConnection;

namespace Projet_AIA_Console_Version
{
    static class RecupBDD
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
            /*cmd.CommandText = "SELECT Infinitif AS Verbe, sum(nbrUtilisation) AS nbrUtilisation, Groupe FROM VerbesConjugues GROUP BY Infinitif, nbrUtilisation, Groupe ORDER BY nbrUtilisation DESC ";
            da.Fill(lesData, "VerbesInfinitifs");*/
            cmd.CommandText = "SELECT * FROM VerbesInfinitifs ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "VerbesInfinitifs");
            cmd.CommandText = "SELECT * FROM VerbesInfinitifs ORDER BY nbrUtilisation DESC";
            da.Fill(lesData, "InfosVerbesInfinitifs");
            cmd.CommandText = "SELECT * FROM AdjectifsExceptionsS2P ORDER BY GroupS2P ASC";
            da.Fill(lesData, "AdjectifsExceptionsS2P");
            cmd.CommandText = "SELECT * FROM AdjectifsExceptionsM2F ORDER BY GroupM2F ASC";
            da.Fill(lesData, "AdjectifsExceptionsM2F");
            cmd.CommandText = "SELECT * FROM Conjugaison";
            da.Fill(lesData, "Conjugaison");
            // à supprimer
            cmd.CommandText = "SELECT Infinitif, Groupe FROM VerbesConjugues GROUP BY Infinitif, Groupe";
            da.Fill(lesData, "InsertInfinitive");


            FonctionMask.CreateMaskList();
        }
    }
}
