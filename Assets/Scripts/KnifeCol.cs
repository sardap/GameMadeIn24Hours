using UnityEngine;

public class KnifeCol : MonoBehaviour
{
	Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

	//Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (
			collision.gameObject.tag == CustomTags.FaceElement
		)
        {
			_rigidbody.isKinematic = true;
			transform.parent = collision.transform;
        } 
		else 
		{
			_rigidbody.velocity = new Vector3(0, 20, 0);
		}
    }
}
