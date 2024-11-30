[System.Serializable]
public class GamePlay
{
    // all parameters will reset on start
    public int levelDuration;
    public int cassette;
    public int playerLevel;
    public int playerExperienceHave;
    public int score;
    public SerializableDictionary<string, int> skillsLevel = new();
    public SerializableDictionary<string, int> skillDamageType = new();
    public SerializableDictionary<string, bool> roomSpawned = new();
}