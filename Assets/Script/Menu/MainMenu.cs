using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // TODO: Mettre sous un Thread pour avoir une barre de chargement
    public void OnClickNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
