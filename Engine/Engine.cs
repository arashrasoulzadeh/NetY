
using System.Collections.Generic;
using UnityEngine;

public class Engine{
    //singleton
    public static Engine instance;
    public static Engine getInstance(){
        if (Engine.instance == null)
        {
            Engine.instance = new Engine();
            Debug.Log("NetY Engine Started !");
        }
        return Engine.instance;
    }

    //private variables
    int activeRequestsCount=0, inactiveRequestsCount=0, doneRequests=0, failedRequests=0;
    //public variables
    public List<RequestStatus> requestLog=new List<RequestStatus>();


  


    //geters
    public int getActiveRequestsCount()
    {
        return this.activeRequestsCount;
    }
    public int getInActiveRequestsCount()
    {
        return this.inactiveRequestsCount;
    }
    public int getFailedRequestsCount()
    {
        return this.failedRequests;
    }
    public int getDoneRequestsCount()
    {
        return this.doneRequests;
    }

    public int getStatusIndexById(int id)
    {
        for (int i = 0; i < requestLog.Count;i++){
            if (requestLog[i].id == id){
                return i;
            }
        }
        return -1;
    }


    //setters
    void increaseActiveRequestsCount()
    {
        this.activeRequestsCount++;

    }
    void increaseInActiveRequestsCount()
    {
        this.inactiveRequestsCount++;

    }
    void increaseFailedRequestsCount()
    {
        this.failedRequests++;

    }
    void increaseDoneRequestsCount()
    {
        this.doneRequests++;

    }

    void decreaseActiveRequestsCount()
    {
        this.activeRequestsCount--;
    }
    void decreaseInActiveRequestsCount()
    {
        this.inactiveRequestsCount--;
    }

    public void triggerDone(int id)
    {
        this.requestLog[getStatusIndexById(id)].setStatus(RequestStatus.Status.done);
        this.increaseDoneRequestsCount();
        this.decreaseActiveRequestsCount();
    }
    
    public void triggerFailed(int id)
    {
        this.requestLog[getStatusIndexById(id)].setStatus(RequestStatus.Status.failed);
        this.increaseFailedRequestsCount();
        this.decreaseActiveRequestsCount();

    }

    public void triggerStart(int id)
    {
        this.requestLog[getStatusIndexById(id)].setStatus(RequestStatus.Status.pending);
        this.increaseActiveRequestsCount();
        this.decreaseInActiveRequestsCount();

    }
    public void triggerInit(int id,string url)
    {
        this.requestLog.Add(new RequestStatus(id,url));
        this.increaseInActiveRequestsCount();
        this.requestLog[getStatusIndexById(id)].setStatus(RequestStatus.Status.init);

    }



}