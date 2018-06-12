using UnityEngine;
using UnityEditor; // Dont forget to add this as we are extending the Editor
using System.Collections;

[CustomEditor(typeof(NetY))]
public class NetYEditor : Editor {

    NetY _target;
    Vector2 scrollPos;

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
        GUILayout.BeginVertical();
        for (int i = 0; i < Engine.getInstance().requestLog.Count;i++){
            string statusstring = "";
            GUIStyle style = new GUIStyle();
            switch (Engine.getInstance().requestLog[i].status){
                case RequestStatus.Status.init:
                    statusstring = "I";
                    style = colorBackground(new Color32(0, 123, 255, 255));
                    break;
                case RequestStatus.Status.pending:
                    style = colorBackground(new Color32(255, 193, 7, 255));
                    statusstring = "P";
                    break;
                case RequestStatus.Status.done:
                    style = colorBackground(new Color32(40, 167, 69, 255));
                    statusstring = "D";
                    break;
                case RequestStatus.Status.failed:
                    style = colorBackground(new Color32(200, 53, 69, 255));
                    statusstring = "F";
                    break;

            }
            if (GUILayout.Button("[" + statusstring + "] => " + Engine.getInstance().requestLog[i].url,style))
            {
                EditorUtility.DisplayDialog("Request #" + Engine.getInstance().requestLog[i].id , Engine.getInstance().requestLog[i].url+"\nLog:\n"+Engine.getInstance().requestLog[i].getLog(), "ok");

            }

        }

        GUILayout.EndVertical();
        GUILayout.EndVertical();

        //If we changed the GUI aply the new values to the script
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_target);
        }

    }




    public GUIStyle colorBackground(Color32 color){
        GUIStyle myStyle = new GUIStyle();
        Texture2D tex = new Texture2D(2, 2);
        SetColor(tex,color);//r,g,b,a 
        myStyle.normal.background = tex;
        return myStyle;
    }
     

    public void SetColor( Texture2D tex2, Color32 color)
    {


        var fillColorArray = tex2.GetPixels32();

        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = color;
        }

        tex2.SetPixels32(fillColorArray);

        tex2.Apply();
    }
     

}
