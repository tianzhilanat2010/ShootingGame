using UnityEngine;
using System.Collections;

public class Thunder : SpellCardBase 
{
    public ObjectPool bulletPool;
    public float ways;
    public float shotTime;
    public float shotInterval;
    public float shotSpeed;
    public float moveStartTime;
    public float moveTime;
    public float moveInterval;
    public RangeFloat moveDistance;
    public RangeFloat moveRangeX;

    private bool isStopped;
    private GameObject mEnemy;
    private Vector2 mEnemyStartPosition;

    void Awake()
    {
        mEnemy = (GameObject)GameObject.FindGameObjectWithTag("Enemy");
    }

    public override IEnumerator startSpell()
    {
        mEnemyStartPosition = mEnemy.rigidbody2D.position;
        bulletPool.create();
        isStopped = false;
        StartCoroutine(shotBullet());
        StartCoroutine(enemyMove());
        yield return null;
    }

    public IEnumerator shotBullet()
    {
        bulletPool.create();
        isStopped = false;
        while (!isStopped)
        {

            float baseAngle = Random.Range(0.0f, 360.0f);
            float additionalAngle = 360.0f / ways;
            float startTime = Time.time;
            while (startTime + shotTime > Time.time)
            {
				AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
                for (int i = 0; i < ways; i++)
                {
                    if (!isStopped)
                    {
                        GameObject bullet = bulletPool.createObject();
                        bullet.SetActive(true);
                        bullet.transform.position = mEnemy.transform.position;

                        float angle = baseAngle + additionalAngle * i;
                        bullet.rigidbody2D.velocity = new Vector2(
          Mathf.Sin(angle * Mathf.Deg2Rad) * shotSpeed
        , Mathf.Cos(angle * Mathf.Deg2Rad) * shotSpeed);
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    yield return new WaitForFixedUpdate();
                }
            }
            yield return new WaitForSeconds(shotInterval);
        }
    }

    public IEnumerator enemyMove()
    {
        yield return new WaitForSeconds(moveStartTime);
        int direction = 1;
        while (!isStopped)
        {
            float dis = moveDistance.randomValue;

            if(mEnemy.transform.position.x < moveRangeX.min)
            {
                direction = 1;
            }
            else if (mEnemy.transform.position.x > moveRangeX.max)
            {
                direction = -1;
            }
            else
            {
                direction = -direction;
            }
            dis *= direction;
            float distancePerFrame = dis / (moveTime * 60.0f);
            float startTime = Time.time;
            while (startTime + moveTime > Time.time && !isStopped)
            {
                mEnemy.transform.position = mEnemy.transform.position + new Vector3(distancePerFrame, 0, 0);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(moveInterval);
        }
    }

    public override IEnumerator stopSpell()
    {
        isStopped = true;
        bulletPool.destroy();
        mEnemy.rigidbody2D.position = mEnemyStartPosition;
        yield return null;
    }
}
