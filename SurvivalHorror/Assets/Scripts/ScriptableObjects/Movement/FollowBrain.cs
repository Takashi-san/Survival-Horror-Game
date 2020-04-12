using UnityEngine;

[CreateAssetMenu(fileName = "FollowBrain", menuName = "ScriptableObjects/MovementBrain/Follow Brain")]
public class FollowBrain : MovementBrain {
	[SerializeField] string _tag = "";
	GameObject _target = null;

	public override Vector2 GetMovement(Transform owner) {
		if (!_target) {
			if (_tag != "") {
				_target = GameObject.FindWithTag(_tag);
				if (!_target) {
					return Vector2.zero;
				}
			}
			else {
				return Vector2.zero;
			}
		}

		Vector2 delta = _target.transform.position - owner.position;
		owner.LookAt(owner.position + Vector3.forward, (Vector3)delta + _target.transform.position);
		return delta.normalized;
	}
}
