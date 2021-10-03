using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public float cryInterval = 15f;
    private float timeLeft;
    private enum BabyState { idle, crying }
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 10;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.babyRage > 10) {
            Globals.gameOver();
        }
        BabyState state = timeLeft > (cryInterval - 5) ? BabyState.crying : BabyState.idle;
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            Cry();
            float newTime = cryInterval - Globals.babyRage / 2;
            timeLeft = newTime >= 5 ? newTime : 5;
            if(Globals.babyRage >= 9) {
                timeLeft = 5;
            }
        }
        anim.SetInteger("state", (int)state);
    }

    void Cry()
    {
        Globals.debuffs.Clear();
        Globals.babyRage++;
        DebuffRandomizer.ApplyDebuffs();
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
