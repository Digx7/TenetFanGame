using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public Door[] doors;
    public SpriteRenderer sprite;

    public void Awake()
    {
      sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void toggle()
    {
      sprite.flipX = !sprite.flipX;
      foreach(Door _door in doors)
      {
        if(_door.isOpen == false) _door.OpenDoor();
        else _door.CloseDoor();
      }
    }
}
