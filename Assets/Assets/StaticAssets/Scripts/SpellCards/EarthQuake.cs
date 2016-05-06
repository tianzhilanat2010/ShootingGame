using UnityEngine;
using System.Collections;

public class EarthQuake : SpellCardBase {
    public ObjectPool bulletPool;
    public int shotWays;
    public RangeFloat shotPointRange;
    public float shotInterval;
    public float speed;
    public RangeFloat speedQuake;
    public float ScreenShakeStartTime;
    public float ScreenShakeInterval;
    public float ScreenShakeTime;
    public float ScreenShakeFactor;

    public delegate void Callback(EarthQuake spell);
    public event Callback onShakeStart;
    public event Callback onShakeEnd;

	private Vector3 mSceneTextureOriginalPosition;
    private bool mIsStopped;

    void Awake()
    {
		mSceneTextureOriginalPosition = GameController.Instance.SceneTexture.transform.localPosition;
    }

    void Start()
    {

    }


    public override IEnumerator startSpell()
    {
        bulletPool.create();
        mIsStopped = false;
        StartCoroutine(shakeScreen());
        while (!mIsStopped)
        {
            float baseAngle = Random.Range(0.0f, 360.0f);
            float additionalAngle = 360.0f / shotWays;
            Vector3 position = GameController.Instance.Enemy.transform.position + new Vector3(shotPointRange.randomValue, shotPointRange.randomValue, 0);
            for(int i = 0; i < shotWays; i++)
            {
                GameObject bullet = bulletPool.createObject();
                bullet.SetActive(true);
                bullet.transform.position = position;
                float angle = baseAngle + additionalAngle * i;
                bullet.rigidbody2D.velocity = new Vector2(
                      Mathf.Sin(angle * Mathf.Deg2Rad) * speed
                    , Mathf.Cos(angle * Mathf.Deg2Rad) * speed);
                Bullet_SpellEarthQuakeSpecific script = bullet.GetComponent<Bullet_SpellEarthQuakeSpecific>();
                script.setup(this);	
				SpriteRenderer renderer = bullet.GetComponent<SpriteRenderer>();
				renderer.sortingOrder = i;
            }
			AudioManager.Instance.playSfx(AudioManager.SFX.BulletShot00);
            yield return new WaitForSeconds(shotInterval);
        }
    }

    public override IEnumerator stopSpell()
    {
        mIsStopped = true;
        bulletPool.destroy();
		GameController.Instance.SceneTexture.transform.localPosition = mSceneTextureOriginalPosition;
        yield return null;
    }

    public IEnumerator shakeScreen()
    {
        yield return new WaitForSeconds(ScreenShakeStartTime);
        while (!mIsStopped)
        {
			AudioManager.Instance.playSfx(AudioManager.SFX.Charge02);
			yield return new WaitForSeconds(1.0f);
            float startTime = Time.time;
            if (null != onShakeStart)
            {
                onShakeStart(this);
            }
            while (startTime + ScreenShakeTime > Time.time)
            {
				float shakeFactor = ScreenShakeFactor *  (Time.time - startTime)/ ScreenShakeTime;
                Vector3 v = new Vector3(
					Random.Range(-shakeFactor, shakeFactor),
					Random.Range(-shakeFactor, shakeFactor),
                    0);
				GameController.Instance.SceneTexture.transform.localPosition = mSceneTextureOriginalPosition + v;
                yield return new WaitForFixedUpdate();
            }
			GameController.Instance.SceneTexture.transform.localPosition = mSceneTextureOriginalPosition;
            if(null != onShakeEnd)
            {
                onShakeEnd(this);
            }
            yield return new WaitForSeconds(ScreenShakeInterval);
        }
		GameController.Instance.SceneTexture.transform.localPosition = mSceneTextureOriginalPosition;
    }

}
