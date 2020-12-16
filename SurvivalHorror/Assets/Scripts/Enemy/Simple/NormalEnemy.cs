using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfind2D;

public class NormalEnemy : MonoBehaviour {

	[Header("Debug")]
	[SerializeField] bool _debugMode = false;
	[SerializeField] TextMeshProUGUI _stateText = null;

	[Header("Reference")]
	[SerializeField] [Min(0)] float _closeEnoughDistance = 0.3f;
	[SerializeField] LayerMask _raycastMask = new LayerMask();
	[SerializeField] Transform _lookAtDirection = null;
	[SerializeField] GameObject _deadPrefab = null;
	[SerializeField] EnemyArea _attackArea = null;
	[SerializeField] EnemyAnimatorController _animator = null;
	[SerializeField] AudioSource _constantAudioSource = null;
	[SerializeField] AudioSource _sfxAudioSource = null;
	[SerializeField] AudioClip _normalSound = null;
	[SerializeField] AudioClip _chaseSound = null;
	[SerializeField] AudioClip _seePlayerSound = null;

	[Header("Attack")]
	[SerializeField] GameObject _attackPrefab = null;
	[SerializeField] Transform _attackSpawnPoint = null;
	[SerializeField] [Min(0)] float _attackDelay = 0f;
	[SerializeField] [Min(0)] float _attackCooldown = 0f;

	[Header("Seek Player")]
	[SerializeField] [Min(0)] float _loseSightTime = 0;
	[SerializeField] [Min(0)] float _seekVelocity = 0;

	[Header("Patrol")]
	[SerializeField] bool _doPatrol = false;
	[SerializeField] [Min(0)] float _patrolVelocity = 0;
	[SerializeField] Transform _patrolPositionA = null;
	[SerializeField] Transform _patrolPositionB = null;

	StackFSM _brain;
	SteeringManagerRB2D _steeringManager;
	Rigidbody2D _rb2d;

	Vector3[] _path;
	int _pathTargetIndex;
	bool _pathRequested = false;
	float _pathRefreshTimer = 0;
	float _pathTimeToRefresh = 0.5f;

	bool _seekingPlayer = true;
	float _loseSightTimer = 0;
	bool _inAttack = false;

	bool _playerInAttackArea = false;
	float _attackTimer = 0;
	float _attackCooldownTimer = 0;

	bool _patrolA = true;
	bool _ignoreOnce = false;

	Vector3 _playerLastSawPosition;

	void Awake() {
		_brain = new StackFSM();
		_steeringManager = GetComponent<SteeringManagerRB2D>();
		_rb2d = GetComponent<Rigidbody2D>();
		GetComponent<Health>().healthUpdate += HealthUpdate;
		_attackArea.playerInArea += PlayerInAttackArea;
		GetComponentInChildren<EnemyFieldOfView>().sawPlayer += SawPlayer;
		if (!_debugMode) _stateText.text = "";

		if (_doPatrol) {
			_brain.PushState(StatePatrol);
			return;
		}
		_brain.PushState(StateIdle);

		_constantAudioSource.clip = _normalSound;
		_constantAudioSource.Play();
	}

	void FixedUpdate() {
		if (_seekingPlayer) {
			_loseSightTimer += Time.fixedDeltaTime;
			if (_loseSightTimer > _loseSightTime) {
				_seekingPlayer = false;

				_constantAudioSource.clip = _normalSound;
				_constantAudioSource.Play();

				_brain.PopState();
				_path = null;
				_pathRequested = false;
				_pathRefreshTimer = 0;
				if (_doPatrol) {
					_brain.PushState(StatePatrol);
				}
				else {
					_brain.PushState(StateIdle);
				}
			}
		}

		if (_playerInAttackArea) {
			_attackCooldownTimer += Time.fixedDeltaTime;
			if (_attackCooldownTimer >= _attackCooldown) {
				_attackCooldownTimer = 0;
				PlayerInAttackArea(true);
			}
		}

		_brain.UpdateState();
		_steeringManager.MovementUpdate();
	}

	#region States

	void StateIdle() {
		if (_debugMode) _stateText.text = "idle";

		_steeringManager.SeekDirection(Vector2.zero);
	}

	void StatePatrol() {
		if (_debugMode) _stateText.text = "patrol";
		_steeringManager.SetMaxVelocity(_patrolVelocity);

		if (_path == null) {
			if (!_pathRequested) {
				if (_patrolA) {
					PathRequestManager.instance.RequestPath(transform.position, _patrolPositionA.position, OnPathFound);
				}
				else {
					PathRequestManager.instance.RequestPath(transform.position, _patrolPositionB.position, OnPathFound);
				}
				_pathRequested = true;
			}
		}
		else {
			if (_pathTargetIndex < _path.Length) {
				_steeringManager.SeekPosition(_path[_pathTargetIndex]);

				float __diff = (transform.position - _path[_pathTargetIndex]).magnitude;
				if (__diff < _closeEnoughDistance) {
					_pathTargetIndex++;
				}
			}
			else {
				_path = null;
				_patrolA = !_patrolA;
			}
		}

		_lookAtDirection.LookAt(transform.position + Vector3.forward, _rb2d.velocity.normalized);
	}

	void StatePathPlayer() {
		if (_debugMode) _stateText.text = "path player";
		_pathRefreshTimer += Time.fixedDeltaTime;
		_steeringManager.SetMaxVelocity(_seekVelocity);

		if (Player.instance == null) return;

		if (_path == null && !_pathRequested || _pathRefreshTimer > _pathTimeToRefresh) {
			PathRequestManager.instance.RequestPath(transform.position, Player.instance.transform.position, OnPathFound);
			_pathRefreshTimer = 0;
			_pathRequested = true;
		}
		else {
			if (_path != null) {
				if (_pathTargetIndex < _path.Length) {
					_steeringManager.SeekPosition(_path[_pathTargetIndex]);

					float __diff = (transform.position - _path[_pathTargetIndex]).magnitude;
					if (__diff < _closeEnoughDistance) {
						_pathTargetIndex++;
					}
				}
				else {
					_path = null;
				}
			}
		}

		_lookAtDirection.LookAt(transform.position + Vector3.forward, _rb2d.velocity.normalized);

		Vector3 __direction = (Player.instance.transform.position - transform.position).normalized;
		RaycastHit2D __ray = Physics2D.Raycast(transform.position, __direction, float.MaxValue, _raycastMask);
		Debug.DrawLine(transform.position, __ray.point, Color.red);
		if (__ray.collider != null) {
			if (__ray.transform.GetComponent<Player>() != null) {
				//if (__ray.transform.tag == "Player") {
				_brain.PopState();
				_brain.PushState(StateSeekPlayer);
				_path = null;
				_pathRequested = false;
				_pathRefreshTimer = 0;
			}
		}
	}

	void StateSeekPlayer() {
		if (_debugMode) _stateText.text = "seek player";
		_steeringManager.SetMaxVelocity(_seekVelocity);

		Vector3 __direction = Vector3.zero;
		if (Player.instance != null) {
			__direction = (Player.instance.transform.position - transform.position).normalized;
		}
		_steeringManager.SeekDirection(__direction);

		_lookAtDirection.LookAt(transform.position + Vector3.forward, __direction);

		RaycastHit2D __ray = Physics2D.Raycast(transform.position, __direction, float.MaxValue, _raycastMask);
		Debug.DrawLine(transform.position, __ray.point, Color.red);
		if (__ray.collider != null) {
			if (__ray.transform.GetComponent<Player>() != null) {
				return;
			}
		}

		_brain.PopState();
		_brain.PushState(StatePathPlayer);
	}

	void StateAttackPlayer() {
		if (_debugMode) _stateText.text = "attack player";
		_inAttack = true;

		_steeringManager.SeekDirection(Vector2.zero);
		Vector3 __direction = (Player.instance.transform.position - transform.position).normalized;
		_lookAtDirection.LookAt(transform.position + Vector3.forward, __direction);

		_attackTimer += Time.fixedDeltaTime;
		if (_attackTimer > _attackDelay) {
			//attack
			Debug.Log("Enemy attacked!");
			_animator.SetAttack();

			_inAttack = false;
			_attackTimer = 0;
			_brain.PopState();
		}
	}

	#endregion

	public void SawPlayer(Vector3 p_position) {
		_playerLastSawPosition = p_position;

		if (!_seekingPlayer) {
			_sfxAudioSource.clip = _seePlayerSound;
			_sfxAudioSource.Play();

			_constantAudioSource.clip = _chaseSound;
			_constantAudioSource.Play();
		}

		_seekingPlayer = true;
		_loseSightTimer = 0;
		if (!_inAttack) {
			_brain.PopState();
			_brain.PushState(StateSeekPlayer);
		}
	}

	public void OnPathFound(Vector3[] p_path, bool p_success) {
		if (p_success) {
			_path = p_path;
			_pathTargetIndex = 0;
		}
		else {
			_path = null;
		}
		_pathRequested = false;
	}

	void OnDrawGizmosSelected() {
		if (_path != null && _debugMode) {
			for (int i = _pathTargetIndex; i < _path.Length; i++) {
				Gizmos.color = Color.blue;
				Gizmos.DrawCube(_path[i], Vector3.one * 0.5f);

				if (i == _pathTargetIndex) {
					Gizmos.DrawLine(transform.position, _path[i]);
				}
				else {
					Gizmos.DrawLine(_path[i - 1], _path[i]);
				}
			}
		}
	}

	void PlayerInAttackArea(bool p_isInArea) {
		Debug.Log("attack comand");
		_inAttack = true;
		_playerInAttackArea = p_isInArea;

		if (p_isInArea) {
			_brain.PushState(StateAttackPlayer);
		}
		else {
			_attackCooldownTimer = 0;
		}
	}

	public void DoAttack() {
		Instantiate(_attackPrefab, _attackSpawnPoint);
	}

	void HealthUpdate(int p_health) {
		_animator.SetHurt();
		if (p_health == 0) {
			Debug.Log("Normal enemy died!");
			GetComponent<Health>().healthUpdate -= HealthUpdate;
			if (_deadPrefab != null) {
				Instantiate(_deadPrefab, transform.position, _lookAtDirection.rotation);
			}
			Destroy(gameObject);
		}
		else {
			if (_ignoreOnce) {
				SawPlayer(transform.position);
			}
			_ignoreOnce = true;
		}
	}
}
