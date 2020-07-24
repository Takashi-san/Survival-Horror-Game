using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField] float _speed = 0;
	[SerializeField] float _timeToDestroy = 1;
	Rigidbody2D _rb = null;
	float _timer = 0;
	int _damage = 0;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_rb.AddRelativeForce(Vector2.up * _speed, ForceMode2D.Impulse);
	}

	void Update() {
		_timer += Time.deltaTime;
		if (_timer > _timeToDestroy) {
			Destroy(gameObject);
		}
	}

	public void SetDamage(int damage) {
		_damage = damage;
	}

	void OnHit() {
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.GetComponent<Projectile>() != null)
			return;

		Health targetLife = other.gameObject.GetComponent<Health>();
		if (targetLife) {
			targetLife.DealDamage(_damage);
		}
		OnHit();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponent<Projectile>() != null)
			return;

		Health targetLife = other.gameObject.GetComponent<Health>();
		if (targetLife) {
			targetLife.DealDamage(_damage);
		}
		OnHit();
	}
}
