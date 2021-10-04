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
    public static int currNumPacifiers = 0;
    public static int maxNumPacifiers = 1;  // maximum number of pacifiers to exist in the level

    // keep track of player debuffs
    public enum DebuffState { slow, fast, invert, moon, rewind } 
    public static HashSet<DebuffState> debuffs = new HashSet<DebuffState>();

    public static void gameOver() {
        SceneManager.LoadScene("GameOverScreen");
        Debug.Log("DEAD");
    }

    public static void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}