using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

	public static GuiController Instance { get{ return smInstance; } }
	private static GuiController smInstance = null;


	public GUIText FPS;
	public GUIText RemainTime;
	public GUIText SpellCardName;
	public GUIText DeathTime;
	public GUIText Result;

	public GameObject MenuMain;
	public GameObject MenuHUD;
	public GameObject MenuSpellSelection;
	public GameObject[] MenuList;



	private GameObject mActiveMenu;
	public GameObject ActiveMenu
	{
		get { return mActiveMenu; }
		set 
		{
			bool found = false;
			if (mActiveMenu == value)
			{
				return;
			}
			foreach(GameObject m in MenuList)
			{
				if(m == value)
				{
					found = true;
					mActiveMenu = value;
					m.SetActive(true);
				}
				else
				{
					m.SetActive(false);
				}
			}
		}
	}

	public GuiController()
	{
		smInstance = this;
	}

	// Use this for initialization
	void Start () 
	{
		ActiveMenu = MenuList[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
