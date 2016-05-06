using UnityEngine;
using System.Collections;

public class PlayerBullet_Move00 : MonoBehaviour 
{
	public float Speed = 0;

	private Vector3 mVelocity;
	// Use this for initialization
	void Start () 
	{
		mVelocity = new Vector3 (0, Speed, 0);
	}

	void OnEnable()
	{
		AudioManager.Instance.playSfx(AudioManager.SFX.PlayerBullet00);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.Translate (mVelocity);
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("RemoveZone")) 
		{	
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Enemy")) 
		{						
			//AudioManager.Instance.playSfx(AudioManager.SFX.PlayerBulletHit00);
			gameObject.SetActive(false);
		} 
	}
}
