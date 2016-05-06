using UnityEngine;
using System.Collections;

public class Bullet_ChangeSize : MonoBehaviour 
{
    public float StartTime;
    public float StartSize;
    public float EndSize;
    public float LerpTime;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnEnable()
    {
        StartCoroutine(changeSize());
    }

    IEnumerator changeSize()
    {
        rigidbody2D.transform.localScale = new Vector3(StartSize, StartSize, 0);
        yield return new WaitForSeconds(StartTime);
        float increaseFactor = (EndSize - StartSize) / (LerpTime * 60.0f);
        Vector3 increaseAmount = new Vector3(increaseFactor, increaseFactor, 0);
        for (int i = 0; i < LerpTime * 60.0f; i++ )
        {
            rigidbody2D.transform.localScale = rigidbody2D.transform.localScale + increaseAmount;
            yield return new WaitForFixedUpdate();
        }
        rigidbody2D.transform.localScale = new Vector3(EndSize, EndSize, 0);
    }
}
