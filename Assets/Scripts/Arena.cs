using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private float timeToRaiseEnemyGates;
    [SerializeField] private float timeToStartSpawningEnemies;

    private GateController[] enemyGates;
    private GateController playerGate;

    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        enemyGates = transform.Find("EnemyRooms").GetComponentsInChildren<GateController>();
        playerGate = transform.Find("PlayerRoom").GetComponentInChildren<GateController>();
    }

    // Starts the coroutine PrepareForBattleCoroutine()
    public void PrepareForBattle()
    {
        StartCoroutine(PrepareForBattleCoroutine());
    }

    // Raise up all of the enemy gates, lower the player gate and start spawning monsters
    public IEnumerator PrepareForBattleCoroutine()
    {
        playerGate.LowerHarrows();

        yield return new WaitForSeconds(timeToRaiseEnemyGates);

        RaiseAllEnemyGates();

        yield return new WaitForSeconds(timeToStartSpawningEnemies);

        spawnManager.StartAllSpawns();
    }

    // Raise up all of the enemy gates for monster deploying
    private void RaiseAllEnemyGates()
    {
        foreach (GateController enemyGate in enemyGates)
        {
            enemyGate.RaiseHarrows();
        }
    }
}
