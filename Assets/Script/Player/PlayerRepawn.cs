using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Player_Life playerHealth;
    private Player_Life playerLive;
    private UIManager uiManager;
    public Transform respawnPoint;
    private Vector3 initialPosition;

    private void Awake()
    {
        playerHealth = GetComponent<Player_Life>();
        playerLive = GetComponent<Player_Life>();
        uiManager = FindObjectOfType<UIManager>();
        if (respawnPoint == null)
        {
            respawnPoint = transform; // Nếu không có respawnPoint, sử dụng vị trí ban đầu của người chơi
        }
        
    initialPosition = transform.position;  // Lưu vị trí ban đầu của người chơi
    }

    public void CheckRespawn()
    {
        float lives = playerLive.currentLive;
        // Check if checkpoint available
        if (lives <= 0) // 
        {
            // Show game over screen
            if (uiManager != null)
            {
                uiManager.GameOver();
            }

            return; // Don't execute the rest of this function
        }
        else if (currentCheckpoint == null && lives > 0)
        {
            // Respawn at initial position if no checkpoint is available
            playerHealth.Respawn();
            transform.position = initialPosition;
        }
        else if(currentCheckpoint != null && lives > 0)
        {
            // Respawn at the checkpoint
            playerHealth.Respawn(); // Restore player health and reset animation
            transform.position = currentCheckpoint.position; // Move player to checkpoint location
        }
        

        // Move the camera to the checkpoint's room
        // Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance?.PlaySound(checkpoint);

            Collider2D checkpointCollider = collision.GetComponent<Collider2D>();
            if (checkpointCollider != null)
            {
                checkpointCollider.enabled = false;
            }

            //collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}
