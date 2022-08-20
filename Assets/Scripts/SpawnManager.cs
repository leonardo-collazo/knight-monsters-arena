using UnityEngine;

public enum LaunchObjectSpawnPosition { North, South, West, East }
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject[] launchObjects;
    [SerializeField] private GameObject[] effects;
    [SerializeField] private Transform[] enemySpawnPositions;

    [SerializeField] private float enemySpawnTime;
    [SerializeField] private float powerupSpawnTime;
    [SerializeField] private float launchObjectSpawnTime;
    [SerializeField] private float spawnStartDelay;

    [SerializeField] int amountLaunchObjectToSpawn;

    private Transform playerTransform;
    private EnvironmentBoundaries environmentBoundaries;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        environmentBoundaries = GameObject.Find("Environment").GetComponent<EnvironmentBoundaries>();
    }

    #region Spawning methods

    // Starts spawning enemies, powerups and launch objects in random places
    public void StartAllSpawns()
    {
        // Descomentar todo
        // InvokeRepeating("SpawnEnemy", spawnStartDelay, enemySpawnTime);
        // InvokeRepeating("SpawnPowerup", spawnStartDelay, powerupSpawnTime);
        // InvokeRepeating("SpawnLaunchObject", spawnStartDelay, launchObjectSpawnTime);
    }

    // Spawns an enemy in a random place near the limits
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        int enemySpawnPosIndex = Random.Range(0, enemySpawnPositions.Length);

        Vector3 position = enemySpawnPositions[enemySpawnPosIndex].position;

        // Vector3 position = GenerateSpawningEnemyPosition(enemies[enemyIndex].gameObject);

        Instantiate(enemies[enemyIndex], position, enemies[enemyIndex].transform.rotation);
    }

    // Spawns a powerup in a random place
    void SpawnPowerup()
    {
        int powerupIndex = Random.Range(0, powerups.Length);
        Vector3 position = GenerateSpawningPowerupPosition(powerups[powerupIndex].gameObject);

        Instantiate(powerups[powerupIndex], position, powerups[powerupIndex].transform.rotation);
    }

    // Spawns a launch object in a random place near the limits
    void SpawnLaunchObject()
    {
        for (int amountLaunchObjectSpawned = 0; amountLaunchObjectSpawned < amountLaunchObjectToSpawn; amountLaunchObjectSpawned++)
        {
            int launchObjectIndex = Random.Range(0, launchObjects.Length);

            GameObject launchObject = launchObjects[launchObjectIndex];
            Vector3 position = GenerateSpawningLaunchObjectPosition();

            SetLaunchObjectMovement(launchObject.GetComponent<LaunchObjectController>(), position);
            Instantiate(launchObject, position, launchObject.transform.rotation);
        }
    }

    // Spawns the corresponding soul of the enemy in the same position as the enemy
    public void SpawnEnemySoul(GameObject enemy)
    {
        float difference = 0.2f;
        Vector3 position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - difference, enemy.transform.position.z);

        if (enemy.GetComponent<EnemyController>().soulColor == SoulColors.Red)
        {
            Instantiate(effects[0], position, effects[0].gameObject.transform.rotation);
        }
        else if (enemy.GetComponent<EnemyController>().soulColor == SoulColors.Blue)
        {
            Instantiate(effects[1], position, effects[1].gameObject.transform.rotation);
        }
    }

    // Stop all spawnings
    public void StopAllSpawns()
    {
        CancelInvoke();
    }

    #endregion

    #region Generating position methods

    Vector3 GenerateSpawningPowerupPosition(GameObject powerup)
    {
        float xPosition = Random.Range(environmentBoundaries.leftWallPos.position.x, environmentBoundaries.rightWallPos.position.x);
        float zPosition = Random.Range(environmentBoundaries.behindWallPos.position.z, environmentBoundaries.forwardWallPos.position.z);
        
        Vector3 position = new Vector3(xPosition, powerup.transform.localScale.y / 2, zPosition);

        return position;
    }

    Vector3 GenerateSpawningLaunchObjectPosition()
    {
        // Generates a randomly place
        int enumPosition = Random.Range(0, 4);
        Vector3 position = new Vector3(0, playerTransform.transform.localScale.y, 0);

        // Depending on the place, a position is generated
        if ((LaunchObjectSpawnPosition)enumPosition == LaunchObjectSpawnPosition.West)
        {
            position.x = environmentBoundaries.leftWallPos.position.x;
            position.z = Random.Range(environmentBoundaries.behindWallPos.position.z, environmentBoundaries.forwardWallPos.position.z);
        }

        else if ((LaunchObjectSpawnPosition)enumPosition == LaunchObjectSpawnPosition.North)
        {
            position.x = Random.Range(environmentBoundaries.leftWallPos.position.x, environmentBoundaries.rightWallPos.position.x);
            position.z = environmentBoundaries.forwardWallPos.position.z;
        }
        else if ((LaunchObjectSpawnPosition)enumPosition == LaunchObjectSpawnPosition.East)
        {
            position.x = environmentBoundaries.rightWallPos.position.x;
            position.z = Random.Range(environmentBoundaries.behindWallPos.position.z, environmentBoundaries.forwardWallPos.position.z);
        }
        else
        {
            position.x = Random.Range(environmentBoundaries.leftWallPos.position.x, environmentBoundaries.rightWallPos.position.x);
            position.z = environmentBoundaries.behindWallPos.position.z;
        }        

        return position;
    }

    // Sets the movement type of the launchobject in dependence of the given position
    void SetLaunchObjectMovement(LaunchObjectController launchObjectController, Vector3 position)
    {
        if (position.x == environmentBoundaries.leftWallPos.position.x)
        {
            launchObjectController.movementType = LaunchObjectMovementType.Right;
        }
        else if (position.x == environmentBoundaries.rightWallPos.position.x)
        {
            launchObjectController.movementType = LaunchObjectMovementType.Left;
        }
        else if (position.z == environmentBoundaries.forwardWallPos.position.z)
        {
            launchObjectController.movementType = LaunchObjectMovementType.Down;
        }
        else
        {
            launchObjectController.movementType = LaunchObjectMovementType.Up;
        }
    }

    #endregion
}
