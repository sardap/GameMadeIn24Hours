using System.Collections;
using System.Collections.Generic;
using TMPro;
// using TMPro;
using UnityEngine;

public class ScoreInfoUpdater : MonoBehaviour
{
	public class ScoreInfoObv : IGameObserver
	{
		private ScoreInfoUpdater Parent;

		public ScoreInfoObv(ScoreInfoUpdater parent)
		{
			Parent = parent;
		}

		public void OnNotify()
		{
		}
	}
	
	ScoreInfoObv _scoreInfoObv;

	public Distance distance;
	public TextMeshProUGUI progressText;

	// Start is called before the first frame update
	void Start()
    {
		if(distance == null)
		{
			distance = GameObject.FindGameObjectWithTag(CustomTags.GameManger).GetComponent<Distance>();
		}

		_scoreInfoObv = new ScoreInfoObv(this);
		distance.subject.AddObvs(_scoreInfoObv);

		Debug.Assert(distance != null);
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		distance.subject.RemoveObvs(_scoreInfoObv);
	}
}
