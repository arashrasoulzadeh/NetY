using System.Collections;
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


}
