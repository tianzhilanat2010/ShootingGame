using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary2D
{
    public float Top;
    public float Bottom;
    public float Left;
    public float Right;
}

public class Bullet_RemoveByRect : MonoBehaviour 
{
    public Boundary2D boundary;
    private CommonBulletController controller;
	// Use this for initialization
	void Awake () 
    {
        controller = GetComponent<CommonBulletController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (rigidbody2D.position.x >= boundary.Left && rigidbody2D.position.x <= boundary.Right
            && rigidbody2D.position.y >= boundary.Bottom && rigidbody2D.position.y <= boundary.Top)
        {

        }
        else
        {
            controller.currentState = CommonBulletController.stateDisappear;
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector2((boundary.Left + boundary.Right)/2, (boundary.Bottom + boundary.Top )/ 2),
            new Vector2(boundary.Right - boundary.Left, boundary.Top - boundary.Bottom));
    }
}
