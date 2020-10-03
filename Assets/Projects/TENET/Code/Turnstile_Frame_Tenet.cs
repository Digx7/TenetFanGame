using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnstile_Frame_Tenet : MonoBehaviour
{
    public Turnstile_Tenet turnstile;
    public GameObject spawnPoint;

    public void OnCollisionEnter2D(Collision2D col)
    {
      if(col.gameObject.tag == "Player") turnstile.enterTurnstile(col.gameObject, gameObject);
    }

    public void SpawnPlayer(GameObject player)
    {
      Debug.Log("New Player was spawned");
      Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
    }
}
