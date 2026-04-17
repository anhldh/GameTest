using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter;


    private void Awake()
    {
        if (skins == null || skins.Length == 0)
        {
            return;
        }

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        selectedCharacter = Mathf.Clamp(selectedCharacter, 0, skins.Length - 1);

        foreach (GameObject player in skins)
        {
            if (player != null)
            {
                player.SetActive(false);
            }
        }

        if (skins[selectedCharacter] != null)
        {
            skins[selectedCharacter].SetActive(true);
        }
    }

    public void ChangeNext()
    {
        if (skins == null || skins.Length == 0)
        {
            return;
        }

        if (skins[selectedCharacter] != null)
        {
            skins[selectedCharacter].SetActive(false);
        }

        selectedCharacter++;
        if (selectedCharacter == skins.Length)
        {
            selectedCharacter = 0;
        }

        if (skins[selectedCharacter] != null)
        {
            skins[selectedCharacter].SetActive(true);
        }

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void ChangePrevious()
    {
        if (skins == null || skins.Length == 0)
        {
            return;
        }

        if (skins[selectedCharacter] != null)
        {
            skins[selectedCharacter].SetActive(false);
        }

        selectedCharacter--;
        if (selectedCharacter == -1)
        {
            selectedCharacter = skins.Length - 1;
        }

        if (skins[selectedCharacter] != null)
        {
            skins[selectedCharacter].SetActive(true);
        }

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}
