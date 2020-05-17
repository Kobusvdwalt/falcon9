using UnityEngine;
using System.Collections;

public class Bullet_scr : MonoBehaviour {

	public float speed;
	public Vector2 startVelocity;
	Rigidbody2D rigid;
	public GameObject explosionPrefab;
	public GameObject owner;
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigid.velocity = startVelocity + (Vector2)transform.up * speed;
	}
	public void Destroy ()
	{
		GameObject a = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy(gameObject);
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Solid")
		{
			this.Destroy();
		}
		if (tag == "EnemyBullet" && col.tag == "Player")
		{
			this.Destroy();
		}
		if (tag == "PlayerBullet" && col.tag == "Enemy")
		{
			this.Destroy();
		}
	}
}
