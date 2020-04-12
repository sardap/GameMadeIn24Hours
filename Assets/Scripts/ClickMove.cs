using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour
{
	NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
		_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				_agent.destination = hit.point;
			}
		}	
    }
}
