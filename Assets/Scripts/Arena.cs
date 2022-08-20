using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private float timeToRaiseEnemyGates;

    [SerializeField] private GameObject[] enemyGates;
    [SerializeField] private GameObject playerGate;

    private SpawnManager spawnManager;

    public GameObject PlayerGate => playerGate;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Starts the coroutine PrepareForBattleCoroutine()
    public void PrepareForBattle()
    {
        StartCoroutine(PrepareForBattleCoroutine());
    }

    // Raise up all of the enemy gates, lower the player gate and start spawning monsters
    public IEnumerator PrepareForBattleCoroutine()
    {
        playerGate.GetComponent<GateController>().LowerHarrows();

        yield return new WaitForSeconds(timeToRaiseEnemyGates);

        RaiseAllEnemyGates();

        yield return new WaitForSeconds(timeToRaiseEnemyGates);

        spawnManager.StartAllSpawns();
    }

    // Raise up all of the enemy gates for monster deploying
    private void RaiseAllEnemyGates()
    {
        foreach (GameObject enemyGate in enemyGates)
        {
            enemyGate.GetComponent<GateController>().RaiseHarrows();
        }
    }
}
