using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueMethods : MonoBehaviour
{
	public void ExitForGame()
	{
		Application.Quit();
	}

	public void EnterToFight()
	{
		SceneManager.LoadScene("Pole");
	}
}
