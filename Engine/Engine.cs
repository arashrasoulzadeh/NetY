
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


    //setters
    public void increaseActiveRequestsCount()
    {
        this.activeRequestsCount++;

    }
    public void increaseInActiveRequestsCount()
    {
        this.inactiveRequestsCount++;

    }
    public void increaseFailedRequestsCount()
    {
        this.failedRequests++;

    }
    public void increaseDoneRequestsCount()
    {
        this.doneRequests++;

    }

    public void decreaseActiveRequestsCount()
    {
        this.activeRequestsCount--;
    }
    public void decreaseInActiveRequestsCount()
    {
        this.inactiveRequestsCount--;
    }

    public void triggerDone()
    {
        this.increaseDoneRequestsCount();
        this.decreaseActiveRequestsCount();
    }

    public void triggerFailed()
    {
        this.increaseFailedRequestsCount();
        this.decreaseActiveRequestsCount();

    }

    public void triggerStart()
    {
        this.increaseActiveRequestsCount();
        this.decreaseInActiveRequestsCount();

    }
    public void triggerInit()
    {
        this.increaseInActiveRequestsCount();

    }



}