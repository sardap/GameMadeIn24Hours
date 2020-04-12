using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCol : MonoBehaviour
{
	public GameObject player;
	
	Rigidbody _rigidbody;
	LifeAmount _lifeAmount;
	Vector3 _maxVol;
	bool _disableCol;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		_lifeAmount = GetComponent<LifeAmount>();
		_maxVol = new Vector3();
		
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag(CustomTags.Player);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(_rigidbody.velocity.y < _maxVol.y)
		{
			_maxVol = _rigidbody.velocity;
		}

		if(_disableCol)
		{
			_disableCol = false;
		}
    }

	void OnCollisionExit(Collision other) 
	{
		Debug.Log(string.Format("OnCollisionExit {0}", gameObject.GetInstanceID()));
	}

	//Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
		if(_disableCol)
		{
			return;
		}

		_disableCol = true;

		Debug.Log(string.Format("OnCollisionEnter {0} Other {1}", gameObject.GetInstanceID(), collision.gameObject.GetInstanceID()));

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (
			collision.gameObject.tag == CustomTags.FaceDamagingElement
		)
        {
			player.GetComponent<LifeAmount>().ApplyChange();
        }

		if (
			collision.gameObject.tag == CustomTags.FaceDamagingElement ||
			collision.gameObject.tag == CustomTags.FaceElement
		)
        {
			_rigidbody.velocity = new Vector3(_maxVol.x, -_maxVol.y, _maxVol.z);
			_lifeAmount.ApplyChange(-1);
        }
    }
}
