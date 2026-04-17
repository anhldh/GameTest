using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collier Parameters")]
    [SerializeField] private float collierDistance;
    [SerializeField] private CapsuleCollider2D boxCollier;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip soundAttack;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private Health playerHealth;
    private Player_movement playerMovement;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player_movement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (anim == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("attack");
            SoundManager.instance?.PlaySound(soundAttack);
        }

        if (Input.GetMouseButtonDown(1) && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("attack2");
            SoundManager.instance?.PlaySound(soundAttack);
        }

    }

    private void IsFlip()
    {
        
    }

    private bool PlayerInSight()
    {
        if (boxCollier == null || playerMovement == null)
        {
            return false;
        }

        float facingDirection = playerMovement.FacingDirection;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollier.bounds.center + transform.right * range * transform.localScale.x * facingDirection * collierDistance,
           new Vector3(boxCollier.bounds.size.x * range, boxCollier.bounds.size.y, boxCollier.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        if (boxCollier == null || playerMovement == null)
        {
            return;
        }

        float facingDirection = playerMovement.FacingDirection;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollier.bounds.center + transform.right * range * transform.localScale.x * facingDirection * collierDistance,
            new Vector3(boxCollier.bounds.size.x * range, boxCollier.bounds.size.y, boxCollier.bounds.size.z));
    }

    private void DamagePlayer()
    {

        //If player still in range damage him
        if (PlayerInSight())
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
