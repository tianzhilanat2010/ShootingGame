using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public static GameController Instance { get { return smInstance; } }
	private static GameController smInstance = null;

    public GameObject[] SpellCardList;
	public GameObject Enemy;
	public GameObject Player;
	public GameObject SceneTexture;

    private float mLastTime;
    private float mSpellStartTime;
    private float mSpellTotalTime;

    private SpellNameAnimation mSpellNameAnimation;
	private GuiController mGuiController;

	public GameController()
	{
		smInstance = this;
	}

    void Awake()
    {
		Application.targetFrameRate = 60;
		mGuiController = GuiController.Instance;
        mLastTime = Time.time;
        mSpellNameAnimation = GameObject.Find("GuiTextSpellName").GetComponent<SpellNameAnimation>();

    }

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mGuiController.ActiveMenu == mGuiController.MenuHUD) 
		{
			float newTime = Time.time;
			int fps = (int)(1.0f / (newTime - mLastTime));
			mLastTime = newTime;
			mGuiController.FPS.text = ((int)Mathf.Clamp(fps,0,60)).ToString();
			
			float time = mSpellTotalTime - (Time.time - mSpellStartTime);
			string stringTime = ((int)Mathf.Clamp(time, 0, mSpellTotalTime)).ToString();
			if (mGuiController.RemainTime.text != stringTime)
			{
				mGuiController.RemainTime.text = stringTime;
				if((int)time < 3 && (int) time >= 0)
				{
					AudioManager.Instance.playSfx(AudioManager.SFX.TimeOut1);
				}
				else if ((int)time < 10 && (int) time >= 3)
				{
					AudioManager.Instance.playSfx(AudioManager.SFX.TimeOut0);
				}
			}

		}
	}

	private void ActivateMainScene()
	{
		mGuiController.ActiveMenu = mGuiController.MenuHUD;
		Enemy.SetActive (true);
		Player.SetActive (true);
		AudioManager.Instance.playBgm (AudioManager.BGM.BossBattle01);
	}

	private void InactivateMainScene()
	{
		Enemy.SetActive (false);
		Player.SetActive (false);
		AudioManager.Instance.stopBgm();
	}

	public void startGame()
	{
		ActivateMainScene ();
		StartCoroutine("FullGameProcess");
	}



	public void startSpell(int index)
	{
		ActivateMainScene ();
		mSpellCardIndex = index;
		StartCoroutine("SpellCardProcess");
	}

	private int mSpellCardIndex;
    IEnumerator SpellCardProcess()
	{
		GameObject instance = (GameObject)GameObject.Instantiate(SpellCardList[mSpellCardIndex]);
		SpellCardBase spell = instance.GetComponent<SpellCardBase>();
		instance.SetActive(true);
		mGuiController.SpellCardName.enabled = true;
		mGuiController.SpellCardName.text = spell.spellName;
		yield return StartCoroutine(mSpellNameAnimation.startSpellNameAnimation());
		
		yield return StartCoroutine(spell.preStart());
		mSpellStartTime = Time.time;
		mSpellTotalTime = spell.spellTime;
		StartCoroutine(spell.startSpell());
		yield return new WaitForSeconds(spell.spellTime);
		yield return StartCoroutine(spell.stopSpell());
		GameObject.Destroy(instance);
		mGuiController.SpellCardName.enabled = false;
		yield return new WaitForSeconds(0.5f);

		InactivateMainScene ();
		mGuiController.ActiveMenu = mGuiController.MenuSpellSelection;
	}

	IEnumerator FullGameProcess()
    {
        foreach (GameObject obj in SpellCardList)
        {
            GameObject instance = (GameObject)GameObject.Instantiate(obj);
            SpellCardBase spell = instance.GetComponent<SpellCardBase>();
            instance.SetActive(true);
            mGuiController.SpellCardName.enabled = true;
			mGuiController.SpellCardName.text = spell.spellName;
            yield return StartCoroutine(mSpellNameAnimation.startSpellNameAnimation());

            yield return StartCoroutine(spell.preStart());
            mSpellStartTime = Time.time;
            mSpellTotalTime = spell.spellTime;
            StartCoroutine(spell.startSpell());
            yield return new WaitForSeconds(spell.spellTime);
            yield return StartCoroutine(spell.stopSpell());
            GameObject.Destroy(instance);
			mGuiController.SpellCardName.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
		mGuiController.Result.text = "躺枪次数：" + mGuiController.DeathTime.text;

		InactivateMainScene ();
		mGuiController.ActiveMenu = mGuiController.MenuMain;

    }
}
