using UnityEngine;
using System.Collections;
using InControl;

public class Input_scr : MonoBehaviour {

	public static bool OnPowerUp()
	{
		if (Input.GetKey(KeyCode.Z))
		{
			return true;
		}
		return false;
	}
	public static bool OnPowerUpPressed ()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			return true;
		}

		return false;
	}

	public static bool OnFire()
	{		
		if (Input.GetKey(KeyCode.X))
		{
			return true;
		}
		if (Input.GetKey(KeyCode.Space))
		{
			return true;
		}
		return false;
	}

	public static bool OnFirePressed ()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			return true;
		}
		return false;
	}
	public static bool OnLeft ()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnLeftPressed ()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnRight ()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnRightPressed ()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnThrottle ()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnThrottlePressed ()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			return true;
		}
		return false;
	}
	public static bool OnUIDown ()
	{
		if (Input.GetKey(KeyCode.DownArrow))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIDownPressed ()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			return true;
		}

		return false;
	}

	public static bool OnUIUp ()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIUpPressed ()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIForward ()
	{
		if (Input.GetKey(KeyCode.Return))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIForwardPressed ()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			return true;
		}

		InputDevice controller = InputManager.ActiveDevice;
		if (controller.Action2.WasPressed)
		{
			return true;
		}
		return false;
	}

	public static bool OnUIBackward ()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIBackwardPressed ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			return true;
		}

		return false;
	}

	public static bool OnUILeft ()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			return true;
		}

		return false;
	}
	public static bool OnUILeftPressed ()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			return true;
		}

		return false;
	}

	public static bool OnUIRight ()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			return true;
		}

		return false;
	}
	public static bool OnUIRightPressed ()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			return true;
		}

		return false;
	}
}