using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.Entity
{
    public class DataAfterLoadController : MonoBehaviour
    {
        [SerializeField] private List<Hero> heroList;
        private GameSession _session;
        [SerializeField] private GameObject _panel;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            heroList.AddRange(_session.Heroes);
            Instantiate(heroList[0], _panel.transform);
        }

    }
}