using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public PongMain Main;
    public MenuCamController MenuCam;
    public GameObject PongCamera;
    public GameObject Menu, StartGameMenu, OptionsMenu;
    public GameObject GameUIPanel, ScorePanel, WinnerPanel, InfoPanel, Player2CtrlPanel, PausePanel;
    public GameObject Player2Score;
    public Text WinnerText;
    public Button NormalBtn;
    public Button GreenTableBtn;
    public bool GameStartBtnPressed;
    public bool GameStarted;
    public bool Winner;
    public bool GamePaused;
    public int GameMode;

	// Use this for initialization
	void Start ()
    {
        GameMode = 0;
        GameStartBtnPressed = false;
        Winner = false;
        GamePaused = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(GameStartBtnPressed && !Winner)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                InfoPanel.SetActive(false);
                ScorePanel.SetActive(true);
                Main.StartGame(GameMode);
                if (GameMode == 1 || GameMode == 2)
                    Player2Score.SetActive(true);
                else if (GameMode == 3)
                    Player2Score.SetActive(false);
                GameStartBtnPressed = false;
                GamePaused = false;
                GameStarted = true;
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                GameStartBtnPressed = false;
                Winner = false;
                BackToMainMenu();
            }
        }

        if(GameStarted && !GamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GamePaused = true;
                PausePanelCtrl(GamePaused);
                Time.timeScale = 0;
            }
        }

        if(GamePaused)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                GamePaused = false;
                PausePanelCtrl(GamePaused);
                Time.timeScale = 1;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GamePaused = false;
                GameStarted = false;
                PausePanelCtrl(GamePaused);
                Time.timeScale = 1;
                BackToMainMenu();
            }
        }

        if (Winner)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                WinnerPanel.SetActive(false);
                Main.StartGame(GameMode);
                GameStarted = true;
                Winner = false;
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                WinnerPanel.SetActive(false);
                GameStarted = false;
                Winner = false;
                MenuCam.CameraMode = 1;
                BackToMainMenu();
            }
        }
	}

    public void OpenMainMenu()
    {
        Menu.SetActive(true);
        StartGameMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        GameUIPanel.SetActive(false);
    }

    public void OpenStartGameMenu()
    {
        Menu.SetActive(false);
        StartGameMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        Menu.SetActive(false);
        StartGameMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void StartGame(int Mode)
    {
        StartGameMenu.SetActive(false);
        GameUIPanel.SetActive(true);
        WinnerPanel.SetActive(false);
        InfoPanel.SetActive(true);
        Main.HideBackgroundElements();
        switch (Mode)
        {
            case 1:
                Player2CtrlPanel.SetActive(false);
                break;
            case 2:
                Player2CtrlPanel.SetActive(true);
                break;
            case 3:
                Player2CtrlPanel.SetActive(false);
                break;
        }
        MenuCam.CameraMode = 2;
        GameMode = Mode;
        GameStartBtnPressed = true;
    }

    public void ShowWinnerPanel(int Player, int Score)
    {
        if(Player == 1 || Player == 2)
            WinnerText.text = "PLAYER " + Player + " WINS!";
        else if (Player == 3)
            WinnerText.text = "SCORE: " + Score;
        WinnerPanel.SetActive(true);
        Winner = true;
        GameStartBtnPressed = false;
        GameStarted = false;
    }

    public void PausePanelCtrl(bool Paused)
    {
        if (Paused)
            PausePanel.SetActive(true);
        else
            PausePanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        ScorePanel.SetActive(false);
        InfoPanel.SetActive(false);
        WinnerPanel.SetActive(false);
        PausePanel.SetActive(false);
        MenuCam.CameraMode = 1;
        OpenMainMenu();
        Main.PrepareBackground();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
