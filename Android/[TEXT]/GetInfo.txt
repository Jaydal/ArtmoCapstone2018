using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GetInfo : MonoBehaviour
{

    // Use this for initialization
    public TextMeshProUGUI txtGTerm;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDonor;
    public TextMeshProUGUI txtDescription;
    public Image artifactImage;
    public GameObject pnlToast;
    private string date;
    private int count=0;
	List<String> list=new List<string>();
	// String[] label=new String[]{"Native Term","English Term","Description","Donor","Date Acquired"};
    void Start()
    {
        GetArtifactInfo(PlayerPrefs.GetString("id"));
    }
    void Update(){
        count++;
        if(pnlToast.activeSelf==true){
            if(count>50){
                pnlToast.gameObject.SetActive(false);
            }
        }else{
            count=0;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
         ReturnListScene(); 
    }
    private void GetArtifactInfo(string id)
    {
        string d="";
        if(PlayerPrefs.GetString("localization")=="ilocano"){
			d="Nangited";
            date="Naala idi tawen ";
		}else{
			d="Donated by";
            date="Date Acquired ";
		}
        list = ArtmoLoader.GetDetails(Convert.ToInt32(id));
        txtGTerm.text = list[3];
        txtName.text = list[2];
       
        txtDonor.text = (list[5].Length>1)?d+" : "+list[5]:"";
        date =date+" "+list[6].Substring(0,4);
        if(date.Contains("0001")){
            date="";
        }
         txtDescription.text = "\t"+list[4]+" "+date; 
        var img = Convert.FromBase64String(list[1]);
        Texture2D text = new Texture2D(2, 2);
        text.LoadImage(img);
        Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        sprite.name = "artifactImage";
        artifactImage.GetComponent<Image>().sprite = sprite;
    }
    public void ReturnListScene(){
       StartCoroutine(LoadScene("ItemsScene"));
    }
    IEnumerator LoadScene(string scene){
		AsyncOperation operation=SceneManager.LoadSceneAsync(scene);

		 while (!operation.isDone)
        {
            yield return null;
        }
	}
    public void CopyToClipboard(){
        GUIUtility.systemCopyBuffer=txtName.text+" ("+txtGTerm.text+") >\n "+txtDescription.text+"\n"+txtDonor.text;
        pnlToast.gameObject.SetActive(!pnlToast.activeSelf);
    }
}
