using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
	public Color SelectedColor = new Color(0, 0, 255, 255);
	public Color UnselectedColor = new Color(255, 0, 0, 255);


	ClickMove _clickMove;
	Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _clickMove = GetComponent<ClickMove>();
		_renderer = GetComponent<Renderer>();
		Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Select()
	{
		_clickMove.enabled = true;
		_renderer.material.SetColor("_FirstOutlineColor", SelectedColor);
		_renderer.material.SetColor("_SecondOutlineColor", SelectedColor);
	}

	public void Deselect()
	{
		_clickMove.enabled = false;
		_renderer.material.SetColor("_FirstOutlineColor", UnselectedColor);
		_renderer.material.SetColor("_SecondOutlineColor", UnselectedColor);
	}
}
