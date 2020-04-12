﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionInput : MonoBehaviour
{
	Selectable _selected = null;

    // Update is called once per frame
    void Update()
    {
		if(Input.GetMouseButtonDown(0)) 
		{
			RaycastHit hit;
			if(
				Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)
			)
			{
				Selectable newSelected = hit.collider.gameObject.GetComponentInParent<Selectable>();

				if(newSelected != null)
				{
					if(_selected != null) 
					{
						_selected.Deselect();
					}

					_selected = newSelected;
					_selected.Select();
				} 
				else 
				{
					if(_selected != null) 
					{
						_selected.Deselect();
						_selected = null;
					}
				}
			}
		}
    }
}
