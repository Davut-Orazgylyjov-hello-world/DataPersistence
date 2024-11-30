using System;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    public static Action<SaveSlot> OnSelected;
    public int SlotIndex { get; private set; }
    public Profile Profile { get; private set; }
    [SerializeField] private TextMeshProUGUI slotName;
    [SerializeField] private GameObject slotSelectedUI;
    [SerializeField] private GameObject emptySlot, notEmptySlot;

    public void LoadData(Profile profile)
    {
        Profile = profile;
        SlotIndex = profile.indexProfile;
        slotName.SetText(Profile.profileName);
        InitUI();
    }

    private void InitUI()
    {
        emptySlot.SetActive(Profile.isEmpty);
        notEmptySlot.SetActive(!Profile.isEmpty);
        
        if (Profile.isEmpty)
        {
        
        }
        else
        {
            
        }
    }

    public void SelectProfile() => OnSelected?.Invoke(this);

    public void SetUISelectSlot(bool active)
    {
        slotSelectedUI.SetActive(active);
    }
}