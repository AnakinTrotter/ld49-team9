using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class BlindManage : MonoBehaviour
{
    public GameObject volume;
    private PostProcessVolume pog;
    void Start()
    {
        volume.SetActive(true);
        pog = volume.GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Globals.debuffs.Contains(Globals.DebuffState.blind));
        // if(Globals.debuffs.Contains(Globals.DebuffState.blind))
        // {
        //     volume.SetActive(true);
        //     pog.isGlobal = true;
        // } else {
        //     volume.SetActive(false);
        //     pog.isGlobal = false;
        // }
    }


}
