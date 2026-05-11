using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreboardManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrickSystem _trickSystem;
    [SerializeField] private Timer _timer;

    [Header("Best Run UI")]
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _bestTimeText;

    [Header("Last Runs UI (3 entrées)")]
    [SerializeField] private TextMeshProUGUI[] _lastScoreTexts; // 3 éléments
    [SerializeField] private TextMeshProUGUI[] _lastTimeTexts;  // 3 éléments

    private const string SaveKey = "RunSaveData";

    public void ShowScoreboard()
    {
        int currentScore = _trickSystem.Score;
        float currentTime = _timer.Elapsed;

        // Charger les données sauvegardées
        RunSaveData data = Load();

        // Ajouter la run actuelle
        data.lastRuns.Add(new RunData(currentScore, currentTime));
        if (data.lastRuns.Count > 3)
            data.lastRuns.RemoveAt(0);

        // Mettre à jour la meilleure run
        if (data.bestRun == null || currentScore > data.bestRun.score ||
            (currentScore == data.bestRun.score && currentTime < data.bestRun.time))
            data.bestRun = new RunData(currentScore, currentTime);

        // Sauvegarder
        Save(data);

        // Afficher la meilleure run
        if (data.bestRun != null)
        {
            _bestScoreText.text = "Score : " + data.bestRun.score;
            _bestTimeText.text = "Temps : " + FormatTime(data.bestRun.time);
        }

        // Afficher les dernières runs
        for (int i = 0; i < _lastScoreTexts.Length; i++)
        {
            int index = data.lastRuns.Count - 1 - i;
            if (index >= 0)
            {
                _lastScoreTexts[i].text = "Score : " + data.lastRuns[index].score;
                _lastTimeTexts[i].text = "Temps : " + FormatTime(data.lastRuns[index].time);
            }
            else
            {
                _lastScoreTexts[i].text = "-";
                _lastTimeTexts[i].text = "-";
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int centiseconds = Mathf.FloorToInt((time * 100f) % 100f);
        return $"{minutes:00}:{seconds:00}:{centiseconds:00}";
    }

    private void Save(RunSaveData data)
    {
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    private RunSaveData Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
            return JsonUtility.FromJson<RunSaveData>(PlayerPrefs.GetString(SaveKey));
        return new RunSaveData();
    }
}