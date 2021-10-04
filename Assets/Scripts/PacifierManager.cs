using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PacifierManager : MonoBehaviour
{
    private List<PacifierSpawner> children;
    // Start is called before the first frame update
    void Start()
    {
        Globals.currNumPacifiers = 0;
        children = new List<PacifierSpawner>();
        foreach(Transform t in transform)
        {
            children.Add(t.GetComponentInChildren<PacifierSpawner>());
        }
        if(Globals.level > Globals.maxNumPacifiers) {
            Globals.level = Globals.maxNumPacifiers;
        }
        for(int i = 0; i < Globals.level + 1; i++) {
            int idx = Random.Range(0, children.Count);
            children[idx].Spawn();
            Globals.currNumPacifiers++;
            Debug.Log(children[idx]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Globals.shouldSpawn) {
        //     int idx = Random.Range(0, children.Count);
        //     children[idx].Spawn();
        //     Globals.currNumPacifiers++;
        //     Globals.shouldSpawn = false;
        //     Debug.Log(children[idx]);
        // }
        if(Globals.currNumPacifiers <= 0) {
            Globals.level++;
            SceneManager.LoadScene("LevelComplete");
            //Globals.RestartLevel();
        }
    }
}
