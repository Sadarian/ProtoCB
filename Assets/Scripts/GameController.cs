using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float noSpawnRadius = 2;
	public float respawnTime = 2000;
	public int bombMinRespawn = 20;
	public int bombMaxRespawn = 300;
	
	public GameObject bombPref;
	
	
	private float nextBombAt;
	
	void Awake()
	{
		SpawnBomb();	
	}
	
	
	void Update () 
	{
		if(Time.time > nextBombAt)
			SpawnBomb();
		
	}
	
	public void SpawnBomb()
	{
		GameObject spawnPoint = GameObject.FindGameObjectWithTag(Tags.spawnPoint);
		GameObject unit = (GameObject)GameObject.Instantiate(bombPref,spawnPoint.transform.position,spawnPoint.transform.rotation);
		nextBombAt = Time.time + Random.Range(bombMinRespawn,bombMaxRespawn);
		Debug.Log("Next Bomb in "+ (nextBombAt-Time.time)+" seconds");
	}
}
