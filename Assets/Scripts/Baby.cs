using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public float cryInterval = 10f;
    private float timeLeft;
    private enum BabyState { idle, crying }
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 5;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BabyState state = timeLeft > 5 ? BabyState.crying : BabyState.idle;
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            Cry();
            float newTime = cryInterval - Globals.level;
            timeLeft = newTime >= 5 ? newTime : 5;
        }
        anim.SetInteger("state", (int)state);
    }

    void Cry()
    {
        Globals.debuffs.Clear();
        Globals.babyRage++;
        // possible stuff to do:
        // give the player a debuff until the next tantrum
        // trigger a change in the house
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player")) {
            Globals.babyRage -= Globals.pacifiers;
            if(Globals.babyRage < 0) {
                Globals.babyRage = 0;
            }
            Globals.pacifiers = 0;
            
            // do something
        }
    }
}
