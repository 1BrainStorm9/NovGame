using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayingHeroInfo : MonoBehaviour
{
    [SerializeField] private Transform _heroInfoPanel;

    public void RefreshInfoPanel(Entity player)
    {

        _heroInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"����: {player.damage} ������: {player.protect.ToString()} \n��������: {player.health.ToString()} ���������: {player.evasionChance.ToString()}";
    }



    public void ClearInfoPanel()
    {
        _heroInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
