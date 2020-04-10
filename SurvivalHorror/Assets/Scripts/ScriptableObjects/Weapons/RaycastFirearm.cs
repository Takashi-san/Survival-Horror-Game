using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastFirearm", menuName = "ScriptableObjects/Firearm/RaycastFirearm")]
public class RaycastFirearm : Firearm {
	public override void Fire(GameObject owner, Quaternion direction) {
		RaycastHit2D target = Physics2D.Raycast(owner.transform.position, direction * Vector3.up);

		if (target.collider != null) {
			Debug.Log("Hit!");
			Debug.DrawLine(owner.transform.position, target.point, Color.yellow, 1);
		}
	}
}
