using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingsButton()
    {
        //gogogogo gadgetoolololololololo
        Debug.Log("Settings button clicked");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
} 