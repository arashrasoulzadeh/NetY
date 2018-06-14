using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class NetYRequest<T> : BaseRequest  where T : Model
{
    private int successCode=200;
    public delegate void ResponseCallback(T response);
    public delegate void ErrorCallback(int code, string response);

    public ResponseCallback done;
    public ErrorCallback error;

    /// <summary>
    /// initialize  request.
    /// </summary>
    /// <param name="url">url of request</param>
    public NetYRequest(string url){
        this.requestUrl = url;
        Engine.getInstance().triggerInit(this.GetInstanceID(), url);
        this.error = (code, response) => { print(code + " => error:" + response); };
        this.done = (response) => { print("done:" + response); };

    }


    /// <summary>
    /// response of request callback.when request is completed.
    /// </summary>
    /// <param name="response">response</param>
    public void onResponse(T response) 
    {
        done.Invoke(response);
    }
    /// <summary>
    /// response of request callback.
    /// </summary>
    /// <param name="code">status code</param>
    /// <param name="response">response</param>
    public void onFailed(int code, string response)
    {
        error.Invoke(code, response);
    }


    /// <summary>
    /// initialize request.
    /// </summary>
    /// <param name="url">url of request</param>
    public NetYRequest(string url,int successCode)
    {
        this.requestUrl = url;
        this.successCode = successCode;
        Engine.getInstance().triggerInit(this.GetInstanceID(), url);

    }

    /// <summary>
    /// set body of the request.
    /// </summary>
    /// <param name="body">string of request body</param>
    public NetYRequest<T> setBody(string body)
    {
        this.requestBody = body;
        return this;
    }

    /// <summary>
    /// initialize  request.
    /// </summary>
    /// <param name="body">implementation of <c>Model</c> of request body</param>
    public NetYRequest<T> setBody(Model body)
    {
        this.requestBody = body.toString();
        return this;
    }

    /// <summary>
    /// change success code.
    /// </summary>
    /// <param name="code">desired success code</param>
    public NetYRequest<T> setSuccessCode(int code)
    {
        this.successCode = code;
        return this;

    }
  

    /// <summary>
    /// send the request.
    /// </summary>
    public NetYRequest<T> send()
    {
        if (this.father == null)
        { 
            onFailed(-1, "Request is not attached to a gameobject");
        }else{
            this.father.GetComponent<MonoBehaviour>().StartCoroutine(GetRequest(requestUrl, requestBody));

        }

        return this;

    }
  
    /// <summary>
    /// debug request in console.
    /// </summary>
    public NetYRequest<T> debug()
    {
        print(" Url=> "+this.requestUrl+"\nBody=>"+this.requestBody);
        return this;
    }







     IEnumerator GetRequest(string url, string bodyJsonString)
    {
        Engine.getInstance().triggerStart(this.GetInstanceID());
        print("running : " + url);
        yield return new WaitForEndOfFrame();

        WWW www;

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
 

        if (requestBody!=null){
            
        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
            www = new WWW(url, formData, headers);

        }else{
            www = new WWW(url, null, headers);

        }
 
        yield return new WaitUntil(() => www.isDone);
        int code = getResponseCode(www);
        if (code == this.successCode)
        {
            onResponse(prepareData(www.text));
            Engine.getInstance().triggerDone(this.GetInstanceID());
            Destroy(this);
            
        }else{
            this.onFailed(code, www.text);
            Engine.getInstance().triggerFailed(this.GetInstanceID());
            if (retryCount>0){
                retryCount--;
                this.send();
            }else{
                Destroy(this);

            }
        }


    }

    /// <summary>
    /// cast to the given type
    /// </summary>
    /// <param name="response">response string</param>

    private T prepareData(string response){
        return JsonConvert.DeserializeObject<T>(response);
    }


    /// <summary>
    /// response of request callback
    /// </summary>
    /// <param name="callback"><c>ResponseCallback</c> object</param>
    public NetYRequest<T> next(ResponseCallback callback)
    {
        this.done = callback;
        this.classType = this.GetType().ToString();
        return this;
    }

    /// <summary>
    /// response of request callback.when request is failed
    /// </summary>
    /// <param name="callback"><c>ErrorCallback</c> object</param>
    public NetYRequest<T> failed(ErrorCallback callback)
    {
        this.error = callback;
        return this;
    }
    /// <summary>
    /// retry times (on failed)
    /// </summary>
    /// <param name="times">times</param>
    public NetYRequest<T> retry(int times)
    {
        this.retryCount = times;
        return this;
    }


    /// <summary>
    /// required , attach to gameobject
    /// </summary>
    /// <param name="gameObject"><c>GameObject</c> to attach</param>
    public NetYRequest<T> attach(GameObject gameObject)
    {
        this.father = gameObject;
        return this;
    }
}
