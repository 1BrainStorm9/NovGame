using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChoiseTarget : MonoBehaviour
{
    public static Action<Creature> OnSelectTarget;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<ColliderCreationController>().DeleteCollider();
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            GetComponent<SpawnTargetMarker>().DeleteMarker();

            if (hit.collider?.tag == "Enemy")
            {
                var target = hit.collider.gameObject.transform.Find("TargetPosition");
                GetComponent<SpawnTargetMarker>().SpawnEnemyMarker(target);
                OnSelectTarget?.Invoke(hit.collider.gameObject.GetComponent<Creature>());
            }
            else if (hit.collider?.tag == "Player")
            {
                var target = hit.collider.gameObject.transform.Find("TargetPosition");
                GetComponent<SpawnTargetMarker>().SpawnTeamMarker(target);
                OnSelectTarget?.Invoke(hit.collider.gameObject.GetComponent<Creature>());
            }
        }
    }
}