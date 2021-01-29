using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] int CountToLeave;

    public Transform EndPos;
    public Transform MiddlePos;
    public Transform StartPos;
    public GameObject Wall;

    void Start()
    {
    }
}
