using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public float cryInterval = 10f;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = cryInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            Cry();
            float newTime = cryInterval - Globals.level;
            timeLeft = newTime >= 5 ? newTime : 5;
        }
    }

    void Cry()
    {
        Globals.debuffs.Clear();
        // possible stuff to do:
        // give the player a debuff until the next tantrum
        // trigger a change in the house
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player")) {
            int temp = Globals.pacifiers;
            Globals.pacifiers = 0;
            
            // do something
        }
    }
}
