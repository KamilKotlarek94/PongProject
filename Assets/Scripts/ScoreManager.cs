using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public PongMain Main;
    private int[] Scores = new int[2];
    public Text[] ScoreTexts = new Text[2];
    public bool Checking;

	// Use this for initialization
	void Start ()
    {
        ResetScores();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Checking)
            CheckScore();
	}

    private void CheckScore()
    {
        if(Scores[0] >= 10)
        {
            if (Scores[1] <= (Scores[0] - 2))
            {
                Main.FinishGame(1, Scores[0]);
                Checking = false;
                ResetScores();
            }
        }
        if(Scores[1] >= 10)
        {
            if (Scores[0] <= (Scores[1] - 2))
            {
                Main.FinishGame(2, Scores[1]);
                Checking = false;
                ResetScores();
            }
                
        }
    }

    public void UpdateScore(int Player)
    {
        if (Player != 3)
        {
            Scores[Player - 1]++;
            ScoreTexts[Player - 1].text = Scores[Player - 1].ToString();
        }
        else
            Main.FinishGame(Player, Scores[0]);
    }

    public void ResetScores()
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            Scores[i] = 0;
            ScoreTexts[i].text = 0.ToString();
        }
    }
}
