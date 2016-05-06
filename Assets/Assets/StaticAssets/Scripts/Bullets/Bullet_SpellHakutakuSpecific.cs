using UnityEngine;
using System.Collections;

public class Bullet_SpellHakutakuSpecific : MonoBehaviour {

    public float AppearInterval;
    public float WaitInterval;
    public float MoveSpeed;
    public float TransformInterval;
    public Vector2 TransformScale;

    private SpriteRenderer mRenderer;
    private Color mColor;
    private GameObject mPlayer;
    private Vector2 mOriginalScale;

    void Awake()
    {
        mPlayer = (GameObject)GameObject.FindGameObjectWithTag("Player");
        mRenderer = gameObject.GetComponent<SpriteRenderer>();
        mColor = mRenderer.color;
        mOriginalScale = rigidbody2D.transform.localScale;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        mColor.a = 0;
        mRenderer.color = mColor;
        rigidbody2D.transform.localScale = mOriginalScale;
        StartCoroutine(hakutakuSpecific());
    }

    IEnumerator hakutakuSpecific()
    {
        float increaseAmount = 1.0f / (AppearInterval * 60.0f);
        while (mColor.a <= 1.0f)
        {
            mColor.a += increaseAmount;
            mRenderer.color = mColor;
            yield return new WaitForFixedUpdate();
        }
        mColor.a = 1.0f;
        mRenderer.color = mColor;
        collider2D.enabled = true;
        yield return new WaitForSeconds(WaitInterval);

		AudioManager.Instance.playSfx (AudioManager.SFX.LaserShot00);
        Vector2 velocity = mPlayer.rigidbody2D.position - rigidbody2D.position;
        velocity = velocity.normalized * MoveSpeed;
        rigidbody2D.velocity = velocity;

        
        Vector3 scaleAmount = (TransformScale - mOriginalScale) / (TransformInterval * 60.0f);
        for (int i = 0; i < TransformInterval * 60; i++ )
        {
            rigidbody2D.transform.localScale = rigidbody2D.transform.localScale + scaleAmount;
            yield return new WaitForFixedUpdate();
        }
        rigidbody2D.transform.localScale = TransformScale;
    }
}
