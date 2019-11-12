using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;

    public GameObject PauseUI;
    private GameObject Player;

    private bool prevPlayerController;
    private bool prevRaycast;
    private CursorLockMode prevCursorMode;
    private bool prevCursorVisible;

    void Start () {
        Player = GameObject.Find ("/Player").gameObject;

        prevPlayerController = Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled;
        prevRaycast = Player.GetComponentInChildren<RaycastInteraction> ().enabled;
        prevCursorMode = Cursor.lockState;
        prevCursorVisible = Cursor.visible;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume ();
            } else {
                Pause ();
            }
        }
    }

    public void Resume () {
        PauseUI.SetActive (false);
        Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = prevPlayerController;
        Player.GetComponentInChildren<RaycastInteraction> ().enabled = prevRaycast;
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = prevCursorMode;
        Cursor.visible = prevCursorVisible;
    }

    void Pause () {
        prevPlayerController = Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled;
        prevRaycast = Player.GetComponentInChildren<RaycastInteraction> ().enabled;
        prevCursorMode = Cursor.lockState;
        prevCursorVisible = Cursor.visible;

        PauseUI.SetActive (true);
        Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
        Player.GetComponentInChildren<RaycastInteraction> ().enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu () {
        Debug.Log ("Changes to Main Menu");
        SceneManager.LoadScene (0, LoadSceneMode.Single);
    }

    public void QuitGame () {
        Debug.Log ("Quitting game...");
        Application.Quit ();
    }
}