using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health = 150;
	public int points = 150;
	public GameObject laserPrefab;
	public float laserSpeed;
	public float shotsPerSeconds = 0.5f;
	public GameObject burst;
	public AudioClip deathSound;
	public AudioClip appearSound;

	private LevelManager levelmngr;

	void Start() {
		Invoke("PlayAppear", 0.8f);
	}

	void PlayAppear(){
		AudioSource.PlayClipAtPoint(appearSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D col) {
		Laser missile = col.gameObject.GetComponent<Laser>();
		PlayerController player = col.gameObject.GetComponent<PlayerController>();
		if (missile) {
			//Debug.Log ("hit by laser");
			health -= missile.GetDamage();
			missile.Hit();
			GetComponent<AudioSource>().Play();
			if (health <= 0) {
				Die();
			}
		}
		if (player) {
			levelmngr = GameObject.FindObjectOfType<LevelManager>();
			levelmngr.LoadLevel("Lose");
			Debug.Log ("collided with enemy");
		}
	}

	void Update () {

		float probability = Time.deltaTime*shotsPerSeconds;
		if (Random.value < probability) {
			Fire();
		}
	}

	void Fire() {
		//Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, -laserSpeed);
		laser.GetComponent<AudioSource>().Play();
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		GameObject smokePuff = Instantiate(burst, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy(gameObject);
		GameObject.Find("count").GetComponent<Score>().Scored(points);
	}
}
