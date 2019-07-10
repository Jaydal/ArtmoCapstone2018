using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateControl : MonoBehaviour {     private float _sensitivity;
  float rotationSpeed = 2f;
	void OnMouseDrag()
	{
		float XaxisRotation = Input.GetAxis("Mouse X")*rotationSpeed;
		float YaxisRotation = Input.GetAxis("Mouse Y")*rotationSpeed;
		// select the axis by which you want to rotate the GameObject
		transform.Rotate (Vector3.down, XaxisRotation,Space.World);
		transform.Rotate (Vector3.right, YaxisRotation,Space.World);
 
	}
}
