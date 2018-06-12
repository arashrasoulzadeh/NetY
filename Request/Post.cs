using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Post : Request
{
    /// <summary>
    /// initialize post request.
    /// </summary>
    /// <param name="url">url of request</param>
    public Post init(string url){
        this.requestUrl = url;
        Engine.getInstance().triggerInit();

        return this;
    }

    /// <summary>
    /// set body of the request.
    /// </summary>
    /// <param name="body">string of request body</param>
    public Post setBody(string body)
    {
        this.requestBody = body;
        return this;
    }

    /// <summary>
    /// initialize post request.
    /// </summary>
    /// <param name="body">implementation of <c>Model</c> of request body</param>
    public Post setBody(Model body)
    {
        this.requestBody = body.toString();
        return this;
    }


    /// <summary>
    /// send the request.
    /// </summary>
    public Post send()
    {
        StartCoroutine(PostRequest(requestUrl, requestBody));
        return this;

    }
  
    /// <summary>
    /// debug request in console.
    /// </summary>
    public Post debug()
    {
        print(" Url=> "+this.requestUrl+"\nBody=>"+this.requestBody);
        return this;
    }


     IEnumerator PostRequest(string url, string bodyJsonString)
    {
        Engine.getInstance().triggerStart();
        print("running : " + url);
        yield return new WaitForEndOfFrame();

        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        www = new WWW(url, formData, postHeader);
 
        yield return new WaitUntil(() => www.isDone);
        int code = getResponseCode(www);
        if (code == 200)
        {
            this.onResponse(www.text);

            Engine.getInstance().triggerDone();
            Destroy(this);

        }else{
            this.onFailed(code, www.text);
            Engine.getInstance().triggerFailed();
            if (retryCount>0){
                retryCount--;
                this.send();
            }else{
                Destroy(this);

            }
        }


    }



    /// <summary>
    /// response of request callback
    /// </summary>
    /// <param name="callback"><c>ResponseCallback</c> object</param>
    public Post next(ResponseCallback callback)
    {
        this.done = callback;
        return this;
    }

    /// <summary>
    /// response of request callback.when request is failed
    /// </summary>
    /// <param name="callback"><c>ErrorCallback</c> object</param>
    public Post failed(ErrorCallback callback)
    {
        this.error = callback;
        return this;
    }
    /// <summary>
    /// retry times (on failed)
    /// </summary>
    /// <param name="times">times</param>
    public Post retry(int times)
    {
        this.retryCount = times;
        return this;
    }


    /// <summary>
    /// required , attach to gameobject
    /// </summary>
    /// <param name="gameObject"><c>GameObject</c> to attach</param>
    public Post attach(GameObject gameObject)
    {
        this.father = gameObject;
        Post post = this.father.AddComponent<Post>();
        return post;
    }
}
