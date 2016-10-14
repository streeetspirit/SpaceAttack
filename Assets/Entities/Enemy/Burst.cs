using UnityEngine;
using System.Collections;

public class Burst : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Destroy(gameObject)", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
