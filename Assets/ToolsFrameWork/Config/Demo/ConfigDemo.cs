using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigDemo : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SkillTable.GetInstance()[1].desc);
        text.text = SkillTable.GetInstance()[1].desc;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(SkillTable.GetInstance()[1].desc);
            text.text = SkillTable.GetInstance()[1].desc;
        }
    }
}
