using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPosition : MonoBehaviour
{
    [Header("створи новий дочірній обєкт Positions з всіми позиціями")]public string ReadMe;

    List<Transform> childrenTransforms = new List<Transform>();
    Transform positions;

    void Start()
    {
        positions = transform.Find("Positions");
        GetRandomChild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRandomChild()
    {
        if (positions != null)
        {
            Transform randomChild;

            childrenTransforms.Add(transform);

            for (int i = 0; i < positions.childCount; i++)
            {
                childrenTransforms.Add(positions.GetChild(i));
                print("obj");
            }

            randomChild = childrenTransforms[Random.Range(0, childrenTransforms.Count)];
            print(randomChild.position);
            transform.position = randomChild.position;
            transform.localEulerAngles = randomChild.localEulerAngles;
        }

    }
}
