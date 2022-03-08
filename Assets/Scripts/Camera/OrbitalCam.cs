using UnityEngine;
using System.Collections;

public class OrbitalCam : MonoBehaviour {

	public Camera cam;
	public bool userControl = true;
	public float speed = 1f;
	public float zoom = 1.5f;
    public float minZoom = 1f;
	public float maxZoom = 10f;

	public float pitch { get; private set; }
	public float yaw { get; private set; }

	// Use this for initialization
	void Awake () {
		//transform.localPosition = new Vector3 (0, 0, radius);
		cam.transform.localPosition = new Vector3 (0, 0, -zoom);
		pitch = 0f;
		yaw = 0f;
	}
	
	void Update () {
		GetZoomInput();
		if(userControl) GetPanInput();
	}

	void GetZoomInput() {
		float zoomDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom (zoomDelta);
		}
	}

	void GetPanInput() {
		float xDelta = Input.GetAxis ("Horizontal");
		float yDelta = Input.GetAxis ("Vertical");
		if (xDelta != 0f || yDelta != 0f) {
			AdjustPosition (xDelta, yDelta);
		}
	}

	void AdjustZoom (float delta) {
		zoom *= 1 - delta;
		zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
		cam.transform.localPosition = new Vector3 (0f, 0f, -zoom);
	}

	void AdjustPosition (float xDelta, float zDelta) {
		pitch += zDelta * speed;
		if (pitch > 90) pitch = 90;
		if (pitch < -90) pitch = -90;
		yaw -= xDelta * speed * (Mathf.Cos (pitch * Mathf.PI / 180) / 2 + 0.5f);

		SetRotations(pitch, yaw);
	}

	public void SetRotations(float pitch, float yaw) {
		this.pitch = pitch;
		this.yaw = yaw;
		transform.eulerAngles = new Vector3 (pitch, yaw, 0f);
	}
}
