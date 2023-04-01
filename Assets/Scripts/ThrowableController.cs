using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    void Update()
    {
        // make it move with transform.position or with rigidbody
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        // when collision happens, to stabilize the object, make it kinetic and usable as platform for player
    }
}
