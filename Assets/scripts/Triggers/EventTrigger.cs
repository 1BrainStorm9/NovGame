using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
	private GameObject EventText;
    public Text etext;
	private int t;
	private int s;

	public void Awake()
	{
		EventText = GameObject.Find("EventText");
		EventText.SetActive(false);
		
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		s = Random.Range(0, 100);

		if (col.tag == "Player" && s >=50)
		{
			EventText.SetActive(true);
			t = Random.Range(0, 5);
		}
	}

	public void Update()
	{
		

		switch (t)
		{
			case 1:
				this.etext.text = "��� ���������� ������ ������ �����. ������� ��� ��������� �������."; //������ ���� ������������ ������ � ��������, �� ��� ���� �� ������ � ���, �� �������� �������� �����
				break;

			case 2:
				this.etext.text = "��� �� ����� �� �������� �������� �����. ������� ����� � ������ ������, �� ������, ��� ����� �������. ��� ������� ������� �����."; //����� ������� ��������� � �������� �����
				break;

			case 3:
				this.etext.text = "������� ���� �����, �� ������ �������� �������� � ������ � ��������. ������� ��� ��������."; //��� ��� �� ��� � ����, �� ��� �������� ��� ��� ��� ���� �� ������ 
				break;

			case 4:
				this.etext.text = "������� ���� �����, �� ��������� ����������� � ���� ����, ��� �� �������� ��������, ������� � ����, � ������ ��� ���. �������� ��������, ��� ������ ������� �� ��������."; //������ ��� ��������� ������ �������, ���������� � ���, ��� ����� ��������� ������ ������
				break;

			case 5:
				this.etext.text = "�������� �� ������ ������������ �������������� �������. �������� ���������� ���� �������� ����� ������ ���: ��, ������ ���� ���� ��������, ������ �� ���."; //������ ���������, ��� �������� ������ �� ������ ����, �� � � �����, �.�. ��� ��������� ������ �����
				break;

			default:
				this.etext.text = "�� �� �������� ������ ����������.";
				break;
		}
	}

	public void Accept()
	{
		EventText.SetActive(false);
	}
}
