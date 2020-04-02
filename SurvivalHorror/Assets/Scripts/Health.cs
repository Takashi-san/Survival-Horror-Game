using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {
	[SerializeField] HealthInfo _healthInfo = null;
	public int _health = 0;
	public int _maxHealth = 0;

	public Action<int> healthUpdate;

	void Awake() {
		// Load all health info.
		_maxHealth = _healthInfo.maxHealth;
		_health = _maxHealth;
	}

	void Start() {
		// Send initial value.
		if (healthUpdate != null) {
			healthUpdate(_health);
		}
	}

	public void DealDamage(int damage) {
		if (damage > 0) {
			_health -= damage;
			_health = _health < 0 ? 0 : _health;
			if (healthUpdate != null) {
				healthUpdate(_health);
			}
		}
	}

	public void HealDamage(int heal) {
		if (heal > 0) {
			_health += heal;
			_health = _health > _maxHealth ? _maxHealth : _health;
			if (healthUpdate != null) {
				healthUpdate(_health);
			}
		}
	}

	/*
	// Test.
	void Update() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			DealDamage(5);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			HealDamage(5);
		}
	}
	*/
}
