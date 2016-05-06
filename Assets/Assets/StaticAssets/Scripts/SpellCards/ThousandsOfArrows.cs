using UnityEngine;
using System.Collections;

public class ThousandsOfArrows : SpellCardBase 
{

    public ObjectPool bulletPool;
    public RangeFloat spawnPositionX;
    public RangeFloat spawnPositionY;
    public RangeFloat startSpeed;
    public RangeFloat startAngle;
    private bool mIsStopped;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override IEnumerator startSpell()
    {
        bulletPool.create();
        while (!mIsStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            GameObject bullet = bulletPool.createObject();
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(spawnPositionX.randomValue, spawnPositionY.randomValue, 0.0f);
            float angle = startAngle.randomValue;
            float speed = startSpeed.randomValue;
            bullet.rigidbody2D.velocity = new Vector2(
      Mathf.Sin(angle * Mathf.Deg2Rad) * speed
    , Mathf.Cos(angle * Mathf.Deg2Rad) * speed);

            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }

    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
        yield return null;
    }
}
