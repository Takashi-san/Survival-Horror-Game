using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyArea : MonoBehaviour {
	public Action<bool> playerInArea;
	public bool PlayerInArea => _playerInArea;
	bool _playerInArea = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("Collided player");
			_playerInArea = true;
			if (playerInArea != null) {
				playerInArea(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("Collided player out");
			_playerInArea = false;
			if (playerInArea != null) {
				playerInArea(false);
			}
		}
	}
}
