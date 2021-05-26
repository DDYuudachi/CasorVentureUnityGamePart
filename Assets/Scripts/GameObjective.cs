using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjective : MonoBehaviour
{
    public List<Enemy> enemy;
    public Text _obj_kills;
    public int kills;
    public Transform gameWinUI;
    public int isWin;
    public bool winState;
    FMOD.Studio.EventInstance winSound;

    private void Start()
    {
        kills = 0;
        isWin = 1;
        winState = false;
    }

    private void Awake()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        checkIfEnemyIsKilled();
        setObjectiveKills();
        checkIsWin();
    }

    private void checkIsWin()
    {
        if(kills == 4 && !winState)
        {
            winState = true;
            winSound = FMODUnity.RuntimeManager.CreateInstance("event:/Music/WinLose");
            winSound.setParameterByName("WinLose", isWin);
            winSound.start();
            winSound.release();
            gameWinUI.gameObject.SetActive(true);
        }
    }

    private void setObjectiveKills()
    {
        _obj_kills.text = "  ( " + kills + " / 4 )";
    }

    private void checkIfEnemyIsKilled()
    {
        foreach(Enemy enemies in enemy)
        {
            if(enemies.HP <= 0)
            {
                enemy.Remove(enemies);
                kills++;
                return;
            }
        }
    }
}
