using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour 
{
	public Movement mMovement;
	public PulseGenerator pulseGenerator;
	public float speed=5f;
	public Vector2 velocity;
	public float maxVelocity = 5f;

	[Header("Input")]
	public string mInputButtonPulse = "Fire1";
	public string mInputButtonFreqUp = "FreqUp";
	public string mInputButtonFreqDown = "FreqDown";
	public string mInputButtonInteract = "Fire2";

	[Header("Interactions")]
	public bool interacting;
	public InteractuableItem interactuableOnArea;

	[Header("FrequencyManager")]
	public FrequencyManager freqManager;
	private float freqCD = 0f;
	public float freqMaxCD = 0.2f;
	public float holdFreqMult = 1f;
	private int lastHold = 0;

	[Header("Pulse attributes")]
	public float pulseMaxCD = 1.5f;
	public float pMaxRadius = 1f;
	public AnimationCurve pCurve;
	public float pMaxSeconds;
	public float pFrequency;


	private float pulseCD = 0f;

	void Update()
	{
		ApplyGravity();
		if(!interacting)
		{
			CheckMovementInput();
			CheckJumpInput();
			CheckFrequencyInput();
			CheckPulseInput();
		}
		else
		{
			velocity.x = 0;
		}

		CheckInteractInput();

		velocity = Vector2.ClampMagnitude(velocity, maxVelocity);
		UpdateCounters();
	}

	void LateUpdate()
	{
		ApplyMovement();
	}

	void UpdateCounters()
	{
		UpdatePulseCD();
		UpdateFreqCD();
	}

	void UpdatePulseCD()
	{
		if(pulseCD > 0f)
		{
			pulseCD = Mathf.Clamp( pulseCD - Time.deltaTime, 0f, pulseMaxCD);
		}
	}

	void UpdateFreqCD()
	{
		if(freqCD > 0f)
		{
			freqCD = Mathf.Clamp( freqCD - Time.deltaTime, 0f, freqMaxCD);
		}
	}

	void CheckInteractInput()
	{
		if(Input.GetButtonDown(this.mInputButtonInteract))
		{
			Debug.Log("InteractButton!");
			if(!interacting)
			{
				List<Collider2D> col = new List<Collider2D> (Physics2D.OverlapBoxAll(transform.position, Vector2.one, 0f));
				if(col != null)
				{
					foreach (var item in col) 
					{
						InteractuableItem i = item.GetComponent<InteractuableItem>();	
						if(i!= null && i.canInteract)
						{
							interactuableOnArea = i;
							Debug.Log("Found interactable: "+i.gameObject.ToString());
						}
					}
					if(interactuableOnArea != null)
					{
						interactuableOnArea.Interact();
						interacting = true;
					}
				}
			}
			else
			{
				interactuableOnArea.Interact();
			}
		}	
	}

	void CheckJumpInput()
	{
		if(this.mMovement.grounded)
		{
		}
	}

	void CheckFrequencyInput()
	{
		bool up = Input.GetButton(mInputButtonFreqUp);
		bool down = Input.GetButton(mInputButtonFreqDown);

		if(up)
		{
			Debug.Log("UP");
		}


		if( (up && down) || (up && lastHold < 0) || (down && lastHold > 0) || !(up || down))
		{
			holdFreqMult = 1f;
		}
		else if(up || down)
		{
			holdFreqMult = Mathf.Clamp(holdFreqMult+= 2* Time.deltaTime, 1f, 10f);
		}

		if(freqCD == 0f && (up || down) && !(up && down))
		{
			int amount = Mathf.CeilToInt( up ? 1 : -1);
			freqManager.frequency += amount * Mathf.CeilToInt(holdFreqMult);
			freqCD = freqMaxCD;
		}

		lastHold = up? 1 : -1;
	}

	void CheckPulseInput()
	{
		if(Input.GetButtonDown(mInputButtonPulse) && pulseCD == 0)
		{
			pulseGenerator.SendPulse(pMaxRadius, pCurve, pMaxSeconds, freqManager.frequency);
		}
	}

	void ApplyGravity()
	{
		if(!this.mMovement.grounded)
		{
			velocity += Vector2.down * 0.198f * Time.deltaTime;
		}
	}


	void CheckMovementInput()
	{
		Vector2 m = new Vector2(Input.GetAxis("Horizontal"),0);
		velocity+= m*Time.deltaTime*speed;
		if(m.x == 0 && mMovement.grounded)
		{
			velocity *= 0.4f*Time.deltaTime;
		}
	}

	void ApplyMovement()
	{
		bool colx;
		bool coly;
		mMovement.Move(velocity, out colx, out coly);
		
		if(colx)
		{
			velocity.x = 0;
		}
		if(coly)
		{
			velocity.y = 0;
		}
	}

	public void FinishedDialog()
	{
		Debug.Log("Finished DIalog: PLayer");
		this.interacting = false;
		this.interactuableOnArea = null;
	}

}
