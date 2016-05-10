using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceMgr : MonoBehaviorSingleton<ResourceMgr>
{
    public delegate void CreateResourceFinishCallBackType(string name, GameObject ob);
    public delegate void LoadAudioFinishCallBackType(string name, AudioClip ac);
    public delegate void LoadTextureFinishCallBackType(string name, Texture tx);
    public delegate void LoadDataFinishCallBackType(string name, Object tx);

    Dictionary<string, Object> mLoadedRes = new Dictionary<string, Object>();
    List<string> mInLoadingRes = new List<string>();
    Dictionary<string, List<CreateResourceFinishCallBackType>> mWaitCreateRes = new Dictionary<string, List<CreateResourceFinishCallBackType>>();
    Dictionary<string, List<LoadAudioFinishCallBackType>> mWaitLoadAudio = new Dictionary<string, List<LoadAudioFinishCallBackType>>();
    Dictionary<string, List<LoadTextureFinishCallBackType>> mWaitLoadTexture = new Dictionary<string, List<LoadTextureFinishCallBackType>>();
    Dictionary<string, List<LoadDataFinishCallBackType>> mWaitLoadData = new Dictionary<string, List<LoadDataFinishCallBackType>>();

    bool mIsLoading = false;
    bool mIsLoadedFinish = false;
    public void CreateResource(string resName, CreateResourceFinishCallBackType onCreateFinish)
    {
        if(mLoadedRes.ContainsKey(resName))
        {
            GameObject ob = mLoadedRes[resName] as GameObject;
            GameObject ob1 = GameObject.Instantiate(ob) as GameObject;
            onCreateFinish(resName, ob1);
        }
        else
        {
            if(!mWaitCreateRes.ContainsKey(resName))
            {
                mWaitCreateRes[resName] = new List<CreateResourceFinishCallBackType>();
            }
            mWaitCreateRes[resName].Add(onCreateFinish);
            if(!mInLoadingRes.Contains(resName))
            {
                mInLoadingRes.Add(resName);
            }
            if(!mIsLoading)
            {
                mIsLoading = true;
                StartCoroutine(ProcessLoad());
            }
        }
    }

    public void LoadAudio(string audioName, LoadAudioFinishCallBackType OnLoadFinish)
    {
        if (mLoadedRes.ContainsKey(audioName))
        {
            AudioClip clip = mLoadedRes[audioName] as AudioClip;
            OnLoadFinish(audioName, clip);
        }
        else
        {
            if (!mWaitLoadAudio.ContainsKey(audioName))
            {
                mWaitLoadAudio[audioName] = new List<LoadAudioFinishCallBackType>();
            }
            mWaitLoadAudio[audioName].Add(OnLoadFinish);
            if (!mInLoadingRes.Contains(audioName))
            {
                mInLoadingRes.Add(audioName);
            }
            if (!mIsLoading)
            {
                mIsLoading = true;
                StartCoroutine(ProcessLoad());
            }
        }
    }
    
    public void LoadTexture(string texName, LoadTextureFinishCallBackType OnLoadFinish)
    {
        if (mLoadedRes.ContainsKey(texName))
        {
            Texture tex = mLoadedRes[texName] as Texture;
            OnLoadFinish(texName, tex);
        }
        else
        {
            if (!mWaitLoadTexture.ContainsKey(texName))
            {
                mWaitLoadTexture[texName] = new List<LoadTextureFinishCallBackType>();
            }
            mWaitLoadTexture[texName].Add(OnLoadFinish);
            if (!mInLoadingRes.Contains(texName))
            {
                mInLoadingRes.Add(texName);
            }
            if (!mIsLoading)
            {
                mIsLoading = true;
                StartCoroutine(ProcessLoad());
            }
        }
    }

    public void LoadData(string dataName, LoadDataFinishCallBackType OnLoadFinish)
    {
        if (mLoadedRes.ContainsKey(dataName))
        {
            OnLoadFinish(dataName, mLoadedRes[dataName]);
        }
        else
        {
            if (!mWaitLoadData.ContainsKey(dataName))
            {
                mWaitLoadData[dataName] = new List<LoadDataFinishCallBackType>();
            }
            mWaitLoadData[dataName].Add(OnLoadFinish);
            if (!mInLoadingRes.Contains(dataName))
            {
                mInLoadingRes.Add(dataName);
            }
            if (!mIsLoading)
            {
                mIsLoading = true;
                StartCoroutine(ProcessLoad());
            }
        }
    }

    IEnumerator ProcessLoad()
    {
        while(mInLoadingRes.Count > 0)
        {
            mIsLoadedFinish = false;
            string name = mInLoadingRes[0];
            mInLoadingRes.RemoveAt(0);
            Object ob = Resources.Load(name);
            OnResLoadedFinish(name, ob);
            while (!mIsLoadedFinish)
                yield return null;
        }
        mIsLoading = false;
    }

    void OnResLoadedFinish(string resName, Object ob)
    {
        mIsLoadedFinish = true;
        if (ob == null)
        {
            Debug.LogError(resName + " Load Failed ");
            mWaitCreateRes.Remove(resName);
            return;
        }
        mLoadedRes[resName] = ob;
        if(mWaitCreateRes.ContainsKey(resName))
        {
            GameObject prefab = ob as GameObject;
            List<CreateResourceFinishCallBackType> createList = mWaitCreateRes[resName];
            for(int i = 0; i < createList.Count; i++)
            {
                GameObject createOb = GameObject.Instantiate(prefab) as GameObject;
                createList[i](resName, createOb);
            }
            mWaitCreateRes.Remove(resName);
        }
        else if(mWaitLoadAudio.ContainsKey(resName))
        {
            AudioClip clip = ob as AudioClip;
            List<LoadAudioFinishCallBackType> callBackList = mWaitLoadAudio[resName];
            for(int i = 0; i < callBackList.Count; i++)
            {
                callBackList[i](resName, clip);
            }
            mWaitLoadAudio.Remove(resName);
        }
        else if (mWaitLoadTexture.ContainsKey(resName))
        {
            Texture tex = ob as Texture;
            List<LoadTextureFinishCallBackType> callBackList = mWaitLoadTexture[resName];
            for (int i = 0; i < callBackList.Count; i++)
            {
                callBackList[i](resName, tex);
            }
            mWaitLoadTexture.Remove(resName);
        }
        else if (mWaitLoadData.ContainsKey(resName))
        {
            Debug.LogError("-----------OnResLoadedFinish-------------" + resName);
            List<LoadDataFinishCallBackType> callBackList = mWaitLoadData[resName];
            for (int i = 0; i < callBackList.Count; i++)
            {
                callBackList[i](resName, ob);
            }
            mWaitLoadData.Remove(resName);
        }
    }
}
