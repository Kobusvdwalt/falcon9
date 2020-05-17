using UnityEngine;
using System.Collections;

public class Weapon_scr : MonoBehaviour {

	public float fireRate;
	public float bulletSpread;
	public float bulletSpeed;
	public GameObject bulletPrefab;

	GameObject cam;

	int count;
	void Start () {
		cam = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (count < 0)
		{
			count = Mathf.RoundToInt(fireRate / Time.fixedDeltaTime);

			if (Input_scr.OnFire())
			{
				GameObject a = Instantiate(bulletPrefab);
				Vector3 offset = new Vector3(Mathf.Cos((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), Mathf.Sin((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), 0);
				a.transform.position = transform.position + offset;

				a.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, Random.Range(-bulletSpread, bulletSpread));
				a.GetComponent<Bullet_scr>().startVelocity = GetComponent<Rigidbody2D>().velocity;
				a.GetComponent<Bullet_scr>().owner = gameObject;
				a.GetComponent<Bullet_scr>().speed = bulletSpeed;
				cam.GetComponent<Camera_scr>().ShakePosition(0.5f, 0.4f, 0, 1);
				GetComponent<AudioSource>().Play();

				transform.position -= new Vector3(Mathf.Cos((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), Mathf.Sin((transform.localEulerAngles.z+90)* Mathf.Deg2Rad), 0) * 0.2f;
			}
			else
			{
			}
		}
		count --;

	}
}
