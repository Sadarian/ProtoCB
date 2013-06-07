using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float minRadius = 0.68f;
	public float maxRadius = 1.64f;
	public float explosionSpeed = 3f;
	private float radiusMargin = 0.2f;
	
	private SphereCollider explosionCollider;
	// Use this for initialization
	void Awake () {
		explosionCollider = GetComponent<SphereCollider>();
		explosionCollider.radius = minRadius;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(explosionCollider.radius < maxRadius)
		{
			explosionCollider.radius += (explosionSpeed*Time.deltaTime)*(maxRadius-minRadius);	
		}
		else
		{
			//Explosion:
			GameObject.Destroy(transform.parent.gameObject);
			if(GameObject.FindGameObjectsWithTag(Tags.bomb).Length == 1)
				GameObject.FindGameObjectWithTag(Tags.gamecontroller).GetComponent<GameController>().SpawnBomb();
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == Tags.unit)
			GameObject.Destroy(other.gameObject);
		else if(other.gameObject.tag == Tags.player)
		{
			GameObject.Destroy(other.gameObject);
			GameObject[] bases = GameObject.FindGameObjectsWithTag(Tags.basis);
			foreach(GameObject basis in bases)
			{
				if(basis.GetComponent<Base>().myBase)
					basis.GetComponent<Base>().SpawnUnit();
					
			}
		}
	}
	
}
