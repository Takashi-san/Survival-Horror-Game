using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
	public string interactionText;

	virtual public void Interact() {
		Debug.Log("Interacted");
	}
}
