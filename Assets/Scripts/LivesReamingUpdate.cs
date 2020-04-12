using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesReamingUpdate : MonoBehaviour
{
	private class LifeObvs : IGameObserver
	{
		LivesReamingUpdate _parent;

		public LifeObvs(LivesReamingUpdate parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			_parent._text.text = "Lives Remaning: " + _parent._lifeAmount.CurrentHealth;
		}
	}

	public GameObject player;
	
	LifeAmount _lifeAmount;
	TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
		{
			player = GameObject.FindGameObjectWithTag(CustomTags.Player);
		}

		_lifeAmount = player.GetComponent<LifeAmount>();
		_text = GetComponent<TextMeshProUGUI>();

		_lifeAmount.Subject.AddObvs(new LifeObvs(this));
    }
}
