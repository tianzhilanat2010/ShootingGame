using UnityEngine;
using System.Collections;

public class Majinfukusyou : SpellCardBase
{
    public Vector2[] SpawnPoint;
    public ObjectPool BulletPool0;
    public ObjectPool BulletPool1;
    public ObjectPool BulletPool2;
    public ObjectPool BulletPool3;
    public ObjectPool BulletPool4;
    public ObjectPool SoftLaserPool;

    private GameObject mEnemy;
    private GameObject mPlayer;
    private bool mIsStopped;

    void Awake()
    {
        mEnemy = (GameObject)GameObject.FindGameObjectWithTag("Enemy");
        mPlayer = (GameObject)GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(startSpell());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator startSpell()
    {
        StartCoroutine(shootBullet0());
        StartCoroutine(shootBullet1());
        StartCoroutine(shootBullet2());
        StartCoroutine(shootBullet3());
        StartCoroutine(shootBullet4());
        StartCoroutine(shootSoftLaser());
        yield return new WaitForSeconds(10.0f);
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        BulletPool0.destroy();
        BulletPool1.destroy();
        BulletPool2.destroy();
        BulletPool3.destroy();
        BulletPool4.destroy();
        SoftLaserPool.destroy();
        yield return null;
    }

    public IEnumerator shootBullet0()
    {
        BulletPool0.create();
        float angle = 180.0f;
        float changeAngle = 0.0f;
        float addScale = -0.4f;
        while (!mIsStopped)
        {
            if (changeAngle < -7.0f)
            {
                addScale = 0.4f;
            }
            else if (changeAngle > 7.0f)
            {
                addScale = -0.4f;
            }
            changeAngle += addScale;
            for (int i = 0; i < SpawnPoint.Length; i++)
            {
                float a = (i == 0 || i == 3)? changeAngle : - changeAngle;
                spawnBullet0(SpawnPoint[i], angle + a + 0.0f, 5.0f);
                spawnBullet0(SpawnPoint[i], angle + a + 60.0f, 5.0f);
                spawnBullet0(SpawnPoint[i], angle + a - 60.0f, 5.0f);
            }
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }
    }

    public void spawnBullet0(Vector2 position, float angle, float speed)
    {
        GameObject bullet = BulletPool0.createObject();
        bullet.transform.position = position;
        bullet.SetActive(true);
        float radius = angle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * speed;
        bullet.rigidbody2D.velocity = velocity;
    }

    public IEnumerator shootBullet1()
    {
        BulletPool1.create();
        while (!mIsStopped)
        {
            Vector2 v = mPlayer.rigidbody2D.position - mEnemy.rigidbody2D.position;
            float angle = Vector2.Angle(Vector2.up, v);
            if (v.x < 0)
            {
                angle = -angle;
            }
            spawnBullet1(mEnemy.rigidbody2D.position, angle + 0.0f,  3.0f);
            spawnBullet1(mEnemy.rigidbody2D.position, angle + 60.0f, 3.0f);
            spawnBullet1(mEnemy.rigidbody2D.position, angle - 60.0f, 3.0f);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void spawnBullet1(Vector2 position, float angle, float speed)
    {
        GameObject bullet = BulletPool1.createObject();
        bullet.transform.position = position;
        bullet.SetActive(true);
        float radius = angle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * speed;
        bullet.rigidbody2D.velocity = velocity;
    }

    public IEnumerator shootBullet2()
    {
        BulletPool2.create();
        float angle = 180.0f;
        float changeAngle = 0.0f;
        float addScale = 20.0f;
        while (!mIsStopped)
        {
            if (changeAngle < -90.0f)
            {
                addScale = 20.0f;
            }
            else if (changeAngle > 90.0f)
            {
                addScale = -20.0f;
            }
            changeAngle += addScale;
            spawnBullet2(mEnemy.rigidbody2D.position, angle + changeAngle + 0.0f, 4.0f);

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }
    }

    public void spawnBullet2(Vector2 position, float angle, float speed)
    {
        GameObject bullet = BulletPool2.createObject();
        bullet.transform.position = position;
        bullet.SetActive(true);
        float radius = angle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * speed;
        bullet.rigidbody2D.velocity = velocity;
    }

    public IEnumerator shootBullet3()
    {
        BulletPool3.create();
        int side = 1;
        while (!mIsStopped)
        {
            side = -side;
            Vector2 pos = new Vector2(Random.Range(1.0f, 3.0f)* side, Random.Range(3.0f, 5.0f));
            spawnBullet3(pos, 180.0f, 2.5f);

            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void spawnBullet3(Vector2 position, float angle, float speed)
    {
        GameObject bullet = BulletPool3.createObject();
        bullet.transform.position = position;
        bullet.SetActive(true);
        float radius = angle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * speed;
        bullet.rigidbody2D.velocity = velocity;
    }

    public IEnumerator shootBullet4()
    {
        BulletPool4.create();

        while (!mIsStopped)
        {
            Vector2 v = mPlayer.rigidbody2D.position - mEnemy.rigidbody2D.position;
            float angle = Vector2.Angle(Vector2.up, v);
            if (v.x < 0)
            {
                angle = -angle;
            }

            for (int i = 0; i < 16; i++)
            {
                spawnBullet4(mEnemy.rigidbody2D.position, angle + i * (360.0f / 16.0f), 3.5f);
            }

            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void spawnBullet4(Vector2 position, float angle, float speed)
    {
        GameObject bullet = BulletPool4.createObject();
        bullet.transform.position = position;
        bullet.SetActive(true);
        float radius = angle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * speed;
        bullet.rigidbody2D.velocity = velocity;
    }

    public IEnumerator shootSoftLaser()
    {
        SoftLaserPool.create();
        for (int i = 0; i < SpawnPoint.Length; i++)
        {
            GameObject laser = SoftLaserPool.createObject();
            laser.transform.position = SpawnPoint[i];
            MeshRenderer r = laser.GetComponent<MeshRenderer>();
            r.sortingOrder = i;
            SoftLaser script = laser.GetComponent<SoftLaser>();
            script.StartDirectionIsLeft = (i == 0 || i == 3) ? true : false;
            laser.SetActive(true);
        }
        yield return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < SpawnPoint.Length; i++)
        {
            Vector2 position = SpawnPoint[i];
            Gizmos.DrawSphere(position, 0.3f);
        }
    }
}

