using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour {
	public bool PlayerInArea => _playerInArea;

	bool _playerInArea = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			_playerInArea = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			_playerInArea = false;
		}
	}
}
