using UnityEngine;

public static class CarData
{
    public const string SelectedCarKey = "SelectedCar";
    public const int DefaultCar = 0;

    public static int GetSelectedCar()
    {
        return PlayerPrefs.GetInt(SelectedCarKey, DefaultCar);
    }

    public static void SetSelectedCar(int index)
    {
        PlayerPrefs.SetInt(SelectedCarKey, index);
        PlayerPrefs.Save();
    }
}