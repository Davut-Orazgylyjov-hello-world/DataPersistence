using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotsManagement : MonoBehaviour
{
    private enum ActiveWindow
    {
        False,
        SlotsWindow,
        CreatNewSlotWindow
    }

    public static SaveSlotsManagement Initial;
    public static bool WindowIsOpened { get; private set; }
    [SerializeField] private GameObject slotsWindow, createNewSlotWindow;
    [SerializeField] private TMP_InputField inputFieldNewProfileName;
    [SerializeField] private SaveSlot[] saveSlots;
    [SerializeField] private GameObject uiWindow;
    [SerializeField] private Button buttonDeleteSlot, buttonNewGame, buttonPlay;
    private SaveSlot _selectedSaveSlot;

    private void OnEnable() => SaveSlot.OnSelected += SetSelectedSlot;

    private void OnDisable() => SaveSlot.OnSelected -= SetSelectedSlot;

    private void Awake() 
    {
        if(Initial == null)
            Initial = this;
    }

    public void Start() => Init();

    private void Init()
    {
        if (DataPersistence.GameData.CurrentProfile.isEmpty == false)
        {
            SelectCurrentSlot();
            PlaySelectedProfile();
        }
        else
        {
            NoSelectedSlot();
            OpenSaveSlotsWindow();
        }
    }

    public void OpenSaveSlotsWindow() => SetCurrentActiveWindow(ActiveWindow.SlotsWindow);

    private void LoadSlotsData()
    {
        for (int i = 0; i < saveSlots.Length; i++)
            saveSlots[i].LoadData(DataPersistence.GameData.profiles[i]);
    }

    private void NoSelectedSlot()
    {
        LoadSlotsData();
        foreach (var saveSlot in saveSlots)
            saveSlot.SetUISelectSlot(false);

        UpdateButtons();
    }

    private void SelectCurrentSlot() => SetSelectedSlot(saveSlots[DataPersistence.GameData.currentIndexProfile]);

    private void SetSelectedSlot(SaveSlot selectedSaveSlot)
    {
        LoadSlotsData();
        _selectedSaveSlot = selectedSaveSlot;

        foreach (var saveSlot in saveSlots)
            saveSlot.SetUISelectSlot(saveSlot == _selectedSaveSlot);

        DataPersistence.GameData.SelectProfile(_selectedSaveSlot.SlotIndex);
        UpdateButtons();
    }

    public void PlaySelectedProfile() => SetCurrentActiveWindow(ActiveWindow.False);

    public void NewGameProfile()
    {
        inputFieldNewProfileName.text = String.Empty;
        SetCurrentActiveWindow(ActiveWindow.CreatNewSlotWindow);
        inputFieldNewProfileName.Select();
    }

    public void CreateNewProfile()
    {
        if (inputFieldNewProfileName.text.Length <= 1) return;
        _selectedSaveSlot.Profile.isEmpty = false;
        _selectedSaveSlot.Profile.profileName = inputFieldNewProfileName.text;
        LoadSlotsData();
        UpdateButtons();
        PlaySelectedProfile();
    }

    public void DeleteSelectedSlot()
    {
        DataPersistence.GameData.DeleteProfile(_selectedSaveSlot.SlotIndex);
        SelectCurrentSlot();
    }

    private void SetCurrentActiveWindow(ActiveWindow activeWindow)
    {
        uiWindow.SetActive(activeWindow != ActiveWindow.False);
        slotsWindow.SetActive(activeWindow == ActiveWindow.SlotsWindow);
        createNewSlotWindow.SetActive(activeWindow == ActiveWindow.CreatNewSlotWindow);
        WindowIsOpened = activeWindow != ActiveWindow.False;
    }

    private void UpdateButtons()
    {
        if (_selectedSaveSlot == null || _selectedSaveSlot.Profile == null)
        {
            buttonDeleteSlot.gameObject.SetActive(false);
            buttonPlay.gameObject.SetActive(false);
            buttonNewGame.gameObject.SetActive(false);
        }
        else
        {
            buttonDeleteSlot.gameObject.SetActive(_selectedSaveSlot.Profile.isEmpty == false);
            buttonPlay.gameObject.SetActive(_selectedSaveSlot.Profile.isEmpty == false);
            buttonNewGame.gameObject.SetActive(_selectedSaveSlot.Profile.isEmpty);
        }
    }
}