using UnityEngine;

public class Score : MonoBehaviour
{
	public struct ScoreEntry
	{
		public float Value;
		public string Text;
	}

	private float _currentScore;
	private int _top;
	private ScoreEntry[] _scoreHistry;

	public float CurrentScore
	{
		private set;
		get;
	}

	public int NewHistroy
	{
		private set;
		get;
	}

	public int histroyLength = 100;
	public Subject subject;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		subject = new Subject();
	}

	void Start()
	{
		CurrentScore = 0;
		NewHistroy = -1;
		_scoreHistry = new ScoreEntry[histroyLength];
	}

	public void AddToScore(ScoreEntry score)
	{
		_scoreHistry[_top] = score;
		NewHistroy = _top;
		_top++;
		CurrentScore += score.Value;
		subject.Notify();
	}
}