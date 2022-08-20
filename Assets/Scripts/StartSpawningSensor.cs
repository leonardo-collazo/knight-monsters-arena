using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawningSensor : MonoBehaviour
{
    private Arena arena;

    private void Start()
    {
        arena = GameObject.Find("Arena").GetComponent<Arena>();
    }

    // When the player collide with the sensor, the player gate is closed,
    // the monster gates are opened and these are spawned
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            arena.PrepareForBattle();
        }

        Destroy(gameObject);
    }
}
