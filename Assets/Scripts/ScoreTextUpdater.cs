using System;

using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
	private class ScoreObvs : IGameObserver
	{
		ScoreTextUpdater _parent;

		public ScoreObvs(ScoreTextUpdater parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			_parent._text.text = string.Format("Total\nScore\n{0}", Math.Round(_parent._score.CurrentScore));
		}
	}

	ScoreObvs _scoreObvs;
	TextMeshProUGUI _text;
	Score _score;

	// Start is called before the first frame update
	void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
		_score = GameObject.FindGameObjectWithTag(CustomTags.GameManger).GetComponent<Score>();

		_scoreObvs = new ScoreObvs(this);
		_score.subject.AddObvs(_scoreObvs);

		Debug.Assert(_score != null);
	}
}
