using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TiboNoBansan : SpellCardBase  
{
	public class ControlPoint
	{
		public Vector2 position;
		public Vector2 direction;
		public Vector2 binormal;
		public ControlPoint parent;
		public List<ControlPoint> children;
		public float angle;
		public int id;
		public int level;

		public ControlPoint(Vector2 position):this(position, 0, null, new List<ControlPoint>()){}
		public ControlPoint(Vector2 position, float angle):this(position, angle, null, new List<ControlPoint>()){}
		public ControlPoint(Vector2 position, float angle, ControlPoint parent):this(position, angle, parent, new List<ControlPoint>()){}
		public ControlPoint(Vector2 position, float angle, ControlPoint parent, List<ControlPoint> children)
		{
			this.angle = angle;
			this.direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.1f;
			this.binormal = new Vector2(Mathf.Sin(angle + Mathf.PI/2), Mathf.Cos (angle + Mathf.PI/2));
			this.position = position;
			this.binormal = binormal;
			this.parent = parent;
			this.children = children;
			this.level = (null == parent)? 0 : parent.level + 1;
		}
	}

	public ObjectPool bulletPool;

	private Vector3[] mVertexList;
	private Vector3[] mBinormalList;
	private Vector2[] mExtraList;
	private Vector2[] mUvList;
	private Mesh mMesh;
	private MeshRenderer mMeshRenderer;
	List<ControlPoint> controlPointList;
	ControlPoint controlPointRoot;
	bool isStopped;

    const int MaxLevel = 400;
    const float GapDistance = 0.02f;

	void Awake()
	{


		mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
		mMeshRenderer.sortingLayerName = "Laser";
		mMeshRenderer.materials[0].SetFloat ("_scale", 0.05f);
		mMeshRenderer.materials[0].SetFloat ("_rate", 0);
		mMeshRenderer.materials[0].SetFloat ("_destroy", 2.0f);


		generateSubControlPoint ();


	}

	private void generateSubControlPoint ()
	{
		controlPointList = new List<ControlPoint> ();
		Vector2 rootPosition = Vector2.zero;
		controlPointRoot = new ControlPoint(rootPosition);
		controlPointRoot.id = controlPointList.Count;
		controlPointList.Add(controlPointRoot);
		for (int i = 0; i < 10; i++) 
		{
			float angle = Mathf.PI * 2 / 10 * (i + Random.Range(0.0f, 0.7f));
			Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.01f;
			ControlPoint p = new ControlPoint(rootPosition + direction, angle, controlPointRoot);
			controlPointRoot.children.Add(p);
			p.id = controlPointList.Count;
			controlPointList.Add(p);
			generateSubControlPointRecursive(p);
		}

		mVertexList = new Vector3[controlPointList.Count * 2];
		for (int i = 0; i < controlPointList.Count; i++)
		{
			ControlPoint p = controlPointList[i];
			
			mVertexList[i*2 + 0] = p.position;
			mVertexList[i*2 + 1] = p.position;
		}
		
		mBinormalList = new Vector3[controlPointList.Count * 2];
		for (int i = 0; i < controlPointList.Count; i++)
		{
			ControlPoint p = controlPointList[i];
			mBinormalList[i*2 + 0] = - p.binormal;
			mBinormalList[i*2 + 1] = p.binormal;
		}
		
		mUvList = new Vector2[controlPointList.Count * 2];
		for (int i = 0; i < controlPointList.Count; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				mUvList[i * 2 + j] = new Vector2(0, j);
			}
		}
		
		mExtraList = new Vector2[controlPointList.Count * 2];
		for (int i = 0; i < controlPointList.Count; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				mExtraList[i * 2 + j] = new Vector2((1.0f - controlPointList[i].level/ (float)MaxLevel), j);
			}
		}
		
		int[] indexList = new int[(controlPointList.Count - 1) * 6];
		for (int i = 1; i < controlPointList.Count ; i++)
		{
			ControlPoint p = controlPointList[i];
			int id = p.id;
			int pid = p.parent.id;
			indexList[(i - 1) * 6 + 0] = pid * 2 + 0;
			indexList[(i - 1) * 6 + 1] = pid * 2 + 1;
			indexList[(i - 1) * 6 + 2] = id * 2 + 0;
			indexList[(i - 1) * 6 + 3] = pid * 2 + 1;
			indexList[(i - 1) * 6 + 4] = id * 2 + 1;
			indexList[(i - 1) * 6 + 5] = id * 2 + 0;
		}
		
		mMesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mMesh;
		mMesh.vertices = mVertexList;
		mMesh.uv = mUvList;
		mMesh.triangles = indexList;
		mMesh.normals = mBinormalList;
		mMesh.uv1 = mExtraList;

		System.GC.Collect();
	}

	private void generateSubControlPointRecursive (ControlPoint basePoint)
	{
		int subNum = 0;
		int level = basePoint.level + 1;
        if (level == MaxLevel) subNum = 0;
		else if((level %100) == 0) subNum = Random.Range(1,3);
		else subNum = 1;

		for (int i = 0; i < subNum; i++) 
		{
			float randomRange = 2.0f * Mathf.PI * 2.0f / 360.0f ;
			float angle = basePoint.angle + Random.Range(-randomRange, randomRange) + i * Mathf.PI / 180.0f * 30.0f;
            Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * GapDistance;
			ControlPoint p = new ControlPoint(basePoint.position + direction, angle, basePoint);
			basePoint.children.Add(p);
			p.id = controlPointList.Count;
			controlPointList.Add(p);
			generateSubControlPointRecursive(p);
		}
	}

	public void Draw(int frame)
	{
		mMeshRenderer.materials[0].SetFloat ("_destroy", Mathf.Max(0.0f, 2.0f - frame / 180.0f));
		mMeshRenderer.materials[0].SetFloat ("_rate", Mathf.Min(1.0f, frame / 180.0f));
	}

	public override IEnumerator startSpell()
	{
		bulletPool.create ();
		isStopped = false;
		transform.position = GameController.Instance.Enemy.transform.position;
		int frame = 0;
		while (!isStopped) 
		{
			Draw (frame);

			foreach(ControlPoint p in controlPointList)
			{
                if ((Random.Range(0, 15) == 0) && (p.level / (float)MaxLevel * 180.0f < frame - 180.0f) && (p.level / (float)MaxLevel * 180.0f >= frame - 180.0f - 1))
				{
					GameObject bullet = bulletPool.createObject();
					bullet.transform.position = transform.position + (Vector3)p.position;
					bullet.SetActive(true);
					float angle = Random.Range(0, 2 * Mathf.PI);
					bullet.rigidbody2D.velocity = new Vector2(
						Mathf.Sin(angle),
						Mathf.Cos(angle)) * 1.0f;
				}
			}
			frame++;

			if (frame >= 360.0f)
			{
				frame = 0;
				generateSubControlPoint ();
			}
			yield return new WaitForFixedUpdate();
		}
		yield return null;
	}
	
	public override IEnumerator stopSpell()
	{
		isStopped = true;
		yield return null;
	}
}
