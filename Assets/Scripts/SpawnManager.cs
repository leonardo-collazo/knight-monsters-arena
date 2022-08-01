using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject[] launchObjects;
    [SerializeField] private GameObject[] effects;

    [SerializeField] private float spawnLeftLimit = -14.0f;
    [SerializeField] private float spawnUpperLimit = 7.7f;
    [SerializeField] private float spawnRightLimit = 14.0f;
    [SerializeField] private float spawnLowerLimit = -7.7f;

    [SerializeField] private float enemySpawnTime = 5.0f;
    [SerializeField] private float powerupSpawnTime = 10.0f;
    [SerializeField] private float launchObjectSpawnTime = 7.0f;
    [SerializeField] private float spawnStartDelay = 1.0f;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #region Spawning methods

    // Starts spawning enemies, powerups and launch objects in random places
    public void StartAllSpawns()
    {
        InvokeRepeating("SpawnEnemy", spawnStartDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerup", spawnStartDelay, powerupSpawnTime);
        InvokeRepeating("SpawnLaunchObject", spawnStartDelay, launchObjectSpawnTime);
    }

    // Spawns an enemy in a random place near the limits
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        Vector3 position = GenerateSpawningEnemyPosition(enemies[enemyIndex].gameObject);

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
        int launchObjectsIndex = Random.Range(0, launchObjects.Length);
        Vector3 position = GenerateSpawningLaunchObjectPosition(launchObjects[launchObjectsIndex]);

        Instantiate(launchObjects[launchObjectsIndex], position, launchObjects[launchObjectsIndex].transform.rotation);
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

    Vector3 GenerateSpawningEnemyPosition(GameObject enemy)
    {
        // A vector is created with several places and generate one randomly
        int fromWhere = Random.Range(0, 4);
        Vector3 position = new Vector3(0, enemy.transform.localScale.y / 2, 0);

        // Depending on the place, a position is generated
        if ((EnemySpawningPlaces)fromWhere == EnemySpawningPlaces.fromLeft)
        {
            position.x = spawnLeftLimit;
            position.z = Random.Range(spawnLowerLimit, spawnUpperLimit);
        }
        else if ((EnemySpawningPlaces)fromWhere == EnemySpawningPlaces.fromAbove)
        {
            position.x = Random.Range(spawnLeftLimit, spawnRightLimit);
            position.z = spawnUpperLimit;
        }
        else if ((EnemySpawningPlaces)fromWhere == EnemySpawningPlaces.fromRight)
        {
            position.x = spawnRightLimit;
            position.z = Random.Range(spawnLowerLimit, spawnUpperLimit);
        }
        else
        {
            position.x = Random.Range(spawnLeftLimit, spawnRightLimit);
            position.z = spawnLowerLimit;
        }

        return position;
    }

    Vector3 GenerateSpawningPowerupPosition(GameObject powerup)
    {
        float xPosition = Random.Range(spawnLeftLimit, spawnRightLimit);
        float zPosition = Random.Range(spawnLowerLimit, spawnUpperLimit);
        Vector3 position = new Vector3(xPosition, powerup.transform.localScale.y / 2, zPosition);

        return position;
    }

    Vector3 GenerateSpawningLaunchObjectPosition(GameObject launchObject)
    {
        // Generates a randomly place
        int toWhere = Random.Range(0, 4);
        Vector3 position = new Vector3(0, playerTransform.transform.localScale.y / 2, 0);

        // Depending on the place, a position is generated
        if ((LaunchObjectMovementType)toWhere == LaunchObjectMovementType.Right)
        {
            position.x = spawnLeftLimit;
            position.z = Random.Range(spawnLowerLimit, spawnUpperLimit);
            launchObject.GetComponent<LaunchObjectController>().typeMovement = LaunchObjectMovementType.Right;
        }
        else if ((LaunchObjectMovementType)toWhere == LaunchObjectMovementType.Down)
        {
            position.x = Random.Range(spawnLeftLimit, spawnRightLimit);
            position.z = spawnUpperLimit;
            launchObject.GetComponent<LaunchObjectController>().typeMovement = LaunchObjectMovementType.Down;
        }
        else if ((LaunchObjectMovementType)toWhere == LaunchObjectMovementType.Left)
        {
            position.x = spawnRightLimit;
            position.z = Random.Range(spawnLowerLimit, spawnUpperLimit);
            launchObject.GetComponent<LaunchObjectController>().typeMovement = LaunchObjectMovementType.Left;
        }
        else
        {
            position.x = Random.Range(spawnLeftLimit, spawnRightLimit);
            position.z = spawnLowerLimit;
            launchObject.GetComponent<LaunchObjectController>().typeMovement = LaunchObjectMovementType.Up;
        }

        return position;
    }

    #endregion
}
