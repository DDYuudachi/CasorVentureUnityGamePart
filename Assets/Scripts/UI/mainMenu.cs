using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    FMOD.Studio.EventInstance music;

    private void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MenuMusic");
        music.start();
    }

    public void startGame()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        music.release();
        SceneManager.LoadScene("Scenes/CasorAdventure");
    }

    public void quitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
