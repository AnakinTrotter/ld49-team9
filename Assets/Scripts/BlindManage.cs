using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindManage : MonoBehaviour
{
    public GameObject volume;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.debuffs.Contains(Globals.DebuffState.blind))
        {
            volume.SetActive(true);
        } else {
            volume.SetActive(false);
        }
    }


}
