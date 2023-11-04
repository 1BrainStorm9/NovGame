using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public TextAsset _inkJSON;

    private bool _isPlayerEnter;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private void Start()
	{
        _isPlayerEnter = false;

        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update()
	{
        if (_dialogueWindow.IsPlaying == true || _isPlayerEnter == false)
		{
            return;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
            _dialogueController.EnterDialogueMode(_inkJSON);
		}
	}

    private void OnTriggerEnter2D(Collider2D collider)
	{
        GameObject obj = collider.gameObject;

        if(obj.GetComponent<Player>() != null)
		{
            _isPlayerEnter = true;
		}
	}

    private void OnTriggerExit2D(Collider2D collider)
	{
        GameObject obj = collider.gameObject;

        if (obj.GetComponent<Player>() != null)
        {
            _isPlayerEnter = false;
        }
    }
}
