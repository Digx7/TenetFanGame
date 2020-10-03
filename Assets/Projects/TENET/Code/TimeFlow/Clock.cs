using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeFlow {
  public class Clock : MonoBehaviour
  {
      /*---Description---
            This script will be the universal clock behind every level
      */

      static public double currentTime = 0,
                    minTime = -60,
                    maxTime = 60;

      static public int timeFlowRate = 1;

      static public bool timeIsMovingForward = true,
                         isPaused = false,
                         gameIsPaused = false;

      // ---Setup-------------------------

      static public void setTime (double newCurrentTime = 0, double newMinTime = -60, double newMaxTime = 60)
      {
        currentTime = newCurrentTime;
        minTime = newMinTime;
        maxTime = newMaxTime;
      }

      static public void setTimeFlowRate (int newTimeFlowRate)
      {
        timeFlowRate = newTimeFlowRate;
      }

      static public void setTimeDirectionNormal()
      {
        timeIsMovingForward = true;
      }

      static public void setTimeDirectionInverted()
      {
        timeIsMovingForward = false;
      }

      static public void flipTimeDirection()
      {
        timeIsMovingForward = !timeIsMovingForward;
      }

      static public void setTimePaused(bool i)
      {
        isPaused = i;
      }

      static public void pause()
      {
        isPaused = true;
      }

      static public void resume()
      {
        isPaused = false;
      }

      // ---Get---------------------------

      static public double getCurrentTime()
      {
        return currentTime;
      }

      static public double getMinTime()
      {
        return minTime;
      }

      static public double getMaxTime()
      {
        return maxTime;
      }

      static public int getTimeFlowRate()
      {
        return timeFlowRate;
      }

      static public bool isTimeMovingForward()
      {
        return timeIsMovingForward;
      }

      static public bool isTimePaused()
      {
        return isPaused;
      }
  }
}
