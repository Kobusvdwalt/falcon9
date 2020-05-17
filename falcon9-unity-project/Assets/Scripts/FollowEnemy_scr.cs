using UnityEngine;
using System.Collections;

public class FollowEnemy_scr : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;
	public int health;
	public float lifetime;
	public GameObject bulletPrefab;
	public GameObject audioGOPrefab;
	public GameObject explosionPrefab;
	public AudioClip explosion;

	GameObject player;
	Rigidbody2D rigid;
	GameObject scoreBar;
	GameObject multiplierBar;
	GameObject cam;

	int lifetimeCount;
	void Start () {
		player = GameObject.FindWithTag("Player");
		rigid = GetComponent<Rigidbody2D>();
		maxSpeed *= Random.Range(0.8f, 1.3f);
		scoreBar = GameObject.Find("ScoreBar");
		multiplierBar = GameObject.Find("MultiplierBar");
		cam = GameObject.FindWithTag("MainCamera");

		lifetimeCount = Mathf.RoundToInt(lifetime / Time.fixedDeltaTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localEulerAngles += new Vector3(0, 0, 20);
		Vector2 delta = new Vector2(transform.position.x - player.transform.position.x, 
									transform.position.y - player.transform.position.y);
		delta.Normalize();
		rigid.AddForce(-delta * acceleration);

		if (rigid.velocity.magnitude > maxSpeed)
		{
			rigid.AddForce(-rigid.velocity);
		}

		lifetimeCount --;
		if (lifetimeCount < 0 || Vector2.Distance(transform.position, player.transform.position) < 0.8f)
		{
			for (int i=0; i < 6; i ++)
			{
				GameObject a = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
				a.transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + i*60);
				a.GetComponent<Bullet_scr>().speed = 30;
				a.GetComponent<Bullet_scr>().owner = gameObject;
			}

			Destroy(gameObject);
			GameObject b = Instantiate(audioGOPrefab);
			b.transform.position = transform.position;
			b.GetComponent<AudioSource>().clip = explosion;
			b.GetComponent<AudioSource>().Play();

			cam.GetComponent<Camera_scr>().ShakePosition(2.8f, 0.3f, 0, 1);

			GameObject c = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "PlayerBullet")
		{
			DoDamage(1);
			col.GetComponent<Bullet_scr>().Destroy();
		}
	}

	public void DoDamage (int damage)
	{
		if (scoreBar == null)
		{
			player = GameObject.FindWithTag("Player");
			scoreBar = GameObject.Find("ScoreBar");
			multiplierBar = GameObject.Find("MultiplierBar");
			cam = GameObject.FindWithTag("MainCamera");
		}

		health -= damage;
		if (health < 0)
		{
			for (int i=0; i < 6; i ++)
			{
				GameObject a = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
				a.transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + i*60);
				a.GetComponent<Bullet_scr>().speed = 30;
				a.GetComponent<Bullet_scr>().owner = gameObject;
			}

			scoreBar.GetComponent<ScoreBar_scr>().Shake();
			player.GetComponent<Player_scr>().score += 15 * player.GetComponent<Player_scr>().multiplier;

			multiplierBar.GetComponent<MultiplierBar_scr>().Shake();
			player.GetComponent<Player_scr>().multiplier ++;
			player.GetComponent<Player_scr>().multiplierCooldown += 70;

			Destroy(gameObject);
			GameObject b = Instantiate(audioGOPrefab);
			b.transform.position = transform.position;
			b.GetComponent<AudioSource>().clip = explosion;
			b.GetComponent<AudioSource>().Play();

			cam.GetComponent<Camera_scr>().ShakePosition(2.8f, 0.3f, 0, 1);

			GameObject c = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		}
	}
}
