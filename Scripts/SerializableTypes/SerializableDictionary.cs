using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    //Don't remove SerializeField, else desirialization will dont work
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load the dictionary from lists
    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys ("
                           + keys.Count + ") does not match the number of values (" + values.Count
                           + ") which indicates that something went wrong");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }

    public TValue GetValue(TKey key)
    {
        TryGetValue(key, out var value);
        return value;
    }

    public TValue GetValue(TKey key, TValue defaultValue)
    {
        return TryGetValue(key, out var value) ? value : defaultValue;
    }

    public void SetValue(TKey key, TValue value)
    {
        if (ContainsKey(key))
            Remove(key);

        Add(key, value);
    }
}