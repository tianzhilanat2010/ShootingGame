using UnityEngine;
using System.Collections;

public class HugeArrow : SpellCardBase
{
    public ObjectPool bulletPool;
    public int shotWays;
    public float shotInterval;
    public float speed;
    public ObjectPool subBulletPool;

    private GameObject mEnemy;
    private GameObject mPlayer;
    private bool mIsStopped;

    void Awake()
    {
        mEnemy = (GameObject)GameObject.FindGameObjectWithTag("Enemy");
        mPlayer = (GameObject)GameObject.FindGameObjectWithTag("Player");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override IEnumerator startSpell()
    {
        bulletPool.create();
        subBulletPool.create();
        mIsStopped = false;
        while (!mIsStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.Charge02);
			yield return new WaitForSeconds(1.0f);
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot00);
            Vector2 direction = mPlayer.rigidbody2D.position - mEnemy.rigidbody2D.position;
            float baseAngle = Vector2.Angle(Vector2.up, direction);
            if (direction.x < 0)
            {
                baseAngle = -baseAngle;
            }
            float additionalAngle = 360.0f / shotWays;
            Vector3 position = mEnemy.transform.position;
            for (int i = 0; i < shotWays; i++)
            {
                GameObject bullet = bulletPool.createObject();
                Bullet_ShotSubBullet script = bullet.GetComponent<Bullet_ShotSubBullet>();
                script.bulletPool = subBulletPool;
                bullet.SetActive(true);
                bullet.transform.position = position;
                float angle = baseAngle + additionalAngle * i;
                bullet.rigidbody2D.velocity = new Vector2(
                      Mathf.Sin(angle * Mathf.Deg2Rad) * speed
                    , Mathf.Cos(angle * Mathf.Deg2Rad) * speed);
            }
            yield return new WaitForSeconds(shotInterval);
        }
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
        subBulletPool.destroy();
        yield return null;
    }
}
