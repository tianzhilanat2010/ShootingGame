using UnityEngine;
using System.Collections;

public class Laser_Accelarator : MonoBehaviour 
{
    public float StartAngle;
    public float EndAngle;
    public float AngleChangeInterval;
    public float Speed;
    public ObjectPool FlowerBulletPool;

    private float mCurrentAngle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnEnable()
    {
        StartCoroutine(updateVelocity());
        StartCoroutine(updateFlower());
    }

    private IEnumerator updateFlower()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            int makeFlower = Random.Range(0, 10);
            if (0 == makeFlower)
            {
                StartCoroutine(startMakeFlower());
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator startMakeFlower()
    {
        Vector3 position = gameObject.transform.position;
		if (position.x >= -3.5f && position.x <= 3.5f
			&& position.y >= -5.2f && position.y <= 5.2f) 
		{
			yield return new WaitForSeconds (0.5f);
			float baseAngle = Random.Range (0.0f, 360.0f);
			for (int i = 0; i < 5; i++) {
					float angle = baseAngle + i * (360.0f / 5.0f);
					float radius = angle * Mathf.Deg2Rad;
					GameObject bullet = FlowerBulletPool.createObject ();
					bullet.transform.position = position;
					bullet.SetActive (true);
					bullet.rigidbody2D.velocity = new Vector2 (Mathf.Sin (radius), Mathf.Cos (radius)) * 0.2f;
			}
			AudioManager.Instance.playSfx (AudioManager.SFX.BulletShot00);
		}
    }

    IEnumerator updateVelocity()
    {
        mCurrentAngle = StartAngle;
        
        float changeTimes = (EndAngle - StartAngle) / AngleChangeInterval;
        if(changeTimes < 0)
        {
            changeTimes = - changeTimes;
            AngleChangeInterval = -AngleChangeInterval;
        }
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < changeTimes; i++)
        {
            mCurrentAngle += AngleChangeInterval;
            float radius = mCurrentAngle * Mathf.Deg2Rad;
            Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * Speed;
            rigidbody2D.velocity = velocity;

            yield return new WaitForFixedUpdate();
        }

        mCurrentAngle = EndAngle;
        float radiusEnd = mCurrentAngle * Mathf.Deg2Rad;
        rigidbody2D.velocity = new Vector2(Mathf.Sin(radiusEnd), Mathf.Cos(radiusEnd)) * Speed;

    }

    void FixedUpdate()
    {

    }


}
