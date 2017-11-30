﻿using UnityEngine;

public sealed class MainMenu : MonoBehaviour {

    public UnityEngine.UI.Text[] mainMenuTexts;

    // TODO: Ajouter toutes les autres vérifications et initialisations
    private void Awake()
    {
            // Language

        string[] s = System.IO.Directory.GetFiles(".", "lang.fr.xml", System.IO.SearchOption.AllDirectories);
        Lang.DefineLanguage(s[0], Application.systemLanguage.ToString());

            // Init all texts

        mainMenuTexts[0].text = Lang.GetString("mainmenu.startbutton");
    }

    // TODO: Passer la méthode en asynchrone pour avoir une barre de chargement
    public void OnClickNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // TODO: Ajouter un pop-up demandant au joueur s'il veut vraiment quitter ou non
    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
