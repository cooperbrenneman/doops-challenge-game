using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int TotalBones = 0;
    public Dictionary<string,KeySet> Keys;
    public List<string> Shoes;

    public Inventory(int level)
    {
        Keys = new Dictionary<string, KeySet>();
        // Key Setup
        // Hardcoded For Level 1
        if(level == 1)
        {
            Keys.Add("Blue", new KeySet("Blue"));
            Keys.Add("Red", new KeySet("Red"));
            Keys.Add("Yellow", new KeySet("Yellow"));
            Keys.Add("Green", new KeySet("Green", canBeRemoved: false));
        }

        // Boots Setup
        if(level == 3)
        {
            Shoes = new List<string>();
        }
    }

    public void AddBone()
    {
        TotalBones += 1;
    }
}
