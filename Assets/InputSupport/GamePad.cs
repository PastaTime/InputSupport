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
	private bool _restrictInput = false;
	//Determines Whether input will be disabled or not.
	private bool _disableInput = false;
	
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
		if (_disableInput) { return; }
		if (_restrictInput) {
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
	public void SetRestrictInput(bool restriction) { _restrictInput = restriction; }
	public bool GetRestrictedInput() { return _restrictInput; }
	
	/*
		Sets disabled input. When true, input from this game pad will not be polled.
	*/
	public void SetDisableInput(bool disable) {	_disableInput = disable; }
	public bool GetDisableInput() { return _disableInput; }
}
