using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    public Text statusText;
    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<Text>();
        statusText.text = "starting...";
    }

    // Update is called once per frame
    void Update()
    {
        statusText.text = $"Level: {Globals.level.ToString()}   Lives: {Globals.lives.ToString()}   Pacifiers: {Globals.pacifiers.ToString()}  " +
            $"Baby Rage: {Globals.babyRage.ToString()}";
    }
}
