using UnityEngine;
using System.Collections;

public class Turret_scr : MonoBehaviour {

	public float fireRate;
	public float range;
	public int health;
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	public GameObject audioGOPrefab;
	public AudioClip explosion;

	GameObject canon;
	GameObject player;
	GameObject scoreBar;
	GameObject multiplierBar;
	GameObject cam;

	float targetAimAngle;
	float aimAngle;

	int count;

	void Start () {
		canon = transform.Find("Canon").gameObject;
		player = GameObject.FindWithTag("Player");
		scoreBar = GameObject.Find("ScoreBar");
		multiplierBar = GameObject.Find("MultiplierBar");
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector2.Distance(transform.position, player.transform.position) < range)
		{
			Vector2 delta = new Vector2(transform.position.x - player.transform.position.x, 
									transform.position.y - player.transform.position.y);
			targetAimAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg +180;
			targetAimAngle = To360Angle(targetAimAngle);
			if ((targetAimAngle > transform.eulerAngles.z && targetAimAngle < transform.eulerAngles.z+180) || 
				(transform.eulerAngles.z >= 270 && (targetAimAngle > transform.eulerAngles.z || targetAimAngle < To360Angle(transform.eulerAngles.z+180))) )
			{
				aimAngle = Mathf.LerpAngle(aimAngle, targetAimAngle-transform.eulerAngles.z, 0.1f);
				canon.transform.localPosition = new Vector3(Mathf.Cos(aimAngle* Mathf.Deg2Rad)*0.5f, Mathf.Sin(aimAngle* Mathf.Deg2Rad)*0.5f, 0);
				canon.transform.localEulerAngles = new Vector3(0, 0, aimAngle-90);

				if (count < 0)
				{
					count = Mathf.RoundToInt(fireRate / Time.fixedDeltaTime);

					GameObject a = Instantiate(bulletPrefab);
					Vector3 offset = new Vector3(Mathf.Cos((targetAimAngle)* Mathf.Deg2Rad), Mathf.Sin((targetAimAngle)* Mathf.Deg2Rad), 0);
					a.transform.position = canon.transform.position + offset*1;
					a.transform.eulerAngles = new Vector3(0, 0, targetAimAngle -90 + Random.Range(-10, 10));
				}
				count--;
			}
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
				player.GetComponent<Player_scr>().score += 15 * player.GetComponent<Player_scr>().multiplier;

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

	// Helper function to normilize angle
    protected float To360Angle(float angle)
    {
        while(angle<0.0f) angle+=360.0f;
        while(angle>=360.0f) angle-=360.0f;
        return angle;
    }
}
