using UnityEngine;

public enum LaunchObjectMovementType { None, Up, Right, Down, Left }

public class LaunchObjectController : MonoBehaviour
{
    private float movementSpeed = 15;
    private float maxTorque = 10.0f;
    
    public int physicalDamage;

    private Environment environment;
    private Rigidbody rb;

    public LaunchObjectMovementType movementType;

    void Start()
    {
        environment = FindObjectOfType<Environment>();
        rb = GetComponent<Rigidbody>();

        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (IsOutsideBoundaries())
        {
            Destroy(gameObject);
        }
    }

    // Moves the launch object
    void Move()
    {
        if (movementType == LaunchObjectMovementType.Right)
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.right, Space.World);
        }
        else if (movementType == LaunchObjectMovementType.Down)
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.back, Space.World);
        }
        else if (movementType == LaunchObjectMovementType.Left)
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.left, Space.World);
        }
        else if (movementType == LaunchObjectMovementType.Up)
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward, Space.World);
        }
    }

    // Returns a random torque between two stablished values
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Checks if the launch object is outside of the boundaries
    bool IsOutsideBoundaries()
    {
        return transform.position.x < environment.leftWallPos.position.x || 
            transform.position.x > environment.rightWallPos.position.x || 
            transform.position.z < environment.behindWallPos.position.z || 
            transform.position.z > environment.forwardWallPos.position.z;
    }

    // Sets the movement type of the launchobject in dependence of the given position
    public void SetLaunchObjectMovement()
    {
        if (transform.position.x == environment.leftWallPos.position.x)
        {
            movementType = LaunchObjectMovementType.Right;
        }
        else if (transform.position.x == environment.rightWallPos.position.x)
        {
            movementType = LaunchObjectMovementType.Left;
        }
        else if (transform.position.z == environment.forwardWallPos.position.z)
        {
            movementType = LaunchObjectMovementType.Down;
        }
        else
        {
            movementType = LaunchObjectMovementType.Up;
        }
    }
}
