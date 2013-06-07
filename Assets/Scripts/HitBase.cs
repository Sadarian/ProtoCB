using UnityEngine;
using System.Collections;

public class HitBase : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.bomb) 
        {
            Debug.Log("Tor! Base Destroyed");
            Application.LoadLevel(0);
        }
    }
}
