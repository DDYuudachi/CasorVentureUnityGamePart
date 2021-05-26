using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public int isInCombat;
    public PlayerStats player;
    public FMOD.Studio.EventInstance music;
    public bool isGameEnd;


    private void Start()
    {
        isInCombat = 0;
        isGameEnd = false;
        // music = this.GetComponent<FMODUnity.StudioEventEmitter>();
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
        music.start();
    }

    private void FixedUpdate()
    {
        changeMusic();
        changeMusicByPlayerHP();
    }

    public void gameEnd()
    {
        if(isGameEnd)
        {
            music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music.release();
        }
    }

    private void changeMusicByPlayerHP()
    {
        music.setParameterByName("HP", player.health);
    }

    private void changeMusic()
    {
        if (isInCombat == 0)
        {
            music.setParameterByName("Enemy", isInCombat);
        }
        else
        {
            music.setParameterByName("Enemy", isInCombat);
        }
    }
}
