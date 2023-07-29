using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayingHeroInfo : MonoBehaviour
{
    [SerializeField] private Transform _heroInfoPanel;

    public void RefreshInfoPanel(Creature player)
    {

        _heroInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Урон: {player.damage} Защита: {player.protect.ToString()} \nЗдоровье: {player.health.ToString()} Уклонение: {player.evasionChance.ToString()}";
    }



    public void ClearInfoPanel()
    {
        _heroInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
