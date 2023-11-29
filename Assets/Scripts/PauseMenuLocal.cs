using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLocal : MonoBehaviour
{

	public GameObject settingsMenu;

    public void Pause()
    {
		MenuManager.instance.Pause();
    }

    public void Resume()
    {
		MenuManager.instance.Resume();
	}

    public void Settings()
    {
		settingsMenu.SetActive(true);
		MenuManager.instance.menu.SetActive(false);
    }

	public void SettingsBack()
	{
		settingsMenu.SetActive(false);
		MenuManager.instance.menu.SetActive(true);
	}

    public void MainMenu()
    {
		Application.Quit();
    }
}
