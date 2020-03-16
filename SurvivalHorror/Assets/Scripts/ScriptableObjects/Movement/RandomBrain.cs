using UnityEngine;

[CreateAssetMenu(fileName = "RandomBrain", menuName = "ScriptableObjects/MovementBrain/Random Brain")]
public class RandomBrain : MovementBrain {
	[SerializeField] float _stopTime = 0;
	[SerializeField] float _moveTime = 0;
	float _timer = 0;
	bool _isMoving = false;
	Vector2 _movement;

	public override Vector2 GetMovement(Transform owner) {
		_timer += Time.deltaTime;

		if (_isMoving) {
			if (_timer >= _moveTime) {
				_isMoving = false;
				_timer = 0;

				_movement = Vector2.zero;
			}
		}
		else {
			if (_timer >= _stopTime) {
				_isMoving = true;
				_timer = 0;

				_movement.x = Random.Range(-1f, 1f);
				_movement.y = Random.Range(-1f, 1f);
			}
		}
		if (_moveTime == 0) {
			_movement = Vector2.zero;
		}

		return _movement.normalized;
	}
}