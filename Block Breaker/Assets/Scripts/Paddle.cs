using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    private float screenWidthInUnits = 16f;
    private float minX = 1f;
    private float maxX = 15f;

    GameSession gameSession;
    Ball ball;

	// Use this for initialization
	void Start () {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameSession.GetIsPaused())
        {
            Vector2 paddlePos = transform.position;
            paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
            transform.position = paddlePos;
        }
	}

    private float GetXPos()
    {
        if (gameSession.GetAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
