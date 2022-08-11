using UnityEngine;

public enum LaunchObjectMovementType { Up, Right, Down, Left }

public class LaunchObjectController : MonoBehaviour
{
    private float movementSpeed = 15;
    private float maxTorque = 10.0f;
    
    public int physicalDamage;

    private EnvironmentBoundaries environmentBoundaries;
    private Rigidbody rb;

    public LaunchObjectMovementType movementType;

    void Start()
    {
        environmentBoundaries = GameObject.Find("Environment").GetComponent<EnvironmentBoundaries>();
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
        else
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
        return transform.position.x < environmentBoundaries.leftWallPos.position.x || 
            transform.position.x > environmentBoundaries.rightWallPos.position.x || 
            transform.position.z < environmentBoundaries.behindWallPos.position.z || 
            transform.position.z > environmentBoundaries.forwardWallPos.position.z;
    }
}
