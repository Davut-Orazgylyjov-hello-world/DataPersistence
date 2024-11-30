using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static GameData GameData { get; private set; }
    [Header("Debugging")] [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")] [SerializeField]
    private bool autoSaveByTimer;

    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private FileDataHandler _fileDataHandler;
    private string _selectedProfileId = "ProfileID";
    private static DataPersistence _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        LoadGame();
        
        if (autoSaveByTimer)
            StartCoroutine(AutoSave());
    }

    public static void ChangeSelectedProfileId(string newProfileId)
    {
        // update the profile to use for saving and loading
        _instance._selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    public static void DeleteProfileData(string profileId)
    {
        // delete the data for this profile id
        _instance._fileDataHandler.Delete(profileId);
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }
    
    public static void DeleteSelectedProfileData()
    {
        DeleteProfileData(_instance._selectedProfileId);
    }

    public static void NewGameData()
    {
        GameData = new GameData();
    }

    private static void LoadGame()
    {
        // return right away if data persistence is disabled
        if (_instance.disableDataPersistence)
            return;

        // load any saved data from a file using the data handler
        GameData = _instance._fileDataHandler.Load(_instance._selectedProfileId);

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (GameData == null && _instance.initializeDataIfNull)
            NewGameData();

        // if no data can be loaded, don't continue
        if (GameData == null)
            Debug.LogError("No data was found. A New Game needs to be started before data can be loaded.");
    }

    private static void SaveGame()
    {
        // return right away if data persistence is disabled
        if (_instance.disableDataPersistence)
            return;

        // if we don't have any data to save, log a warning here
        if (GameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        // IDataPersistence dataPersistenceObj.SaveData(gameData);

        // timestamp the data so we know when it was last saved
        GameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        _instance._fileDataHandler.Save(GameData, _instance._selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public bool HasGameData()
    {
        return GameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}