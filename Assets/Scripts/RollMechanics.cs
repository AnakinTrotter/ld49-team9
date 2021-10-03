using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMechanics : MonoBehaviour
{

    void Update() {
        // roll cooldown
        Debug.Log(PlayerController.rollTimer);
        if(PlayerController.rollTimer > 0) {
            PlayerController.rollTimer -= Time.deltaTime;
        }
    }
    
    void RollStart()
    {
        PlayerController.IsRolling = true;
    }

    void RollEnd()
    {
        PlayerController.IsRolling = false;
        PlayerController.rollTimer = PlayerController.rollCooldown;
    }
}
