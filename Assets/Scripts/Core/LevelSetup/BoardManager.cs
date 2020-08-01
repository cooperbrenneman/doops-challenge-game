using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardManager : MonoBehaviour
{
    public int rows = 32;
    public int columns = 32;

    public static int RequiredBoneCount;
    public abstract void GenerateLevel();

}
