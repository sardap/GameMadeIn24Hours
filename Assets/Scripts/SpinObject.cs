using System.Threading;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
	public float spinX;
	public float spinY;
	public float spinZ;
	public float speed = 1;

	void Update()
	{
		transform.Rotate (spinX * Time.deltaTime * speed, spinY * Time.deltaTime * speed, spinZ * Time.deltaTime * speed);
	}
}