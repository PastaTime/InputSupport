using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
The button class represents a possible joystick button.
*/
public class Button {
	private string _virtualName;
	//The type of controller this button maps too (See ControlType.cs)
	private ControlType _controlType; 
	//The button type being mapped (See ButtonType.cs)
	private ButtonType _buttonType; 
	//Whether this button was down last poll
	private bool _down = false; 
	
	/*
		Constructor must specify both Controller and Button Type
	*/
	public Button(ControlType control,ButtonType button) {
		_controlType = control;
		_buttonType = button;
		_virtualName = "joystick";
		if (_controlType != ControlType.Any) {
			_virtualName += " " + ((int)(_controlType));	
		}
		_virtualName += " button " + ((int)(_buttonType));
	}
	
	/*
		Used to poll the button status
		Buttons who are polled as down are stored in currentlyPressed and passed to GamePad
	*/
	public void Refresh() {
		_down = Input.GetKey(_virtualName);
		if (_down) {
			Controller.ActiveInput.Add(this.ToKeyCode());
		} else {
			Controller.ActiveInput.Remove(this.ToKeyCode());
		}
	}
	
	public bool isPressed() {
		return _down;
	}
	
	public override string ToString() {
		return _virtualName + "::" + this.ToKeyCode().ToString();
	}
	
	//Formula to convert Joystick number and button number to corresponding keycode.
	public KeyCode ToKeyCode() {
		return ((KeyCode)(330 + ((int)(_buttonType)) + 20 * ((int)(_controlType))));
	}

}