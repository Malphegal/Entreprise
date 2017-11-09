using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // TODO: Mettre sous un Thread pour avoir une barre de chargement
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
