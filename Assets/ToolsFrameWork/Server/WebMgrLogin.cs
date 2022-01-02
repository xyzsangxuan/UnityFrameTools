using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebMgrLogin : Singleton<WebMgrLogin> {
    /// <summary>
    /// 创建表单
    /// </summary>
    /// <param name="inputName">Name.</param>
    /// <param name="inputPassword">Pass.</param>
    public void CreatWFrom(bool action,StrLogin strLogin)
    {

        WWWForm form = new WWWForm();
        form.AddField("name", strLogin.userName);
        form.AddField("password", strLogin.password);

        if (action == true)
        {
            form.AddField("action", "login");
        }
        else
        {
            form.AddField("action", "regist");
        }
        MonoMgr.GetInstance().StartCoroutine(SendPost(PathCfg.URL_USERLOGIN, form));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="wForm"></param>
    /// <returns></returns>
    IEnumerator SendPost(string url, WWWForm wForm)
    {
        UnityWebRequest www = UnityWebRequest.Post(url, wForm);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text.ToString() == PathCfg.WEB_SUCCESS)
            {
                Debug.Log("登陆成功");
                ScenesMgr.GetInstance().LoadSceneAsyn("Lobby", callback);
                UIManager.GetInstance().HidePanel(PathCfg.PREFAB_PANEL_LOGIN);
                UIManager.GetInstance().ShowPanel<MessagePanel>(PathCfg.PREFAB_PANEL_MESSAGE, E_UI_Layer.Mid, ShowMessage);
            }
            else
            {
                Debug.Log("登陆失败");
            }

        }
    }

    public void callback()
    {

    }
    public void ShowMessage(MessagePanel panel)
    {
        MonoMgr.GetInstance().StartCoroutine(panel.MessageShow(Dialog.d1));
    }
    public class StrLogin
    {
        public string userName;

        public string password;

        public bool rememberMe;
    }
}
public static class Dialog
{
    public static string d1 = "欢迎来到这个穷*的世界!";

    public static string d2 = "在这个世界没有酷炫的魔法、没有畅快的战斗、有的是比你现实世界还要苦*的生活！";

    public static string d3 = "你可以好好考虑一下你的名字，毕竟在这个世界除了努力之外这是你唯一能做的事情！";

    public static string d4 = "那么，就开始吧！";
}

