using UnityEngine;
using System.Collections;

public class ReverseUpDown : SpellCardBase {

    public ObjectPool bulletPool;
    public float screenRotationTime;
    public RangeFloat spawnPositionX;
    public RangeFloat spawnPositionY;
    public RangeFloat spawnVelocityY;
    private bool mIsStopped;
    private int mLoopTimes;
    private float mLoopAngle;

    void Awake()
    {
        mLoopTimes = (int)(screenRotationTime * 60.0f);
        mLoopAngle = 180.0f / mLoopTimes;
    }

    void Start()
    {

    }

    public override IEnumerator startSpell()
    {
		AudioManager.Instance.playSfx(AudioManager.SFX.Charge02);
		yield return new WaitForSeconds (1.0f);
		yield return StartCoroutine(RotateScreen());
        bulletPool.create();
        mIsStopped = false;
        while (!mIsStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
            GameObject bullet = bulletPool.createObject();
            bullet.SetActive(true);
            bullet.transform.position = new Vector3(spawnPositionX.randomValue, spawnPositionY.randomValue, 0.0f);
            bullet.rigidbody2D.velocity = new Vector2(0.0f, -spawnVelocityY.randomValue);
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForFixedUpdate();
            }
			if(mIsStopped) break;
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot01);
			bullet = bulletPool.createObject();
			bullet.SetActive(true);
			bullet.transform.position = new Vector3(spawnPositionX.randomValue, spawnPositionY.randomValue, 0.0f);
			Vector2 velocity = GameController.Instance.Player.rigidbody2D.position - bullet.rigidbody2D.position;
			velocity = velocity.normalized * spawnVelocityY.randomValue;
			bullet.rigidbody2D.velocity = velocity;
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
        yield return StartCoroutine(RotateScreen());
        GameController.Instance.SceneTexture.transform.localRotation = Quaternion.identity;
        
    }

    public IEnumerator RotateScreen()
    {
		AudioManager.Instance.playSfx(AudioManager.SFX.ReverseScreen);
		Vector3 eulerAngle = GameController.Instance.SceneTexture.transform.localRotation.eulerAngles;
        for (int i = 0; i < mLoopTimes; i++)
        {
			eulerAngle.x += mLoopAngle;
			GameController.Instance.SceneTexture.transform.localRotation = Quaternion.Euler(eulerAngle);
            yield return new WaitForFixedUpdate();
        }
    }
}
