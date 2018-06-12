using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestStatus  {
    public List<RequestStatusLog> logs = new List<RequestStatusLog>();
    public enum Status{
        init,pending,done,failed
    }
    public Status status;
    public string url;
    public int id = 0;
    public void setStatus(Status status)
    {
        this.status = status;
        logs.Add(new RequestStatusLog(status, Time.time));
    }

    public string getLog()
    {
        string response = "";
        for (int i = 0; i < logs.Count;i++){
            response += "[" + logs[i].time + "] => " + logs[i].status;
            response += "\n";
        }
        return response;
    }

    public RequestStatus(int id,string url){
        this.id = id;
        this.url = url;
    }
}
