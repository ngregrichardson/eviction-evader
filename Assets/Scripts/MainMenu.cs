using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
	public void Credits()
	{
		SceneManager.LoadScene(3);
	}


	public void Quit()
    {
        Application.Quit();
    }
}
