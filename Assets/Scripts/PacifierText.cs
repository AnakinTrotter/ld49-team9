using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacifierText : MonoBehaviour
{
    // public GameObject backdrop;
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
        // backdrop.transform.localScale = new Vector3(Globals.pacifiers < 10 ? 40 : 80, 50, 1);
        // backdrop.transform.position = new Vector3(Globals.pacifiers < 10 ? -40 : -25, 2, 0);
        statusText.text = Globals.pacifiers + "";
    }
}
