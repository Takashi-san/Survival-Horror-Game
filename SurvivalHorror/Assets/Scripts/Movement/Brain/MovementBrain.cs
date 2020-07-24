using UnityEngine;

public abstract class MovementBrain : ScriptableObject {
	[SerializeField] protected float _speed = 0;
	protected float _speedModifier = 1;

	public float Speed => _speed * _speedModifier;

	// Returns a normalized Vector2 that indicates the direction that the object should move.
	// The function assumes that it is called once in the Update() function.
	public abstract Vector2 GetMovement(Transform owner);
	public virtual void Setup(GameObject owner) { }
}