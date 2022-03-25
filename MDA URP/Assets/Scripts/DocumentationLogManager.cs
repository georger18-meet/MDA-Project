using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentationLogManager : MonoBehaviour
{
    public TextMeshProUGUI UIDisplayer;
    private string myLog;
    private string[] _queueArray = new string[6];
    private int _queueIndex = 0;

    //// Update is called once per frame
    void Update()
    {
        if (_queueArray[5] != null)
        {
            Dequeue();
        }
        UIDisplayer.text = _queueArray[0] + _queueArray[1] + _queueArray[2] + _queueArray[3] + _queueArray[4];
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "[" + type + "]: " + myLog + "\n----------------------------------------\n";
        Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in _queueArray)
        {
            myLog += mylog;
        }
    }

    public void Enqueue(string word)
    {
        if (_queueIndex < _queueArray.Length)
        {
            _queueArray[_queueIndex] = word;
            _queueIndex++;
        }
    }

    public void Dequeue()
    {
        _queueArray[0] = null;
        if (_queueArray[0] == null)
        {
            for (int b = 0; b < _queueArray.Length; b++)
            {
                if (b == _queueArray.Length - 1)
                {
                    _queueArray[b] = null;
                }
                else
                {
                    _queueArray[b] = _queueArray[b + 1];
                }
            }
        }
        _queueIndex--;
    }
}
