using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelText : MonoBehaviour
{
    [SerializeField] private Text txt;
    // Start is called before the first frame update
    void Start()
    {
        string level = (Globals.level + 1).ToString();
        string textOn = "Level " + level + '\n' + "Collect " + level + " pacifiers";
        txt.text = textOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey || Input.GetMouseButtonDown(0))
        SceneManager.LoadScene("Rooms");
    }
}
