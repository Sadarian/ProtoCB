using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	
	public float shootPower = 2; 
	public int minTick = 30;
	public int maxTick = 50;
	private float explodeAt;
	private bool readyForExplosion = false;
	// Update is called once per frame
	void Awake () {
			explodeAt = Time.time + Random.Range(minTick,maxTick);
			Debug.Log("Explodes in "+(explodeAt-Time.time)+" seconds");
	}
	
	void Update () {
		if(Time.time > explodeAt && !transform.FindChild("explosion").gameObject.activeSelf)
		{
			readyForExplosion = true;
			Debug.Log("Ready for Explosion");
		}
	}
	
	void OnTriggerStay(Collider other) 
	{
		GameObject player = GameObject.FindGameObjectWithTag(Tags.player);

        if (other.gameObject == player)
		{
			if(Input.GetButtonDown("Shoot"))
			{
				Vector3 shootDir = transform.position - player.transform.position;
				shootDir.Normalize();
				rigidbody.AddForce(shootDir * shootPower,ForceMode.Impulse);
			}
		}
		
		if(readyForExplosion && (other.gameObject.tag == Tags.player || other.gameObject.tag == Tags.unit))
			StartExplosion();
	}
	
	public void StartExplosion()
	{
		transform.FindChild("explosion").gameObject.SetActive(true);
	}
}
