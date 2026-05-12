using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelector : MonoBehaviour
{
    [Header("Images des voitures")]
    [SerializeField] private Image _carImage;
    [SerializeField] private Sprite[] _carSprites; // 3 sprites : voiture 1, voiture 2, coming soon

    [Header("Buttons")]
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _buyPriceText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Header("Settings")]
    [SerializeField] private int[] _carPrices; // prix de chaque voiture (0 = gratuit)

    private int _currentIndex = 0;
    private int _totalCars = 3;

    private void Start()
    {
        UpdateUI();
    }

    public void GoLeft()
    {
        _currentIndex = Mathf.Max(0, _currentIndex - 1);
        UpdateUI();
    }

    public void GoRight()
    {
        _currentIndex = Mathf.Min(_totalCars - 1, _currentIndex + 1);
        UpdateUI();
    }

    public void SelectCar()
    {
        CarData.SetSelectedCar(_currentIndex);
        UpdateUI();
    }

    public void BuyCar()
    {
        int price = _carPrices[_currentIndex];
        if (CoinManager.Instance.SpendCoins(price))
        {
            // Sauvegarde que la voiture est achetée
            PlayerPrefs.SetInt("CarUnlocked_" + _currentIndex, 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
        else
        {
            Debug.Log("Need more coins !");
        }
    }

    private bool IsUnlocked(int index)
    {
        if (index == 0) return true; // voiture de base toujours débloquée
        return PlayerPrefs.GetInt("CarUnlocked_" + index, 0) == 1;
    }

    private void UpdateUI()
    {
        if (_carSprites == null || _carSprites.Length == 0) return;
        if (_carPrices == null || _carPrices.Length == 0) return;

        // Image
        _carImage.sprite = _carSprites[_currentIndex];

        // Flèches
        _leftArrow.gameObject.SetActive(_currentIndex > 0);
        _rightArrow.gameObject.SetActive(_currentIndex < _totalCars - 1);

        // Coins
        _coinsText.text = "Coins : " + CoinManager.Instance.TotalCoins;

        bool unlocked = IsUnlocked(_currentIndex);
        bool isComingSoon = _currentIndex == 2;
        bool isSelected = CarData.GetSelectedCar() == _currentIndex;

        // Boutons
        _selectButton.gameObject.SetActive(unlocked && !isComingSoon);
        _buyButton.gameObject.SetActive(!unlocked && !isComingSoon);

        // Texte prix
        if (!unlocked && !isComingSoon)
            _buyPriceText.text = _carPrices[_currentIndex] + " coins";
            _buyPriceText.gameObject.SetActive(!unlocked && !isComingSoon);

        // Highlight si sélectionnée
        _selectButton.interactable = !isSelected;
        _selectButton.GetComponentInChildren<TextMeshProUGUI>().text = isSelected ? "Selected" : "Select";
    }
}