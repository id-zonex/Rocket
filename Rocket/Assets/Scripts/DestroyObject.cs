using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [Header("Шанс що обєкт буде знищено не рекомендую юзати разом з порталами!!")]
    [Range(0.0f, 100.0f)] [SerializeField] int chanse;
    void Start()
    {
        int num = Random.Range(0, 100);
        if (num < chanse)
        {
            Destroy(this.gameObject);
        }
    }
}
