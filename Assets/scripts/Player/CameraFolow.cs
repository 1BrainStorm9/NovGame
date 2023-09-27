using UnityEngine;

public class CameraFolow : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private Transform playerTransform;
	[SerializeField] private string playerTag;
	[SerializeField] private float movingSpeed;

	[SerializeField] float leftLimit;
	[SerializeField] float rightLimit;
	[SerializeField] float bottomLimit;
	[SerializeField] float upperLimit;


	private void Awake()
	{
		if(this.playerTransform == null)		//поиск игрока по факту
		{
			if(this.playerTag == "")
			{
				this.playerTag = "Player";
			}

			this.playerTransform = GameObject.FindGameObjectWithTag(this.playerTag).transform;
		}

		this.transform.position = new Vector3()		//слегка смещение камеры от игрока
		{
			x = this.playerTransform.position.x,
			y = this.playerTransform.position.y,
			z = this.playerTransform.position.z - 10,
		};
	}

	private void Update()
	{
		if (this.playerTransform)		//передвижение камеры за игроком
		{
			Vector3 target = new Vector3()
			{
				x = this.playerTransform.position.x,
				y = this.playerTransform.position.y,
				z = this.playerTransform.position.z - 10,
			};

			Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

			this.transform.position = pos;
		}

		transform.position = new Vector3		//–амки установлены в самом юнити, их не мен€ть если не мен€етс€ камера
			(
			Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
			Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
			transform.position.z
			);

	}
}
