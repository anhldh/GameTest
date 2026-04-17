using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player_Life playerLife = collision.GetComponent<Player_Life>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);
            }
        }
    }
}
