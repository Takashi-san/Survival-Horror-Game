using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFirearm", menuName = "ScriptableObjects/Firearm/ProjectileFirearm")]
public class ProjectileFirearm : Firearm {
	[SerializeField] GameObject _projectile = null;
	public override void Fire(GameObject owner, Quaternion direction) {
		if (_nBullets > 1) {
			for (int i = 0; i < _nBullets; i++) {
				Quaternion scatteredDirection = direction * Quaternion.AngleAxis(Random.Range(-_bulletScatter, _bulletScatter), Vector3.forward);
				Instantiate(_projectile, owner.transform.position, scatteredDirection).GetComponent<Projectile>().SetDamage(_damage);

				Debug.DrawRay(owner.transform.position, direction * Quaternion.AngleAxis(_bulletScatter, Vector3.forward) * Vector3.up * 5, Color.blue, 1);
				Debug.DrawRay(owner.transform.position, direction * Quaternion.AngleAxis(-_bulletScatter, Vector3.forward) * Vector3.up * 5, Color.blue, 1);
			}
		}
		else {
			Instantiate(_projectile, owner.transform.position, direction).GetComponent<Projectile>().SetDamage(_damage);
		}
	}
}
