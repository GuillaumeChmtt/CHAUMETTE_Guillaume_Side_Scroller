using UnityEngine;

public class DebugCoins : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerPrefs.SetInt("TotalCoins", CoinManager.Instance.TotalCoins + 1000);
            PlayerPrefs.Save();
            CoinManager.Instance.RefreshUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
            PlayerPrefs.Save();
            CoinManager.Instance.RefreshUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            CoinManager.Instance.RefreshUI();
        }
    }
}