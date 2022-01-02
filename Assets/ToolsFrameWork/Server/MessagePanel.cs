using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    Text mes;
    private void Start()
    {
        mes = this.GetControl<Text>(PathCfg.TEXT_MESSAGE_PROMPT);
        Button btn = this.GetControl<Button>(PathCfg.BUTTON_MESSAGE_CLOSE);

        btn.onClick.AddListener(MessageClose);     
  
    }

    public IEnumerator MessageShow(string message)
    {
        yield return null;
        if (mes!=null)
        {
            mes.DOText(message,PathCfg.TIME_UI_TEXTING).SetEase(Ease.Linear);

        }
        else
        {
            Debug.Log("没有找到："+ PathCfg.TEXT_MESSAGE_PROMPT);
        }
       
    }

    public void MessageClose()
    {
        UIManager.GetInstance().HidePanel(PathCfg.PREFAB_PANEL_MESSAGE);
    }
}
