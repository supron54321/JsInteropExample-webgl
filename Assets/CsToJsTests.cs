using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TinyUtils.JsInterop;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CsToJsTests : MonoBehaviour
{
    [JsFunction("window.alert")]
    public static extern void Alert(string message);
    [JsFunction("window.prompt")]
    public static extern string Prompt(string message, string defaultValue);

    public TMP_InputField alertMessage;
    public TMP_InputField promptMessage;
    public TMP_InputField promptDefaultValue;

    public TextMeshProUGUI promptReturnedValue;

    public TextMeshProUGUI receivedValue;

    private static CsToJsTests _instance;

    private void Awake()
    {
        WebGLInput.captureAllKeyboardInput = false;
        _instance = this;
        
    }

    // Start is called before the first frame update
    public void OnAlertClick()
    {
        Alert(alertMessage.text);
    }
    
    public void OnPromptClick()
    {
        promptReturnedValue.text = $"returned: {Prompt(promptMessage.text, promptDefaultValue.text)}";
    }

    public class TestClass
    {
        public string TextField;
        public int IntField;
        public double DoubleField;
    }

    [JsCallback("window.JsInterop.SendToCs")]
    public static int ReceiveObject(TestClass value)
    {
        if (value == null)
            _instance.receivedValue.text = "null";
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(TestClass.TextField)}: {value.TextField}");
            sb.AppendLine($"{nameof(TestClass.IntField)}: {value.IntField}");
            sb.AppendLine($"{nameof(TestClass.DoubleField)}: {value.DoubleField}");
            _instance.receivedValue.text = sb.ToString();
        }

        return 1;
    }
}

