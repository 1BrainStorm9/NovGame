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
				this.etext.text = "¬ам перебежала дорогу чЄрна€ кошка.  ажетс€ вас постигнут неудачи."; //„ерные коты перебегающие дорогу к неудачам, но вот если ты живешь с ним, то наоборот огромный успех
				break;

			case 2:
				this.etext.text = "»д€ по траве вы заметили странный блеск. ѕодойд€ ближе и подн€в вещицу, вы пон€ли, что нашли подкову. ¬ас ожидает немного удачи."; //Ќайти подкову считалось к огромной удачи
				break;

			case 3:
				this.etext.text = "ѕроход€ мимо домов, вы успели заметить лестницу и обошли еЄ стороной.  ажетс€ все обошлось."; //¬се так же как и ниже, но тут персонаж под ней все таки не прошел 
				break;

			case 4:
				this.etext.text = "ѕроход€ мимо домов, вы настолько погрузились в свои думы, что не заметили лестницу, сто€щую у дома, и прошли под ней. ќстаетс€ наде€тс€, что ничего плохого не случитс€."; //ѕройти под лестницей плоха€ примета, говорилось о том, что можно встретить скорую смерть
				break;

			case 5:
				this.etext.text = "Ќевольно вы начали насвистывать незамысловатую мелодию. ¬незапно проход€ща€ мимо старушка стала ругать вас: ќх, совсем бесы теб€ попутали, нельз€ же так."; //–аньше считалось, что свистеть нельз€ не только дома, но и в целом, т.к. это считалось пением бесов
				break;

			default:
				this.etext.text = "¬ы не заметили ничего необычного.";
				break;
		}
	}

	public void Accept()
	{
		EventText.SetActive(false);
	}
}
