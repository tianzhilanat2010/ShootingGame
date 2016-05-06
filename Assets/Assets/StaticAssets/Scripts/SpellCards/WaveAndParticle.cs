using UnityEngine;
using System.Collections;

public class WaveAndParticle : SpellCardBase {

    public ObjectPool bulletPool;
    public float changeSpeed;
    public float maxChangeSpeed;
    public float bulletSpeed;
    public float ways;

    float mBaseRotation;
    float mBaseChange;
    float mRotationStep;
    GameObject mEnemy;
    private bool mIsStopped;

    void Awake()
    {
        mEnemy = GameObject.Find("Enemy");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override IEnumerator startSpell()
    {
        mBaseRotation = 0;
        mBaseChange = 0;
        bulletPool.create();
        mIsStopped = false;
        while (!mIsStopped)
        {
            mBaseChange += changeSpeed;
            mBaseRotation += maxChangeSpeed * Mathf.Abs(Mathf.Sin(mBaseChange));
            float additionalAngle = Mathf.Deg2Rad * 360.0f / ways;
            for (int i = 0; i < ways; i++)
            {
                GameObject bullet = bulletPool.createObject();
                bullet.transform.position = mEnemy.transform.position;
                bullet.SetActive(true); 
                bullet.rigidbody2D.velocity = new Vector2(
                    Mathf.Sin(mBaseRotation + i * additionalAngle),
                    Mathf.Cos(mBaseRotation + i * additionalAngle)) * bulletSpeed;
            }
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            mRotationStep++;
            yield return new WaitForFixedUpdate();
        }
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
        yield return null;
    }
}
