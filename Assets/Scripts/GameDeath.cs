using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeath : MonoBehaviour
{
	private class LifeObvs : IGameObserver
	{
		GameDeath _parent;

		public LifeObvs(GameDeath parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			if(_parent._lifeAmount.CurrentHealth <= 0)
			{
				_parent.Kill();
			}
		}
	}

	LifeAmount _lifeAmount;

    // Start is called before the first frame update
    void Start()
    {
		_lifeAmount = GetComponent<LifeAmount>();
		_lifeAmount.Subject.AddObvs(new LifeObvs(this));
    }

	void Kill()
	{
		Destroy(gameObject);
	}
}
