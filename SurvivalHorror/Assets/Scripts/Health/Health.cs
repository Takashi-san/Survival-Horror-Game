using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {
	[SerializeField] HealthInfo _healthInfo = null;
	int _health = 0;
	int _maxHealth = 0;

	public bool IsFull { get => _health == _maxHealth; }
	public Action<int> healthUpdate;

	void Awake() {
		// Load all health info.
		_maxHealth = _healthInfo.maxHealth;
		_health = _maxHealth/2;
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
}
