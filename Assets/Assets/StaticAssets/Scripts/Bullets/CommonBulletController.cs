using UnityEngine;
using System.Collections;

public partial class CommonBulletController : MonoBehaviour 
{
	public Sprite SpriteFog;
	public Material MaterialFog;
	public Sprite SpriteBullet;
	public Material MaterialBullet;
	public Sprite SpriteDisappear;
	public Material MaterialDisappear;
    public bool UseDefaultColliderController = true;

	public float FogDuration;

	private SpriteRenderer mSpriteRenderer;
    private Animator mAnimator;
	private IState xCurrentState;
	public IState currentState
	{
		set
		{
			if (null != xCurrentState)
			{
				xCurrentState.onExit(this);
			}
			xCurrentState = value;
			xCurrentState.onEnter(this);
		}
		get {return xCurrentState;}
	}

	static IState xStateFog = new StateFog();
	static IState xStateNormal = new StateNormal();
	static IState xStateDisappear = new StateDisappear();

	public static IState stateFog {get{return xStateFog;}}
	public static IState stateNormal {get{return xStateNormal;}}
	public static IState stateDisappear {get{return xStateDisappear;}}


	void Awake()
	{
		mSpriteRenderer = GetComponent<SpriteRenderer> ();
        mAnimator = GetComponent<Animator>();
        if (mAnimator)
        {
            mAnimator.enabled = false;
        }
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position += mVelocity;
		currentState.onUpdate (this);
	}

	void OnEnable()
	{
		reset ();
		//rigidbody2D.velocity = transform.TransformDirection(Vector3.up).normalized * 5.0f;

	}

	private void reset()
	{
		currentState = stateFog;
	}
}

public partial class CommonBulletController : MonoBehaviour 
{
	public interface IState
	{
		void onEnter(CommonBulletController controller);
		void onUpdate(CommonBulletController controller);
		void onExit(CommonBulletController controller);
	}
}

public partial class CommonBulletController : MonoBehaviour 
{
	class StateFog: IState
	{
		public void onEnter(CommonBulletController controller)
		{
			controller.mSpriteRenderer.sprite = controller.SpriteFog;
			controller.mSpriteRenderer.material = controller.MaterialFog;
			controller.collider2D.enabled = false;
			controller.StartCoroutine (goToNormalState(controller));
		}
		public void onUpdate(CommonBulletController controller)
		{

		}
		public void onExit(CommonBulletController controller)
		{
			
		}

		private IEnumerator goToNormalState(CommonBulletController controller)
		{
			yield return new WaitForSeconds (controller.FogDuration);
			controller.currentState = stateNormal;
		}
	}
}

public partial class CommonBulletController : MonoBehaviour 
{
	class StateNormal: IState
	{
		public void onEnter(CommonBulletController controller)
		{
			controller.mSpriteRenderer.sprite = controller.SpriteBullet;
			controller.mSpriteRenderer.material = controller.MaterialBullet;
            if (controller.UseDefaultColliderController)
            {
                controller.collider2D.enabled = true;
            }

            if (controller.mAnimator)
            {
                controller.mAnimator.enabled = true;
            }
		}
		public void onUpdate(CommonBulletController controller)
		{
			
		}
		public void onExit(CommonBulletController controller)
		{
            if (controller.mAnimator)
            {
                controller.mAnimator.enabled = false;
            }
		}

	}
}

public partial class CommonBulletController : MonoBehaviour 
{
	class StateDisappear: IState
	{
		public void onEnter(CommonBulletController controller)
		{
			controller.mSpriteRenderer.sprite = controller.SpriteDisappear;
			controller.mSpriteRenderer.material = controller.MaterialDisappear;
			controller.rigidbody2D.velocity = Vector2.zero;
            controller.collider2D.enabled = false;
            controller.StartCoroutine(destroyBullet(controller));
		}
		public void onUpdate(CommonBulletController controller)
		{
			
		}
		public void onExit(CommonBulletController controller)
		{
			
		}

		private IEnumerator destroyBullet(CommonBulletController controller)
		{
			yield return new WaitForSeconds (0.1f);
			controller.gameObject.SetActive (false);
		}
	}
}
