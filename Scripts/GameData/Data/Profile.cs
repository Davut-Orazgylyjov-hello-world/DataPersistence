[System.Serializable]
public class Profile
{
    public int indexProfile;
    public bool isEmpty;
    public string profileName;

    public SerializableDictionary<string, int> boostLevel;
    public int coins;
    public int currentIndexFloor;
    public bool continuePlaying;
    public int currentDifficultyLevel;
    public int indexSelectedCharacter;
    public bool[] characterIsBayed;
    public int foundedCharacters;
    public GamePlay gamePlay;

    public Profile(int indexProfile)
    {
        this.indexProfile = indexProfile;
        isEmpty = true;
        characterIsBayed = new bool[50];
        gamePlay = new GamePlay();
        boostLevel = new SerializableDictionary<string, int>();
    }
    
    // public int GetPercentageComplete() 
    // {
    //     // figure out how many coins we've collected
    //     int totalCollected = 0;
    //     foreach (bool collected in coinsCollected.Values) 
    //     {
    //         if (collected) totalCollected++;
    //     }
    //
    //     // ensure we don't divide by 0 when calculating the percentage
    //     int percentageCompleted = -1;
    //     if (coinsCollected.Count != 0) 
    //     {
    //         percentageCompleted = (totalCollected * 100 / coinsCollected.Count);
    //     }
    //     return percentageCompleted;
    // }
}