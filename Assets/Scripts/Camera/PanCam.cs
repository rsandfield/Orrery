using UnityEngine;
using System.Collections;

public class PanCam : MonoBehaviour {
	Camera cam;

	float zoom = 1f;
	public float minZoomAngle = 90;
	public float maxZoomAngle = 45;
	public float maxZoomDistance = 5;
	public float minZoomDistance = 50;
	public float minZoomMovementSpeed = 400;
	public float maxZoomMovementSpeed = 100;
	public Vector2 minPosition = new Vector2(0, 0);
	public Vector2 maxPosition = new Vector2(1, 1);
	float yaw;
	float pitch;
	public float rotationSpeed = 180;


	void Awake () {
		cam = transform.GetComponentInChildren<Camera>();
		Reset();
	}
	
	void Update () {
		float zoomDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom (zoomDelta);
		}

		float xDelta = Input.GetAxis ("Horizontal");
		float zDelta = Input.GetAxis ("Vertical");
		if (xDelta != 0f || zDelta != 0f) {
			AdjustPosition (xDelta, zDelta);
		}

		float rotationDelta = Input.GetAxis ("Rotation");
		if (rotationDelta != 0f) {
			AdjustRotation (rotationDelta);
		}
	}

	public void Reset() {
		AdjustZoom (0f);
		AdjustPosition (0f, 0f);
		AdjustRotation (0f);
	}

	void AdjustZoom (float delta) {
		zoom = Mathf.Clamp01 (zoom + delta);

		float distance = Mathf.Lerp (minZoomDistance, maxZoomDistance, zoom);
		cam.transform.localPosition = new Vector3 (0f, 0f, -distance);

		float pitch = Mathf.Lerp (minZoomAngle, maxZoomAngle, zoom);
		SetRotations(pitch, this.yaw);
	}

	void AdjustPosition (float xDelta, float zDelta) {
		Vector2 direction = new Vector2(xDelta, zDelta).normalized;
		direction = direction.Rotate(-transform.rotation.GetPitchYawRollDeg().z);

		float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
		float distance = Mathf.Lerp(minZoomMovementSpeed, maxZoomMovementSpeed, zoom) * damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += new Vector3(direction.x, 0, direction.y) * distance;
		transform.localPosition = ClampPosition(position);
	}

	// https://answers.unity.com/questions/756467/getting-object-rotation-on-just-1-axis.html


	Vector3 ClampPosition (Vector3 position) {
		if(position.x < minPosition.x) position.x = minPosition.x;
		if(position.x > maxPosition.x) position.x = maxPosition.x;
		if(position.z < minPosition.y) position.z = minPosition.y;
		if(position.z > maxPosition.y) position.z = maxPosition.y;

		return position;
	}

	void AdjustRotation (float delta) {
		yaw += delta * rotationSpeed * Time.deltaTime;

		if (yaw < 0f) {
			yaw += 360f;
		} else if (yaw > 360f) {
			yaw -= 360f;
		}

		SetRotations(this.pitch, yaw);
	}

	public void SetRotations(float pitch, float yaw) {
		this.pitch = pitch;
		this.yaw = yaw;
		transform.eulerAngles = new Vector3 (pitch, yaw, 0f);
	}
}
