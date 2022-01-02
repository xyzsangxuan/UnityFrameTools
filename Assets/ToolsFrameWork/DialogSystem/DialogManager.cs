using UnityEngine;
using UnityEngine.Events;

namespace _0._1FrameWork.DialogSystem
{
    public class DialogManager : MonoSingleton<DialogManager>
    {
    

        [Header("运行时分配")]
        public GameObject Dialog;
        public GameObject DialogMachine;
        TextAsset textfile;

        Sprite face01, face02;
        public UnityAction unityAction;
        public bool isattack;
        public bool isButtonActive;

        public delegate void DialogDelegate();
        public DialogDelegate dialogDelegate;
        public void SetDialogInfo(TextAsset file,Sprite f01, Sprite f02,DialogDelegate _dialogDelegate)
        {
            textfile = file;
            face01 = f01;
            face02 = f02;
            dialogDelegate = _dialogDelegate;
            if(Dialog!=null)
                Dialog.SetActive(true);
        }

        public void Show()
        {
            dialogDelegate();
        }

        public TextAsset Textfile
        {
            get { return textfile; }
        }

        public Sprite Face01
        {
            get { return face01; }
        }

        public Sprite Face02
        {
            get { return face02; }
        }

        public void SetDefaultDialogUI()
        {
            Dialog = DialogMachine;
        }

    }
}
