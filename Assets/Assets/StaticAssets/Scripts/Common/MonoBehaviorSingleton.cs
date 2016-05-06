using UnityEngine;
using System.Collections;

public class MonoBehaviorSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T Instance
    {
        get { return _instance; }
    }

    protected void Awake()
    {
        _instance = this as T;
    }

    protected void OnDestroy()
    {
        _instance = null;
    }
}
