using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTriggerEnter : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float forse;
    [SerializeField] float time;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("lolkkk");
            StartCoroutine(Fall());
        }
    } 

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(time);
        _rigidbody.isKinematic = false;
        _rigidbody.AddRelativeForce(Vector3.down * forse, ForceMode.VelocityChange);
    }
}
