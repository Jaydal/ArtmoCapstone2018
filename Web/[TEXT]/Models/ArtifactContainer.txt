using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ArtmoWeb.WebApplication.Models
{
    [Serializable, XmlRoot("Artifacts")]
    public class Artifact
    {
        [XmlElement("id")]
        public string ID { get; set; }
    }
}