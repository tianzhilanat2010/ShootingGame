using UnityEngine;
using System.Collections;

public class Bullet_RemoveByTime : MonoBehaviour 
{
    public float waitTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        StartCoroutine(waitForRemove());
    }
     
    IEnumerator waitForRemove()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
