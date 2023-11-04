using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DialogueChoise))]

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _displayName;
    [SerializeField] private TextMeshProUGUI _displayText;

    [SerializeField] private GameObject _dialogueWindow;

    [SerializeField, Range(0f, 20f)] public float _cooldownNewLetter;

    private DialogueChoise _dialogueChoise;

    public bool IsStatusAnswer { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool CanContinueToNextLine { get; private set; }

    public float CoolDownNewLetter
	{
        get => _cooldownNewLetter;
        private set
		{
            _cooldownNewLetter = CheckCooldown(value);
		}
	}

    private void CheckCooldown(float value)
	{
        if(value < 0)
		{
            throw new ArgumentException("Значение задержки между буквами было отрицательное");
		}

        return value;
	}

    public void Init()
	{
        IsStatusAnswer = false;
        CanContinueToNextLine = false;

        _dialogueChoise = GetComponent<DialogueChoise>();
        _dialogueChoise.Init();
	}

    public void SetActive(bool active)
	{
        IsPlaying = active;
        _dialogueWindow.SetActive(active);
	}

    public void SetText(string text)
	{
        _displayText.text = text;
	}

    public void Add(string text)
	{
        _displayText.text += text;
	}
    public void Add(char letter)
    {
        _displayText.text += letter;
    }

    public void ClearText()
    {
        SetText("");
    }

    public void SetName(string namePerson)
	{
        _displayName.text = namePerson;
	}

    public void SetCoolDown(float cooldown)
	{
        CoolDownNewLetter = cooldown;
	}

    public void MakeChoice()
	{
        if(CanContinueToNextLine == false)
		{
            return;
		}

        IsStatusAnswer = false;
	}

    public IEnumerator DisplayLine(Story story)
	{
        string line = story.Continue();

        ClearText();

        _dialogueChoise.HideChoices();

        CanContinueToNextLine = false;


        bool isAddingRishText = false;

        yield return new WaitForSeconds(0.001f); //Оно блять ломается без этой строки(((

        foreach (char letter in line.ToCharArray())
		{
			if (Input.GetMouseButtonDown(0))
			{
                SetText(line);
                break;
			}

            isAddingRishText = letter == '<' || isAddingRishText;

            if(letter == '>')
			{
                isAddingRishText = false;
			}

            Add(letter);

            if(isAddingRishText == false)
			{
                yield return new WairForSeconds(_cooldownNewLetter);
			}
        }

        CanContinueToNextLine = true;

        IsStatusAnswer = _dialogueChoise.DisplayChoices(story);
	}
}
