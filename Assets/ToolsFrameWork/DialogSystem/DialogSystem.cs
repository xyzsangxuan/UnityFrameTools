using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _0._1FrameWork.DialogSystem
{
    public class DialogSystem : MonoBehaviour
    {
        [Header("UI组件")]
        public Text textLabel;
        public Image faceImage;
        public float textSpeed;
    
        int index = 0;
        bool textFinished;
    
        Sprite face01, face02;
        TextAsset textfile;


        List<string> textList = new List<string>();

        public Button yes, no;

        public bool touch;
        private void OnEnable()
        {
            textfile = DialogManager.Instance.Textfile;
            face01 = DialogManager.Instance.Face01;
            face02 = DialogManager.Instance.Face02;
            GetTextFromFile(textfile);
            index = 0;
            StartCoroutine(SetTextUI());
        }

        private void Update()
        {

            if((DialogManager.Instance.isattack||(Input.GetKeyDown(KeyCode.R)))&& index >= textList.Count)
            {
                ExitDialog();

                return;
            }
            if ((DialogManager.Instance.isattack || (Input.GetKeyDown(KeyCode.R))) && textFinished)
            {
            
                StartCoroutine(SetTextUI());
            
            }

            //离开一定区域，这里是指离开触发器，也就是npc身上的button不显示，则关闭dialog，并初始化
            //清空列表，index归位
            if (!DialogManager.Instance.isButtonActive)
            {
                textList.Clear();
                index = 0;
                this.gameObject.SetActive(false);
            }
        }

    

        IEnumerator SetTextUI()
        {
            if(textfile != null)
            {
                textFinished = false;
                textLabel.text = "";

                switch (textList[index])
                {
                    case "A\r":
                        yes.gameObject.SetActive(false);
                        no.gameObject.SetActive(false);
                        faceImage.sprite = face01;
                        index++;
                        for (int i = 0; i < textList[index].Length; i++)
                        {
                            textLabel.text += textList[index][i];
                            yield return new WaitForSeconds(textSpeed);
                        }
                        textFinished = true;
                        index++;
                        break;
                    case "B\r":
                        faceImage.sprite = face02;
                        index++;
                        for (int i = 0; i < textList[index].Length; i++)
                        {
                            textLabel.text += textList[index][i];
                            yield return new WaitForSeconds(textSpeed);
                        }
                        textFinished = true;
                        index++;
                        break;
                    case "C\r":
                        index++;
                        //用委托添加
                        yes.gameObject.SetActive(true);
                        no.gameObject.SetActive(true);
                        no.onClick.AddListener(ExitDialog);
                        //yes
                        yes.onClick.RemoveAllListeners();
                        yes.onClick.AddListener(Show);

                        break;
                    case "A":
                    
                        yes.gameObject.SetActive(false);
                        no.gameObject.SetActive(false);
                        faceImage.sprite = face01;
                        index++;
                        for (int i = 0; i < textList[index].Length; i++)
                        {
                            textLabel.text += textList[index][i];
                            yield return new WaitForSeconds(textSpeed);
                        }
                        textFinished = true;
                        index++;
                        break;
                    case "B":
                        faceImage.sprite = face02;
                        index++;
                        for (int i = 0; i < textList[index].Length; i++)
                        {
                            textLabel.text += textList[index][i];
                            yield return new WaitForSeconds(textSpeed);
                        }
                        textFinished = true;
                        index++;
                        break;
                    case "C":
                        index++;
                        //用委托添加
                        yes.gameObject.SetActive(true);
                        no.gameObject.SetActive(true);
                        no.onClick.AddListener(ExitDialog);
                        //yes
                        yes.onClick.RemoveAllListeners();
                        yes.onClick.AddListener(Show);

                        break;
                    default:
                        Debug.Log("无法匹配到A或者B");
                        break;
                }
            }
       
        }


        public void Show()
        {
            ExitDialog();
            DialogManager.Instance.Show();
        }
        private void ExitDialog()
        {
            this.gameObject.SetActive(false);
            //UIManager.GetInstance().HidePanel("Dialog Panel");
            //UIManager.GetInstance().ShowPanel<DialogPanel>("Dialog Panel", E_UI_Layer.Mid, null);
            index = 0;
        }

        public void GetTextFromFile(TextAsset file)
        {
            if (file == null) return;
            textList.Clear();
            index = 0;
            var lineData =  file.text.Split('\n');

            foreach (var item in lineData)
            {
                textList.Add(item);
            }
        }

        public void PutDown()
        {
            DialogManager.Instance.isattack = true;
        }
        public void PutUp()
        {
            DialogManager.Instance.isattack = false;
        }



    }
}
