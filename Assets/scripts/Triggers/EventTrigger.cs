using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] protected GameObject EventText;
    public Text etext;
    protected int t;
    protected int s;

	public void Awake()
	{
		EventText.SetActive(false);
		
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		s = Random.Range(0, 100);

		if (col.tag == "Player" && s >=50)
		{
            t = Random.Range(0, 5);
            UpdateTextInEvent(t);
            EventText.SetActive(true);
		}
	}

    protected virtual void UpdateTextInEvent(int t)
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
