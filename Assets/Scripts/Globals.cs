using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Globals {
    public static int level = 0; // counts the level for difficulty scaling
    public static int pacifiers = 0; // counts how many pacifiers the player has
    public static int lives = 2;
    public static int babyRage = 0;
    public static bool shouldSpawn = true;

    // keep track of player debuffs
    public enum DebuffState { invert, slow } 
    public static HashSet<DebuffState> debuffs = new HashSet<DebuffState>();

    public static void gameOver() {
        Debug.Log("DEAD");
    }

    public static void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}