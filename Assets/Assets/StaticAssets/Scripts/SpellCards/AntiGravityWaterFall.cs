using UnityEngine;
using System.Collections;

[System.Serializable]
public class RangeFloat
{
    public float min;
    public float max;
    public float randomValue 
    {
        get { return Random.Range(min, max); }
        
    }
}

public class AntiGravityWaterFall: SpellCardBase
{
    public ObjectPool bulletPool;
    public RangeFloat spawnPositionX;
    public RangeFloat spawnPositionY;
    public RangeFloat startSpeed;
    public RangeFloat endSpeed;
    public RangeFloat lerpTime;
    private bool isStopped;
    public override IEnumerator startSpell()
    {
        bulletPool.create();
        isStopped = false;
        while (!isStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            GameObject bullet = bulletPool.createObject();
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(spawnPositionX.randomValue, spawnPositionY.randomValue, 0.0f);
            Bullet_Gravity script = bullet.GetComponent<Bullet_Gravity>();
            script.initialStartVelocity.y = startSpeed.randomValue;
            script.initialEndVelocity.y = endSpeed.randomValue;
            script.LerpTime = lerpTime.randomValue;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public override IEnumerator stopSpell()
    {
        isStopped = true;
        bulletPool.destroy();
        yield return null;
    }
}
