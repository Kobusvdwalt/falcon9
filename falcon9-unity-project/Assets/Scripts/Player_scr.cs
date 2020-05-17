using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Player_scr : MonoBehaviour {

	public float acceleration;
	public float deceleration;
	public float maxSpeed;
	public float turnSpeed;
	public float maxTurnSpeed;

	public int health = 100;
	public int maxHealth = 100;

	public int multiplier = 0;
	public int maxMultiplier = 30;

	public int multiplierCooldown = 200;
	public int maxMultiplierCooldown = 200;

	public int score;
	public float healthRegenRate;
	public float healthRegenCooldown;
	public float turnMultiplier;


	float turnSpeedMultiplier;
	int healthRegenCooldownCount = 0;
	int healthRegenRateCount = 0;

	Rigidbody2D rigid;
	GameObject cam;
	GameObject healthBar;
	float particlesAngle = 0;
	GameObject particles;
	ParticleSystem.EmissionModule particlesEM;
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		cam = GameObject.FindWithTag("MainCamera");
		particles = transform.Find("Particles").gameObject;
		particlesEM = particles.GetComponent<ParticleSystem>().emission;
		healthBar = GameObject.Find("HealthBar");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input_scr.OnUIBackwardPressed())
		{
			SceneManager.LoadScene("Menu");
			Camera_scr.timeScale = 1;
			Time.timeScale = 1;
		}
	}
	void FixedUpdate () {
		if (multiplierCooldown > 0)
		{
			multiplierCooldown --;
		}
		else
		{
			multiplier = 1;
		}
		multiplier = Mathf.Clamp(multiplier, 1, maxMultiplier);
		multiplierCooldown = Mathf.Clamp(multiplierCooldown, 0, maxMultiplierCooldown);

		if (healthRegenRateCount < 0)
		{
			healthRegenRateCount = Mathf.RoundToInt(healthRegenRate / Time.fixedDeltaTime);
			if (health < maxHealth && healthRegenCooldownCount < 0)
			{
				health ++;
			}
		}

		healthRegenCooldownCount --;
		healthRegenRateCount --;

		particles.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(particles.transform.localEulerAngles.z, particlesAngle, 0.1f));

		// Steering
		if (Input_scr.OnLeft())
		{
			rigid.AddTorque(turnSpeed);
			particlesAngle = -30;
		}
		else
		if (Input_scr.OnRight())
		{
			rigid.AddTorque(-turnSpeed);
			particlesAngle = 30;
		}
		else
		{
			rigid.angularVelocity = 0;
			particlesAngle = 0;
		}

		// Movement
		if (Input_scr.OnThrottle())
		{
			rigid.AddForce(transform.up*acceleration);
			particlesEM.enabled = true;
			GetComponents<AudioSource>()[1].mute = false;

			if (GetComponents<AudioSource>()[1].pitch > 0.8f)
			{
				GetComponents<AudioSource>()[1].pitch -= 0.02f;
			}
		}
		else
		{
			turnSpeedMultiplier = 1;
			particlesEM.enabled = false;
			GetComponents<AudioSource>()[1].mute = true;
			GetComponents<AudioSource>()[1].pitch = 1.6f;
		}
		if (Input_scr.OnThrottle() == false && rigid.velocity.magnitude > 10)
		{
			rigid.AddForce(-rigid.velocity * 0.5f);
		}

		if (Input_scr.OnThrottle())
		{
			turnSpeedMultiplier = turnMultiplier;
		}
		else
		if (Input_scr.OnFire())
		{
			turnSpeedMultiplier = 0.8f;
		}
		else
		{
			turnSpeedMultiplier = 1f;
		}

		// Constraints
		rigid.angularVelocity = Mathf.Clamp(rigid.angularVelocity, -maxTurnSpeed*turnSpeedMultiplier, maxTurnSpeed*turnSpeedMultiplier);
		if (rigid.velocity.magnitude > maxSpeed)
		{
			rigid.AddForce(-rigid.velocity * deceleration);
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "EnemyBullet")
		{
			DoDamage(10);
			col.GetComponent<Bullet_scr>().Destroy();
		}

	}

	public void DoDamage (int damage)
	{
		healthRegenCooldownCount = Mathf.RoundToInt(healthRegenCooldown / Time.fixedDeltaTime);
		cam.GetComponent<Camera_scr>().Pause(2);
		cam.GetComponent<Camera_scr>().ShakeAngle(6, 0);
		cam.GetComponent<Camera_scr>().ShakePosition(0.5f, 0.5f, 0, 0);



		healthBar.GetComponent<HealthBar_scr>().Shake();

		health -= damage;
		if (health <= 0)
		{
			GameObject.Find("GameOver").GetComponent<GameOver_scr>().Show();
		}
	}
}
