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
    public Post init(string url,string body){
        this.requestUrl = url;
        this.requestBody = body;
        Engine.getInstance().triggerInit();

        return this;
    }

    public Post send()
    {
        StartCoroutine(PostRequest(requestUrl, requestBody));
        return this;

    }
  

    public Post debug()
    {
        print(" Url=> "+this.requestUrl+"\nBody=>"+this.requestBody);
        return this;
    }


    public IEnumerator PostRequest(string url, string bodyJsonString)
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
        this.onResponse(www.text);

        Engine.getInstance().triggerDone();

        Destroy(this);

    }




    public Post next(ResponseCallback callback)
    {
        this.done = callback;
        return this;
    }


    public Post failed(ErrorCallback callback)
    {
        this.error = callback;
        return this;
    }



    public Post attach(GameObject gameObject)
    {
        this.father = gameObject;
        Post post = this.father.AddComponent<Post>();
        return post;
    }
}
