using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObject : MonoBehaviour {
	[SerializeField] int _dmg = 0;

	bool _hit = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (_hit) return;
		if (other.tag == "Player") {
			_hit = true;
			other.GetComponent<Health>().DealDamage(_dmg);
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (_hit) return;
		if (other.tag == "Player") {
			_hit = false;
			other.GetComponent<Health>().DealDamage(_dmg);
			Destroy(gameObject);
		}
	}
}
