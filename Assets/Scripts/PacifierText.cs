using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacifierText : MonoBehaviour
{
    private Text statusText;
    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<Text>();
        statusText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        statusText.text = Globals.pacifiers + "";
    }
}
