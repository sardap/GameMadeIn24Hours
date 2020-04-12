using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
	private class LifeObvs : IGameObserver
	{
		PlayerDamage _parent;

		public LifeObvs(PlayerDamage parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			if(_parent._lifeAmount.CurrentHealth < _parent._lastHealth)
			{
				_parent._cameara.GetComponent<SpinObject>().spinZ = Random.Range(80, 150);
				_parent._spinTimeLeft = 0.3f;
				_parent._spining = true;
			}

			_parent._lastHealth = _parent._lifeAmount.CurrentHealth;
		}
	}

	bool _spining = false;
	float _spinTimeLeft;
	int _lastHealth; 
	LifeObvs _lifeObvs;
	LifeAmount _lifeAmount;
	GameObject _cameara;

    // Start is called before the first frame update
    void Start()
    {
		_lifeAmount = GetComponent<LifeAmount>();
		_lifeObvs = new LifeObvs(this);
		_lifeAmount.Subject.AddObvs(_lifeObvs);

		_lastHealth = _lifeAmount.CurrentHealth;

		_cameara = GameObject.FindGameObjectWithTag(CustomTags.MainCamera);
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		_lifeAmount.Subject.RemoveObvs(_lifeObvs);
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(_spining)
		{
			_spinTimeLeft -= Time.deltaTime;
			if(_spinTimeLeft < 0)
			{
				_spining = false;
				_cameara.GetComponent<SpinObject>().spinZ = 0f;
			}
		}
	}
}
