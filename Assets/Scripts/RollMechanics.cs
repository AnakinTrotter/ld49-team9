using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMechanics : MonoBehaviour
{

    void Update() {
        // roll cooldown
        if(PlayerController.rollTimer > 0) {
            PlayerController.rollTimer -= Time.deltaTime;
        }
    }
    
    void RollStart()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollStart;
        HitDetection.hideHitbox = true;
    }

    void RollEnd()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollEnd;
        HitDetection.hideHitbox = false;
        PlayerController.rollTimer = PlayerController.rollCooldown;
    }
}
