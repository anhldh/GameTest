using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collier Parameters")]
    [SerializeField] private float collierDistance;
    [SerializeField] private BoxCollider2D boxCollier;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip soundAttack;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private Player_Life playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();

        boxCollier = GetComponent<BoxCollider2D>(); // auto lấy luôn
    }

    private void Update()
    {
        if (anim == null || boxCollier == null)
        {
            return;
        }

        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight()){

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance?.PlaySound(soundAttack);
            }
        }
       if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }


    private bool PlayerInSight()
    {
        // Set vùng tấn công của kẻ địch
        RaycastHit2D hit = Physics2D.BoxCast(boxCollier.bounds.center + transform.right* range * transform.localScale.x * collierDistance,
           new Vector3(boxCollier.bounds.size.x * range ,boxCollier.bounds.size.y, boxCollier.bounds.size.z) , 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Player_Life>();
        }

        return hit.collider != null;

    }

    private void OnValidate()
    {
        if (boxCollier == null)
            boxCollier = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (boxCollier == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollier.bounds.center + transform.right * range * transform.localScale.x * collierDistance,
            new Vector3(boxCollier.bounds.size.x * range, boxCollier.bounds.size.y, boxCollier.bounds.size.z)
        );
    }

    private void DamagePlayer()
    {

        //If player still in range damage him
        if(PlayerInSight())
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
