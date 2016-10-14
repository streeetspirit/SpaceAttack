using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text displayScore = GetComponent<Text>();
		displayScore.text = Score.shipCount.ToString();
		Score.Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
