using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Globals {
    public static int level = 0; // counts the level for difficulty scaling
    public static int pacifiers = 0; // counts how many pacifiers the player has
    public static int lives = 2;
    public static int babyRage = 0;
    public static bool shouldSpawn = true;
    public static int currNumPacifiers = 0;
    public static int maxNumPacifiers = 5;  // maximum number of pacifiers to exist in the level
    public static bool debuffChanged = false;
    // keep track of player debuffs
    public enum DebuffState { slow, fast, invert, moon, rewind } 
    public static HashSet<DebuffState> debuffs = new HashSet<DebuffState>();

    public static void gameOver() {
        SceneManager.LoadScene("GameOverScreen");
        Debug.Log("DEAD");
    }

    public static void RestartLevel()
    {
        GameObject endScreen = GameObject.Find("LevelComplete");
        GameObject player = GameObject.Find("Player");
        Color col = endScreen.GetComponent<Image>().color;
        col.a = 1f;
        endScreen.GetComponent<Image>().color = col;
        player.GetComponent<PlayerController>().StartCoroutine(restartAfterPress());
    }

    public static IEnumerator restartAfterPress()
    {
        PlayerLife.Inv = true;
        yield return new WaitForSeconds(2);
        while (Input.anyKey)
            yield return null;
        while (!Input.anyKey)
            yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}