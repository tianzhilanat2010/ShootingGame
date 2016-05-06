using UnityEngine;
using System.Collections;

public class HomingLaser : SpellCardBase
{
    public ObjectPool LaserPool;
    public ObjectPool FlowerBullerPool;
    public float[] StartAngleDiffList;
    public float Speed;
    public float ShotInterval;
    public float AngleChangeInterval;

    private GameObject mPlayer;
    private GameObject mEnemy;

    private bool mIsStopped;

    void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }

	// Use this for initialization
	void Start () {
        //StartCoroutine(startSpell());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override IEnumerator startSpell()
    {
        LaserPool.create();
        FlowerBullerPool.create();
        mIsStopped = false;
        while (!mIsStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.Charge02);
			yield return new WaitForSeconds(1.0f);
            Vector2 v = mPlayer.rigidbody2D.position - mEnemy.rigidbody2D.position;
            float angle = Vector2.Angle(Vector2.up, v);
            if (v.x < 0)
            {
                angle = -angle;
            }
            for (int i = 0; i < StartAngleDiffList.Length; i++)
            {
                GameObject laser = LaserPool.createObject();
                GameObject header = laser.GetComponent<HenyoriLaser>().mHeader;
                header.transform.position = mEnemy.transform.position;
                Laser_Accelarator script = header.GetComponent<Laser_Accelarator>();
                script.StartAngle = angle + StartAngleDiffList[i];
                script.EndAngle = angle;
                script.AngleChangeInterval = AngleChangeInterval;
                script.Speed = Speed;
                script.FlowerBulletPool = FlowerBullerPool;
                laser.SetActive(true);
            }
			AudioManager.Instance.playSfx(AudioManager.SFX.LaserShot00);
            yield return new WaitForSeconds(ShotInterval);
        }
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        LaserPool.destroy();
        FlowerBullerPool.destroy();
        yield return null;
    }
}
