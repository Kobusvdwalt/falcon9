using UnityEngine;
using System.Collections;

public class EnemyShip_scr : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;
	public float range;
	public float fireRate;
	public int health;
	public float bulletSpeed;
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	public GameObject audioGOPrefab;
	public AudioClip explosion;

	GameObject player;
	GameObject scoreBar;
	GameObject multiplierBar;
	GameObject cam;
	Rigidbody2D rigid;
	int count;

	void Start () {
		player = GameObject.FindWithTag("Player");
		rigid = GetComponent<Rigidbody2D>();
		maxSpeed *= Random.Range(0.8f, 1.3f);
		scoreBar = GameObject.Find("ScoreBar");
		multiplierBar = GameObject.Find("MultiplierBar");
		fireRate *= Random.Range(0.8f, 1.3f);
		cam = GameObject.FindWithTag("MainCamera");
	}

	void FixedUpdate () {
		Vector2 delta = new Vector2(transform.position.x - player.transform.position.x,
									transform.position.y - player.transform.position.y);
		
		delta.Normalize();
		rigid.AddForce(-delta * acceleration);

		if (rigid.velocity.magnitude > maxSpeed)
		{
			rigid.AddForce(-rigid.velocity);
		}

		transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg + 90);
		if (count < 0)
		{
			count = Mathf.RoundToInt(fireRate / Time.fixedDeltaTime);

			if (Vector2.Distance(transform.position, player.transform.position) < range)
			{
				GameObject a = Instantiate(bulletPrefab, transform.position - (Vector3)delta *2, Quaternion.identity) as GameObject;
				a.transform.localEulerAngles = transform.localEulerAngles + new Vector3 (0, 0, Random.Range(-10, 10));
				a.GetComponent<Bullet_scr>().startVelocity = rigid.velocity;
				a.GetComponent<Bullet_scr>().speed = bulletSpeed;
				a.GetComponent<Bullet_scr>().owner = gameObject;
				GetComponents<AudioSource>()[0].Play();
			}
			 
		}
		count --;
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "PlayerBullet")
		{
			DoDamage(1);
		}
		if (col.tag == "Player")
		{
			col.GetComponent<Player_scr>().DoDamage(10);
			DoDamage(10);
		}
	}
	public void DoDamage (int damage)
	{
		health -= damage;
		if(health < 0)
		{
			Destroy(gameObject);
			scoreBar.GetComponent<ScoreBar_scr>().Shake();
			player.GetComponent<Player_scr>().score += 25;

			multiplierBar.GetComponent<MultiplierBar_scr>().Shake();
			player.GetComponent<Player_scr>().multiplier ++;
			player.GetComponent<Player_scr>().multiplierCooldown += 70;

			GameObject a = Instantiate(audioGOPrefab);
			a.transform.position = transform.position;
			a.GetComponent<AudioSource>().clip = explosion;
			a.GetComponent<AudioSource>().Play();
			GameObject b = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;

			cam.GetComponent<Camera_scr>().ShakePosition(2.8f, 0.3f, 0, 1);
		}
	}
}
