using System.Threading;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
	public float spinX;
	public float spinY;
	public float spinZ;


	void Update()
	{
		transform.Rotate (spinX * Time.deltaTime, spinY * Time.deltaTime, spinZ * Time.deltaTime);
	}
}