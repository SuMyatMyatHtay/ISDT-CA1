using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadClickButton : MonoBehaviour
{
    public GameObject keypadButton;

    

    public void KeyPadButtonIsClicked()
    {
        keypadButton.GetComponent<KeypadButton>().PressButton();
    }

}
