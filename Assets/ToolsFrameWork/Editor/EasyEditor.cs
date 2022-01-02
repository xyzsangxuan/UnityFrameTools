using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class EasyEditor : Editor
{
    [MenuItem("Custom/GotoSetup")]
    public static void GotoSetup()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Setup.unity");
    }

    [MenuItem("Custom/GotoUIEditor")]
    public static void GotoUIEditor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/UIEditor.unity");
    }

    //把配置文件放入Resources目录下
    
}
