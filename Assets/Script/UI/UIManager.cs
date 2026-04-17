using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Next Level")]
    [SerializeField] private GameObject nextLevelScreen;
    [SerializeField] private AudioClip nextLevelSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Text coinsTotalText;
    private void Awake()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (pauseScreen != null)
            pauseScreen.SetActive(false);
    }

    private void Update()
    {
        // If pause screen already active unpause and viceversa
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseScreen != null && pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }


    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        SoundManager.instance?.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        // Reset the time scale to 1 before reloading the scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    //Complete Game
    public void CompleteGame()
    {
        if (nextLevelScreen != null)
        {
            nextLevelScreen.SetActive(true);
        }

        SoundManager.instance?.PlaySound(nextLevelSound);
    }

    //NextLevel
    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        if (pauseScreen != null)
            pauseScreen.SetActive(status);

        Time.timeScale = status ? 0 : 1;
    }

    public void SoundVolume() 
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ChangeSoundVolume(0.2f);
        }

    }

    public void SoundVolumeDecrease() {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ChangeSoundVolume(-0.2f);
        }
    }

    public void MusicVolume()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ChangeMusicVolume(0.2f);
        }
    }

    public void MusicVolumeDecrease()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ChangeMusicVolume(-0.2f);
        }
    }
    #endregion

    
}
