[System.Serializable]
public class RunData
{
    public int score;
    public float time;

    public RunData(int score, float time)
    {
        this.score = score;
        this.time = time;
    }
}

[System.Serializable]
public class RunSaveData
{
    public System.Collections.Generic.List<RunData> lastRuns = new();
    public RunData bestRun = null;
}