using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastFirearm", menuName = "ScriptableObjects/Firearm/RaycastFirearm")]
public class RaycastFirearm : Firearm {
	public override void Fire(GameObject owner, Quaternion direction) {
		List<RaycastHit2D> target = new List<RaycastHit2D>();

		if (_nBullets > 1) {
			for (int i = 0; i < _nBullets; i++) {
				Quaternion scatteredDirection = direction * Quaternion.AngleAxis(Random.Range(-_bulletScatter, _bulletScatter), Vector3.forward);
				target.Add(Physics2D.Raycast(owner.transform.position, scatteredDirection * Vector3.up));
			}
		}
		else {
			target.Add(Physics2D.Raycast(owner.transform.position, direction * Vector3.up));
		}


		for (int i = 0; i < target.Count; i++) {
			if (target[i].collider != null) {
				Debug.DrawLine(owner.transform.position, target[i].point, Color.yellow, 1);
				Debug.DrawRay(owner.transform.position, direction * Quaternion.AngleAxis(_bulletScatter, Vector3.forward) * Vector3.up * 5, Color.blue, 1);
				Debug.DrawRay(owner.transform.position, direction * Quaternion.AngleAxis(-_bulletScatter, Vector3.forward) * Vector3.up * 5, Color.blue, 1);

				Health targetLife = target[i].collider.GetComponent<Health>();
				if (targetLife) {
					targetLife.DealDamage(_damage);
				}
			}
		}
	}
}
