using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
[Serializable]
public class AssetQuest : ScriptableObject
{
    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public bool IsComplete => _isComplete;

    [SerializeField] private bool _rewardGiven;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private bool _isComplete;

    public bool IsRewardGiven()
    {
        return _rewardGiven;
    }

    public void SetRewardGiven(bool given)
    {
        _rewardGiven = given;
    }
}
