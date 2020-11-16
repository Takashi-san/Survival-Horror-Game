using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : InteractableObject {
	[SerializeField] bool _needKey = false;
	[SerializeField] ItemType _key = null;
	[SerializeField] HingeJoint2D _joint = null;
	JointAngleLimits2D _openLimit;
	JointAngleLimits2D _closedLimit;
	bool _isOpen;

	void Awake() {
		_openLimit = new JointAngleLimits2D { min = _joint.limits.min, max = _joint.limits.max };
		_closedLimit = new JointAngleLimits2D { min = 0f, max = 0f };
		CloseDoor();
	}

	public override void Interact() {
		if (_needKey) {
			if (!InventorySystem.instance.HasItem(_key)) {
				return;
			}
		}
		if (_isOpen) {
			CloseDoor();
		}
		else {
			OpenDoor();
		}
	}

	void OpenDoor() {
		interactionText = "Close Door";
		_joint.limits = _openLimit;
		_isOpen = true;
	}

	void CloseDoor() {
		interactionText = "Open Door";
		_joint.limits = _closedLimit;
		_isOpen = false;
	}
}
