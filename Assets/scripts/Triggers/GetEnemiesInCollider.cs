using UnityEngine;

public class GetEnemiesInCollider : MonoBehaviour
{
    public delegate void EnemyTriggerEnterEvent(Enemy enemy);
    public static event EnemyTriggerEnterEvent OnEnemyTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnEnemyTriggerEnter?.Invoke(enemy);
        }
    }
}
