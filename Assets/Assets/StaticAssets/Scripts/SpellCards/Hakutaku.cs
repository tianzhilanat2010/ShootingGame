using UnityEngine;
using System.Collections;

public class Hakutaku : SpellCardBase 
{
    public ObjectPool bulletPool;
    public int oneWaveBuletNumber;
    public float oneWaveSpawnInterval;
    public float bulletWaitTime;
    public float shotInterval;
    public RangeFloat spawnPositionX;
    public RangeFloat spawnPositionY;
    private bool mIsStopped;

    void Awake()
    {
    }

    void Start()
    {

    }


    public override IEnumerator startSpell()
    {
        bulletPool.create();
        mIsStopped = false;
        while (!mIsStopped)
        {
            float startTime = Time.time;
            for (int i = 0; i < oneWaveBuletNumber; i++)
            {
                if (!mIsStopped)
                {
					AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
                    GameObject bullet = bulletPool.createObject();
                    Bullet_SpellHakutakuSpecific script = bullet.GetComponent<Bullet_SpellHakutakuSpecific>();
                    script.WaitInterval = bulletWaitTime - (Time.time - startTime);
                    bullet.transform.position = new Vector3(spawnPositionX.randomValue, spawnPositionY.randomValue, 0);
                    bullet.SetActive(true);
                    yield return new WaitForSeconds(oneWaveSpawnInterval);
                }
            }
            yield return new WaitForSeconds(shotInterval);
        }
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
        yield return null;
    }

}
