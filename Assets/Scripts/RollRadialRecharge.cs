using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RollRadialRecharge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().fillAmount = (PlayerController.rollCooldown - PlayerController.rollTimer) / PlayerController.rollCooldown;
    }
}
