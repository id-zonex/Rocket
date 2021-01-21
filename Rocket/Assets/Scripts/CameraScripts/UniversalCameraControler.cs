using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalCameraControler : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] float smoothTime = 0.2f;
    Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
         Vector3 targetPosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);
         transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
