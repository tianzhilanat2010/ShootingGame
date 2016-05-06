using UnityEngine;
using System.Collections;

public class Bullet_SyncRotationWithVelocity : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody2D.velocity != Vector2.zero)
        {
            float rot = Vector2.Angle(Vector2.up, rigidbody2D.velocity);
            if(rigidbody2D.velocity.x > 0)
            {
                rot = -rot;
            }
            rigidbody2D.rotation = rot;
        }
    }
}
