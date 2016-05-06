using UnityEngine;
using System.Collections;


using System.Collections.Generic;

public class HenyoriLaser : MonoBehaviour {

    public int PointSize;
    public float Width;
    public float CollisionWidth;
    public GameObject mHeader;

    private Vector2 mLastPosition;
    private List<Vector2> mPositionList;
    private Vector3[] mVertexList;
    private Mesh mMesh;
    private MeshRenderer mMeshRenderer;

    private GameObject mPlayer;

    void Awake()
    {
        mPositionList = new List<Vector2>();
        for (int i = 0; i < PointSize; i++)
        {
            mPositionList.Add(Vector2.zero);
        }

        mVertexList = new Vector3[PointSize * 2];
        for (int i = 0; i < mVertexList.Length; i++)
        {
            mVertexList[i] = Vector3.zero;
        }

        Vector2[] uvList = new Vector2[PointSize * 2];
        for (int i = 0; i < PointSize; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                uvList[i * 2 + j] = new Vector2(j, 1.0f - ((float)i) / ((float)PointSize - 1));
                // Debug.Log("" + i + "," + uvList[i].x + "," + uvList[i].y);
            }
        }

        int[] indexList = new int[(PointSize - 1) * 6];
        for (int i = 0; i < (PointSize- 1) * 2; i++ )
        {
            indexList[i * 3 + 0] = i + 0;
            indexList[i * 3 + 1] = i + 1;
            indexList[i * 3 + 2] = i + 2;
        }

        mMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mMesh;
        mMesh.vertices = mVertexList;
        mMesh.uv = uvList;
        mMesh.triangles = indexList;



        mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        mMeshRenderer.sortingLayerName = "Laser";

        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {

    }

    void OnEnable()
    {
        for (int i = 0; i < mVertexList.Length ; i++)
        {
            mVertexList[i] = mHeader.transform.position;
        }

    }

    void FixedUpdate()
    {
        Vector2 vec = mHeader.rigidbody2D.position - mLastPosition;

        Vector3 normalVector = new Vector3(vec.y, -vec.x, 0).normalized *(Width / 2);
        for (int i = mVertexList.Length - 1; i >= 2 ; i--)
        {
            mVertexList[i] = mVertexList[i - 2];
        }
        mVertexList[0] = mHeader.transform.position + normalVector;
        mVertexList[1] = mHeader.transform.position - normalVector;
        mMesh.vertices = mVertexList;


        mLastPosition = mHeader.rigidbody2D.position;
        mPositionList.Insert(0, mLastPosition);
        mPositionList.RemoveAt(PointSize);
        checkCollision();
    }

    void OnDrawGizmos()
    {
//         Gizmos.color = Color.red;
//         foreach (Vector2 position in mPositionList)
//         {
//             Gizmos.DrawWireSphere(position, CollisionWidth/2);
//         }
    }

    void checkCollision()
    {
        foreach (Vector2 position in mPositionList)
        {
            if ((mPlayer.rigidbody2D.position - position).sqrMagnitude < CollisionWidth/2 * CollisionWidth/2)
            {
                PlayerController script = mPlayer.GetComponent<PlayerController>();
                script.addDamage();
            }
        }
    }
}
