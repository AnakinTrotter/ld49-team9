using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player")) {
            Globals.pacifiers++;
            Destroy(transform.root.gameObject);
            // Debug.Log(Globals.pacifiers);
        }
    }
}