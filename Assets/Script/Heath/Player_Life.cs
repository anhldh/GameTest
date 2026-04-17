using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    [Header("Health")]
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float startingHealth;
    [SerializeField] private float startingLive;
    
    public float currentHealth { get; private set; }
    public float currentLive { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private SpriteRenderer spriteRend;
    private bool invulnerable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        currentLive = startingLive;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void TakeDamage(float _damage)
    {
        if(invulnerable)
        {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        { 
            //anim.SetTrigger("hurt");
            SoundManager.instance?.PlaySound(hurtSound);
            StartCoroutine(Invunerability());
        }
        else
        {
            Die();
        }


    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        currentLive--;
        AddHealth(startingHealth);
        if (anim != null)
        {
            anim.ResetTrigger("death");
        }

        //anim.Play("Ilde_bow");
        if (anim != null)
        {
            anim.Play("idle");
        }

        StartCoroutine(Invunerability());
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            if (spriteRend != null)
            {
                spriteRend.color = new Color(1, 0, 0, 0.7f);
            }

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            if (spriteRend != null)
            {
                spriteRend.color = Color.white;
            }

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 7, false);
        invulnerable = false;
    }

    public void Die()
    {
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (anim != null)
        {
            anim.SetTrigger("death");
        }

        SoundManager.instance?.PlaySound(deathSound);

    }

    //private void RestartLevel()
    //{
     //   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //} 
}
