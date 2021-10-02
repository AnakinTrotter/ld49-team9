using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerLife
{
    public static void TakeDamage(){
        Globals.lives--;
        if(Globals.lives <= 0){
            Die();
        }
    }

    public static void TakeDamage(int x){
        Globals.lives -= x;
        Debug.Log("HP: " + Globals.lives);
        if(Globals.lives <= 0){
            Die();
        }
    }

    public static void Die(){
        Globals.gameOver();
    }
}

