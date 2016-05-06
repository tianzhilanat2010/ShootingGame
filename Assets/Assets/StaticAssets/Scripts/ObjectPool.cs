using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPool
{
	public GameObject ProtoType;
	public int Size;

	private GameObject[] mBulletList;
	private int mFirstEmptyObjectIndex;
	public int mMaxObjectUsed;
	private int mObjectUsed;
	
	public void create()
	{
		mBulletList = new GameObject[Size];
		for (int i = 0; i < Size; i++) 
		{
			GameObject obj = (GameObject)GameObject.Instantiate(ProtoType);
			obj.SetActive(false);
			PoolableObject p = obj.GetComponent<PoolableObject>();
			p.Index = i + 1;
			p.pool = this;
			mBulletList[i] = obj;
		}
        mFirstEmptyObjectIndex = 0;
		mMaxObjectUsed = 0;
		mObjectUsed = 0;
	}

    public void destroy()
    {
        for (int i = 0; i < Size; i++)
        {
            GameObject.Destroy(mBulletList[i]);
        }
    }

	public GameObject createObject()
	{
		if (mFirstEmptyObjectIndex >= Size)
			return null;
		GameObject obj = mBulletList [mFirstEmptyObjectIndex];
		PoolableObject p = obj.GetComponent<PoolableObject>();
		int swap = p.Index;
		p.Index = mFirstEmptyObjectIndex;
		mFirstEmptyObjectIndex = swap;
		mObjectUsed++;
		mMaxObjectUsed = Mathf.Max (mMaxObjectUsed, mObjectUsed);
		return obj;
	}

	public void destroyObject(GameObject obj)
	{
		PoolableObject p = obj.GetComponent<PoolableObject>();
		int swap = p.Index;
		p.Index = mFirstEmptyObjectIndex;
		mFirstEmptyObjectIndex = swap;
		obj.SetActive (false);
		mObjectUsed--;
	}



}
