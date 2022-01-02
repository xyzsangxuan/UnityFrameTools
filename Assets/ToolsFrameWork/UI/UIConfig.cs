using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// UI相关配置器
/// </summary>
public class UIConfig : Singleton<UIConfig>
{
    private Vector2 screenSize = new Vector2(750, 1334);
    /// <summary>
    /// 初始化UIcanvas、UIEventSystem、UICamera
    /// </summary>
    public void Init()
    {
        //创建Canvas 让其过场景的时候 不被移除
        UIMgr.GetInstance().UICanvas = new GameObject("UICanvas");
        UIMgr.GetInstance().UICanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        UIMgr.GetInstance().UICanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        UIMgr.GetInstance().UICanvas.GetComponent<CanvasScaler>().referenceResolution = screenSize;
        UIMgr.GetInstance().UICanvas.AddComponent<GraphicRaycaster>();

        Transform canvas = UIMgr.GetInstance().UICanvas.transform;
        canvas.gameObject.layer = LayerMask.NameToLayer("UI");

        screenSize.x = UIMgr.GetInstance().UICanvas.GetComponent<RectTransform>().sizeDelta.x;
        screenSize.y = UIMgr.GetInstance().UICanvas.GetComponent<RectTransform>().sizeDelta.y;
        GameObject.DontDestroyOnLoad(UIMgr.GetInstance().UICanvas);

        //找到各层
        RectTransform _bot = new GameObject("Bot").AddComponent<RectTransform>();
        //_bot.sizeDelta = new Vector2(Screen.width, Screen.height);
        _bot.sizeDelta = screenSize;
        _bot.anchorMin = Vector2.zero;
        _bot.anchorMax = Vector2.one;
        _bot.SetParent(canvas);
        _bot.anchoredPosition3D = Vector3.zero;

        _bot.gameObject.layer = LayerMask.NameToLayer("UI");

        RectTransform _mid = new GameObject("Mid").AddComponent<RectTransform>();
        //_mid.sizeDelta = new Vector2(Screen.width, Screen.height);
        _mid.sizeDelta = screenSize;
        _mid.anchorMin = Vector2.zero;
        _mid.anchorMax = Vector2.one;
        _mid.SetParent(canvas);
        _mid.anchoredPosition3D = Vector3.zero;
        _mid.gameObject.layer = LayerMask.NameToLayer("UI");

        RectTransform _top = new GameObject("Top").AddComponent<RectTransform>();
        //_top.sizeDelta = new Vector2(Screen.width, Screen.height);
        _top.sizeDelta = screenSize;
        _top.anchorMin = Vector2.zero;
        _top.anchorMax = Vector2.one;
        _top.SetParent(canvas);
        _top.anchoredPosition3D = Vector3.zero;
        _top.gameObject.layer = LayerMask.NameToLayer("UI");

        RectTransform _system = new GameObject("System").AddComponent<RectTransform>();
        //_system.sizeDelta = new Vector2(Screen.width, Screen.height);
        _system.sizeDelta = screenSize;
        _system.anchorMin = Vector2.zero;
        _system.anchorMax = Vector2.one;
        _system.SetParent(canvas);
        _system.anchoredPosition3D = Vector3.zero;
        _system.gameObject.layer = LayerMask.NameToLayer("UI");

        RectTransform _twice = new GameObject("Twice").AddComponent<RectTransform>();
        //_twice.sizeDelta = new Vector2(Screen.width, Screen.height);
        _twice.sizeDelta = screenSize;
        _twice.anchorMin = Vector2.zero;
        _twice.anchorMax = Vector2.one;
        _twice.SetParent(canvas);
        _twice.anchoredPosition3D = Vector3.zero;
        _twice.gameObject.layer = LayerMask.NameToLayer("UI");

        UIMgr.GetInstance().bot = canvas.Find("Bot");
        UIMgr.GetInstance().mid = canvas.Find("Mid");
        UIMgr.GetInstance().top = canvas.Find("Top");
        UIMgr.GetInstance().system = canvas.Find("System");
        UIMgr.GetInstance().twice = canvas.Find("Twice");

        //创建EventSystem 让其过场景的时候不被移除
        UIMgr.GetInstance().UIEventSystem = new GameObject("UIEventSystem");
        UIMgr.GetInstance().UIEventSystem.AddComponent<EventSystem>();
        UIMgr.GetInstance().UIEventSystem.AddComponent<StandaloneInputModule>();
        GameObject.DontDestroyOnLoad(UIMgr.GetInstance().UIEventSystem);

        //创建UICamera
        UIMgr.GetInstance().UICamera = new GameObject("UIcamera").AddComponent<Camera>();
        UIMgr.GetInstance().UICamera.clearFlags = CameraClearFlags.Depth;
        //UIMgr.GetInstance().UICamera.AddComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        //UIMgr.GetInstance().UICamera.AddComponent<Camera>().backgroundColor = Color.black;
        string[] temp = new String[1];
        temp[0] = "UI";
        UIMgr.GetInstance().UICamera.cullingMask = LayerMask.GetMask(temp);
        UIMgr.GetInstance().UICamera.orthographic = true;
        GameObject.DontDestroyOnLoad(UIMgr.GetInstance().UICamera);
        UIMgr.GetInstance().UICanvas.GetComponent<Canvas>().worldCamera = UIMgr.GetInstance().UICamera.GetComponent<Camera>();
    }

    public void SetCameraClearFlagsSolidColor()
    {
        UIMgr.GetInstance().UICamera.clearFlags = CameraClearFlags.SolidColor;
    }
    public void SetCameraClearFlagsDepthOnly()
    {
        UIMgr.GetInstance().UICamera.clearFlags = CameraClearFlags.Depth;
    }
}