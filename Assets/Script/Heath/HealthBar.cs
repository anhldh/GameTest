using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // public Player_Life playerHealth;
    // public Player_Life playerLive;
    // public Image totalhealthBar;
    // public Image currenthealthBar;
    // public Text livesText;

    //private void Start()
    //{
    //    totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    //    livesText.text = playerLive.currentLive.ToString();
    //}


    //private void Update()
    //{
    //    currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    //    livesText.text = playerLive.currentLive.ToString();

    //}
    private Player_Life player;

    public Image totalhealthBar;
    public Image currenthealthBar;
    public Text livesText;

    public void SetPlayer(Player_Life p)
    {
        player = p;
    }

    private void Update()
    {
        if (player == null) return;

        if (totalhealthBar != null)
        {
            totalhealthBar.fillAmount = 1f;
        }

        if (currenthealthBar != null)
        {
            currenthealthBar.fillAmount = player.currentHealth / 10f;
        }

        if (livesText != null)
        {
            livesText.text = player.currentLive.ToString();
        }
    }

}
