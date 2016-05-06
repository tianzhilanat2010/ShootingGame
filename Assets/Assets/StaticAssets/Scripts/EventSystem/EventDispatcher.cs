using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventDispatcher : Singleton<EventDispatcher>
{
    Dictionary<int, GameEvent> mEvents = new Dictionary<int, GameEvent>();
    public GameEvent GetEvent(int id)
    {
        if (!mEvents.ContainsKey(id))
        {
            mEvents[id] = new GameEvent();
        }
        return mEvents[id];
    }

    public void Dispatch(int id)
    {
        GameEvent evt = GetEvent(id);
        evt.Dispatch();
    }

    public void Register(int id, GameEvent.EventListernerType onCallBack)
    {
        GameEvent evt = GetEvent(id);
        evt.RegisterEvent(onCallBack);
    }

    public void UnRegister(int id, GameEvent.EventListernerType onCallBack)
    {
        GameEvent evt = GetEvent(id);
        evt.UnRegisterEvent(onCallBack);
    }
}
