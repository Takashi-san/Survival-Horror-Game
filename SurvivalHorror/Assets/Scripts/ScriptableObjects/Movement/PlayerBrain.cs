using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBrain", menuName = "ScriptableObjects/MovementBrain/Player Brain")]
public class PlayerBrain : MovementBrain {
	public override Vector2 GetMovement(Transform owner) {
		Vector2 movement = Vector2.zero;
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");
		return movement.normalized;
	}
}