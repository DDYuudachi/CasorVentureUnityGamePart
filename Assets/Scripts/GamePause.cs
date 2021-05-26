using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public Transform canvas;
    public PlayerControl playerControl;
    public PlayerMovementV2 playerMovementV2;
    public Transform mainMenuUI;
    public Transform optionMenuUI;
    public Transform gameWinUI;
    public Transform gameLoseUI;
    public MusicManager musicManager;

    //https://www.youtube.com/watch?v=PyEmRVRHWL8&t=366s  a simple pause menu UI
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            triggerPauseMenu();
        }
    }

    public void triggerPauseMenu()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            playerControl.enabled = false;
            playerMovementV2.enabled = false;
            Cursor.visible = true;
            Time.timeScale = 0;  //stop the time
        }
        else
        {
            canvas.gameObject.SetActive(false);
            playerControl.enabled = true;
            playerMovementV2.enabled = true;
            mainMenuUI.gameObject.SetActive(true);
            optionMenuUI.gameObject.SetActive(false);
            Time.timeScale = 1; //resume time
            Cursor.visible = false;
        }
    }

    public void LoadMainMenu()
    {
        musicManager.isGameEnd = true;
        musicManager.gameEnd();
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void reloadLevel()
    {
        musicManager.isGameEnd = true;
        musicManager.gameEnd();
        SceneManager.LoadScene("Scenes/CasorAdventure");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void activeLoseMenu()
    {
        gameLoseUI.gameObject.SetActive(true);
        Cursor.visible = true;
    }
}
