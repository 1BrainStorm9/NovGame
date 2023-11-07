using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChoiseTarget : MonoBehaviour
{
    public static Action<Creature> OnSelectTarget;
    private GameController gameController;
    private HudManager hudManager;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        hudManager = FindObjectOfType<HudManager>();
    }

    private void Update()
    {
        var canChoise = gameController.IsPlayerTurn();
        if (canChoise && Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<ColliderCreationController>().DeleteCollider();
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider?.tag == "Enemy")
            {
                hudManager.ShowRightSide(false);
                GetComponent<SpawnTargetMarker>().DeleteMarker();
                var target = hit.collider.gameObject.transform.Find("TargetPosition");
                GetComponent<SpawnTargetMarker>().SpawnEnemyMarker(target);
                OnSelectTarget?.Invoke(hit.collider.gameObject.GetComponent<Creature>());
                return;
            }
            else if (hit.collider?.tag == "Player")
            {
                GetComponent<SpawnTargetMarker>().DeleteMarker();
                hudManager.ShowRightSide(true);
                var target = hit.collider.gameObject.transform.Find("TargetPosition");
                GetComponent<SpawnTargetMarker>().SpawnTeamMarker(target);
                OnSelectTarget?.Invoke(hit.collider.gameObject.GetComponent<Creature>());
                return;
            }
        }
    }
}