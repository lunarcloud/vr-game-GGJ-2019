using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyboardDelegateVendorMenu : GvrKeyboardDelegateBase
{
    public GvrKeyboard keyboard;

    public Text KeyboardText;

    public override event EventHandler KeyboardHidden;

    public override event EventHandler KeyboardShown;

    public event EventHandler KeyboardEnterPressed;

    private const string DD_KEYBOARD_NOT_INSTALLED_MSG = "Please update the Daydream Keyboard app from the Play Store.";
    
    public override void OnKeyboardShow() => KeyboardShown?.Invoke(this, null);

    public override void OnKeyboardHide() => KeyboardHidden?.Invoke(this, null);
    
    public override void OnKeyboardUpdate(string text)
    {
        if (KeyboardText != null)
        {
            KeyboardText.text = text;
        }
    }

    public override void OnKeyboardEnterPressed(string text) => KeyboardEnterPressed?.Invoke(this, null);

    public override void OnKeyboardError(GvrKeyboardError errCode)
    {
        Debug.Log("Calling Keyboard Error Delegate: ");
        switch (errCode)
        {
            case GvrKeyboardError.UNKNOWN:
                Debug.Log("Unknown Error");
                break;
            case GvrKeyboardError.SERVICE_NOT_CONNECTED:
                Debug.Log("Service not connected");
                break;
            case GvrKeyboardError.NO_LOCALES_FOUND:
                Debug.Log("No locales found");
                break;
            case GvrKeyboardError.SDK_LOAD_FAILED:
                Debug.LogWarning(DD_KEYBOARD_NOT_INSTALLED_MSG);
                if (KeyboardText != null)
                {
                    KeyboardText.text = DD_KEYBOARD_NOT_INSTALLED_MSG;
                }
              
                break;
        }
    }

    public void LaunchPlayStore()
    {
#if !UNITY_ANDROID
        Debug.LogError("GVR Keyboard available only on Android.");
#else
        GvrKeyboardIntent.Instance.LaunchPlayStore();
#endif  // !UNITY_ANDROID
    }
}