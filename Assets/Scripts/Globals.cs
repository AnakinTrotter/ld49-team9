using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals {
    public static int level = 1; // counts the level for difficulty scaling
    public static int pacifiers = 0; // counts how many pacifiers the player has

    // keep track of player debuffs
    public enum DebuffState { invert } 
    public static HashSet<DebuffState> debuffs = new HashSet<DebuffState>();
}