using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestStatusLog  {
    public RequestStatus.Status status;
    public float time;
    public RequestStatusLog(RequestStatus.Status status,float time){
        this.status = status;
        this.time = time;
    }
}
