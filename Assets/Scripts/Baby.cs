using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public GameObject returningPacifier;
    public float cryInterval = 25f;
    private float timeLeft;
    private enum BabyState { idle, crying }
    private Animator anim;
    // Start is called before the first frame update

    private float debuffCooldown = 10f;
    private float debuffTimer = 10f;

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
        if(Globals.babyRage < 0) {
            Globals.babyRage = 0;
        }
        if(debuffTimer > 0 && Globals.debuffs.Count > 0) {
            debuffTimer -= Time.deltaTime;
        } else {
            Globals.debuffs.Clear();
            debuffTimer = debuffCooldown;
            Globals.debuffChanged = true;
        }
        BabyState state = timeLeft > (cryInterval - 5) ? BabyState.crying : BabyState.idle;
        if(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        } else {
            Cry();
            float newTime = cryInterval - Globals.babyRage;
            // Debug.Log(newTime + " " + cryInterval + " " + Globals.babyRage);
            timeLeft = newTime >= 5 ? newTime : 5;
        }
        anim.SetInteger("state", (int)state);
    }

    void Cry()
    {
        Globals.babyRage++;
        DebuffRandomizer.ApplyDebuffs();
        Globals.debuffChanged = true;
        // possible stuff to do:
        // give the player a debuff until the next tantrum
        // trigger a change in the house
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject pacifierSprite = GameObject.Find("PacifierSprite");
        if(col.gameObject.name.Equals("Player") && Globals.pacifiers > 0) {
            StartCoroutine(collectPacifiers());
            
            // do something
        }
    }

    IEnumerator collectPacifiers()
    {
        GameObject pacifierSprite = GameObject.Find("PacifierSprite");
        while (Globals.pacifiers > 0)
        {
            Instantiate(returningPacifier, pacifierSprite.transform);
            Globals.pacifiers--;
            Globals.currNumPacifiers--;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
