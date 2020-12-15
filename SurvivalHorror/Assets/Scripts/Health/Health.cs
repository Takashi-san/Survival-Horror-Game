using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour {
	public int CurrentHealth => _health;
	public bool IsFull { get => _health == _maxHealth; }
	public Action<int> healthUpdate;

	[SerializeField] HealthInfo _healthInfo = null;
	[SerializeField] AudioClip[] _hurtSound = null;
	[SerializeField] AudioSource _audioSource = null;
	int _health = 0;
	int _maxHealth = 0;

	void Awake() {
		// Load all health info.
		_maxHealth = _healthInfo.maxHealth;
		_health = _maxHealth / 2;
	}

	void Start() {
		// Send initial value.
		if (healthUpdate != null) {
			healthUpdate(_health);
		}
	}

	public void DealDamage(int damage) {
		if (damage > 0) {
			if (_audioSource != null) {
				_audioSource.clip = _hurtSound[Random.Range(0, _hurtSound.Length)];
				_audioSource.Play();
			}

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
