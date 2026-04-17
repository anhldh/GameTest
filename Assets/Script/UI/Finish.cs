using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : MonoBehaviour
{
    private UIManager uiManager;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("CheckFinish", 0.3f);
            
        }
    }

    private void CheckFinish()
    {
        if (uiManager != null)
        {
            uiManager.CompleteGame();
        }
    }
}


