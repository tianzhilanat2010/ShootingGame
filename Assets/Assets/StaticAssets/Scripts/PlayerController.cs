using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private Camera mMainCamera;
    private Vector2 mScreenToWorldTanslate;
    private Vector2 mScreenToWorldScale;
	private Vector2 mLastPosition;
    private Rect mMovableField;

    public int Damage { get { return mDamage; }}
    private int mDamage = 0;

	public Vector2 DeltaPosition
	{
		get { return mDeltaPosition; }
	}

	private Vector2 mDeltaPosition;

    void Awake()
    {
    }

	// Use this for initialization
	void Start ()
	{
		GuiController.Instance.DeathTime.text = "0";
		mScreenToWorldTanslate = Vector2.zero;
		mMainCamera = GameObject.Find("FrameCamera").camera;
        Vector2 v0 = mMainCamera.WorldToScreenPoint(Vector2.zero);
        Vector2 v1 = mMainCamera.WorldToScreenPoint(new Vector2(1.0f, 1.0f));
        mScreenToWorldTanslate = v0;
        mScreenToWorldScale = v1 - v0;
        mScreenToWorldScale.x = 1.0f / mScreenToWorldScale.x;
        mScreenToWorldScale.y = 1.0f / mScreenToWorldScale.y;


		GameObject movableField = GameObject.FindGameObjectWithTag("MovableField");
		Collider2D colliderMovableField = movableField.GetComponent<BoxCollider2D> ();
		Collider2D colliderThis = gameObject.GetComponent<BoxCollider2D> ();

		mMovableField.min = colliderMovableField.bounds.min + colliderThis.bounds.size / 2;
		mMovableField.max = colliderMovableField.bounds.max - colliderThis.bounds.size / 2;
		Destroy (colliderMovableField);
		Destroy (colliderThis);
	}

	// Update is called once per frame
	void Update ()
	{

	}

    public void addDamage()
    {
		AudioManager.Instance.playSfx (AudioManager.SFX.PlayerDeath);
        mDamage++;
        GuiController.Instance.DeathTime.text = mDamage.ToString();
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Enemy")) 
		{
            addDamage();
		} 
		else if (other.gameObject.CompareTag ("Bullet")) 
		{
			CommonBulletController bullet = other.gameObject.GetComponent<CommonBulletController>();
			bullet.currentState = CommonBulletController.stateDisappear;
            addDamage();
		}
	}

	void FixedUpdate()
	{
		UpdateTouch ();
		if (IsTouchDown) 
		{
			mDeltaPosition = Vector2.zero;
			mLastPosition = TouchPosition;
		}
		else if (IsTouchUp)
		{
			mDeltaPosition = Vector2.zero;
			mLastPosition = TouchPosition;
		}
		else if (IsTouched)
		{
			Vector2 touchPosition = TouchPosition;
			mDeltaPosition = touchPosition - mLastPosition;
			mLastPosition = touchPosition;

			Vector2 newPos = rigidbody2D.position + mDeltaPosition;
			newPos.x = Mathf.Clamp (newPos.x, mMovableField.min.x, mMovableField.max.x);
			newPos.y = Mathf.Clamp (newPos.y, mMovableField.min.y, mMovableField.max.y);
			rigidbody2D.MovePosition(newPos);
		}
	}

	bool mIsTouchDown = false;
	bool mIsTouchUp = false;
	bool mIsLastTouched = false;
	private void UpdateTouch()
	{
		bool isTouched = Input.GetMouseButton(0);
		mIsTouchDown = (!mIsLastTouched) && isTouched;
		mIsTouchUp = mIsLastTouched && (!isTouched);
		mIsLastTouched = isTouched;

	}

	private bool IsTouchDown
	{
		get
		{
			return mIsTouchDown;
		}
	}

	private bool IsTouched
	{
		get
		{
			return mIsLastTouched;
		}

	}

	private bool IsTouchUp
	{
		get
		{
			return mIsTouchUp;
		}
	}

	private Vector2 TouchPosition
	{
		get
		{
            Vector2 v = (Vector2)Input.mousePosition - mScreenToWorldTanslate;
            v.x *= mScreenToWorldScale.x;
            v.y *= mScreenToWorldScale.y;
            v += mScreenToWorldTanslate;
            return v;
		}

	}

}
