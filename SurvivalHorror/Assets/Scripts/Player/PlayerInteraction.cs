using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour {
	[SerializeField] TextMeshProUGUI _interactionText = null;
	List<InteractableObject> _interactables = new List<InteractableObject>();
	InteractableObject _closestInteractable = null;


	public void Interact() {
		if (_closestInteractable != null) {
			_closestInteractable.Interact();
		}
	}

	void Update() {
		float __minDistance = float.MaxValue;
		_closestInteractable = null;

		foreach (var __interactable in _interactables) {
			float __distance = (transform.position - __interactable.transform.position).magnitude;
			if (__distance < __minDistance) {
				_closestInteractable = __interactable;
			}
		}

		if (_closestInteractable != null) {
			_interactionText.text = _closestInteractable.interactionText;
			_interactionText.gameObject.SetActive(true);
		}
		else {
			_interactionText.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		InteractableObject __interactable = other.GetComponent<InteractableObject>();
		if (__interactable != null) {
			if (!_interactables.Contains(__interactable)) {
				_interactables.Add(__interactable);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		InteractableObject __interactable = other.GetComponent<InteractableObject>();
		if (__interactable != null) {
			if (_interactables.Contains(__interactable)) {
				_interactables.Remove(__interactable);
			}
		}
	}
}
