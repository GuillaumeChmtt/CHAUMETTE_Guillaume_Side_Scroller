using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private TextMeshProUGUI _coinText;

    private int _sessionCoins = 0;
    private const string SaveKey = "TotalCoins";

    public int TotalCoins => PlayerPrefs.GetInt(SaveKey, 0);
    public int SessionCoins => _sessionCoins;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        _sessionCoins += amount;
        UpdateUI();
    }

    public void SaveSessionCoins()
    {
        int total = TotalCoins + _sessionCoins;
        PlayerPrefs.SetInt(SaveKey, total);
        PlayerPrefs.Save();
    }

    public bool SpendCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            PlayerPrefs.SetInt(SaveKey, TotalCoins - amount);
            PlayerPrefs.Save();
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        if (_coinText != null)
            _coinText.text = "Coins : " + (TotalCoins + _sessionCoins);
    }

    public void RefreshUI()
    {
        UpdateUI();
    }
}