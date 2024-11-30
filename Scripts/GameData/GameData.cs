[System.Serializable]
public class GameData
{
    //don't use Get; Set; its not working with deserialization
    public long lastUpdated;
    public int lustGameVersion;
    public Profile[] profiles = GetEmptyProfiles();
    public Options options = new();
    public int currentIndexProfile;

    public GameData()
    {
        // the values defined in this constructor will be the default values
        // the game starts with when there's no data to load
    }

    public Profile CurrentProfile => profiles[currentIndexProfile];

    public void DeleteProfile(int indexProfile)
    {
        profiles[indexProfile] = new Profile(indexProfile);
    }

    public void SelectProfile(int indexProfile)
    {
        currentIndexProfile = indexProfile;
    }

    public static Profile[] GetEmptyProfiles()
    {
        return new Profile[] {new(0), new(1), new(2)};
    }

    public void ResetAllProfiles()
    {
        profiles = GetEmptyProfiles();
    }
}