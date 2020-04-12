using UnityEngine;

public class Distance : MonoBehaviour
{
	float _currentDistance = 0;

	public float targetDistance = 1000;
	public Subject subject;

	public float CurrentDistance
	{
		get
		{
			return _currentDistance;
		}
		set
		{
			_currentDistance = value;
			subject.Notify();
		}
	}

	public float CurrentSpeed
	{
		get;
		set;
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		subject = new Subject();
	}

	void Start()
	{
	}

	public float DistanceTraveledPercent()
	{
		return _currentDistance / targetDistance;
	}

	public float DistanceTraveled()
	{
		return _currentDistance;
	}

	public float KilometersPerHour()
	{
		return CurrentSpeed;
	}
}