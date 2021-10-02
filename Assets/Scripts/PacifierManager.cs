using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacifierManager : MonoBehaviour
{
    private List<PacifierSpawner> children;
    // Start is called before the first frame update
    void Start()
    {
        children = new List<PacifierSpawner>();
        foreach(Transform t in transform)
        {
            children.Add(t.GetComponentInChildren<PacifierSpawner>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.shouldSpawn) {
            int idx = Random.Range(0, children.Count);
            children[idx].Spawn();
            Globals.shouldSpawn = false;
            Debug.Log(children[idx]);
        }
    }
}
