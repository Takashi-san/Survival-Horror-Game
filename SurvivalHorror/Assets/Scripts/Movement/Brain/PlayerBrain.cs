using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBrain", menuName = "ScriptableObjects/MovementBrain/Player Brain")]
public class PlayerBrain : MovementBrain {
	[SerializeField] [Range(0, 1)] float _walkModifier = 1;
	bool _isAiming = false;

	public override void Setup(GameObject owner) {
		owner.GetComponentInChildren<Shooter>().isAiming += IsAiming;
	}

	public override Vector2 GetMovement(Transform owner) {
		// Walk command.
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || _isAiming) {
			_speedModifier = _walkModifier;
		}
		else {
			_speedModifier = 1;
		}

		Vector2 movement = Vector2.zero;
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");
		return movement.normalized;
	}

	void IsAiming(bool isAiming) {
		_isAiming = isAiming;
	}
}