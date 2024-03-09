using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;     //запись и чтение xml файла
using System.IO;

[XmlRoot("dialogue")]//функция библиотеки, которая юудет читать пространство имен
public class Dialogue
{

    [XmlElement("text")]//отдельный элемент каждого элемента в классе Dialogue(пр npctext))
    public string text;

    [XmlElement("node")]//все реплики NPC
    public Node[]  nodes;

    public static Dialogue Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialogue));
        StringReader reader = new StringReader(_xml.text);
        Dialogue dial = serializer.Deserialize(reader) as Dialogue;
        return dial;
    }
}

[System.Serializable]//чтобы не было отображения в инспекторе
public class Node
{
    [XmlElement("npctext")]
    public string Npctext;

    [XmlArray("answers")]//массив ответов
    [XmlArrayItem("answer")]
    public Answer[] answers;//записываем сюда наши ответы
}

public class Answer//
{
    [XmlAttribute("tonode")]
    public int nextNode;//инфа, какая реплика npc после нашей реплики
    [XmlElement("text")]
    public string text;
    [XmlElement("dialend")]//отвечает за конец диалога
    public string end;

}
