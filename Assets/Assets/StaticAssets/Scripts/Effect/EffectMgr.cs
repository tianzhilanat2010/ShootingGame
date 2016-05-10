using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EffectMgr : Singleton<EffectMgr> 
{
    Dictionary<string, Dictionary<string, List<NormalEffect>>> mLoadedEffect = new Dictionary<string, Dictionary<string, List<NormalEffect>>>();
    public Transform EffectRoot
    {
        get { return mEffectRoot; }
    }
    Transform mEffectRoot = null;
    const string path = "Effect/";

    public T GetEffect<T>(string name) where T : NormalEffect
    {
        string typeName = typeof(T).ToString();
        //Debug.Log(typeName);
        if (!mLoadedEffect.ContainsKey(typeName))
        {
            mLoadedEffect[typeName] = new Dictionary<string, List<NormalEffect>>();
        }
        Dictionary<string, List<NormalEffect>> typeEffect = mLoadedEffect[typeName];
        if (typeEffect.ContainsKey(name))
        {
            List<NormalEffect> effectList = typeEffect[name];
            for (int i = 0; i < effectList.Count; i++)
            {
                if (effectList[i].IsEnd)
                {
                    effectList[i].Play();
                    return effectList[i] as T;
                }
            }
        }
        else
        {
            typeEffect[name] = new List<NormalEffect>();
        }

        GameObject ob = new GameObject();
        ob.name = name;
        T effect = ob.AddComponent<T>();
        effect.SetEffect(path + name);
        if (mEffectRoot == null)
        {
            GameObject rootOb = new GameObject();
            rootOb.name = "EffectMgr";
            mEffectRoot = rootOb.transform;
            GameObject.DontDestroyOnLoad(rootOb);
            mEffectRoot.localPosition = Vector3.zero;
            mEffectRoot.localRotation = Quaternion.identity;
            mEffectRoot.localScale = Vector3.one;
        }
        effect.transform.parent = mEffectRoot;
        typeEffect[name].Add(effect);
        return effect as T;
    }

    public void RemoveEffect(NormalEffect effect)
    {
        string typeName = effect.GetType().ToString();
        if (!mLoadedEffect.ContainsKey(typeName))
        {
            return;
        }
        Dictionary<string, List<NormalEffect>> typeEffect = mLoadedEffect[typeName];
        if (!typeEffect.ContainsKey(effect.name))
        {
            return;
        }
        typeEffect[effect.name].Remove(effect);
    }
}
