using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float rotationSpeed = 1;
	public Transform root;
	private float mouseX, mouseY;
	public float stomachOffset;
	public ConfigurableJoint hipJoint, stomachJoint;

	private void FixedUpdate()
	{
		this.CamControl();
	}

	private void CamControl()
	{
		Vector2 cameraDelta = this._inputActions.Player.MoveSecondary.ReadValue<Vector2>();

		this.mouseX += cameraDelta.x * this.rotationSpeed;
		this.mouseY -= cameraDelta.y * this.rotationSpeed;
		this.mouseY = Mathf.Clamp(this.mouseY, -35, 60);

		Quaternion rootRotation = Quaternion.Euler(this.mouseY, this.mouseX, 0);
		this.root.rotation = rootRotation;

		this.hipJoint.targetRotation = Quaternion.Euler(0, -this.mouseX, 0);
		this.stomachJoint.targetRotation = Quaternion.Euler(-this.mouseY + this.stomachOffset, 0, 0);
	}

	private MainControls _inputActions;

	private void Awake()
	{
		this._inputActions = new MainControls();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnEnable()
	{
		this._inputActions.Enable();
	}

	private void OnDisable()
	{
		this._inputActions.Disable();
	}
}
