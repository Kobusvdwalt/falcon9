using UnityEngine;
using System.Collections;

public class Planet_scr : MonoBehaviour {

	public int maxHealth;
	public float size;
	public GameObject explostionPrefab;
	public GameObject audioGOPrefab;
	public AudioClip explosion;
	public GameObject[] components;

	Vector3[] slotPositions;
	GameObject[] slots;
	int health;
	float maxTurnsSpeed;
	int turnDirection;
	GameObject scoreBar;
	GameObject multiplierBar;
	GameObject player;
	public void Generate () {
		scoreBar = GameObject.Find("ScoreBar");
		multiplierBar = GameObject.Find("MultiplierBar");
		maxTurnsSpeed = 0;
		if (Random.Range(0, 100) > 50)
		{
			maxTurnsSpeed = Random.Range(5, 20) / GetComponent<Rigidbody2D>().mass;
			if (Random.Range(0, 100) > 50)
			{
				turnDirection = 1;
			}
			else
			{
				turnDirection = -1;
			}
		}


		// Physical attributes
		maxHealth = Mathf.RoundToInt(size);
		health = maxHealth;
		transform.localScale = new Vector3(size, size, size);

		// Add components
		int componentCount = Random.Range(1, Mathf.RoundToInt(size)/2);
		if (Random.Range(0, 100) > 90)
		{
			Random.Range(Mathf.RoundToInt(size)/2, Mathf.RoundToInt(size));
		}

		slotPositions = new Vector3[Mathf.RoundToInt(size)];
		slots = new GameObject[Mathf.RoundToInt(size)];
		for (int i=0; i < slots.Length; i ++)
		{
			slotPositions[i] = transform.position + new Vector3(Mathf.Cos(i * 360/slots.Length * Mathf.Deg2Rad), Mathf.Sin(i * 360/slots.Length * Mathf.Deg2Rad), 0) * size /2;
		}

		for (int i=0; i < componentCount; i ++)
		{
			int randomSlot = Random.Range(0, slots.Length);
			while (slots[randomSlot] != null)
			{
				randomSlot = Random.Range(0, slots.Length);
			}

			GameObject a = Instantiate(components[Random.Range(0, components.Length)]);
			a.transform.position = slotPositions[randomSlot];
			float angle = Mathf.Atan2(a.transform.position.y - transform.position.y,
									 a.transform.position.x - transform.position.x) * Mathf.Rad2Deg -90;
			a.transform.localEulerAngles = new Vector3(0, 0, angle);
			a.transform.parent = transform;
			slots[randomSlot] = a;
		}

		transform.localScale = new Vector3(0, 0, 0);
	}
	void Update ()
	{
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
			return;
		}
		GetComponent<Rigidbody2D>().AddTorque(10*GetComponent<Rigidbody2D>().mass * turnDirection);

		if (Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) > maxTurnsSpeed)
		{
			GetComponent<Rigidbody2D>().AddTorque(-GetComponent<Rigidbody2D>().angularVelocity * GetComponent<Rigidbody2D>().mass);
		}
		GetComponent<Rigidbody2D>().angularVelocity = Mathf.Clamp(GetComponent<Rigidbody2D>().angularVelocity, -maxTurnsSpeed, maxTurnsSpeed);
		HSBColor temp = HSBColor.FromColor(GameObject.FindWithTag("MainCamera").GetComponent<Camera>().backgroundColor);
		temp.b = 1;
		temp.a = 1;
		GetComponent<SpriteRenderer>().color = temp.ToColor();

		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3((float)health/(float)maxHealth * size, (float)health/(float)maxHealth * size, 0), 0.2f);


	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "PlayerBullet")
		{
			if (transform.childCount <= 0 && health > 0)
			{
				health --;
				scoreBar.GetComponent<ScoreBar_scr>().Shake();
				player.GetComponent<Player_scr>().score += 2 * player.GetComponent<Player_scr>().multiplier;
				player.GetComponent<Player_scr>().multiplierCooldown += 20;

				if (health <=2)
				{
					GameObject b = Instantiate(explostionPrefab);
					b.transform.position = transform.position;

					GameObject a = Instantiate(audioGOPrefab);
					a.transform.position = transform.position;
					a.GetComponent<AudioSource>().clip = explosion;
					a.GetComponent<AudioSource>().Play();

					player.GetComponent<Player_scr>().multiplierCooldown += 20;
					player.GetComponent<Player_scr>().multiplier ++;

					Destroy(gameObject);
				}
			}
		}
	}
}