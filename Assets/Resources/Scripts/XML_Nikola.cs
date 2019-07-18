﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XML_Nikola : MonoBehaviour
{

    [SerializeField]
    private TextAsset xml;
    private XmlDocument _document = new XmlDocument();

    private Dictionary<string, Vector3> dict = new Dictionary<string, Vector3>();

    private Vector3 getSizeBoxName(string boxName)
    {
        Vector3 vec = new Vector3();
        foreach (XmlNode node in _document.DocumentElement.SelectNodes("/QCARConfig/Tracking/ImageTarget"))
        {
            string name = node.Attributes["name"]?.InnerText;
            
            if (name == boxName + ".Left")
            {
                string atrSize = node.Attributes["size"]?.InnerText;
                string[] size = atrSize.Split(' ');
                vec.x = float.Parse(size[0]);
                vec.y = float.Parse(size[1]);
            }
            if (name == boxName + ".Bottom")
            {
                string atrSize = node.Attributes["size"]?.InnerText;
                string[] size = atrSize.Split(' ');
                vec.z = float.Parse(size[0]);
            }
        }
        dict.Add(boxName, vec);
        return vec;
    }

    public Vector3 getSizeByName(string name)
    {
        return dict[name];
    }
    // Start is called before the first frame update
    void Start()
    {
        _document.LoadXml(xml.ToString());
        List<string> tempName = new List<string>();
        foreach (XmlNode node in _document.DocumentElement.SelectNodes("/QCARConfig/Tracking/ImageTarget"))
        {
            string name = node.Attributes["name"]?.InnerText.Split('.')[0];
            if (!tempName.Contains(name))
            {
                tempName.Add(name);
                getSizeBoxName(name);
            }
        }

        foreach (KeyValuePair<string,Vector3> entry in dict)
        {
            Debug.Log(entry.Key);
            Debug.Log(entry.Value);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}