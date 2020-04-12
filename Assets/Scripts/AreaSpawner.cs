using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
	float _timeToNextSpawn;

	public GameObject toCreate;
	public int spawnRateMin = 5;
	public int spawnRateMax = 25;
	public Collider colider;

	void Start()
	{
		_timeToNextSpawn = NextSpawnTime();
		createObject();
	}

    // Update is called once per frame
    void Update()
    {
		_timeToNextSpawn -= Time.deltaTime;
		if(_timeToNextSpawn < 0)
		{
			_timeToNextSpawn = NextSpawnTime();
			createObject();
		}
    }

	void createObject()
	{
		Vector3 spawn = new Vector3(
			Random.Range(colider.bounds.min.x, colider.bounds.max.x),
			Random.Range(20, 20),
			Random.Range(colider.bounds.min.z, colider.bounds.max.z)
		);
		GameObject newObject = Instantiate(toCreate, spawn, toCreate.transform.rotation);
		newObject.transform.parent = transform;
	}

	int NextSpawnTime()
	{
		return Random.Range(spawnRateMin, spawnRateMax);
	}
}
