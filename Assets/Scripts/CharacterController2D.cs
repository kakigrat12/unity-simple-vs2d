using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CharacterController2D : MonoBehaviour //, IPunObservable
{
	[SerializeField] PhotonView photonView;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, 1)] [SerializeField] private float m_DecelerationSpeed = .5f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, 3)] [SerializeField] private float m_RunSpeed = 1.5f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[Range(0, 1f)] [SerializeField] private float timeToCanJump = 0.1f;
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private CapsuleCollider2D m_CrouchCollider;                // A collider that will be disabled when crouching
	[SerializeField] private Collider2D m_CrouchColliderForHit;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform[] crouchChangeablePoints;

	[SerializeField] private Vector2 differenceCrouchCollider;

	const float k_GroundedRadius = .07f; // Radius of the overlap circle to determine if grounded
										 //private bool m_Grounded;            // Whether or not the player is grounded.
	[SerializeField] private bool canJampInAir;
	[SerializeField] private bool afterJump;
	[SerializeField] private Stairs stairs;
	const float k_CeilingRadius = .00003f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private Camera camera;
	private bool isActive = true;

	private List<Collider2D> selected = new List<Collider2D>();

	//public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	[Header("Events")]
	[Space]

	public BoolEvent OnCrouchEvent;
	private bool _crouch;
	private bool _run;
	private bool m_wasCrouching = false;

	//private PhotonView photonView;

	private void Start()
	{
		camera = Camera.main;

		GameEvents.current.onIsAnyPanelOpened += ChangeIsActive;

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		//photonView = GetComponentInParent<PhotonView>();

		//if (OnLandEvent == null)
		//	OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();


		//GameEvents.current.onFlip += Flip;
		//PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
	}

	private void OnDisable()
	{
		GameEvents.current.onIsAnyPanelOpened -= ChangeIsActive;

		StartCoroutine(Brake());
	}

	private void ChangeIsActive(bool newValue)
	{
		isActive = !newValue;
	}

	//private bool isGrounded()
	//   {
	//	bool wasGrounded = false;

	//	Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
	//	for (int i = 0; i < colliders.Length; i++)
	//	{
	//		if (!wasGrounded)
	//		{
	//			if (colliders[i].gameObject != gameObject)
	//			{
	//				Debug.Log("trueGGG");
	//				OnLandEvent.Invoke();
	//				animator.SetBool("IsJumping", false);
	//				return true;
	//			}
	//		}
	//	}

	//	Debug.Log("falseGGG");
	//	Invoke(nameof(notIsGrounded), timeToCanJump);
	//	return true;
	//}
	private void OnCollisionEnter2D(Collision2D collision)
    {
		GroundCheack();
	}

    private void OnCollisionExit2D(Collision2D collision)
	{
		selected.Remove(collision.collider);

		if (selected.Count == 0 && !afterJump)
        {
			canJampInAir = true;
			afterJump = false;

			Invoke(nameof(CanNotJump), timeToCanJump);
		}
		//Debug.Log("OnCollisionExit2D");
		//      if (GroundCheack())
		//{
		//	canJampInAir = true;
		//	Invoke(nameof(CanNotJump), timeToCanJump);
		//}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (m_CrouchCollider.Distance(collision).distance <= 0.1f)
		{
			var _stairs = collision.GetComponent<Stairs>();
			if (_stairs != null)
			{
				stairs = _stairs;
			}
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (m_CrouchCollider.Distance(collision).distance <= 0.1f)
		{
			var _stairs = collision.GetComponent<Stairs>();
			if (_stairs != null)
			{
				stairs = null;
			}
		}
	}

    //   private void OnTriggerExit2D(Collider2D collision)
    //   {
    //	Debug.Log("OnTriggerExit2D Player");
    //	foreach (var s in platforms)
    //	{
    //		s.rotationalOffset = 0f;
    //		RotatePlatform(s);
    //	}
    //	platforms.Clear();
    //}

    private void GroundCheack()
    {
		selected.Clear();
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				canJampInAir = false;
				afterJump = false;
				selected.Add(colliders[i]);
				//OnLandEvent.Invoke();
				animator.SetBool("IsJumping", false);
			}
		}
	}

 //   private void FixedUpdate()
	//{
	//	bool wasGrounded = m_Grounded;

	//	m_Grounded = false;
	//	// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
	//	// This can be done using layers instead but Sample Assets will not overwrite your project settings.
	//	Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
	//	for (int i = 0; i < colliders.Length; i++)
	//	{
	//		if (!wasGrounded)
	//		{
	//			if (colliders[i].gameObject != gameObject)
	//			{
	//				Debug.Log("trueGGG");
	//				m_Grounded = true;
	//				OnLandEvent.Invoke();

	//				animator.SetBool("IsJumping", false);
	//			}
 //           }
	//	}

	//	if (wasGrounded && !m_Grounded)
	//	{
	//		Debug.Log("falseGGG");
	//		Invoke(nameof(notIsGrounded), timeToCanJump);
	//	}
	//}


	public void Move(float move, bool crouch, bool jumpingOff, bool deceleration, bool run, bool jump)
	{
		//if (move != 0) 
		//{
		//	Debug.Log("MOVE");
		//	//audioSource.clip = steps;
		//	audioSource.mute = false;
		//}
		//      else
		//{
		//	//audioSource.clip = null;
		//	//audioSource.Stop();
		//	audioSource.mute = true;
		//}


		_crouch = crouch;
		// If crouching, check to see if the character can stand up
		if (!_crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				_crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (selected.Count > 0 || m_AirControl)
		{
			// If crouching
			if (_crouch)
			{
				move *= m_CrouchSpeed;
			}
			else if (deceleration)
			{
				move *= m_DecelerationSpeed;
			}
			else if (run)
			{
				move *= m_RunSpeed;
			}

			if (jumpingOff && selected.Count > 0) // && m_Grounded
			{
				foreach(var s in selected)
                {
					var platform = s.GetComponent<PlatformEffector2D>();
					if (platform != null)
					{
						platform.rotationalOffset = 180f;
						StartCoroutine(ReturnPlatformEffector(platform));
					}
				}
            }
			//OnCrouch();

			if (Input.GetMouseButton(1) && isActive)
			{
				var difference = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
				if (difference.x > 0 && !m_FacingRight)
				{
					//GameEvents.current.Flip(photonView.ViewID);

					FlipEvent();
				}
				else if (difference.x < 0 && m_FacingRight)
				{
					//GameEvents.current.Flip(photonView.ViewID);

					FlipEvent();
				}
			}
			else if(!Input.GetMouseButton(1))
			{
				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.

					//GameEvents.current.Flip(photonView.ViewID);
					FlipEvent();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.

					//GameEvents.current.Flip(photonView.ViewID);
					FlipEvent();
				}
			}
		}

		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

		if (stairs != null)
		{
			//лестница
			targetVelocity.y = 1f;
			if (Input.GetButton("Jump"))
			{
				targetVelocity.y = 5f;
			}
			else if (Input.GetButton("JumpingOff"))
			{
				targetVelocity.y = -0.5f;
			}
		}
		else
		{
			// If the player should jump...
			if ((selected.Count > 0 || canJampInAir) && jump && m_Rigidbody2D.velocity.y <= 0.2f && !_crouch) // а если лифт
			{
				// Add a vertical force to the player.
				//m_Grounded = false;

				canJampInAir = false;
				afterJump = true;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + m_JumpForce * -m_Rigidbody2D.velocity.y / 10f)); // для прыжка после падения
				animator.SetBool("IsJumping", true);
			}
		}

		if (_crouch != m_wasCrouching) 
		{ 
			if(photonView == null) 
			{
				OnCrouch(_crouch);
            }
            else
            {
				photonView.RPC(nameof(OnCrouch), RpcTarget.All, _crouch);
			}
		}
		
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		animator.SetFloat("Speed", Mathf.Abs(move));
	}

    private IEnumerator ReturnPlatformEffector(PlatformEffector2D platform)
    {
		yield return new WaitForSeconds(0.1f);
        platform.rotationalOffset = 0f;
    }

    private void CanNotJump()
    {
		canJampInAir = false;
	}

	[PunRPC]
	private void OnCrouch(bool crouch)
    {
		var difference = differenceCrouchCollider;

		m_wasCrouching = crouch;
		OnCrouchEvent.Invoke(crouch);
		animator.SetBool("IsCrouching", crouch);

		if (crouch) difference = -difference;
		m_CrouchColliderForHit.offset = m_CrouchColliderForHit.offset + difference;  // * 1.286f;
		m_CrouchCollider.offset = m_CrouchCollider.offset + difference / 2;
		m_CrouchCollider.size = m_CrouchCollider.size + difference;

		foreach (var point in crouchChangeablePoints)
			point.position += new Vector3(difference.x, difference.y, 0f);
	}

	//public void OnPhotonSerializeView(PhotonStream steram, PhotonMessageInfo info)
 //   {
 //       if (steram.IsWriting)
 //       {
	//		steram.SendNext(_crouch);
 //       }
 //       else
 //       {
	//		_crouch = (bool) steram.ReceiveNext();
	//	}
	//	OnCrouch(_crouch);
	//}

	private void FlipEvent()
    {
		m_FacingRight = !m_FacingRight;
	}

	private IEnumerator Brake()
    {
		m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
		yield return new WaitForFixedUpdate();
		m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}
