using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;

    [SerializeField] private AudioClip impactSound;
    private Animator anim;
    private BoxCollider2D boxCollier;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollier = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if( lifetime >1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gây sats thương cho nhân vật
        if(collision.tag == "Enemy")
        {
            hit = true;
            if (anim != null)
            {
                anim.SetTrigger("explode");
            }

            SoundManager.instance?.PlaySound(impactSound);

            if (boxCollier != null)
            {
                boxCollier.enabled = false;
            }

            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        else if (collision.CompareTag("TileMap") || collision.CompareTag("Trap"))
        {
            hit = true;
            if (boxCollier != null)
            {
                boxCollier.enabled = false;
            }

            gameObject.SetActive(false); // When this hits a wall, deactivate arrow
        }
        
    }
    public void SetDirection(float _direction)
    {
        //Set hướng đi của mũi tên
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        if (boxCollier != null)
        {
            boxCollier.enabled = true;
        }

        float localScaleX = transform.localScale.x;
        if ( Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
        
}
