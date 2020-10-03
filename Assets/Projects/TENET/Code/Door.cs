using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    public GameObject boxCollider;
    public SpriteRenderer render;
    public Sprite closedSprite;
    public Sprite openSprite;


    public void OpenDoor()
    {
      isOpen = true;
      boxCollider.SetActive(false);
      render.sprite = openSprite;
    }

    public void CloseDoor()
    {
      isOpen = false;
      boxCollider.SetActive(true);
      render.sprite = closedSprite;
    }
}
