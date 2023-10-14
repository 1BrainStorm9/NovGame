using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargetMarker : MonoBehaviour
{
    [SerializeField] private GameObject teamMarkerPrefab;
    [SerializeField] private GameObject enemyMarkerPrefab;
    private GameObject marker;

    public void SpawnEnemyMarker(Transform transform)
    {
        marker = Instantiate(enemyMarkerPrefab, transform.position,Quaternion.identity);
    }

    public void SpawnTeamMarker(Transform transform)
    {
        marker = Instantiate(teamMarkerPrefab, transform.position, Quaternion.identity);
    }

    public void DeleteMarker()
    {
        Destroy(marker);
    }

}
