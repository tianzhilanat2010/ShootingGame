using UnityEngine;
using System.Collections;


public class AudioManager : MonoBehaviour 
{
	public enum BGM
	{
		BossBattle01,
	};

	public enum SFX
	{
		PlayerDeath,
		BulletShot00,
		LaserShot00,
		PlayerBullet00,
		PlayerBulletHit00,
		BulletShot01,
		SpellCardStart,
		Charge02,
		ScreenShake,
		ReverseScreen,
		TimeOut0,
		TimeOut1
	};

	public AudioClip[] BgmClipList;
	public AudioClip[] SfxClipList;


	public AudioSource BgmSource;

	private AudioSource[] SfxSource;

	public static AudioManager Instance { get{ return smInstance; } }
	private static AudioManager smInstance = null;

	public AudioManager()
	{
		smInstance = this;
	}

	void Awake()
	{
		SfxSource = new AudioSource[SfxClipList.Length];
		for (int i = 0; i < SfxSource.Length; i++)
		{
			SfxSource[i] = ((GameObject)GameObject.Instantiate(BgmSource.gameObject)).audio;
			SfxSource[i].clip = SfxClipList[i];
			SfxSource[i].loop = false;
		}
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void playBgm(BGM bgmId)
	{
		BgmSource.gameObject.SetActive (true);
		BgmSource.Stop ();
		BgmSource.clip = BgmClipList[(int)bgmId];
		BgmSource.Play ();
	}

	public void stopBgm()
	{
		BgmSource.Stop();
	}

	public void playSfx(SFX sfxId)
	{
		int id = (int)sfxId;
		SfxSource [id].volume = 0.5f;
		SfxSource [id].Stop ();
		SfxSource [id].Play();
	}
}
