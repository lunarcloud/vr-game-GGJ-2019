using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CardboardNumpad : MonoBehaviour
{
    public event EventHandler NumberPressed;

    public Button[] numberButtons = new Button[10];

    public Button BackspaceButton;

    public Text NumberText;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < numberButtons.Length; i++)
        {
            var index = i;
            numberButtons[i].onClick.AddListener(delegate { AddNumber(index); });
        }
        BackspaceButton.onClick.AddListener(delegate { Backspace(); });
    }

    public void AddNumber(int value) {
        NumberText.text += value;
    }

    public void Backspace() {
        NumberText.text = NumberText.text.Substring(0, NumberText.text.Length - 1);
    }

}
