using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
	[SerializeField] [Range(0, 360)] float _fov = 0;
	[SerializeField] [Min(1)] int _rayCount = 1;
	[SerializeField] [Min(0)] float _viewDistance = 0;
	[SerializeField] LayerMask _ignore = new LayerMask();
	Mesh _mesh;

	void Start() {
		_mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = _mesh;
	}

	void Update() {
		Vector3 origin = transform.position;
		Quaternion direction = transform.rotation;
		Quaternion deltaAngle = Quaternion.AngleAxis(_fov / 2, Vector3.forward);

		Vector3[] vertices = new Vector3[_rayCount + 2];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[_rayCount * 3];
		vertices[0] = Vector3.zero;

		for (int i = 0; i < _rayCount + 1; i++) {
			RaycastHit2D hit = Physics2D.Raycast(origin, direction * deltaAngle * Vector3.up, _viewDistance, ~_ignore);
			if (hit.collider == null) {
				vertices[i + 1] = deltaAngle * Vector3.up * _viewDistance;
			}
			else {
				vertices[i + 1] = deltaAngle * Vector3.up * Vector2.Distance(origin, hit.point);
			}

			if (i > 0) {
				triangles[(i - 1) * 3] = 0;
				triangles[(i - 1) * 3 + 1] = i;
				triangles[(i - 1) * 3 + 2] = i + 1;
			}

			deltaAngle *= Quaternion.AngleAxis(-_fov / _rayCount, Vector3.forward);
		}

		_mesh.vertices = vertices;
		_mesh.uv = uv;
		_mesh.triangles = triangles;
		_mesh.bounds = new Bounds(origin, Vector3.one * _viewDistance * 2);
	}
}
