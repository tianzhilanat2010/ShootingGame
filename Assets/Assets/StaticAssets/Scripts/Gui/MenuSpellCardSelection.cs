using UnityEngine;
using System.Collections;

public class MenuSpellCardSelection : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle SystemButtonStyle0;
	public GUIStyle SystemButtonStyle1;
	
	public float TextFactor;
	public float ButtonFactorX;
	public float ButtonFactorY;
	public float ButtonStartPosFactor;
	public int	ButtonNumberPerPage;
	private int mCurrentPage;
	
	private Vector2 mScreenResolution;
	private Vector2 mButtonSize;
	private float mButtonStartPosY;
	
	// Use this for initialization
	void Start () 
	{
		mCurrentPage = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnEnable()
	{
	}
	
	void OnGUI()
	{
		mScreenResolution = new Vector2 (Screen.width, Screen.height);
		mButtonSize = new Vector2 (mScreenResolution.x * ButtonFactorX, mScreenResolution.y * ButtonFactorY);
		mButtonStartPosY = mScreenResolution.y * ButtonStartPosFactor;
		style.fontSize = (int)(mScreenResolution.x * TextFactor);
		SystemButtonStyle0.fontSize = (int)(mScreenResolution.x * TextFactor);
		SystemButtonStyle1.fontSize = (int)(mScreenResolution.x * TextFactor);

		if (GUI.Button (new Rect ((mScreenResolution.x - mButtonSize.x) / 2, 
		                          mButtonStartPosY + (0 - 1.5f) * mButtonSize.y,
		                          mButtonSize.x, 
		                          mButtonSize.y)
		                , "返回"
		                , SystemButtonStyle0)) 
		{
			GuiController.Instance.ActiveMenu = GuiController.Instance.MenuMain;
		}

		for (int i = 0; i < ButtonNumberPerPage; i++) 
		{
			int index = i + mCurrentPage * ButtonNumberPerPage;
			if(index < GameController.Instance.SpellCardList.Length)
			{
				SpellCardBase script = GameController.Instance.SpellCardList[index].GetComponent<SpellCardBase>();
				string No = (index + 1).ToString().PadLeft(3,'0');
				if (GUI.Button (new Rect ((mScreenResolution.x - mButtonSize.x) / 2, 
				                          mButtonStartPosY + (i - 0.5f) * mButtonSize.y,
				                          mButtonSize.x, 
				                          mButtonSize.y)
				                , "No." + No + " " + script.spellName
				                , style)) 
				{
					GameController.Instance.startSpell(index);
				}
			}
		}

		if (mCurrentPage > 0) 
		{
			if (GUI.Button (new Rect ((mScreenResolution.x - mButtonSize.x * 1.0f) / 2, 
			                          mButtonStartPosY + (ButtonNumberPerPage - 0.5f) * mButtonSize.y,
			                          mButtonSize.x/2, 
			                          mButtonSize.y)
			                , "上一页"
			                , SystemButtonStyle0
			                )) 
			{
				mCurrentPage--;
			}
		}

		if (mCurrentPage < (GameController.Instance.SpellCardList.Length - 1)/ButtonNumberPerPage) 
		{
			if (GUI.Button (new Rect ((mScreenResolution.x) / 2, 
			                          mButtonStartPosY + (ButtonNumberPerPage - 0.5f) * mButtonSize.y,
			                          mButtonSize.x/2, 
			                          mButtonSize.y)
			                , "下一页"
			                , SystemButtonStyle1
			                )) 
			{
				mCurrentPage++;
			}
		}

	}
}
