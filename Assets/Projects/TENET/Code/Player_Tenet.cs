using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TimeFlow;

public class Player_Tenet : MonoBehaviour
{
    // needs two states
    // controllable
    // playback

    public bool isInPlayback = false;
    public bool isPaused = false;
    public string id;
    public List<Vector3> positions;
    public int currentIndex = 0;

    public GameObject character;
    public bool isBlue = false;
    public Vector3 currentPosition;
    public float moveSpeed = 1.0f;
    public Vector2 moveInput;
    public Rigidbody2D rb2D;
    public Animator animator;
    public SpriteRenderer sprite;

    public Switch currentSwitch;

    private bool hasInteracted = false;

    // --- Updates ------------------------------------
    public void FixedUpdate()
    {
      if(!isInPlayback) movePlayerInputProcessing(moveInput);

      if(Input.GetKeyDown(KeyCode.Escape)) pauseGame();
    }

    public void Start()
    {
      StartCoroutine(recording());
    }

    // --- Movement -----------------------------------
    public void movePlayerInputProcessing(Vector2 input)
    {
      if(!isPaused)
      {
        playAnimation(new Vector3(input.x,input.y,0.0f));

        rb2D.velocity = input * moveSpeed * Time.deltaTime;
      }
    }

    public void interactPlayer()
    {
      if(!isPaused)
      {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("interact");

        if(currentSwitch != null)currentSwitch.toggle();

        Debug.Log("interact");
      }
    }

    public void pauseTime()
    {
      if(isPaused)
      {
        Clock.resume();
        isPaused = false;
      }
      else
      {
        Clock.pause();
        isPaused = true;
      }
    }

    public void pauseGame()
    {
      pauseTime();

      if(!isPaused)
      {
        Clock.gameIsPaused = false;
      }
      else
      {
        Clock.gameIsPaused = true;
      }
    }

    public void movePlayer(Vector3 position)
    {
      Vector3 _position = new Vector3(position.x, position.y, 0.0f);

      playAnimation(position);

      /*if (!isInPlayback)
      {
        if(_position.x != character.transform.position.x || _position.y != character.transform.position.y)
        {
          animator.SetBool("isMoving", true);
        }
        else
        {
          animator.SetBool("isMoving", false);
        }
        if (_position.x > character.transform.position.x) sprite.flipX = false;
        if(_position.x < character.transform.position.x) sprite.flipX = true;
      }
      else
      {
        if ((int)position.z == 1)
        {
          animator.SetBool("isMoving",true);
        }
        if ((int)position.z == 0)
        {
          animator.SetBool("isMoving",false);
        }
        if ((int)position.z == 2)
        {
          if((int)positions[currentIndex - 1].z != 2) interactPlayer();
        }
        if ((position.z - (int)position.z) == 0.1f) sprite.flipX = true;
        if ((position.z - (int)position.z) == 0.2f) sprite.flipX = false;
      }*/

      character.transform.position = _position;
      //Debug.Log("Moving player to: " + _position);
    }

    public void playAnimation(Vector3 position)
    {
      if (!isInPlayback)
      {
        if(position.x != 0 || position.y != 0)
        {
          animator.SetBool("isMoving", true);
        }
        else
        {
          animator.SetBool("isMoving", false);
        }
        if (position.x > 0) sprite.flipX = false;
        if(position.x < 0) sprite.flipX = true;
      }
      else
      {
        if ((int)position.z == 1)
        {
          animator.SetBool("isMoving",true);
          hasInteracted = false;
        }
        if ((int)position.z == 0)
        {
          animator.SetBool("isMoving",false);
          hasInteracted = false;
        }
        if ((int)position.z == 2)
        {
          if((int)positions[currentIndex - 1].z != 2 || (int)positions[currentIndex + 1].z != 2 && hasInteracted == false)
          {
            hasInteracted = true;
            interactPlayer();
          }
        }
        else hasInteracted = false;

        if ((position.z - (int)position.z) == 0.1f) sprite.flipX = true;
        if ((position.z - (int)position.z) == 0.2f) sprite.flipX = false;
      }
    }

    // --- Misc ---------------------------------

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

    public void goFromInputToPlayback()
    {
      currentIndex = positions.Count;
      isInPlayback = true;
      StartCoroutine(playback());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        currentSwitch = col.gameObject.GetComponent<Switch>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      if(col.gameObject.tag == "Player") LevelState.hasFailedLevel();
      if(col.gameObject.tag == "Finish") LevelState.hasFinishedLevel();
    }

    void OnTriggerExit2D()
    {
      //Debug.Log("Trigger Exit");
      currentSwitch = null;
    }

    // --- Recording ------------------------------

    public void recordPosition(float z = 0.0f)
    {
      if(!isPaused)positions.Add(new Vector3(character.transform.position.x, character.transform.position.y, z));
    }

    public float recordAnimations()
    {
      float r = 0.0f;
      if(animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Red_Player_n")) r = 1.0f;
      if(animator.GetCurrentAnimatorStateInfo(0).IsName("Interact_Red_Player_n")) r = 2.0f;
      if(animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Blue_Player_n")) r = 1.0f;
      if(animator.GetCurrentAnimatorStateInfo(0).IsName("Interact_Blue_Player_n")) r = 2.0f;

      if(sprite.flipX) r += 0.1f;  //left
      if(!sprite.flipX) r += 0.2f; //right
      return r;
    }

    // --- PlayBack ----------------------------------

    public void playBackPosition()
    {
      if(!isPaused) if (isBlue) currentIndex += -timeFlowDirection();
      else if(!isPaused) currentIndex += timeFlowDirection();

      if(currentIndex <= positions.Count -1 && currentIndex >= 0) movePlayer(positions[currentIndex]);
    }

    // --- IEnumerators -------------------------------
    IEnumerator recording()
    {
      Debug.Log("Is in Recording");
      while (!isInPlayback)
      {
        recordPosition(recordAnimations());
        yield return null;
      }
      yield return null;
    }

    IEnumerator playback()
    {
      Debug.Log("Is in Playback");
      while (isInPlayback)
      {
        playBackPosition();
        yield return null;
      }
      yield return null;
    }
}
