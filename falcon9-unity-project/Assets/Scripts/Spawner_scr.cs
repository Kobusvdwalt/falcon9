using UnityEngine;
using System.Collections;

public class Spawner_scr : MonoBehaviour {

	public float spawnRate;
	public float range;
	public GameObject[] enemies;
	public int health;
	public GameObject explosionPrefab;
	public GameObject audioGOPrefab;
	public AudioClip explosion;

	int count = 0;

	GameObject scoreBar;
	GameObject multiplierBar;
	GameObject cam;
	GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");
		count = Random.Range(30, 200);
		scoreBar = GameObject.Find("ScoreBar");
		multiplierBar = GameObject.Find("MultiplierBar");
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector2.Distance(player.transform.position, transform.position) < range)
		{
			if (count < 0)
			{
				count = Mathf.RoundToInt(spawnRate / Time.fixedDeltaTime);

				GameObject a = Instantiate(enemies[Random.Range(0, enemies.Length)]);
				Vector3 offset = new Vector3(Mathf.Cos((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), Mathf.Sin((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), 0);
				a.transform.position = transform.position + offset;

			}
			count --;
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "PlayerBullet")
		{
			health --;

			if (health < 0)
			{
				scoreBar.GetComponent<ScoreBar_scr>().Shake();
				player.GetComponent<Player_scr>().score += 30 * player.GetComponent<Player_scr>().multiplier;

				multiplierBar.GetComponent<MultiplierBar_scr>().Shake();
				player.GetComponent<Player_scr>().multiplier ++;
				player.GetComponent<Player_scr>().multiplierCooldown += 120;

				Destroy(gameObject);
				GameObject a = Instantiate(audioGOPrefab);
				a.transform.position = transform.position;
				a.GetComponent<AudioSource>().clip = explosion;
				a.GetComponent<AudioSource>().Play();

				cam.GetComponent<Camera_scr>().ShakePosition(2.8f, 0.3f, 0, 1);

				GameObject b = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
			}
		}
	}
}
