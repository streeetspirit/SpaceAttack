using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float height; 
	public float width; 
	public float speed;
	public float padding;
	public float spawnDelay = 0.5f;

	private float xmin, xmax;
	private Vector3 move = Vector3.right;
    private float timeLapsed = 0.0f; // SMD
	// create enemy at each position we marked on the playground, one by one

	// SMD
	void Spawn() { // SMD
	  Transform pos = transform.GetChild(1);
	  Vector3 enemyPos = pos.position;
	  enemyPos.x += 200;
	  GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity) as GameObject;
	  enemy.transform.parent = pos;
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition){
				GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = freePosition;

			}

		if (NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	void Start () {
		
		SpawnUntilFull();

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftbottommost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightupmost = Camera.main.ViewportToWorldPoint (new Vector3 (1,1,distance));
		xmin = leftbottommost.x + padding;
		xmax = rightupmost.x - padding;
	}

	public void OnDrawGizmos () {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((transform.position.x + width/2) >= xmax) {
			move = Vector3.left;
		} else if ((transform.position.x - width/2) <= xmin) {
			move = Vector3.right;
		}

		// print ("transform.position.x  "+transform.position.x+"; xmax xmin: "+xmax+xmin+"; move: "+move);
		transform.position += move * speed * Time.deltaTime;

		if (AllMembersDead()){
		  SpawnUntilFull();
		}

		timeLapsed += Time.deltaTime; // SMD
		if (timeLapsed >= 10) {
		  timeLapsed = 0;
		  Spawn();
		}

	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform)
			if (childPositionGameObject.childCount == 0) return childPositionGameObject;
		
		return null;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
