using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCountDown : MonoBehaviour
{
	public int startingTime = 60;

	public Subject Subject
	{
		get;
		private set;
	}

	public float RemaningTime
	{
		get;
		private set;
	}

	void Awake() 
	{
		Subject = new Subject();
	}

	void Start()
	{
		RemaningTime = startingTime;
	}

	void Update()
	{
		RemaningTime -= Time.deltaTime;
		if(RemaningTime <= 0)
		{
			Subject.Notify();
		}
	}
}
