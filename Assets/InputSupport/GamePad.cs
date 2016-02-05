using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GamePad {
	private ControlType _control;
	//All 20 possible button inputs
	private Button[] _inputList = new Button[20];
	//A restricted list of button inputs
	private Button[] _inputListRestricted;
	//Determines Whether input will be polled from inputList or inputListRestricted
	private bool _restrictGamePadSpecificPolling = false;
	//Determines Whether input will be disabled or not.
	private bool _disableGamePadSpecificPolling = false;
	
	public GamePad(ControlType control) {
		_control = control;
		for(int i = 0; i < 20; i++) {
			_inputList[i] = new Button(_control, ((ButtonType)(i)));
		}
	}
	/*
		Polls the all buttons on the game pad for their input. 
		If input is being restricted, then only the list of active buttons 
		will be polled (efficiency).
	*/
	public void RefreshAll() {
		if (_disableGamePadSpecificPolling) { return; }
		if (_restrictGamePadSpecificPolling) {
			foreach (Button value in _inputListRestricted) {
				value.Refresh();
			}
		} else {
			foreach (Button value in _inputList) {
				value.Refresh();
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
		Sets restricted input. When true, only buttons from the 
		restricted input list will be polled for their values.
		A method of improving efficiency (somewhat) under situations 
		when only a selection of known buttons are required.
	*/
	public void SetRestictedPolling(bool restriction) { _restrictGamePadSpecificPolling = restriction; }
	public bool GetRestictedPolling() { return _restrictGamePadSpecificPolling; }
	
	/*
		Sets disabled input. When true, input from this game pad will not be polled.
	*/
	public void SetDisablePolling(bool disable) {	_disableGamePadSpecificPolling = disable; }
	public bool GetDisablePolling() { return _disableGamePadSpecificPolling; }
}
