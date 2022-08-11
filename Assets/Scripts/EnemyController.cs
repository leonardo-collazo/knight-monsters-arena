using System.Collections;
using UnityEngine;

public enum EnemySpawningPlaces { fromLeft, fromRight, fromAbove, fromBelow }
public enum SoulColors { Red, Blue }

public class EnemyController : MonoBehaviour
{
    #region Variables

    public float life;
    public float speed;
    public int physicalDamage;
    public SoulColors soulColor;

    private float timeBeforeDisappear = 2.0f;

    private bool canMove = true;
    private bool canAttack = true;

    private Rigidbody enemyRb;
    private Transform target;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private Animator enemyAnim;

    #endregion

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        enemyAnim = GetEnemyAnimator();
    }

    void FixedUpdate()
    {
        if (gameManager.isGameActive && !IsDead() && canMove)
        {
            MoveEnemy();
        }
    }

    #region Actions

    // The movement of the enemy
    void MoveEnemy()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        enemyRb.velocity = direction * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    // Attacks
    IEnumerator Attack()
    {
        enemyAnim.SetTrigger("Attack_t");
        canAttack = false;
        yield return new WaitForSeconds(gameManager.CombatCooldownTime);
        canAttack = true;
    }

    // Plays the enemy's death animation and destroy the enemy after a few seconds
    IEnumerator Death()
    {
        enemyAnim.SetBool("Dead_b", true);
        yield return new WaitForSeconds(timeBeforeDisappear);
        spawnManager.SpawnEnemySoul(gameObject);
        Destroy(gameObject);
    }

    #endregion

    #region Methods for verify enemy status

    // Return true if the enemy is dead or false otherwise
    bool IsDead()
    {
        return life <= 0;
    }

    // Check if the enemy is attacking
    public bool IsAttacking()
    {
        return enemyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    #endregion

    #region Get methods

    // Subtracts life from the enemy
    void GetHitFromPlayerWeapon(GameObject weapon)
    {
        life -= weapon.GetComponent<WeaponFeatures>().physicalDamage;

        if (life <= 0)
        {
            StartCoroutine(Death());
        }
    }

    // Gets the enemy's animator
    Animator GetEnemyAnimator()
    {
        return GetComponentInChildren<Animator>();
    }

    #endregion

    #region Collision and trigger

    // As long as the enemy is colliding with the player, it will continue attacking the player
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    // If the enemy collides with a weapon and the owner of that weapon is attacking, the enemy will recive a hit
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            PlayerController playerController = other.gameObject.GetComponentInParent<PlayerController>();

            if (playerController.IsAttacking())
            {
                GetHitFromPlayerWeapon(other.gameObject);
            }
        }
    }

    #endregion
}
