using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(runOnClick);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void runOnClick()
    {
        SceneManager.LoadScene("Rooms");
    }
}
