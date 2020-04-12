using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FaceGameManger : MonoBehaviour
{
	private class LifeObvs : IGameObserver
	{
		int _lastHealth;
		FaceGameManger _parent;

		public LifeObvs(FaceGameManger parent)
		{
			_parent = parent;
			_lastHealth = _parent.playerLife.CurrentHealth;
		}

		public void OnNotify()
		{
			if(_parent.playerLife.CurrentHealth < _lastHealth)
			{
				_parent.Speed -= _parent.Speed * 0.74f;
			}
		}
	}

	private class GameCountDownObvs : IGameObserver
	{
		FaceGameManger _parent;

		public GameCountDownObvs(FaceGameManger parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			if(_parent.gameTimeCountDown.RemaningTime <= 0)
			{
				Debug.Log(string.Format("Game time out!"));
				_parent.GameOver();
			}
		}
	}

	float _speed;
	float _updateScoreIn;
	LifeObvs _lifeObvs;
	GameCountDownObvs _countDownObvs;

	float Speed 
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;

			foreach(var spin in dude.GetComponentsInChildren<SpinObject>())
			{
				spin.speed = _speed;
			}

			backgroundVideo.playbackSpeed = _speed;
		}
	}
	
	public FaceRecogniser faceRecogniser;
	public LifeAmount playerLife;
	public GameCountDown gameTimeCountDown;
	public Score score;
	public Distance distance;
	public VideoPlayer backgroundVideo;
	public bool Invincible = false;
	public GameObject dude;

    // Start is called before the first frame update
    void Start()
    {
        if(playerLife == null)
		{
			playerLife = GameObject.FindGameObjectWithTag(CustomTags.Player).GetComponent<LifeAmount>();
		}

		if(gameTimeCountDown == null)
		{
			gameTimeCountDown = GetComponent<GameCountDown>();
		}

		if(faceRecogniser == null)
		{
			faceRecogniser = GetComponent<FaceRecogniser>();
		}

		if(score == null)
		{
			score = GetComponent<Score>();
		}

		if(distance == null)
		{
			distance = GetComponent<Distance>();
		}

		if(backgroundVideo == null)
		{
			backgroundVideo = GameObject.FindGameObjectWithTag(CustomTags.Background).GetComponent<VideoPlayer>();
			Debug.Assert(backgroundVideo != null);
		}

		if(dude == null)
		{
			dude = GameObject.FindGameObjectWithTag(CustomTags.Dude);
		}

		_countDownObvs = new GameCountDownObvs(this);
		_lifeObvs = new LifeObvs(this);
		playerLife.Subject.AddObvs(_lifeObvs);
		gameTimeCountDown.Subject.AddObvs(_countDownObvs);

		_speed = 0.5f;
    }

	void OnDestroy()
	{
		playerLife.Subject.RemoveObvs(_lifeObvs);
		gameTimeCountDown.Subject.AddObvs(_countDownObvs);
	}

	void Update()
	{
		_updateScoreIn -= Time.deltaTime;
		if(_updateScoreIn <= 0)
		{
			score.AddToScore(faceRecogniser.CalcScore());
			_updateScoreIn = 5;
			Speed += (score.CurrentScore / 7000f);
			distance.CurrentSpeed = Speed;
		}
		distance.CurrentDistance += Speed * Time.deltaTime;

		if(distance.DistanceTraveledPercent() >= 0.95)
		{
			GameSuccess();
		}
	}

	public void GameSuccess()
	{
		SceneManager.LoadScene(CustomSceneNames.GameSuccess);
	}

	public void GameOver()
	{
		SceneManager.LoadScene(CustomSceneNames.GameOver);
	}
}
