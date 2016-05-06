using UnityEngine;
using System.Collections;

public class PoolableObject : MonoBehaviour
{
	public int Index{ get; set;}


	public ObjectPool pool{ get; set;}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnDisable() 
	{
		if (null != pool) 
		{
			pool.destroyObject (gameObject);
		}
	}
}
