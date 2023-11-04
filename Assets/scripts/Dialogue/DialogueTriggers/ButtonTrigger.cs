using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    public void ChangeField(int value)
	{
		Story story = new Story(_inkJSON.text);

		story.variablesState["nameField"] = value;

		Debug.Log(story.variablesState["nameField"]);
	}
}
