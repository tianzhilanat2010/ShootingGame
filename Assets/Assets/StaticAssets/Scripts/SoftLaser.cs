using UnityEngine;
using System.Collections;


using System.Collections.Generic;

public class SoftLaser : MonoBehaviour
{

    public int PointSize;
    public float Width;
    public float CollisionWidth;
    public bool StartDirectionIsLeft;

    private Vector2 mLastPosition;
    private Vector2[] mPositionList;
    float mLastAngle;
    float mBaseAngle;
    private Vector3[] mVertexList;
    private Vector2[] mUvList;
    private Mesh mMesh;
    private MeshRenderer mMeshRenderer;

    private Vector2[] mCollisionPolygonList;
    private GameObject mPlayer;
    private float addScale;
    void Awake()
    {
        mPositionList = new Vector2[PointSize];
        mBaseAngle = 180;
        mLastAngle = 0;

        mVertexList = new Vector3[PointSize * 2];
        for (int i = 0; i < mVertexList.Length; i++)
        {
            mVertexList[i] = Vector3.zero;
        }

        mUvList = new Vector2[PointSize * 2];
        for (int i = 0; i < PointSize; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                mUvList[i * 2 + j] = new Vector2(1.0f - ((float)i) / ((float)PointSize - 1), j);
                // Debug.Log("" + i + "," + uvList[i].x + "," + uvList[i].y);
            }
        }

        int[] indexList = new int[(PointSize - 1) * 6];
        
        //for (int i = 0; i < (PointSize - 1) * 2; i++)
        //{
        //    indexList[i * 3 + 0] = i + 0;
        //    indexList[i * 3 + 1] = i + 1;
        //    indexList[i * 3 + 2] = i + 2;
        //}

        
        for (int i = 0; i < (PointSize - 1); ++i)
        {
            indexList[i * 6 + 0] = 2*i + 0;
            indexList[i * 6 + 1] = 2*i + 1;
            indexList[i * 6 + 2] = 2*i + 2;

            indexList[i * 6 + 3] = 2*i + 1;
            indexList[i * 6 + 4] = 2*i + 2;
            indexList[i * 6 + 5] = 2*i + 3;
        }

        mMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mMesh;
        mMesh.vertices = mVertexList;
        mMesh.uv = mUvList;
        mMesh.triangles = indexList;

        mCollisionPolygonList = new Vector2[PointSize * 2 + 1];
        for (int i = 0; i < mCollisionPolygonList.Length; i++)
        {
            mCollisionPolygonList[i] = new Vector2(0, i);
        }


        mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        mMeshRenderer.sortingLayerName = "Laser";

        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {

    }

    void OnEnable()
    {
        if (StartDirectionIsLeft)
        {
            addScale = 0.1f;
        }
        else
        {
            addScale = -0.1f;
        }
    }

    

    void FixedUpdate()
    {
        if (mLastAngle > 7.0f)
        {
            addScale = -0.1f;
        }
        if (mLastAngle < - 7.0f)
        {
            addScale = 0.1f;
        }
        mLastAngle += addScale;

        for (int i = 0; i < PointSize; i++ )
        {
            float radius = (mLastAngle *i / PointSize + mBaseAngle) * Mathf.Deg2Rad;
            mPositionList[i] = new Vector2(Mathf.Sin(radius), Mathf.Cos(radius)) * (0.2f * i);

            if (i >= 1)
            {
                Vector3 start = new Vector3(mPositionList[i].x + rigidbody2D.transform.position.x, mPositionList[i].y + rigidbody2D.transform.position.y, 0f);
                Vector3 end = new Vector3(mPositionList[i - 1].x + rigidbody2D.transform.position.x, mPositionList[i - 1].y + rigidbody2D.transform.position.y, 0f);
                Debug.DrawLine(start, end);
            }
            
        }

        Vector2 vec = mPositionList[1] - mPositionList[0];
        Vector2 normalVector = new Vector3(vec.y, -vec.x).normalized * (Width / 2);
        mVertexList[0] = mPositionList[0] + normalVector;
        mVertexList[1] = mPositionList[0] - normalVector;
        for (int i = 1; i < PointSize; i++)
        {
            vec = mPositionList[i] - mPositionList[i-1];
            normalVector = new Vector3(vec.y, -vec.x, 0).normalized * (Width / 2);
            mVertexList[2 * i + 0] = mPositionList[i] + normalVector;
            mVertexList[2 * i + 1] = mPositionList[i] - normalVector;
        }
        mMesh.vertices = mVertexList;


        for (int i = 0; i < PointSize * 2; i++)
        {
            mUvList[i].x += 0.31415926f / 3;
            mUvList[i].x -= (int)(mUvList[i].x);
        }

        mMesh.uv = mUvList;

        checkCollision();
    }

    void OnDrawGizmos()
    {
//         Gizmos.color = Color.red;
//         for (int i = 1; i < mPositionList.Length; i++)
//         {
//             Vector2 position = mPositionList[i] + rigidbody2D.position;
//             Gizmos.DrawWireSphere(position, CollisionWidth / 2);
//         }
    }

    void checkCollision()
    {
        for (int i = 1; i < mPositionList.Length; i++ )
        {
            Vector2 position = mPositionList[i] + rigidbody2D.position;
            if ((mPlayer.rigidbody2D.position - position).sqrMagnitude < CollisionWidth / 2 * CollisionWidth / 2)
            {
                PlayerController script = mPlayer.GetComponent<PlayerController>();
                script.addDamage();
                return;
            }
        }
    }
}
