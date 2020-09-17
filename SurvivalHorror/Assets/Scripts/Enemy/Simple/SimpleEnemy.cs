using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {
	[SerializeField] [Min(0)] float _moveSpeed = 0;
	[SerializeField] [Min(0)] float _lostPlayerTime = 0;
	[SerializeField] EnemyFieldOfView _fieldOfView = null;
	Rigidbody2D _rb2d = null;
	Vector2 _moveDirection = Vector2.zero;
	Vector3 _playerLastSawPosition;
	float _playerLastSawTimer;

	void Awake() {
		_fieldOfView.sawPlayer += PlayerOnLineOfSight;
		_rb2d = GetComponent<Rigidbody2D>();
		_playerLastSawTimer = _lostPlayerTime;
	}

	void Update() {
		_moveDirection = Vector2.zero;

		// Avaliate if lost player out of sight.
		if (_playerLastSawTimer < _lostPlayerTime) {
			_playerLastSawTimer += Time.deltaTime;
			_moveDirection = _playerLastSawPosition - transform.position;
			transform.LookAt(transform.position + Vector3.forward, _moveDirection);
		}
	}

	void FixedUpdate() {
		// Make move.
		if (_moveDirection != Vector2.zero) {
			Vector2 target = (Vector2)transform.position + (_moveDirection * _moveSpeed * Time.fixedDeltaTime);
			_rb2d.MovePosition(target);
		}
	}

	void PlayerOnLineOfSight(Vector3 p_sawPosition) {
		_playerLastSawPosition = p_sawPosition;
		_playerLastSawTimer = 0;
	}
}
