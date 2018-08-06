using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongMain : MonoBehaviour {

    public ScoreManager Scores;
    public MainMenu Menu;
    public BallBehaviour BallB;
    public GameObject Ball;
    public GameObject RacketOne, RacketTwo, Wall;
    public GameObject ShowPaddle, ShowBall, Net;
    public GameObject Player1Trigger, Player2Trigger;
    public Transform TableTop;
    public int RacketSpeed;
    public int GameMode;

    private float ComputerRacketSpeed;
    private GameObject[] BgElements;
    private Rigidbody ShowBallRb, fRacketRb, sRacketRb;
    private Transform fRacket, sRacket;
    private Transform BallTr;
    private Vector3 ShowBallStartPos;
    private Material GreenTableMaterial;
    private Material BlueTableMaterial;

    // Use this for initialization
    void Start()
    {
        BgElements = new GameObject[3] { ShowPaddle, ShowBall, Net };
        BallTr = Ball.GetComponent<Transform>();
        ShowBallRb = ShowBall.GetComponent<Rigidbody>();
        ShowBallStartPos = ShowBall.GetComponent<Transform>().position;
        fRacketRb = RacketOne.GetComponent<Rigidbody>();
        sRacketRb = RacketTwo.GetComponent<Rigidbody>();
        fRacket = RacketOne.GetComponent<Transform>();
        sRacket = RacketTwo.GetComponent<Transform>();
        GreenTableMaterial = Resources.Load("Materials/Table1", typeof(Material)) as Material;
        BlueTableMaterial = Resources.Load("Materials/Table2", typeof(Material)) as Material;
        PrepareBackground();
        ComputerRacketSpeed = 0.16f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MainControl(GameMode);
    }

    private void MainControl(int mode)
    {
        // Player 1
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (fRacket.position.x > -7.38)
                fRacket.transform.Translate(Vector3.left * Time.deltaTime * RacketSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (fRacket.position.x < 7.38)
                fRacket.transform.Translate(Vector3.right * Time.deltaTime * RacketSpeed);
        }
        else
            fRacketRb.velocity = new Vector3(0, 0, 0);

        if(mode == 1)
        {
            // COMPUTER PLAYER
            if (BallB.Pongs == 1)
                sRacket.position = Vector3.Lerp(sRacket.position, new Vector3(BallTr.position.x, sRacket.position.y, sRacket.position.z), ComputerRacketSpeed);
            else if (BallB.Pongs == 2)
                sRacket.position = Vector3.Lerp(sRacket.position, new Vector3(BallTr.position.x, sRacket.position.y, sRacket.position.z), (ComputerRacketSpeed / 6));

            if (sRacket.position.x < -7.3)
                sRacket.position = new Vector3(-7.25f, 19.05f, -17.064f);
            if (sRacket.position.x > 7.3)
                sRacket.position = new Vector3(7.25f, 19.05f, -17.064f);
        }
        else if(mode == 2)
        {
            // Player 2
            if (Input.GetKey(KeyCode.W))
            {
                if (sRacket.position.x > -7.38)
                    sRacket.transform.Translate(Vector3.left * Time.deltaTime * RacketSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (sRacket.position.x < 7.38)
                    sRacket.transform.Translate(Vector3.right * Time.deltaTime * RacketSpeed);
            }
            else
                sRacketRb.velocity = new Vector3(0, 0, 0);
        }

    }

    public void HideBackgroundElements()
    {
        for(int i = 0; i < BgElements.Length;i++)
            BgElements[i].SetActive(false);
    }

    public void StartGame(int Mode)
    {
        Scores.ResetScores();
        Scores.Checking = true;
        GameMode = Mode;
        switch (Mode)
        {
            case 1:
                RacketOne.SetActive(true);
                RacketTwo.SetActive(true);
                Player1Trigger.SetActive(true);
                Player2Trigger.SetActive(true);
                break;
            case 2:
                RacketOne.SetActive(true);
                RacketTwo.SetActive(true);
                Player1Trigger.SetActive(true);
                Player2Trigger.SetActive(true);
                break;
            case 3:
                RacketOne.SetActive(true);
                Wall.SetActive(true);
                Player1Trigger.SetActive(true);
                Player2Trigger.SetActive(false);
                break;
        }
        Ball.SetActive(true);
        BallB.StartTheBallMoving(Mode);
    }

    public void FinishGame(int Player, int Score)
    {
        Ball.SetActive(false);
        Menu.ShowWinnerPanel(Player, Score);
    }

    public void PrepareBackground()
    {
        Ball.SetActive(false);
        Wall.SetActive(false);
        RacketOne.SetActive(false);
        RacketTwo.SetActive(false);
        Player1Trigger.SetActive(false);
        Player2Trigger.SetActive(false);
        for (int i = 0; i < BgElements.Length; i++)
            BgElements[i].SetActive(true);
        ShowBallRb.velocity = new Vector3(0, 0, 0);
        ShowBall.GetComponent<Transform>().position = ShowBallStartPos;
        ShowBallRb.AddForce(transform.right * 120);
        ShowBallRb.AddForce(transform.up * 40);
    }

    public void ChangeDifficultyLevel(int Level)
    {
        switch(Level)
        {
            case 1:
                ComputerRacketSpeed = 0.1f;
                break;
            case 2:
                ComputerRacketSpeed = 0.16f;
                break;
            case 3:
                ComputerRacketSpeed = 0.5f;
                break;
        }
    }

    public void ChangeMaterial(int color)
    {
        var mats = TableTop.GetComponent<Renderer>().materials;
        if (color == 1)
        {
            mats[0] = GreenTableMaterial;
            TableTop.GetComponent<Renderer>().materials = mats;
        }
        else if (color == 2)
        {
            mats[0] = BlueTableMaterial;
            TableTop.GetComponent<Renderer>().materials = mats;
        }
            
        
    }
}
