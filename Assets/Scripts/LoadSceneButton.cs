using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
	public string nextScene;

	public void OnClick()
	{
		SceneManager.LoadScene(nextScene);
	}
}
