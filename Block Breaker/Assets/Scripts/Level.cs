using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour {

    [SerializeField] int breakableBlocks;

    private void Start()
    {
        FindObjectOfType<GameSession>().RemoveLevelCompletePanel();
        UpdateLevelText();
    }

    

    private void UpdateLevelText()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameObject levelText = FindObjectOfType<Canvas>().transform.Find("Level Text").gameObject;
        levelText.GetComponent<TextMeshProUGUI>().text = " Level " + currentSceneIndex.ToString();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            FindObjectOfType<GameSession>().LevelComplete();
        }
    }
}
