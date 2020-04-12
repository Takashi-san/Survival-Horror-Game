using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFirearm", menuName = "ScriptableObjects/Firearm/ProjectileFirearm")]
public class ProjectileFirearm : Firearm {
	[SerializeField] GameObject _projectile = null;
	public override void Fire(GameObject owner, Quaternion direction) {
		Instantiate(_projectile, owner.transform.position, direction).GetComponent<Projectile>().SetDamage(_damage);
	}
}
