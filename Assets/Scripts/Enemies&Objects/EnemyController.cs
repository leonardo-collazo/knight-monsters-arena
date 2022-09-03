using System.Collections;
using UnityEngine;

public enum EnemySpawningPlaces { fromLeft, fromRight, fromAbove, fromBelow }
public enum SoulColors { Red, Blue }

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float life;
    [SerializeField] private float speed;
    [SerializeField] private float score;
    [SerializeField] private float timeBeforeDisappear;
    [SerializeField] private float timeBeforeAttacking;
    [SerializeField] private int physicalDamage;
    
    [SerializeField] private SoulColors soulColor;

    private bool canMove = true;
    private bool canAttack = true;

    private Rigidbody enemyRb;
    private Collider enemyCollider;
    private Transform targetToFollow;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private Animator enemyAnim;
    private HUD hud;

    public SoulColors SoulColor { get => soulColor; }
    public float PhysicalDamage { get => physicalDamage; }
    public float TimeBeforeAttacking { get => timeBeforeAttacking; }

    #endregion

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        enemyAnim = GetEnemyAnimator();
        hud = FindObjectOfType<HUD>();
    }

    void FixedUpdate()
    {
        if (gameManager.IsGameActive && !IsDead() && canMove)
        {
            Move();
        }
    }

    #region Actions

    // The movement of the enemy
    void Move()
    {
        Vector3 direction = (targetToFollow.position - transform.position).normalized;
        enemyRb.velocity = direction * speed * Time.deltaTime;
        transform.LookAt(targetToFollow);
    }

    // Attacks
    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(TimeBeforeAttacking);
        enemyAnim.SetTrigger("Attack_t");
        yield return new WaitForSeconds(gameManager.CombatCooldownTime);
        canAttack = true;
    }

    // Plays the enemy's death animation and destroy the enemy after a few seconds
    IEnumerator Death()
    {
        enemyAnim.SetBool("Dead_b", true);
        enemyRb.isKinematic = true;
        enemyCollider.enabled = false;

        yield return new WaitForSeconds(timeBeforeDisappear);

        spawnManager.SpawnEnemySoul(gameObject);
        Destroy(gameObject);
    }

    #endregion

    #region Methods for verify status

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

        if (IsDead())
        {
            gameManager.UpdateScore(score);
            
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
        if (collision.gameObject.CompareTag("Player") && canAttack && gameManager.IsGameActive && 
            !collision.gameObject.GetComponent<PlayerController>().IsImmune)
        {
            StartCoroutine(Attack());
        }
    }

    // If the enemy collides with a weapon and the owner of that weapon is attacking, the enemy will recive a hit
    private void OnTriggerEnter(Collider other)
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
