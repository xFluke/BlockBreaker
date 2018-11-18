//using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] Paddle paddle;
    [SerializeField] AudioClip[] ballSounds;
    private float xPush = 2f;
    private float yPush = 15f;
    private float randomFactor = 0.4f;

    Vector2 paddleToBallVector;
    bool hasLaunched = false;
    Vector2 velocityBeforeFreeze;

    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start () {
        paddleToBallVector = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        else
        {
            CheckForFreeze();
        }
    }

    private void CheckForFreeze()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocityBeforeFreeze = myRigidBody2D.velocity;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody2D.velocity = new Vector2(0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            myRigidBody2D.velocity = velocityBeforeFreeze;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = paddle.transform.position;
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasLaunched = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));

        if (hasLaunched)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }

    public void stopMoving()
    {
        velocityBeforeFreeze = myRigidBody2D.velocity;
        myRigidBody2D.velocity = new Vector2(0, 0);
    }

    public void continueMoving()
    {
        myRigidBody2D.velocity = velocityBeforeFreeze;
    }
}
