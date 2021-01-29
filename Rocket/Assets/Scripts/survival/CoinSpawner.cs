using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public int CountToLeave;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; transform.childCount > CountToLeave; i++)
        {
            Transform child = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(child.gameObject);

            if (i == transform.childCount)
            {
                break;
            }
            i++;
        }
    }
}
