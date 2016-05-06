using UnityEngine;
using System.Collections;

public class Bullet_SpellAntiGravityWaterFallSpecific : MonoBehaviour
{
    public float speedStart;
    public float speedEnd;
    private SpriteRenderer mRenderer;
    private Color mColor;

    void Awake()
    {
        mRenderer = gameObject.GetComponent<SpriteRenderer>();
        mColor = mRenderer.color;
    }

    // Use this for initialization
    void Start () 
    {
    }

    void OnEnable()
    {
        mColor.a = 0.0f;
        mRenderer.color = mColor;
        StartCoroutine("fixedUpdateCoroutine");
    }
    
    // Update is called once per frame
    void Update () 
    {

    }

    IEnumerator fixedUpdateCoroutine()
    {
        float alpha = 0.0f;
        mColor.a = alpha;
        mRenderer.color = mColor;
        yield return new WaitForFixedUpdate();
        while (1.0f != alpha)
        {
            alpha = (rigidbody2D.velocity.y - speedStart) / (speedEnd - speedStart);
            alpha = Mathf.Clamp01(alpha);
            mColor.a = alpha;
            mRenderer.color = mColor;
            yield return new WaitForFixedUpdate();
        }

        collider2D.enabled = true;
    }
}
