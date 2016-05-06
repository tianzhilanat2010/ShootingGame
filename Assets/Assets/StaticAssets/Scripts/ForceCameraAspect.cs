using UnityEngine;
using System.Collections;

public class ForceCameraAspect : MonoBehaviour {

	void Awake()
	{
		Camera camera = this.GetComponent<Camera> ();

		camera.orthographicSize = Mathf.Max(5.0f, 5.0f * ((float)Screen.height / (float)Screen.width) * (640.0f/960.0f));
	}
}
