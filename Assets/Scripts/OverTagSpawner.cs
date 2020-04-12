using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTagSpawner : MonoBehaviour
{
	private GameObject[] _candidates;

	float _timeToNextSpawn;

	public GameObject toCreate;
	public int spawnRateMin = 5;
	public int spawnRateMax = 10;
	public string searchTag;

	public float spawnHeight = 20f;

	void Start()
	{
		_candidates = GameObject.FindGameObjectsWithTag(searchTag);
		_timeToNextSpawn = NextSpawnTime();
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
		Vector3 spawnPos = _candidates[Random.Range(0, _candidates.Length)].transform.position;
		spawnPos.y = spawnHeight;

		GameObject newObject = Instantiate(
			toCreate,
			spawnPos,
			toCreate.transform.rotation
		);
		newObject.transform.parent = transform;
	}

	int NextSpawnTime()
	{
		return Random.Range(spawnRateMin, spawnRateMax);
	}
}
