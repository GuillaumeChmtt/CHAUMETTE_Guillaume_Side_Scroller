using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingsWindow;
    public GameObject VideoWindow;
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button clicked");
        SettingsWindow.SetActive(true); 
    }

    private void Update()
    {
        if(SettingsWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsWindow.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void VideoButton()
    {
        Debug.Log("Video button clicked");
        VideoWindow.SetActive(true);
    }
} 