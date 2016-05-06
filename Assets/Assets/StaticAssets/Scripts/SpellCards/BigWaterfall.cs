using UnityEngine;
using System.Collections;

public class BigWaterfall : SpellCardBase
{
    public ObjectPool bulletPool0;
    public RangeFloat spawnPositionX0;
    public RangeFloat spawnPositionY0;
    public RangeFloat startSpeed0;
    public RangeFloat endSpeed0;
    public RangeFloat lerpTime0;

    public ObjectPool bulletPool1;
    public RangeFloat spawnPositionX1;
    public RangeFloat spawnPositionY1;
    public RangeFloat startSpeed1;
    public RangeFloat startAngle1;
    private bool isStopped;
    public override IEnumerator startSpell()
    {
        bulletPool0.create();
        isStopped = false;
        StartCoroutine(startShotBullet1());
        while (!isStopped)
        {
            GameObject bullet = bulletPool0.createObject();
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(spawnPositionX0.randomValue, spawnPositionY0.randomValue, 0.0f);
            Bullet_Gravity script = bullet.GetComponent<Bullet_Gravity>();
            script.initialStartVelocity.y = startSpeed0.randomValue;
            script.initialEndVelocity.y = endSpeed0.randomValue;
            script.LerpTime = lerpTime0.randomValue;
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public IEnumerator startShotBullet1()
    {
        bulletPool1.create();
		yield return new WaitForSeconds (3);
        while (!isStopped)
        {
            GameObject bullet = bulletPool1.createObject();
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(spawnPositionX1.randomValue, spawnPositionY1.randomValue, 0.0f);
            float angle = startAngle1.randomValue;
            float speed = startSpeed1.randomValue;
            bullet.rigidbody2D.velocity = new Vector2(
      Mathf.Sin(angle * Mathf.Deg2Rad) * speed
    , Mathf.Cos(angle * Mathf.Deg2Rad) * speed);

            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }

    }

    public override IEnumerator stopSpell()
    {
        isStopped = true;
        bulletPool0.destroy();
        bulletPool1.destroy();
        yield return null;
    }
}

