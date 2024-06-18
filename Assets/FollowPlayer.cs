using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public Vector3 Offset;
    public float speed;




    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, player.transform.position + Offset, speed * Time.deltaTime);
    }
}
