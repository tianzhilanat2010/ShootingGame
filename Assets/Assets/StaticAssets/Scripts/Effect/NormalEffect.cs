using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalEffect : MonoBehaviour 
{
    List<ParticleSystem> mParticles = new List<ParticleSystem>();
    float _LifeTime = 0;
    float mCurTime = 0;
    bool _isForceEnd = false;

    public bool IsEnd
    {

        get { return _LifeTime < mCurTime || _isForceEnd; }
    }

    public float LifeTime
    {
        set  
        {
            _LifeTime = value;
        }

        get 
        {
            return _LifeTime;
        }
    }

    public void Play()
    {
        gameObject.SetActive(true);
        _isForceEnd = false;
        mCurTime = 0f;
        for (int i = 0; i < mParticles.Count; i++)
        {
            mParticles[i].Play(false);
        }

      //  gameObject.active
        OnPlay();
    }

    protected virtual void OnPlay()
    {

    }

    public void Reset()
    {
        OnReset();
    }

    protected virtual void OnReset()
    {

    }

    public void EndPlay()
    {
        _isForceEnd = true;

        OnEndPlay();
    }

    protected virtual void OnEndPlay()
    {

    }
    public void SetEffect(string name)
    {
        ResourceMgr.Instance.CreateResource(name, OnEffectLoadFinish);
    }
    void OnEffectLoadFinish(string name, GameObject ob)
    {
        if (ob == null)
            return;

        ob.transform.parent = this.transform;

        FillParticle(ob.transform);

        OnEffectLoadFinishCallBack(name,ob);
    }

    protected virtual void OnEffectLoadFinishCallBack(string name, GameObject ob)
    {

    }

    void FillParticle(Transform parent)
    {
        //临时代码：特效格式规范
        ParticleSystem fatherP = parent.GetComponent<ParticleSystem>();
        if (fatherP != null)
        {
            _LifeTime = fatherP.duration;
            mParticles.Add(fatherP);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform trans = parent.GetChild(i);
            ParticleSystem p = trans.GetComponent<ParticleSystem>();
            if (p != null)
            {
                if (p.loop)
                {
                    _LifeTime = 10000f;
                }
                else
                {
                    if (p.startDelay + p.duration > _LifeTime)
                    {
                        _LifeTime = p.startDelay + p.duration;
                    }
                }
                mParticles.Add(p);
            }
        }
    }
    void OnDestroy()
    {
        EffectMgr.Instance.RemoveEffect(this);
    }
	// Update is called once per frame
	void Update () 
    {
        mCurTime += Time.deltaTime;
        if (IsEnd)
        {
            transform.parent = EffectMgr.Instance.EffectRoot;
            gameObject.SetActive(false);
            Reset();
            return;
        }

        OnUpdate();
	}

    protected virtual void OnUpdate()
    {

    }
}
