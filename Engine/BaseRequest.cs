using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest: MonoBehaviour
{
    public string requestBody, requestUrl, responseBody;
    public GameObject father;
    public int retryCount { get; set; }
    public string classType;


    public  BaseRequest()
    {

    }
     

    /// <summary>
    /// returns status code
    /// </summary>
    /// <param name="request"><c>WWW</c> object to get status code</param>
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
    /// <summary>
    /// get status line 
    /// </summary>
    /// <param name="statusLine">string status line</param>

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
