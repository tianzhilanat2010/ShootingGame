using UnityEngine;
using System.Collections;

public class MenuMain : MonoBehaviour {

	public GUIStyle style = new GUIStyle ();

	public string[] ButtonList;

	public float TextFactor;
	public float ButtonFactorX;
	public float ButtonFactorY;
	public float ButtonStartPosFactor;

	private Vector2 mScreenResolution;
	private Vector2 mButtonSize;
	private float mButtonStartPosY;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		mScreenResolution = new Vector2 (Screen.width, Screen.height);
		mButtonSize = new Vector2 (mScreenResolution.x * ButtonFactorX, mScreenResolution.y * ButtonFactorY);
		mButtonStartPosY = mScreenResolution.y * ButtonStartPosFactor;
		style.fontSize = (int)(mScreenResolution.x * TextFactor);
		for (int i = 0; i < ButtonList.Length; i++) 
		{
			if (GUI.Button (new Rect ((mScreenResolution.x - mButtonSize.x) / 2, 
			                          mButtonStartPosY + (i - 0.5f) * mButtonSize.y,
			                          mButtonSize.x, 
			                          mButtonSize.y)
			                , ButtonList[i]
			                , style)) {

				if(0 == i)
				{
					GameController.Instance.startGame();
				}
				else if (1 == i)
				{
					GuiController.Instance.ActiveMenu = GuiController.Instance.MenuSpellSelection;
				}
			}
		}
	}
}
