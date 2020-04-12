using System.Text;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FaceRecogniser : MonoBehaviour
{
	abstract class FaceFeature
	{
		protected FaceRecogniser Parent { get; set;}

		public FaceFeature(FaceRecogniser parent)
		{
			Parent = parent;
		}

		public abstract float CalcScore();
	}

	class EyeDistance : FaceFeature
	{
		public EyeDistance(FaceRecogniser p) : base(p) {}

		public override float CalcScore()
		{
			float currentEyeDistance = Vector3.Distance(Parent.leftEye.transform.position, Parent.rightEye.transform.position);
			float diff = Math.Abs(currentEyeDistance - Parent._idealEyeDistance);
			float result = diff < (Parent._idealEyeDistance - Parent.leftEye.transform.lossyScale.z) ? Parent.eyeDistanceScore * (1 - (diff / Parent._idealEyeDistance)) : 0;

			Debug.Log(string.Format("Eye Distance Score {0}, Diff {1}", result, diff));

			return result;
		}
	}

	class TransfromsAboveZ : FaceFeature
	{
		bool _above;
		Collider _collider;
		string _name;
		Transform[] _transfroms;

		public TransfromsAboveZ(
			FaceRecogniser p, 
			string name, 
			Collider c, 
			Transform[] transforms, 
			float score, 
			bool above = true
		) : base(p)
		{
			_collider = c;
			_name = name;
			_transfroms = transforms;
			_above = above;
		}

		public override float CalcScore()
		{
			float ColiderZ()
			{
				return _collider.bounds.max.z;
			}

			float result = 0;

			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append((_above ? "Above " : "Below ") + _name);
			for(int i = 0; i < _transfroms.Length; i++)
			{
				bool cond = _above && _transfroms[i].position.z > ColiderZ() || !_above && _transfroms[i].position.z < ColiderZ();
				if(cond)
				{
					result += Parent.eyesAboveNoseScore / _transfroms.Length;
				}
				
				stringBuilder.Append(string.Format(" transform {0} result {1} pos {2} target {3}", i, cond, _transfroms[i].position.z, ColiderZ()));
			}

			Debug.Log(stringBuilder.ToString());

			return result;
		}
	}


	List<FaceFeature> _faceFeatures;
	float _idealEyeDistance;

	public float eyeDistanceScore = 100;
	public float eyesAboveNoseScore = 100;
	public float eyesAboveLips = 50;
	public float lipsBelowNose = 50;

	public GameObject nose;
	public GameObject lips;
	public GameObject rightEye;
	public GameObject leftEye;
	
	void Start()
	{
		_faceFeatures = new List<FaceFeature>()
		{
			new TransfromsAboveZ
			(
				this, 
				"Eyes Nose", 
				nose.GetComponent<Collider>(),
				new Transform[]
				{
					rightEye.transform,
					leftEye.transform
				},
				eyesAboveNoseScore
			),
			new TransfromsAboveZ
			(
				this,
				"Eyes Lips",
				lips.GetComponent<Collider>(),
				new Transform[]
				{
					rightEye.transform,
					leftEye.transform
				},
				eyesAboveLips
			),
			new TransfromsAboveZ
			(
				this,
				"Lips Nose",
				nose.GetComponent<Collider>(),
				new Transform[]
				{
					lips.transform
				},
				lipsBelowNose,
				false
			),
			new EyeDistance(this),
		};

		_idealEyeDistance = Vector3.Distance(leftEye.transform.position, rightEye.transform.position);
	}
	
	public Score.ScoreEntry CalcScore()
	{
		float score = 0f;

		for(int i = 0; i < _faceFeatures.Count; i++)
		{
			score += _faceFeatures[i].CalcScore();
		}

		return new Score.ScoreEntry(){Value = score, Text = ""};
	}
}