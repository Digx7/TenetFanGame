using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeFlow {
  public class LevelState : MonoBehaviour
  {

    static public bool levelComplete = false,
                       levelFailed = false;

    static public void hasFailedLevel()
    {
      levelFailed = true;
    }

    static public void hasFinishedLevel()
    {
      levelComplete = true;
    }

    static public void setLevelFailed(bool input)
    {
      levelFailed = input;
    }

    static public void setLevelComplete(bool input)
    {
      levelComplete = input;
    }

  }
}
