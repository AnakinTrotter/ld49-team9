using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebuffRandomizer
{
    public static void ApplyDebuffs() {
        int n = System.Enum.GetValues(typeof(Globals.DebuffState)).Length;
        int i = 0, count = 0;
        while(count < Globals.level + 1) {
            if(i >= n)
                i = 0;
            if(Random.Range(0, 100) > 50)
                Globals.debuffs.Add((Globals.DebuffState) i);
            i++;
            count++;
        }
        if(Globals.debuffs.Count == 0) {
            int idx = Random.Range(0, n);
            Globals.debuffs.Add((Globals.DebuffState) idx);
        }
    }
}
