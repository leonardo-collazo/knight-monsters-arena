using UnityEngine;

public enum LaunchObjectMovementType { Up, Right, Down, Left }

public class LaunchObjectController : MonoBehaviour
{
    private float speed = 15;
    public int physicalDamage;

    private EnvironmentBoundaries environmentBoundaries;
    public LaunchObjectMovementType movementType;

    // Start is called before the first frame update
    void Start()
    {
        environmentBoundaries = GameObject.Find("Environment").GetComponent<EnvironmentBoundaries>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (movementType == LaunchObjectMovementType.Right)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right, Space.World);
        }
        else if (movementType == LaunchObjectMovementType.Down)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.back, Space.World);
        }
        else if (movementType == LaunchObjectMovementType.Left)
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
        return transform.position.x < environmentBoundaries.leftWallPos.position.x || 
            transform.position.x > environmentBoundaries.rightWallPos.position.x || 
            transform.position.z < environmentBoundaries.behindWallPos.position.z || 
            transform.position.z > environmentBoundaries.forwardWallPos.position.z;
    }
}
