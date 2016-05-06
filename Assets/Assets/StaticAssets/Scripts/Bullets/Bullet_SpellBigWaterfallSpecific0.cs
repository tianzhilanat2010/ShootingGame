using UnityEngine;
using System.Collections;

public class Bullet_SpellBigWaterfallSpecific0 : MonoBehaviour 
{
    private CommonBulletController controller;
    // Use this for initialization
    void Awake()
    {
        controller = gameObject.GetComponent<CommonBulletController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void removeBullet()
    {
        controller.currentState = CommonBulletController.stateDisappear;
    }
}
