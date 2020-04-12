using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownRemaningUpdate : MonoBehaviour
{
	TextMeshProUGUI _textCom;
	GameCountDown _countDown;

    // Start is called before the first frame update
    void Start()
    {
		_textCom = GetComponent<TextMeshProUGUI>();
		_countDown = GameObject.FindGameObjectWithTag(CustomTags.GameManger).GetComponent<GameCountDown>();

		Debug.Assert(_countDown != null);
    }

	void Update()
	{
		_textCom.text = Math.Round(_countDown.RemaningTime).ToString();
	}
}