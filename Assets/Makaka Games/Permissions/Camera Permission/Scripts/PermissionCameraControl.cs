﻿/*
===================================================================
Unity Assets by MAKAKA GAMES: https://makaka.org/o/all-unity-assets
===================================================================

Online Docs (Latest): https://makaka.org/unity-assets
Offline Docs: You have a PDF file in the package folder.

=======
SUPPORT
=======

First of all, read the docs. If it didn’t help, get the support.

Web: https://makaka.org/support
Email: info@makaka.org

If you find a bug or you can’t use the asset as you need, 
please first send email to info@makaka.org (in English or in Russian) 
before leaving a review to the asset store.

I am here to help you and to improve my products for the best.
*/

using UnityEngine;
using UnityEngine.Events;

using TMPro;

#pragma warning disable 649

[HelpURL("https://makaka.org/unity-assets")]
public class PermissionCameraControl : MonoBehaviour
{

#if UNITY_EDITOR

    [SerializeField]
    private bool isPermissionDeniedInEditorTesting = false;

#endif

    [Space]
    [SerializeField]
    private UnityEvent OnPermissionsGranted = null;

    [Space]
    [Header("Tutorial is shown for iOS by Default")]

    [SerializeField]
    TextMeshProUGUI textTutorial;

    [SerializeField]
    private GameObject iconsTutorialIOS;

    [SerializeField]
    [TextArea(3, 9)]
    private string messageTutorialAndroid;

    private bool isPermissionGranted = false;

    private bool isPermissionChecked = false;

#if UNITY_ANDROID

    /// <summary>
    /// <para>
    /// When App Settings are changed outside the App (Phone Settings) to "Deny" 
    /// then App will be terminated and Restarted when the user will switch to it
    /// (OnApplicationFocus is not calling).
    /// </para>
    /// <para>
    /// If "Allow" will set on Android then App Session will be continued.
    /// </para>
    /// </summary>
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Program enters here When App Start:
            // iOS - No
            // Android — Yes

            DebugPrinter.Print("Permissions Scene — OnApplicationFocus(true)");

            CheckPermissionOrRequest();
        }
    }

#endif

    /// <summary>
    /// App doesn't enter here When OnApplicationFocus() causes Scene Loading
    /// </summary>
    private void Start()
    {
        DebugPrinter.Print("Camera Permission Scene — Start()");

        CheckPermissionOrRequest();

#if UNITY_ANDROID

        textTutorial.text = messageTutorialAndroid;

        iconsTutorialIOS.SetActive(false);

#endif

    }

    private void CheckPermissionOrRequest()
    {

#if UNITY_EDITOR

        if (isPermissionDeniedInEditorTesting)
        {
            return;
        }

#endif

        if (!isPermissionChecked)
        {
            isPermissionChecked = true;

            isPermissionGranted = CheckPermissionOrRequestBase();

            if (isPermissionGranted)
            {
                OnPermissionsGranted?.Invoke();
            }

            isPermissionChecked = false;
        }
    }

    /// <summary>
    /// Android call contains 2 requests:
    /// Camera, Storage.
    /// </summary> 
    private bool CheckPermissionOrRequestBase()
    {
        NativeCamera.Permission resultOfCheckPermission =
                    NativeCamera.CheckPermission();

        DebugPrinter.Print("CheckCameraPermissionOrRequest");

        switch (resultOfCheckPermission)
        {
            case NativeCamera.Permission.Granted:

                DebugPrinter.Print("Camera Permission Granted");

                return true;

            case NativeCamera.Permission.ShouldAsk:

                NativeCamera.Permission resultOfRequestPermission;

                do
                {
                    resultOfRequestPermission =
                        NativeCamera.RequestPermission();

                    DebugPrinter.Print(2);
                }
                while (resultOfRequestPermission
                    == NativeCamera.Permission.ShouldAsk);

                if (resultOfRequestPermission
                    == NativeCamera.Permission.Granted)
                {
                    DebugPrinter.Print(3.1f);

                    return true;
                }
                else
                {
                    DebugPrinter.Print(3.2f);

                    return false;
                }

            case NativeCamera.Permission.Denied:

                DebugPrinter.Print(4);

                return false;

            default:

                DebugPrinter.Print(5);

                return false;
        }
    }

    public void OpenSetting()
    {
        NativeCamera.OpenSettings();
    }
}
