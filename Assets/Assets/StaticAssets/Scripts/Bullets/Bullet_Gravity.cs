using UnityEngine;
using System.Collections;

public class Bullet_Gravity : MonoBehaviour 
{
    public Vector2 initialStartVelocity;
    public Vector2 initialEndVelocity;
    public float LerpTime;

    // Use this for initialization
    void Start () 
    {

    }

    // Update is called once per frame
    void FixedUpdate() 
    {
    
    }

    void OnEnable()
    {
        StartCoroutine("startAction");
    }

    public IEnumerator startAction()
    {
        gameObject.rigidbody2D.velocity = initialStartVelocity;
        Vector2 accel = (initialEndVelocity - initialStartVelocity) / (LerpTime * 60);
        while ((rigidbody2D.velocity - initialEndVelocity).sqrMagnitude > accel.sqrMagnitude)
        {
            rigidbody2D.velocity += accel;
            yield return new WaitForFixedUpdate();
        }
        rigidbody2D.velocity = initialEndVelocity;
    }
}
