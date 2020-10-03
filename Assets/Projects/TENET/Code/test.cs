using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeFlow;
using TMPro;

public class test : MonoBehaviour
{

  //public TextMeshProUGUI clockUI;

  public Clock_Coroutines code;
  public Player_Tenet player;

  public void Start()
  {
    Clock.setTimeDirectionNormal();
    Debug.Log(Clock.timeIsMovingForward);
    code.StartClock();
  }

  public void Update()
  {
    //clockUI.text = Clock.currentTime.ToString("F2");

    if (Input.GetKeyDown("f")) testInversion();
    if (Input.GetKeyDown("i")) testFlip();
  }

  public void testInversion()
  {
    Clock.pause();
    Clock.flipTimeDirection();
    player.goFromInputToPlayback();
    Clock.resume();
  }

  public void testFlip()
  {
    Clock.pause();
    Clock.flipTimeDirection();
    Clock.resume();
  }
}
