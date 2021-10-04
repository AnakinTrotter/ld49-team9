using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerLife
{
    public static bool Inv;
    public static bool TakeDamage(){
        return TakeDamage(1);
    }

    public static bool TakeDamage(int x){
        bool take = !PlayerController.IsRolling && !Inv;
        if (take)
        {
            Globals.lives -= x;
            Invincible();
        }
        //Debug.Log("HP: " + Globals.lives);
        if(Globals.lives <= 0){
            Die();
        }
        return take;
    }

    public static void Die(){
        Globals.gameOver();
    }

    public static void Invincible()
    {
        Inv = true;
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().StartCoroutine(PlayerLife.invincibleTime(2f));
        Inv = false;
    }

    public static IEnumerator invincibleTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

