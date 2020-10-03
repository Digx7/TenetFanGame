using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TimeFlow;
using UnityEngine.SceneManagement;

public class UI_Manager_Tenet : MonoBehaviour
{
    public bool isInLevel = true;
    [Space]
    public TextMeshProUGUI clockUI;
    public Color normalTimeClockUIColor,
                 invertedTimeClockUIColor;
    public Clock_Coroutines coroutines;
    [Space]
    public GameObject pauseMenu,
                      failedUI,
                      winUI;
    [Space]
    public string mainMenuSceneName;

    public void Awake()
    {
      if(isInLevel)
      {
        LevelState.setLevelFailed(false);
        LevelState.setLevelComplete(false);
        Clock.currentTime = 0;
        Clock.timeFlowRate = 1;
        Clock.timeIsMovingForward = true;
        Clock.isPaused = false;
        Clock.gameIsPaused = false;
        if(coroutines != null) coroutines.StartClock();
      }
    }

    public void Update()
    {
      if(clockUI != null) UpdateClockUI();
      if(pauseMenu != null)UpdatePauseMenu();
      if(isInLevel && LevelState.levelFailed == true) levelFailedUI();
      if(isInLevel && LevelState.levelComplete == true) levelSuccess();
    }

    public void UpdateClockUI()
    {
      clockUI.text = Clock.currentTime.ToString("F2");

      if(Clock.timeIsMovingForward) clockUI.color = normalTimeClockUIColor;
      else clockUI.color = invertedTimeClockUIColor;
    }

    public void UpdatePauseMenu()
    {
      if(Clock.gameIsPaused == true) pauseMenu.SetActive(true);
      else pauseMenu.SetActive(false);
    }

    public void leaveLevel()
    {
      SceneManager.LoadScene(mainMenuSceneName);
    }

    public void loadLevel(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }

    public void levelFailedUI()
    {
      Debug.Log("Level Failed");
      failedUI.SetActive(true);
      Clock.pause();
      GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
      foreach(GameObject _p in p) _p.GetComponent<Player_Tenet>().isPaused = true;
    }

    public void levelSuccess()
    {
      Debug.Log("Level Passed");
      winUI.SetActive(true);
      Clock.flipTimeDirection();
      GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
      foreach(GameObject _p in p) _p.GetComponent<Player_Tenet>().goFromInputToPlayback();
    }

    public void quitGame()
    {
      Debug.Log("Player is closing game");
      Application.Quit();
    }


}
