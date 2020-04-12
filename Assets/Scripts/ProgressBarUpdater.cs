using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBarUpdater : MonoBehaviour
{
	public class DistanceObv : IGameObserver
	{
		private ProgressBarUpdater Parent;

		public DistanceObv(ProgressBarUpdater parent)
		{
			Parent = parent;
		}

		public void OnNotify()
		{
			Parent.progressBar.BarValue = Parent.distance.DistanceTraveledPercent() * 100;
			Parent.progressText.text = Parent.distance.KilometersPerHour() + "kph";
		}
	}

	
	DistanceObv _distanceObv;
	ProgressBar progressBar;

	public Distance distance;
	public TextMeshProUGUI progressText;

	// Start is called before the first frame update
	void Start()
    {
		if(distance == null)
		{
			distance = GameObject.FindGameObjectWithTag(CustomTags.GameManger).GetComponent<Distance>();
		}

		progressBar = GetComponent<ProgressBar>();
		_distanceObv = new DistanceObv(this);
		distance.subject.AddObvs(_distanceObv);

		Debug.Assert(distance != null);
    }

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		distance.subject.RemoveObvs(_distanceObv);
	}
}
