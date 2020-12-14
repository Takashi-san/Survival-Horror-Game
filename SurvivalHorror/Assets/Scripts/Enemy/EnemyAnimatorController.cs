using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour {
	[SerializeField] Animator _animator = null;
	[SerializeField] NormalEnemy _enemy = null;
	[SerializeField] Rigidbody2D _rb = null;

	void Update() {
		if (_rb.velocity == Vector2.zero) {
			_animator.SetBool("Moving", false);
		}
		else {
			_animator.SetBool("Moving", true);
		}
	}

	public void SetAttack() {
		_animator.SetTrigger("Attack");
	}

	public void SetHurt() {
		_animator.SetTrigger("Hurt");
	}

	public void DoAttack() {
		_enemy.DoAttack();
	}
}
