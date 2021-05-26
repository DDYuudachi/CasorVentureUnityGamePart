using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public MusicManager musicManager;
    public Enemy enemy;
    private AIController _aiController;
    private EnemyAnimation enemyAnimation;

    private void Start()
    {
        _aiController = enemy.GetComponent<AIController>();
        enemyAnimation = enemy.GetComponent<EnemyAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            musicManager.isInCombat = 1;
            _aiController.EvasionEnvironmentCollided();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicManager.isInCombat = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicManager.isInCombat = 0;
            _aiController.StopEnemyAction();
        }
    }
}
