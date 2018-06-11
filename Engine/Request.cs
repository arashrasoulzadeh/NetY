﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request: MonoBehaviour
{
    public string requestBody, requestUrl, responseBody;
    public delegate void ResponseCallback(string response);
    public delegate void ErrorCallback(int code,string response);

    public GameObject father;
    public ResponseCallback done;
    public ErrorCallback error;

    public void onResponse(string response)
    {
         done.Invoke(response);
    }

    public void onFailed(int code,string response)
    {
         error.Invoke(code,response);
    }


    public static int getResponseCode(WWW request)
    {
        int ret = 0;
        if (request.responseHeaders == null)
        {
            Debug.LogError("no response headers.");
        }
        else
        {
            if (!request.responseHeaders.ContainsKey("STATUS"))
            {
                Debug.LogError("response headers has no STATUS.");
            }
            else
            {
                ret = parseResponseCode(request.responseHeaders["STATUS"]);
            }
        }

        return ret;
    }

    public static int parseResponseCode(string statusLine)
    {
        int ret = 0;

        string[] components = statusLine.Split(' ');
        if (components.Length < 3)
        {
            Debug.LogError("invalid response status: " + statusLine);
        }
        else
        {
            if (!int.TryParse(components[1], out ret))
            {
                Debug.LogError("invalid response code: " + components[1]);
            }
        }

        return ret;
    }


}