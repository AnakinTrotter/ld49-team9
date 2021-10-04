using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMechanics : MonoBehaviour
{
    void Update() {
    }
    
    void RollStart()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollStart;
        HitDetection.hideHitbox = true;
        PlayerController.rollTimer = PlayerController.rollCooldown;
        //Physics2D.IgnoreLayerCollision(7, 10);
    }

    void RollEnd()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollEnd;
        //Physics2D.IgnoreLayerCollision(7, 10, false);
        HitDetection.hideHitbox = false;
    }
}
