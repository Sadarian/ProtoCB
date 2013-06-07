using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float rotationSpeed = 1;
	public bool clickRequired = true;
	public bool joystick = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!joystick && (Input.GetMouseButton(0)||!clickRequired))
		{
			transform.Rotate(0,Input.GetAxis("Mouse X")*rotationSpeed,0);
		}
		if(joystick)
		{
			transform.Rotate(0,Input.GetAxis("Horizontal")*rotationSpeed,0);
			transform.Rotate(0,Input.GetAxis("CameraX")*rotationSpeed,0);
		}
	}
}
