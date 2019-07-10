using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ArtmoLoader
{

    public static List<string> GetDetails(int id){
        string d="";
         if(PlayerPrefs.GetString("localization")=="ilocano"){
			d="IlocoDesc";
		}else{
			d="EngDesc";
		}
        List<string> list = new List<string>();
        XDocument oXmlDocument = null;
        if (System.IO.File.Exists(Application.persistentDataPath + "/artmo.xml")){
            oXmlDocument = XDocument.Load(Application.persistentDataPath + "/artmo.xml");
        }
        else{
            TextAsset _xml = Resources.Load("artmo") as TextAsset;
            oXmlDocument = XDocument.Parse(_xml.text);
        }
        //    XDocument oXmlDocument = XDocument.Parse(Resources.Load<TextAsset>("artmo.xml"));
            var items = (from item in oXmlDocument.Descendants("Artifact")
                         where Convert.ToInt32(item.Element("id").Value) == id
                         select new ArtmoModel
                         {
                             id = Convert.ToInt32(item.Element("id").Value),
                             Image = Convert.ToString(item.Element("Image").Value),
                             Name = Convert.ToString(item.Element("Name").Value),
                             GTerm = Convert.ToString(item.Element("GTerm").Value),
                             Donor = Convert.ToString(item.Element("Donor").Value),
                             EngDesc = Convert.ToString(item.Element(d).Value),
                             DateAcquired = Convert.ToString(item.Element("DateAcquired").Value).Replace("T00:00:00",""),
                         }).FirstOrDefault();

            if (items != null){
                list.Add(items.id.ToString());
                list.Add(items.Image);
                list.Add(items.Name);
                list.Add(items.GTerm);
                list.Add(items.EngDesc);
                list.Add(items.Donor);
                list.Add(items.DateAcquired.Replace("12:00:00 AM",""));
            }
        return list;
    }

    public static List<ArtmoModel> GetListByMarker(int marker){
  
        List<ArtmoModel> list = new List<ArtmoModel>();
         XDocument oXmlDocument = null;
        if (System.IO.File.Exists(Application.persistentDataPath + "/artmo.xml")){
            oXmlDocument = XDocument.Load(Application.persistentDataPath + "/artmo.xml");
        }
        else{
            TextAsset _xml = Resources.Load("artmo") as TextAsset;
            oXmlDocument = XDocument.Parse(_xml.text);
        }
        var x = from el in oXmlDocument.Descendants("Artifact")
        where Convert.ToInt32(el.Element("marker").Value) == marker
        select el;
            foreach (XElement el in x){
                      ArtmoModel model=new ArtmoModel();
                             model.id = Convert.ToInt32(el.Element("id").Value);
                             model.marker = Convert.ToInt32(el.Element("marker").Value);
                             model.Image = Convert.ToString(el.Element("Image").Value);
                             model.Name = Convert.ToString(el.Element("Name").Value);
                             model.GTerm = Convert.ToString(el.Element("GTerm").Value);
                             model.Donor = Convert.ToString(el.Element("Donor").Value);
                             model.EngDesc = Convert.ToString(el.Element("EngDesc").Value);
                             model.DateAcquired = Convert.ToString(el.Element("DateAcquired").Value);
                list.Add(model);
            };
        return list;
    }
}
