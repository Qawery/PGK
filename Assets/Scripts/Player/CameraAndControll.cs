using UnityEngine;
using System.Collections;

public class CameraAndControll : MonoBehaviour 
{
	public Transform currentCamera;
	public Texture2D cursorTexture;
	private Rigidbody playerRigidBody;
	private Health health;
	
	private Vector3 movementDirection;
	private float playerMovementModifier = 400.0f;
	private Vector3 currentCameraPositionOffset = new Vector3(0.0f, 0.0f, 0.0f);

	public DirectProjectileWeapon weapon_1;

	void Start () 
	{
		if(currentCamera != null)
		{
			currentCameraPositionOffset = currentCamera.transform.position - transform.position;
		}
		playerRigidBody = GetComponent<Rigidbody>();
		health = GetComponent<Health>();
		DrawCursor ();
	}
	
	void Update () 
	{
		if(health == null || health.IsAlive())
		{
			PlayerMovement ();
			PlayerCommands();
		}
		if(health != null && !health.IsAlive())
		{
			Destroy(gameObject);
		}
		if(currentCamera != null) 
		{
			CameraMovement();
		}
		DrawCursor ();
	}
	
	private void PlayerMovement()
	{
		if(currentCamera != null) 
		{
			movementDirection = Vector3.zero;
			movementDirection += Input.GetAxis ("Vertical") * (Quaternion.Euler (0, -90, 0) * currentCamera.transform.right);
			movementDirection += Input.GetAxis ("Horizontal") * currentCamera.transform.right;
			movementDirection = movementDirection.normalized;

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
			movementDirection = movementDirection.normalized;

			if(playerRigidBody != null) 
			{
				playerRigidBody.velocity = movementDirection * playerMovementModifier * Time.deltaTime;
			} 
			else 
			{
				//TODO Movement without RigidBody and Camera
			}
		}

		//Patrzenie gdzie idziemy
		//transform.LookAt (transform.position + movementDirection);
	}
	
	private void CameraMovement()
	{
		currentCamera.transform.position = transform.position + currentCameraPositionOffset;
	}

	private void PlayerCommands()
	{
		//zczytanie gdzie jest kursor myszy
		Vector3 observationPoint = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		observationPoint.y = 0.5f;

		//Strzał
		if(Input.GetButtonDown("Fire1"))
		{
			if(weapon_1 != null)
			{
				weapon_1.aim(observationPoint);
				weapon_1.fire();
			}
		}
		

		if(Input.GetButtonDown("Fire2"))
		{

		}
		

		if(Input.GetButtonDown("Fire3"))
		{

		}
	}

	private void DrawCursor()
	{
		/*TODO: poprawić wyświetlanie
		if(cursorTexture != null)
		{
			CursorMode cursorMode = CursorMode.Auto;
			Vector2 vector = new Vector2 (cursorTexture.width/2, cursorTexture.height/2);
			Cursor.SetCursor (cursorTexture, vector, cursorMode);
		}
		*/
	}

	public float GetPLayerMovementModifier()
	{
		return playerMovementModifier;
	}

	public void SetPlayerMovementModifier(float newPlayerMovementModifier)
	{
		playerMovementModifier = newPlayerMovementModifier;
	}
}
