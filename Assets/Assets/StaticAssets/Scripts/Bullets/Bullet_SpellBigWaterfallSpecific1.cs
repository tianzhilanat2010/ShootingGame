using UnityEngine;
using System.Collections;

public class Bullet_SpellBigWaterfallSpecific1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        Bullet_SpellBigWaterfallSpecific0 bullet = other.gameObject.GetComponent<Bullet_SpellBigWaterfallSpecific0>();
        if (bullet != null)
        {
            bullet.removeBullet();
        }
    }
}
