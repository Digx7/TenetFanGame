using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tenet_Input : MonoBehaviour
{
    /* Description --
     *  This script will handel the player inputs
     */
    /* Notes --
     *  This script may need to be adjusted as I learn more about unitys input system
     */

    public Player_Tenet playerScript;//references the player script
    //inputactions
    public Tenet_Player Player; // is the input action map
    public GameObject _Player;

    public void Awake()
    {
        Player = new Tenet_Player();

        BindInputs();
    }
    // this function will run at the start of the scene


    // this function will bind the needed variables to the player prefab

    public void BindInputs ()
    {
        Player.Character.Move.performed += ctx => playerScript.moveInput = ctx.ReadValue<Vector2>();//ties inputs to given values
        Player.Character.Interact.performed += ctx => playerScript.interactPlayer();
        Player.Character.Pause.performed += ctx => playerScript.pauseTime();
    }



    private void OnEnable()
    {
      Player.Enable();
    }
    // this function will enabled the inputsystem when this gameObject is enabled

    private void OnDisable()
    {
      Player.Disable();
    }
    // this function will disabled the inputsystem when this gameObject is disabled

}
