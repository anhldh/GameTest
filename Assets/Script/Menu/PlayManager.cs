using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    int characterIndex;
    public static Vector2 lastCheckPointPos = new    Vector2 (66.04f, -5.58f);//Vector2(223.4589f, -5.635663f);
    public CinemachineVirtualCamera VCam;
    private ItemCollection itemCollection;
    private Player_Life playerHealth;
    [SerializeField] private Text CoinCount;
    [SerializeField] private Text CoinTotal;
    [SerializeField] private Text CoinDefeat;
    [SerializeField] private Text starsCount;
    [SerializeField] private Text starsComplete;
    [SerializeField] private Image HeathToTal;
    [SerializeField] private Image HealthCurrent;
    [SerializeField] private Text lives;

    private static bool created = false; // Check if the PlayManager is created

    private void Awake()
    {
        if (!created)
        {
            // This ensures that the PlayManager persists between scenes
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // If another PlayManager already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("PlayManager: No player prefab assigned.");
            return;
        }

        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        characterIndex = Mathf.Clamp(characterIndex, 0, playerPrefabs.Length - 1);

        GameObject player = Instantiate(playerPrefabs[characterIndex],lastCheckPointPos,Quaternion.identity);
        if (player == null)
        {
            Debug.LogError("PlayManager: Failed to instantiate player prefab.");
            return;
        }

        if (VCam != null)
        {
            VCam.m_Follow = player.transform;
        }

        // Attach ItemCollection script to the player (assuming it's not already attached)
        itemCollection = player.GetComponent<ItemCollection>();
        if (itemCollection == null)
        {
            itemCollection = player.AddComponent<ItemCollection>();
        }

        // Set the UI text for coin and star collection
        itemCollection.coinsText = CoinCount;
        itemCollection.coinsTotalText = CoinTotal;
        itemCollection.coinsDefeat = CoinDefeat;
        itemCollection.starsText = starsCount;
        itemCollection.starsCompleteText = starsComplete;

        Player_Life playerLife = player.GetComponent<Player_Life>();
        if (playerLife != null)
        {
            HealthBar healthBar = player.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = player.AddComponent<HealthBar>();
            }

            healthBar.SetPlayer(playerLife);
            healthBar.totalhealthBar = HeathToTal;
            healthBar.currenthealthBar = HealthCurrent;
            healthBar.livesText = lives;
        }
        else
        {
            Debug.LogWarning("PlayManager: Player prefab is missing Player_Life.");
        }
    }
}
