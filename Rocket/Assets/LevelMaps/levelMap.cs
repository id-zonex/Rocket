using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelMap")]
public class levelMap : ScriptableObject
{
    public int biomIndex;

    public int[] ScenesIndex;
    public int menuIndex;


    public int firstLevelIndex
    {
        get
        {
            var element = ScenesIndex[0];
            return element;
        }
    }

    public int sceneCount
    {
        get
        {
            return ScenesIndex.Length;
        }
    }


}
