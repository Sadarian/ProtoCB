using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 1;
	private bool _onlive = false;
	public bool onLive 
	{
	    get{return _onlive;}
	    set
	    {   
			//Diese Einheit auf Player schalten
	       	if(!_onlive && value)
			{
				if(GameObject.FindGameObjectsWithTag(Tags.player).Length != 0)
				GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerController>().onLive = false;
				transform.tag = Tags.player;
				GetComponent<CameraController>().enabled = true;
				transform.FindChild("cameraPivot").transform.FindChild("camera").transform.GetComponent<AudioListener>().enabled = true;
			}
			//Player ausschalten
			else if(_onlive && !value)
			{
				transform.tag = Tags.unit;
				GetComponent<CameraController>().enabled = false;
				transform.FindChild("cameraPivot").transform.FindChild("camera").transform.GetComponent<AudioListener>().enabled = false;
			}
			_onlive = value;
	    }
	}
	
	private CharacterController controller;
	
	void Awake () 
	{
		controller = GetComponent<CharacterController>();
		if(transform.tag == Tags.player)
			onLive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!onLive)
			return;
		
		//Blickrichtung der Kamera
		Vector3 forward = transform.FindChild("cameraPivot").transform.FindChild("camera").transform.TransformDirection(Vector3.forward);
		//Höhe ist egal
		forward.y = 0;
		forward.Normalize();
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		Vector3 targetDirection = h * right + v * forward;
		targetDirection = targetDirection.normalized*speed;
		targetDirection.y = -1;
		controller.Move(targetDirection);
	}
	
}
