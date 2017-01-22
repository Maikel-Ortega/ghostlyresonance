using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPulseReceiver : PulseReceiver 
{
	public HOURS currentHour;

	protected override void Reaction ()
	{
		base.Reaction ();

		switch(currentHour)
		{
			case HOURS.THREE: 
				currentHour =HOURS.SIX;
				break;
			case HOURS.SIX: 
				currentHour = HOURS.NINE;
				break;
			case HOURS.NINE: 
				currentHour = HOURS.TWELVE;
				break;
			case HOURS.TWELVE: 
				currentHour = HOURS.THREE;
			break;
		}

		GameObject.FindObjectOfType<HotelManager>().SetGhostsConfigByHour(currentHour);

	}
}

