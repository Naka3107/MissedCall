using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void StartGame () {
        Debug.Log ("Changes to Level");
        SceneManager.LoadScene (1, LoadSceneMode.Single);
    }

    public void QuitGame () {
        Debug.Log ("Quitting game...");
        Application.Quit ();
    }
}