using System.Collections;
using System.Xml;
using UnityEngine;

//string[] s = System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories);
//Lang l = new Lang(s[0], "French");

public static class Lang
{
    private static Hashtable _allStrings;
    public static void DefineLanguage(string path, string language)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);

        _allStrings = new Hashtable();
        XmlElement element = xml.DocumentElement[language];
        if (element != null)
        {
            IEnumerator elemEnumerator = element.GetEnumerator();
            while (elemEnumerator.MoveNext())
            {
                XmlElement xmlElem = (XmlElement)elemEnumerator.Current;
                _allStrings.Add(xmlElem.GetAttribute("name"), xmlElem.InnerText);
            }
        }
        else
            Debug.LogError("The language '" + language + "' does not exist !");
    }
    public static string GetString(string name)
    {
        if (!_allStrings.ContainsKey(name))
        {
            Debug.LogError("[LANG] Problème de mot-clef : " + name);
            return "";
        }
        return (string)_allStrings[name];
    }
}