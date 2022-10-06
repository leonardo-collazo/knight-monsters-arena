using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private float timeToRaiseEnemyGates;
    [SerializeField] private float timeToStartRaisingEnemyGates;
    [SerializeField] private float timeToStartSpawningEnemies;

    private GateController[] enemyGates;
    private GateController playerGate;

    private SpawnManager spawnManager;
    private MusicManager musicManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        musicManager = FindObjectOfType<MusicManager>();
        enemyGates = transform.Find("EnemyRooms").GetComponentsInChildren<GateController>();
        playerGate = transform.Find("PlayerRoom").GetComponentInChildren<GateController>();
    }

    // Starts the first phase of battle preparation
    public void PrepareForBattle()
    {
        StartCoroutine(FirstPhase());
    }

    // Lower the player gate
    private IEnumerator FirstPhase()
    {
        playerGate.LowerHarrows();

        yield return new WaitForSeconds(timeToStartRaisingEnemyGates);

        StartCoroutine(SecondPhase());
    }

    // Raise up all of the enemy gates for monster deploying
    private IEnumerator SecondPhase()
    {
        foreach (GateController enemyGate in enemyGates)
        {
            enemyGate.RaiseHarrows();

            yield return new WaitForSeconds(timeToRaiseEnemyGates);
        }

        yield return new WaitForSeconds(timeToStartSpawningEnemies);

        ThirdPhase();
    }

    // Starts spawning all and prepares battle music
    private void ThirdPhase()
    {
        spawnManager.StartAllSpawns();
        musicManager.PrepareBattleEnvironment();
    }
}
