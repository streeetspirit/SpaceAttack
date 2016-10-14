﻿using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damage;

	public void Hit() {
		Destroy(gameObject);
	}

	public float GetDamage() {
		return damage;
	}
}
