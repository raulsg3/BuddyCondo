using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delroda
{
	public class MyGizmo : MonoBehaviour {

		public enum GizmoType { Sphere, WireSphere, Cube, WireCube, LineToDirection, LineToDestination, Icon}
		[Header("Gizmo")]
		public GizmoType type;
		public Vector3 centerOffset;
		public Vector3 scale = Vector3.one;
		public Color color = Color.green;
		public Texture texture;
		public string assets_gizmos_iconName; 

		[Header("Axis")]
		public bool showAxis;
		public float axisLenght;
			
		public void OnDrawGizmos() {
			Gizmos.color = color;
			if (type == GizmoType.Sphere) Gizmos.DrawSphere(transform.position + centerOffset, scale.x);
			if (type == GizmoType.WireSphere) Gizmos.DrawWireSphere(transform.position + centerOffset, scale.x);
			if (type == GizmoType.Cube) Gizmos.DrawCube(transform.position + centerOffset, scale);
			if (type == GizmoType.WireCube) Gizmos.DrawWireCube(transform.position + centerOffset, scale);
			if (type == GizmoType.LineToDirection) Gizmos.DrawRay(transform.position + centerOffset, transform.right * scale.x + transform.up * scale.y + transform.forward * scale.z);
			if (type == GizmoType.LineToDestination) Gizmos.DrawLine(transform.position + centerOffset, transform.right * scale.x + transform.up * scale.y + transform.forward * scale.z);
			if (type == GizmoType.Icon) Gizmos.DrawIcon(transform.position, assets_gizmos_iconName, true); //Icon placed in the Assets/Gizmos folder.
			
			if (showAxis) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.right * axisLenght));
				Gizmos.color = Color.green;
				Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.up * axisLenght));
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.forward * axisLenght));
			}
		}
	}
}
