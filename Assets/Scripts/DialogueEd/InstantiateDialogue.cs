using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateDialogue : MonoBehaviour
{
    public GameObject Window;


    public Text text;//переменные UI
    public Text firstAnswer;
    public Text secondAnswer;
    public Text thirdAnswer;
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;

    bool dialogueEnded = false;

    GameObject player;
    public TextAsset ta;//xml файл

    [SerializeField]
    public int currentNode = 0;
    public int butClicked;//инфа о том, какая из кнопок нажата
    bool textSet = false;
    Node[] nd;//сюда записываются ответы NPC
    Dialogue dialogue;

    void Start()
    {//получаем кнопки и делаем их неактивными

        secondButton.enabled = false;
        thirdButton.enabled = false;
        Window.SetActive(false);
        player = GameObject.Find("Player");
        dialogue = Dialogue.Load(ta);//загрузка xml файла
        nd = dialogue.nodes;


        text.text = nd[currentNode].Npctext;
        firstAnswer.text = nd[currentNode].answers[currentNode].text;

        firstButton.onClick.AddListener(but1);
        secondButton.onClick.AddListener(but2);
        thirdButton.onClick.AddListener(but3);

        AnswerClicked(14);  //14 - для присвоения начальных значений в диалоге что бы не создавать новую функцию
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5f && dialogueEnded == false)//проверяем дистанцию
        {
            Window.SetActive(true);
        }
        else
        {
                Window.SetActive(false);

        }
            
    }

    private void but1()
    {
        butClicked = 0;
        AnswerClicked(0);
    }
    private void but2()
    {
        butClicked = 1;
        AnswerClicked(1);
    }
    private void but3()
    {
        butClicked = 2;
        AnswerClicked(2);
    }


    public void AnswerClicked(int numberOfButton)
    {

        if (numberOfButton == 14)
            currentNode = 0;
        else
        {
            if (dialogue.nodes[currentNode].answers[numberOfButton].end == "true")
            {
                dialogueEnded = true;
            }
                
           currentNode = dialogue.nodes[currentNode].answers[numberOfButton].nextNode;
        }

        

        text.text = dialogue.nodes[currentNode].Npctext;

        firstAnswer.text = dialogue.nodes[currentNode].answers[0].text;
        if (dialogue.nodes[currentNode].answers.Length == 2)
        {
            secondButton.enabled = true;
            secondAnswer.text = dialogue.nodes[currentNode].answers[1].text;
        }
        else {
            secondButton.enabled = false;
            secondAnswer.text = "";
        }

        if (dialogue.nodes[currentNode].answers.Length == 3)
        {
            thirdButton.enabled = true;
            thirdAnswer.text = dialogue.nodes[currentNode].answers[2].text;
        }
        else {
            thirdButton.enabled = false;
            thirdAnswer.text = "";
        }


    }

}
