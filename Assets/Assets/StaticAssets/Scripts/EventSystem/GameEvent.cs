using System.Collections;
using System.Collections.Generic;

public class EventArg
{
    ArrayList mArgList = new ArrayList();
    public T GetArg<T>(int index)
    {
        if (index >= mArgList.Count)
            return default(T);
        return (T)mArgList[index];
    }

    public System.Object GetArg(int index)
    {
        return GetArg<System.Object>(index);
    }

    public void SetArg<T>(int index, T value)
    {
        if (index >= mArgList.Count)
        {
            int count = mArgList.Count;
            for (int i = count; i < index + 1; i++)
            {
                mArgList.Add(null);
            }
        }
        mArgList[index] = value;
    }

    public void SetArg(int index, System.Object value)
    {
        SetArg<System.Object>(index, value);
    }
}


public class GameEvent 
{
    EventArg mArg = new EventArg();
    
    public delegate void EventListernerType(EventArg arg);

    public event EventListernerType mListener;

    public void SetArg<T>(int index, T value)
    {
        mArg.SetArg<T>(index, value);
    }

    public void SetArg(int index, System.Object value)
    {
        mArg.SetArg(index, value);
    }

    public void Dispatch()
    {
        if (mListener !=null)
        {
            mListener(mArg);
        }
    }

    public EventArg getEventArg()
    {
        return mArg;
    }

    public void RegisterEvent(EventListernerType listener)
    {
        mListener += listener;
    }

    public void UnRegisterEvent(EventListernerType listener)
    {
        mListener -= listener;
    }
}
