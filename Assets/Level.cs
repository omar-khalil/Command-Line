using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int number;
    [HideInInspector]
    public int maxGoals = 0;
    public int curGoals
    {
        set
        {
            CurGoals = value;
            CheckWin();
        }
        get
        {
            return CurGoals;
        }
    }
    private int CurGoals;

    void CheckWin()
    {
        if (CurGoals == maxGoals)
        {
            LevelManager.instance.LoadNextLevel(1, false);
        } else
        {
            print("Didn't win because CurGoals = " + CurGoals + " while maxGoals = " + maxGoals);
        }
    }
}
