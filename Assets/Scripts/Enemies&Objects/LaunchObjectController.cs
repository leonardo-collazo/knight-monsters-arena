using UnityEngine;

public enum LaunchObjectMovementType { None, Up, Right, Down, Left }

public class LaunchObjectController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxTorque;
    [SerializeField] private int physicalDamage;

    private LaunchObjectMovementType movementType;

    private Environment environment;
    private Rigidbody rb;

    public int PhysicalDamage { get => physicalDamage; }

    void Awake()
    {
        environment = FindObjectOfType<Environment>();
        rb = GetComponent<Rigidbody>();

        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        SetLaunchObjectMovement();
    }

    void FixedUpdate()
    {
        Move();
    }

    // Moves the launch object
    void Move()
    {
        switch (movementType)
        {
            case LaunchObjectMovementType.Up:
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward, Space.World);
                break;
            case LaunchObjectMovementType.Right:
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.right, Space.World);
                break;
            case LaunchObjectMovementType.Down:
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.back, Space.World);
                break;
            case LaunchObjectMovementType.Left:
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.left, Space.World);
                break;
        }
    }

    // Returns a random torque between two stablished values
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Sets the movement type of the launchobject in dependence of the given position
    private void SetLaunchObjectMovement()
    {
        if (transform.position.x == environment.LeftLimit.position.x)
        {
            movementType = LaunchObjectMovementType.Right;
        }
        else if (transform.position.x == environment.RightLimit.position.x)
        {
            movementType = LaunchObjectMovementType.Left;
        }
        else if (transform.position.z == environment.UpperLimit.position.z)
        {
            movementType = LaunchObjectMovementType.Down;
        }
        else
        {
            movementType = LaunchObjectMovementType.Up;
        }
    }

    // Destroy this object
    public void DestroyLaunchObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collides with the outside sensor, destroy itself
        if (collision.gameObject.name.Equals("Outside Sensor"))
        {
            DestroyLaunchObject();
        }
    }
}
