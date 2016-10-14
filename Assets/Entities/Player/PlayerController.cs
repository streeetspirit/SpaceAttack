using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

	public GameObject ship;

	public float speed;
	public float padding;
	public GameObject laserPrefab;
	public float laserSpeed, fireRate, health;

	float xmin = -5;
	float xmax = 5;

	float ymin;
	float ymax;
	private LevelManager levelmngr;

	// Use this for initialization
	void Start () {

		// Distance between object and camera
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftbottommost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightupmost = Camera.main.ViewportToWorldPoint (new Vector3 (1,1,distance));

		xmin = leftbottommost.x + padding;
		xmax = rightupmost.x - padding;

		ymin = leftbottommost.y + padding;
		ymax = rightupmost.y - padding;
	}

	void Fire() {
		Vector3 offset = new Vector3(0,1,0);
		GameObject laser = Instantiate(laserPrefab, transform.position+offset, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, laserSpeed, 0f);
		laser.GetComponent<AudioSource>().Play();
	}

	// Update is called once per frame
	void Update () {
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");

		Vector3 move = new Vector3 (moveX, moveY, 0.0f);
		// rb.AddForce (move * speed);

		// new position with adjusted speed
		transform.position += move * speed * Time.deltaTime;

		//restrict ship movement out of the camera
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);

		transform.position = new Vector3 (newX, newY, transform.position.z);

		//firing
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, fireRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		Laser missile = col.gameObject.GetComponent<Laser>();

		 if (missile) {
			//Debug.Log ("hit by laser");
			health -= missile.GetDamage();
			missile.Hit();
			GetComponent<AudioSource>().Play();

			if (health <= 0) {
				Die();
			} else GameObject.Find("health").GetComponent<Score>().UpdateHealth((int)(health/missile.GetDamage()));
		}

	}

	void Die() {
		Destroy(gameObject);
		levelmngr = GameObject.FindObjectOfType<LevelManager>();
		levelmngr.LoadLevel("Lose");
	}
}
