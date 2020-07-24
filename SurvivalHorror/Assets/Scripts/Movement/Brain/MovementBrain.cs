using UnityEngine;

public abstract class MovementBrain : ScriptableObject {
	// Returns a normalized Vector2 that indicates the direction that the object should move.
	// The function assumes that it is called once in the Update() function.
	public abstract Vector2 GetMovement(Transform owner);
}