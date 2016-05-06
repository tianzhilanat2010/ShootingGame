using UnityEngine;
using System.Collections;

public class SpellNameAnimation : MonoBehaviour 
{
    public float startPositionY;
    public float endPositionY;
    public float positionX;
    public float appearDuration;
    public float moveDuration;

    private GUIText mGuiText;
    private Color mColor;
    private Vector3 mPosition;

    void Awake()
    {
        mGuiText = gameObject.GetComponent<GUIText>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator startSpellNameAnimation()
    {
        mColor = mGuiText.color;
        mColor.a = 0;
        mGuiText.color = mColor;
		AudioManager.Instance.playSfx (AudioManager.SFX.SpellCardStart);
        mPosition = new Vector3(positionX, startPositionY, 0);
        mGuiText.transform.position = mPosition;
        int loopTime = (int)(appearDuration * 60);
        float increaseAmount = 1.0f / (loopTime);
        for (int i = 0; i < loopTime; i++)
        {
            mColor.a += increaseAmount;
            mGuiText.color = mColor;
            yield return new WaitForFixedUpdate();
        }
        mColor.a = 1.0f;
        mGuiText.color = mColor;

        loopTime = (int)(moveDuration * 60.0f);
        increaseAmount = (endPositionY - startPositionY) / loopTime;
        for (int i = 0; i < loopTime; i++)
        {
            mPosition.y += increaseAmount;
            mGuiText.transform.position = mPosition;
            yield return new WaitForFixedUpdate();
        }
        mPosition.y = endPositionY;
        mGuiText.transform.position = mPosition;
    }
}
