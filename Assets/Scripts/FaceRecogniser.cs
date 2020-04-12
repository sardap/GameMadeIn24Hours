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

	class EyesAboveZ : FaceFeature
	{
		private Collider _collider;
		private string _name;

		public EyesAboveZ(FaceRecogniser p, string name, Collider c) : base(p) 
		{
			_collider = c;
			_name = name;
		}

		public override float CalcScore()
		{
			float ColiderZ()
			{
				return _collider.bounds.max.z;
			}

			float result = 0;
			
			if(Parent.leftEye.transform.position.z > ColiderZ())
			{
				result += Parent.eyesAboveNoseScore / 2;
			}

			if(Parent.rightEye.transform.position.z > ColiderZ())
			{
				result += Parent.eyesAboveNoseScore / 2;
			}

			Debug.Log(
				string.Format(
					"Above {2} LeftEye {0}, RightEye {1}",
					Parent.leftEye.transform.position.z > ColiderZ(),
					Parent.rightEye.transform.position.z > ColiderZ(),
					_name
				)
			);

			return result;
		}
	}


	List<FaceFeature> _faceFeatures;
	float _idealEyeDistance;

	public float eyeDistanceScore = 100;
	public float eyesAboveNoseScore = 100;
	public GameObject nose;
	public GameObject lips;
	public GameObject rightEye;
	public GameObject leftEye;
	
	void Start()
	{
		_faceFeatures = new List<FaceFeature>()
		{
			new EyesAboveZ(this, "Nose", nose.GetComponent<Collider>()),
			new EyesAboveZ(this, "Lips", lips.GetComponent<Collider>()),
			new EyeDistance(this)
		};

		_idealEyeDistance = Vector3.Distance(leftEye.transform.position, rightEye.transform.position);
	}
	
	public float CalcScore()
	{
		float score = 0f;

		for(int i = 0; i < _faceFeatures.Count; i++)
		{
			score += _faceFeatures[i].CalcScore();
		}

		return score;
	}
}