using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacifierManager : MonoBehaviour
{
    private List<GameObject> children;
    // Start is called before the first frame update
    void Start()
    {
        children = new List<GameObject>();
        foreach(Transform t in transform)
        {
            children.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
