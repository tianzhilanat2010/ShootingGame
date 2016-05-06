using UnityEngine;
using System.Collections;

public class Bullet_SpellEarthQuakeSpecific : MonoBehaviour 
{
    private Vector2 mNormalVelocity;
    private float mSpeedInShake;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setup(EarthQuake spell)
    {
        mSpeedInShake = spell.speedQuake.randomValue;
        spell.onShakeStart += this.onShakeStart;
        //spell.onShakeEnd += this.onShakeEnd;
    }

    public void onShakeStart(EarthQuake spell)
    {
        mNormalVelocity = rigidbody2D.velocity;
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * mSpeedInShake;
        spell.onShakeStart -= this.onShakeStart;
        
    }

}
