using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameOverPanel = FindObjectOfType<Canvas>().transform.Find("Game Over Panel").gameObject;
        gameOverPanel.SetActive(true);
        FindObjectOfType<GameSession>().PauseGame();
    }
}
