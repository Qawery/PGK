using UnityEngine;
using System.Collections;

public class CameraAndControll : MonoBehaviour 
{
	public Transform currentCamera;
	private Rigidbody playerRigidBody;
	private Health health;
	
	private Vector3 movementDirection;
	private float playerMovementModifier = 750.0f;

	private float cameraTurningModifier = 100.0f;
	private const float CAMERA_TURNING_LIMIT = 10.0f;
	private const float MIN_CAMERA_OFFSET = -60.0f;	//Looking up
	private const float MAX_CAMERA_OFFSET = 30.0f;	//Looking down
	private float currentCameraRotationOffset = 0.0f;
	private Vector3 currentCameraPositionOffset = new Vector3(0.0f, 0.0f, 0.0f);
	
	void Start () 
	{
		Cursor.visible = false;
		if(currentCamera != null)
		{
			currentCameraPositionOffset = currentCamera.transform.position - transform.position;
		}
		
		playerRigidBody = GetComponent<Rigidbody>();
		health = GetComponent<Health>();
	}
	
	void Update () 
	{
		if(health == null || health.IsAlive())
		{
			PlayerMovement ();
			if(currentCamera != null) 
			{
				CameraMovement();
				CameraLooking();
			}
		}
		else
		{
			if(currentCamera != null) 
			{
				CameraMovement();
				CameraLooking();
			}
		}
	}
	
	private void PlayerMovement()
	{
		if(currentCamera != null) 
		{
			movementDirection = Vector3.zero;
			movementDirection += Input.GetAxis ("Vertical") * (Quaternion.Euler (0, -90, 0) * currentCamera.transform.right);
			movementDirection += Input.GetAxis ("Horizontal") * currentCamera.transform.right;
			
			if(playerRigidBody != null) 
			{
				playerRigidBody.velocity = movementDirection * playerMovementModifier * Time.deltaTime;
			} 
			else 
			{
				//TODO Movement without RigidBody
			}
		} 
		else 
		{
			movementDirection = Vector3.zero;
			movementDirection += Input.GetAxis ("Vertical") * Vector3.forward;
			movementDirection += Input.GetAxis ("Horizontal") * Vector3.right;
			
			if(playerRigidBody != null) 
			{
				playerRigidBody.velocity = movementDirection * playerMovementModifier * Time.deltaTime;
			} 
			else 
			{
				//TODO Movement without RigidBody and Camera
			}
		}
	}
	
	
	private void CameraMovement()
	{
		currentCamera.transform.position = transform.position + currentCameraPositionOffset;
	}
	
	private void PlayerLooking(float horizontalRotation)
	{
		if(currentCamera != null) 
		{
			transform.RotateAround(transform.position, Vector3.up, horizontalRotation);
		}
	}
	
	private void CameraLooking()
	{
		float horizontalRotation = cameraTurningModifier * Input.GetAxis ("Mouse X");
		horizontalRotation *= Time.deltaTime;
		if(horizontalRotation > CAMERA_TURNING_LIMIT)
		{
			horizontalRotation = CAMERA_TURNING_LIMIT;
		} 
		else if(horizontalRotation < -CAMERA_TURNING_LIMIT)
		{
			horizontalRotation = -CAMERA_TURNING_LIMIT;
		}
		
		float verticalRotation = cameraTurningModifier * Input.GetAxis ("Mouse Y");
		verticalRotation *= Time.deltaTime;
		if(verticalRotation > cameraTurningModifier)
		{
			verticalRotation = cameraTurningModifier;
		} 
		else if(verticalRotation < -cameraTurningModifier)
		{
			verticalRotation = -cameraTurningModifier;
		}
		
		if(currentCameraRotationOffset + verticalRotation > MAX_CAMERA_OFFSET)
		{
			verticalRotation = MAX_CAMERA_OFFSET - currentCameraRotationOffset;
		}
		else if(currentCameraRotationOffset + verticalRotation < MIN_CAMERA_OFFSET)
		{
			verticalRotation = MIN_CAMERA_OFFSET - currentCameraRotationOffset;
		}
		currentCameraRotationOffset += verticalRotation;
		
		currentCamera.transform.RotateAround(transform.position, Vector3.up, horizontalRotation);
		PlayerLooking(horizontalRotation);
		currentCamera.transform.RotateAround(transform.position, currentCamera.transform.right, verticalRotation);
		currentCameraPositionOffset = currentCamera.transform.position - transform.position;
	}
}
