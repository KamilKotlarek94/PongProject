using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public MainMenu Menu;
    public ScoreManager Scores;
    public PongMain Main;
    public int Pongs;

    private int sx, sy;
    private float BallAccel;
    private Vector3 ballStartPos;
    private Rigidbody Rb;
    private Transform Tr;
    private AudioSource PongSound;
    
    // Use this for initialization
    void Start ()
    {
        BallAccel = 1.1f;
        PongSound = GetComponent<AudioSource>();
        Rb = gameObject.GetComponent<Rigidbody>();
        Tr = gameObject.GetComponent<Transform>();
        ballStartPos = Tr.position;
        Rb.maxAngularVelocity = 200;
        Pongs = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
        float speed = Vector3.Magnitude(Rb.velocity);
        //Debug.Log(speed);
        if (speed > 40)
        {
            float brakeSpeed = speed - 40;  
            Vector3 normalisedVelocity = Rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed; 
            Rb.AddForce(-brakeVelocity);
        }
        Tr.transform.Rotate((new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)) * Time.deltaTime), Space.Self);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Racket"))
        {
            PongSound.Play();
            Rb.velocity = new Vector3(Rb.velocity.x * BallAccel, 0, Rb.velocity.z * BallAccel);
            if (System.Math.Abs(Rb.velocity.x) > 2 * System.Math.Abs(Rb.velocity.z))
                Rb.velocity = new Vector3((Rb.velocity.x / 1.5f), 0, Rb.velocity.z * 1.5f);
            if (collision.gameObject.tag == "fRacket")
                Pongs = 1;
            else if (collision.gameObject.tag == "sRacket")
                Pongs = 2;
        }
        if(collision.gameObject.tag == "Wall")
        {
            PongSound.Play();
            Rb.velocity = new Vector3(Rb.velocity.x * BallAccel, 0, Rb.velocity.z * BallAccel);
            Scores.UpdateScore(1);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        string[] Scs = new string[2];
        if (col.tag.Contains("BT"))
            Scs = col.tag.Split('.');
        if (Scs.Length > 1)
        {
            if(Main.GameMode != 3)
                Scores.UpdateScore(System.Int32.Parse(Scs[1]));
            else
                Scores.UpdateScore(3);
        }
        ResetBall(System.Int32.Parse(Scs[1]) == 1 ? 1 : -1);
    }
    
    public void ChangeBallAcceleration(int AccMode)
    {
        switch(AccMode)
        {
            case 1:
                BallAccel = 1.1f;
                break;
            case 2:
                BallAccel = 1.15f;
                break;
            case 3:
                BallAccel = 1.2f;
                break;
        }
    }

    public void StopBall()
    {
        Rb.velocity = new Vector3(0, 0, 0);
        transform.position = ballStartPos;
    }

    public void ResetBall(int direction)
    {
        Rb.velocity = new Vector3(0, 0, 0);
        transform.position = ballStartPos;
        sx = Random.Range(0, 2) == 0 ? 1 : -1;
        Pongs = (direction == -1 ? 1 : 2);
        Rb.velocity = new Vector3(10 * sx, 0, 10 * direction);
    }

    public void StartTheBallMoving(int Mode)
    {
        if(Mode == 1 || Mode == 2)
        {
            sx = Random.Range(0, 2) == 0 ? 1 : -1;
            sy = Random.Range(0, 2) == 0 ? 1 : -1;
            Rb.velocity = new Vector3(10 * sx, 0, 10 * sy);
        }
        else if(Mode == 3)
        {
            sx = Random.Range(0, 2) == 0 ? 1 : -1;
            Rb.velocity = new Vector3(10 * sx, 0, 10 * 1);
        }
    }


}
