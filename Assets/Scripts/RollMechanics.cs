using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMechanics : MonoBehaviour
{
    void RollStart()
    {
        PlayerController.IsRolling = true;
    }

    void RollEnd()
    {
        PlayerController.IsRolling = false;
    }
}
