using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private Animator anim;
    private float cooldownTimer;

    private void Attack()
    {
        if (firePoint == null || arrows == null || arrows.Length == 0)
        {
            return;
        }

        int arrowIndex = FindArrow();
        if (arrowIndex < 0 || arrowIndex >= arrows.Length || arrows[arrowIndex] == null)
        {
            return;
        }

        cooldownTimer = 0;
        arrows[arrowIndex].transform.position = firePoint.position;

        EnemyProjectile projectile = arrows[arrowIndex].GetComponent<EnemyProjectile>();
        if (projectile != null)
        {
            projectile.ActivateProjectile();
        }
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
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
