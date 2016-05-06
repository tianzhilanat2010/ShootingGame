using UnityEngine;
using System.Collections;

public class Bullet_SpellThunderSpecific : MonoBehaviour 
{
    public float changeDirectionInterval;
    public float angleChange;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        StartCoroutine(changeDirection());
    }

    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(changeDirectionInterval);
        Vector2 velocity = rigidbody2D.velocity;
        float length = velocity.magnitude;
        float angle = Vector2.Angle(Vector2.up, velocity);
        if(rigidbody2D.velocity.x < 0)
        {
            angle = -angle;
        }
        Vector2 velocity0 = new Vector2(Mathf.Sin((angle + angleChange) * Mathf.Deg2Rad),
            Mathf.Cos((angle + angleChange) * Mathf.Deg2Rad)) * length;
        Vector2 velocity1 = new Vector2(Mathf.Sin((angle - angleChange) * Mathf.Deg2Rad),
            Mathf.Cos((angle - angleChange) * Mathf.Deg2Rad)) * length;
        while (true)
        {
            rigidbody2D.velocity = velocity0;
            yield return new WaitForSeconds(changeDirectionInterval);
            rigidbody2D.velocity = velocity1;
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }
}
