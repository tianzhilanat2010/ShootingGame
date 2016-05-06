using UnityEngine;
using System.Collections;

public class Bullet_ShotSubBullet : MonoBehaviour 
{
    public ObjectPool bulletPool{get;set;}
    public float shotInterval;
    public float shotWays;
    public float shotSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        StartCoroutine(shotSubBullet());
    }

    IEnumerator shotSubBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            float baseAngle = Random.Range(0.0f, 360.0f);
            float additionalAngle = 360.0f / shotWays;
            Vector3 position = transform.position;
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot00);
            for (int i = 0; i < shotWays; i++)
            {
                GameObject bullet = bulletPool.createObject();
                bullet.SetActive(true);
                bullet.transform.position = position;
                float angle = baseAngle + additionalAngle * i;
                bullet.rigidbody2D.velocity = new Vector2(
                      Mathf.Sin(angle * Mathf.Deg2Rad) * shotSpeed
                    , Mathf.Cos(angle * Mathf.Deg2Rad) * shotSpeed);
            }
        }
    }
}
