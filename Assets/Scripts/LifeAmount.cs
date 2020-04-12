using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAmount : MonoBehaviour
{
	public int startingHealth = 3;

	public int CurrentHealth
	{
		get;
		private set;
	}

	public Subject Subject
	{
		get;
		private set;
	}

	void Awake() 
	{
		Subject = new Subject();
	}

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = startingHealth;
		ApplyChange(0);
    }

	bool CheckDie()
	{
		return CurrentHealth < 0;
	}

	public void ApplyChange(int amount = -1)
	{
		CurrentHealth += amount;
		// Debug.Log(string.Format("Health changed: {0}, new value {1}", amount, CurrentHealth));
		Subject.Notify();
	}
}
