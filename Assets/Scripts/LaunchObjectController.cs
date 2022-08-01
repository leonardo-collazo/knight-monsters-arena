using UnityEngine;

public enum LaunchObjectMovementType { None, Up, Right, Down, Left }

public class LaunchObjectController : MonoBehaviour
{
    private float speed = 10;
    public int physicalDamage;

    private EnvironmentBoundaries environmentBoundaries;
    public LaunchObjectMovementType typeMovement = LaunchObjectMovementType.None;

    // Start is called before the first frame update
    void Start()
    {
        environmentBoundaries = GameObject.Find("Environment").GetComponent<EnvironmentBoundaries>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (IsOutsideBoundaries())
        {
            Destroy(gameObject);
        }
    }

    // Moves the launch object
    private void Move()
    {
        if (typeMovement == LaunchObjectMovementType.Right)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right, Space.World);
        }
        else if (typeMovement == LaunchObjectMovementType.Down)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.back, Space.World);
        }
        else if (typeMovement == LaunchObjectMovementType.Left)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.World);
        }
    }

    // Checks if the launch object is outside of the boundaries
    bool IsOutsideBoundaries()
    {
        return transform.position.x < environmentBoundaries.leftBoundary || transform.position.x > environmentBoundaries.rightBoundary
           || transform.position.z < environmentBoundaries.lowerBoundary || transform.position.z > environmentBoundaries.upperBoundary;
    }
}
