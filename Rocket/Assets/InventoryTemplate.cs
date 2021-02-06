using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory")]
public class InventoryTemplate : ScriptableObject
{
    [SerializeField] private GameObject _datafullSkin;


    [SerializeField] private List<GameObject> _unlockedSkins = new List<GameObject>();

    /// <summary>
    /// Read only
    /// </summary>
    public List<GameObject> UnlockedSkins => _unlockedSkins;


    [SerializeField] private GameObject _currentSkin;

    /// <summary>
    /// Read only
    /// </summary>
    public GameObject CurrentSkin => _currentSkin;


    public void SetCurrentSkin(GameObject skin)
    {
        _currentSkin = skin;
    }

    public void AddSkin(GameObject skin)
    {
        _unlockedSkins.Add(skin);
    }

    public void Reset()
    {
        _currentSkin = _datafullSkin;
        _unlockedSkins.Clear();
    }

    
}
