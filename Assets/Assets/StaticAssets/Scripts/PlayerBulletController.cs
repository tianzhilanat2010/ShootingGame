using UnityEngine;
using System.Collections;

public class PlayerBulletController : MonoBehaviour 
{
	public ObjectPool BulletPool;
	public Vector2[] PositionList;
	// Use this for initialization
	void Start () 
	{
		BulletPool.create ();
	}

	void FixedUpdate()
	{
		foreach (Vector2 pos in PositionList) 
		{
			GameObject bullet = BulletPool.createObject();
			bullet.transform.position = pos + rigidbody2D.position;
			bullet.SetActive(true);
		}

	}


}
