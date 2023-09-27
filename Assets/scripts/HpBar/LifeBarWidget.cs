
using UnityEngine;

namespace PixelCrew.UI.Widgets
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _lifeBar;

        private float _maxHp;

        private void Start()
        {
            _maxHp = GetComponentInParent<Creature>().maxHealth;
        }


        public void HpChanged(float hp)
        {
            var progress =  hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

    }
}