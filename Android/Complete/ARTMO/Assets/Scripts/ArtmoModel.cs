
using System.Xml.Serialization;

public class ArtmoModel {

	[XmlElement("id")]
	public int id { get; set; }
	[XmlElement("marker")]
	public int marker { get; set; }
	[XmlElement("Image")]
	public string Image { get; set; }
	[XmlElement("Name")]
	public string Name { get; set; }
	[XmlElement("GTerm")]
	public string GTerm { get; set; }
	[XmlElement("Donor")]
	public string Donor { get; set; }
	[XmlElement("EngDesc")]
	public string EngDesc { get; set; }
	[XmlElement("DateAcquired")]
	public string DateAcquired { get; set; }
}
