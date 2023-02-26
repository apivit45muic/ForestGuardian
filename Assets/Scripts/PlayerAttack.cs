using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private AudioClip bowLoadingSound;
    [SerializeField] private AudioClip bowReleaseSound;
    [SerializeField] private AudioClip daggerSound;
    [SerializeField] private AudioClip getHitSound;
    [SerializeField] private AudioSource _audioSource;
    


    [Header ("Melee Attack Parameters")]
    [SerializeField] private float meleeAttackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private CapsuleCollider2D boxCollider;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private Health enemyHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > meleeAttackCooldown && playerMovement.canAttack())
            Attack();
        else if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            rangeAttack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {   
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
    private void rangeAttack()
    {
        anim.SetTrigger("rangeAttack");
        cooldownTimer = 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    
    private void DamageEnemy()
    {
        if (PlayerInSight())
            enemyHealth.TakeDamage(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void ReleaseArrow()
    {
        arrows[FindArrow()].transform.position = arrowPoint.position;
        arrows[FindArrow()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void playbowLoadingSound(){_audioSource.PlayOneShot(bowLoadingSound, 2F);}
    private void playbowReleaseSound(){_audioSource.PlayOneShot(bowReleaseSound, 3F);}
    private void playdaggerSound(){_audioSource.PlayOneShot(daggerSound, 0.3F);}
    private void playgetHitSound(){_audioSource.PlayOneShot(getHitSound, 0.3F);}
    
}