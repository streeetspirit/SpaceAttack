using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static int shipCount=0;
	private static int totalPoints=0;
	private Text myScore;
	private Text myHealth;

	// Use this for initialization
	void Start () {
		myScore = GetComponent<Text>();
		myHealth = GetComponent<Text>();
	}


	public static void Reset(){
		shipCount = 0;
		totalPoints = 0;
		//myScore.text = shipCount.ToString();
	}

	public void ResetHealth(){
		myHealth.text = "IIIII";
	}

	public void Scored(int points){
		shipCount++;
		totalPoints+=points;
		myScore.text = shipCount.ToString();
	}

	public void UpdateHealth(int lives){
		myHealth.text = "";
		for (int i=0; i<lives;i++) {
			myHealth.text+="I";
		}
	}
}
