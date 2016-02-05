using UnityEngine;
using System.Collections.Generic;
public class Controller : MonoBehaviour {
	//Set to true to disable input
	public bool disableInput = false; 
	//Set to true if you only want the user to have restricted input access for this scene
	public bool restrictInputs = false; 
	//User editable list of gamepads to monitor. Note, gamepadlist will not update dynamically.
	public ControlType[] controllerList;
	//The list of gamepads being monitored.
	private GamePad[] _gamePadList;
	//The keycodes which are currently pressed down from keyboard and the active inputs on all controllers
	public static ICollection<KeyCode> ActiveInput = new HashSet<KeyCode>();


	/** Utility Functions **/
	/*
		Replacement for Input.GetKeyDown() to include Joystick buttons.
		Note: Any form of GetKeyDown(String) will be assumed to be a keyboard input poll.
	*/
	public static bool GetKeyDown(KeyCode key) {
		return GetActiveInputs().Contains(key);
	}
	
	/*
		Boolean inverse of GetKeyDown(KeyCode key)
	*/
	public static bool GetKeyUp(KeyCode key) {
		return !(GetKeyDown(key));
	}
	
	/*
		To maintain usability. Passes string inputs thru to Input manager.
	*/
	public static bool GetKeyDown(string key) {
		return Input.GetKeyDown(key);
	}
	
	/*
		Boolean inverse of GetKeyDown(string key)
	*/
	public static bool GetKeyUp(string key) {
		return !(GetKeyDown(key));
	}

	/*
		Returns a collection of Keycodes which represent the joystick buttons which are currently active.
		Primarily used in Controller.GetKeyDown().
	*/
	public static ICollection<KeyCode> GetActiveInputs() {
		return ActiveInput;
	}
	
	/*
		Returns the button that was last pressed by the user.
		Actually returns the first button pressed by the user during 
		a frame (if multiple were pressed).
	*/
	public static KeyCode GetLastButton() {
		foreach (KeyCode key in ActiveInput) {
			return key;
		}
		return KeyCode.None;
		//Cannot call extension method
		//return ActiveInput.Last();
	}
	
	/*
		Initializes Game pads with all possible buttons.
	*/
	void Start() {
		_gamePadList = new GamePad[controllerList.Length];
		for (int i = 0; i < controllerList.Length; i++) {
			_gamePadList[i] = new GamePad(controllerList[i]);
			_gamePadList[i].SetRestrictInput(restrictInputs);
			_gamePadList[i].SetDisableInput(disableInput);
		}
	}
	
	/*
		Polls controller for button statuses.
	*/
	void Update() {
		if (!disableInput) {
			foreach (GamePad value in _gamePadList) {
				value.RefreshAll();
			}
		}
	}
	/*
		Polls for keyboard input.
	*/
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey) {
			if (e.type == EventType.KeyUp) {
				 ActiveInput.Remove(e.keyCode);
			} else if (e.type == EventType.KeyDown) {
				 ActiveInput.Add(e.keyCode);
			}
		}
	}
	
	/*
		Retrieve the gamepad list. Helpful if you want different game pads 
		too have different settings.
		
		Example:
			Controller.getGamePadList()[1].SetRestrictInput(true);
			Controller.getGamePadList()[2].SetDisableInput(true);
		Grab the 2nd GamePad from the GamePad array and Restrict its input.
		Grab the 3rd GamePad from the GamePad array and disable its input.
	*/
	public GamePad[] getGamePadList() {
		return _gamePadList;
	}

	
}