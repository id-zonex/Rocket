using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] Transform spawnPosition;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Teleport(other.transform);
                break;
        }
    }

    void Teleport(Transform obj)
    {
        obj.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
        // такий поворот буде при виході з порталу тобто на другому порталі
        print(spawnPosition.localEulerAngles.z);
        obj.rotation = Quaternion.Euler(spawnPosition.eulerAngles.x, spawnPosition.eulerAngles.y, spawnPosition.eulerAngles.z);
    } 
}
