using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator title;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("glitch", 1f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void glitch()
    {
        AudioSource audio = GetComponent<AudioSource>();
        title.SetTrigger("Glitch");
        audio.Play();
    }

    public void StartGame()
    {
        Debug.Log("Changes to Level");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
