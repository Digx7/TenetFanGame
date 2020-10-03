using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeFlow;

public class Turnstile_Tenet : MonoBehaviour
{

  public GameObject playerRedPreFab,   // Two player prefabs
                    playerBluePreFab,
                    frame01,           // Two frames on turnstile
                    frame02;

  //hi
  public void enterTurnstile (GameObject playerType, GameObject frame)
  {
    if (playerType.GetComponent<Player_Tenet>().isInPlayback == false)
    {
      Clock.pause();
      Clock.flipTimeDirection();
      playerType.GetComponent<Player_Tenet>().goFromInputToPlayback();

      if(playerType.GetComponent<Player_Tenet>().isBlue)
      {
        if(frame == frame01) SpawnPlayer(frame02, playerRedPreFab);
        if(frame == frame02) SpawnPlayer(frame01, playerRedPreFab);
      }
      else
      {
        if(frame == frame01) SpawnPlayer(frame02, playerBluePreFab);
        if(frame == frame02) SpawnPlayer(frame01, playerBluePreFab);
      }

      Clock.resume();
    }
  }

  public void SpawnPlayer(GameObject spawner, GameObject objectToSpawn)
  {
    spawner.GetComponent<Turnstile_Frame_Tenet>().SpawnPlayer(objectToSpawn);
  }

}
