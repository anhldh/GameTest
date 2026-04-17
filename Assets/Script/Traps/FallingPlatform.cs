using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay;
    [SerializeField] private float destroyDelay;

    private bool falling = false;
    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Avoid calling the coroutine multiple time if it's already been called (falling)
        if (falling)
        {
            return;
        }
        //If the player landed on the platform, start falling
        if(collision.transform.tag == "Player")
        {
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;
        //Wait for a few seconds before droping
        yield return new WaitForSeconds(fallDelay);

        //Enable rigidboy and destroy after a few second
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        Destroy(gameObject, destroyDelay);
    }

}
