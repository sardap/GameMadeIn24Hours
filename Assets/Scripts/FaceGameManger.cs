using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceGameManger : MonoBehaviour
{
	private class LifeObvs : IGameObserver
	{
		FaceGameManger _parent;

		public LifeObvs(FaceGameManger parent)
		{
			_parent = parent;
		}

		public void OnNotify()
		{
			if(_parent.playerLife.CurrentHealth < 0 && !_parent.Invincible)
			{
				Debug.Log(string.Format("Player died"));
				_parent.GameOver();
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
				_parent.GameSuccess();
			}
		}
	}

	float _updateScoreIn;
	LifeObvs _lifeObvs;
	GameCountDownObvs _countDownObvs;
	
	public FaceRecogniser faceRecogniser;
	public LifeAmount playerLife;
	public GameCountDown gameTimeCountDown;
	public bool Invincible = false;

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

		_countDownObvs = new GameCountDownObvs(this);
		_lifeObvs = new LifeObvs(this);
		playerLife.Subject.AddObvs(_lifeObvs);
		gameTimeCountDown.Subject.AddObvs(_countDownObvs);
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
			Debug.Log(string.Format("Score {0}", faceRecogniser.CalcScore()));
			_updateScoreIn = 5;
		}
	}

	public void GameSuccess()
	{
		
	}

	public void GameOver()
	{
		SceneManager.LoadScene(CustomSceneNames.Main);
	}
}
