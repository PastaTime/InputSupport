using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GamePad {
	private ControlType _control;
	//All 20 possible button inputs
	private Button[] _inputList = new Button[20];
	//A restricted list of button inputs
	private Button[] _inputListRestricted;
	//A collection of buttons which are currently pressed
	private ICollection<Button> _currentlyPressed = new HashSet<Button>();
	
	private bool _restrictInput = false;
	
	
	public GamePad(ControlType control) {
		_control = control;
		for(int i = 0; i < 20; i++) {
			_inputList[i] = new Button(_control, ((ButtonType)(i)));
		}
	}
	/*
		Polls the all buttons on the game pad for their input. 
		If only Active is set, then only the list of active buttons 
		will be polled (efficiency).
	*/
	public void RefreshAll() {
		if (_restrictInput) {
			foreach (Button value in _inputListRestricted) {
				value.Refresh(_currentlyPressed);
			}
		} else {
			foreach (Button value in _inputList) {
				value.Refresh(_currentlyPressed);
			}
		}
	}
	
	/*
		Getter for the restricted input list
	*/
	public Button[] GetRestrictedList() {
		return _inputListRestricted;
	}
	/*
		Allows the user to set the restricted input list
	*/
	public void SetRestrictedList(Button[] inputListRestricted) {
		_inputListRestricted = inputListRestricted;
	}
	/*
		Toggles restricted input. When true, only buttons from the 
		restricted input list will be polled for their values.
		A method of improving efficiency (somewhat) under situations 
		when only a selection of known buttons are required.
	*/
	public void RestrictInput(bool restriction) {
		_restrictInput = restriction;
	}
	
	/*
		Retrieves a collection of currently pressed down buttons.
	*/
	public ICollection<Button> GetButtons() {
		return _currentlyPressed;
	}
	
	/*
		Retrieves a collection of key codes representing the 
		currently pressed down buttons.
	*/
	public ICollection<KeyCode> GetKeyCodes() {
		ICollection<KeyCode> output = new HashSet<KeyCode>();
		foreach (Button but in _currentlyPressed) {
			output.Add(but.ToKeyCode());
		}
		return output;
	}
}
