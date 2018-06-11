using UnityEngine;
using UnityEditor; // Dont forget to add this as we are extending the Editor
using System.Collections;

[CustomEditor(typeof(NetY))]
public class NetYEditor : Editor {

    NetY _target;
    void OnEnable()
    {
        _target = (NetY)target;
    }

    public override void OnInspectorGUI(){
        GUILayout.BeginVertical();
        GUILayout.Label("Active : "+Engine.getInstance().getActiveRequestsCount(), EditorStyles.boldLabel);
        GUILayout.Label("Inactive : "+Engine.getInstance().getInActiveRequestsCount(), EditorStyles.boldLabel);
        GUILayout.Label("Completed : "+Engine.getInstance().getDoneRequestsCount(), EditorStyles.boldLabel);
        GUILayout.Label("Failed : "+Engine.getInstance().getFailedRequestsCount(), EditorStyles.boldLabel);
       
        GUILayout.EndVertical();

        //If we changed the GUI aply the new values to the script
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }

    }

}
