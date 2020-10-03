using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeFlow;

public class Clock_Coroutines : MonoBehaviour
{
    public void StartClock()
    {
      StartCoroutine(running());
    }

    public void StopClock()
    {
      StopCoroutine(running());
    }

    public int timeFlowDirection()
    {
      if(Clock.timeIsMovingForward)
      {
        return Mathf.Abs(Clock.timeFlowRate);
      }
      else
      {
        return -Mathf.Abs(Clock.timeFlowRate);
      }
    }

    public void updateTime(double unit = 0)
    {
      if (unit == 0) unit = Time.deltaTime;
      Clock.currentTime += unit * Clock.timeFlowRate * timeFlowDirection();
    }

    IEnumerator running()
    {
      while(Clock.currentTime < Clock.maxTime)
      {
        yield return null;

        updateTime();

        if (Clock.currentTime < Clock.minTime) break;
        if (Clock.isPaused)
        {
            StartCoroutine(paused());
            break;
        }
      }

      if (Clock.currentTime > Clock.maxTime || Clock.currentTime < Clock.minTime) LevelState.hasFailedLevel();
      Debug.Log("The clock passed a limit");
    }

    IEnumerator paused()
    {
      while(Clock.isPaused)
      {
        yield return null;
      }
      StartCoroutine(running());
      yield return null;
    }
}
