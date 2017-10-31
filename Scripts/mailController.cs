using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization; //Needed for XML Functionality
using System.Text;
using System.IO;
using System;
using System.Linq;
public class mailController : MonoBehaviour {
	
	public static TextAsset XML_Mail;
	public static string from;
	public static string to;
	public static string content;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static ArrayList getMail(int id) {
		XML_Mail = (TextAsset) Resources.Load("XML_Mail");
		//Debug.Log(XML_Mail);
		string Xmltext = XML_Mail.text;
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(Xmltext); // load the file.
		XmlNodeList mailsList = xmlDoc.GetElementsByTagName("Mail"); // array of the level nodes.
		ArrayList ar = new ArrayList();
		
			
		foreach (XmlNode mail in mailsList) {
			XmlNodeList mailContent = mail.ChildNodes;
			int idMail = Int32.Parse(mail.Attributes.GetNamedItem("id").Value);
			if(idMail == id){
				content = mail.InnerText;
				from = mail.Attributes.GetNamedItem("from").Value;
				to = mail.Attributes.GetNamedItem("object").Value;
				ar.Add(from);
				ar.Add(to);
				ar.Add(content);
			}
		}
		return ar;
	}
}
