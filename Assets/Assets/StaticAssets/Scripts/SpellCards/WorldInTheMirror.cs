using UnityEngine;
using System.Collections;

public class WorldInTheMirror : SpellCardBase 
{
    public ObjectPool bulletPool;
    public float screenRotationTime;
    public float screenRotationInterval;
    public RangeFloat spawnPositionX;
    public RangeFloat spawnPositionY;
    public RangeFloat spawnVelocityY;
    private bool mIsStopped;
    private int mLoopTimes;
    private float mLoopAngle;
    private bool mIsScreenReversed;
    private bool mIsScreenRotating;
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
        bulletPool.create();
        mIsStopped = false;
        gameObject.SetActive(true);
        StartCoroutine("RotateScreen");
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
        }

    }



    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
        while (mIsScreenReversed || mIsScreenRotating)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public IEnumerator RotateScreen()
    {
        mIsScreenReversed = false;
        mIsScreenRotating = true;
        while (!mIsStopped)
        {
            yield return new WaitForSeconds(screenRotationInterval);
			AudioManager.Instance.playSfx(AudioManager.SFX.Charge02);
			yield return new WaitForSeconds (1.0f);
			AudioManager.Instance.playSfx(AudioManager.SFX.ReverseScreen);
            mIsScreenRotating = true;
			Vector3 eulerAngle = GameController.Instance.SceneTexture.transform.localRotation.eulerAngles;
            for (int i = 0; i < mLoopTimes; i++)
            {
				eulerAngle.y += mLoopAngle;
				GameController.Instance.SceneTexture.transform.localRotation = Quaternion.Euler(eulerAngle);
                yield return new WaitForFixedUpdate();
            }
            mIsScreenRotating = false;
            mIsScreenReversed = !mIsScreenReversed;
        }

        if (mIsScreenReversed)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.ReverseScreen);
            mIsScreenRotating = true;
			Vector3 eulerAngle = GameController.Instance.SceneTexture.transform.localRotation.eulerAngles;
            for (int i = 0; i < mLoopTimes; i++)
            {
				eulerAngle.y += mLoopAngle;
				GameController.Instance.SceneTexture.transform.localRotation = Quaternion.Euler(eulerAngle);
                yield return new WaitForFixedUpdate();
            }
            mIsScreenRotating = false;
            mIsScreenReversed = !mIsScreenReversed;
        }
		GameController.Instance.SceneTexture.transform.localRotation = Quaternion.identity;
    }
}
