using UnityEngine;
using System.Collections;

public class ShootAway : MonoBehaviour {
	
	public float shootPower;
	public bool explosion = false;
	void OnTriggerStay(Collider other) 
	{
		if(!GetComponent<PlayerController>().onLive && other.gameObject.tag == Tags.bomb)
		{
			if(!explosion)
			{
				Vector3 shootDir = transform.forward;
				shootDir.Normalize();
				other.gameObject.rigidbody.AddForce(shootDir * shootPower,ForceMode.Impulse);
			}
			else
			{
				other.GetComponent<BombController>().StartExplosion();
			}
		}
	}
}
