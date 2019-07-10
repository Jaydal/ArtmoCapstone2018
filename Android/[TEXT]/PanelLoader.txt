using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLoader : MonoBehaviour {

	// Use this for initialization
	public GameObject PanelSettings;
	public GameObject PanelMap;
	public GameObject PanelInfo;
	public GameObject Canvas;
	public GameObject ExitPanel;
	public TMPro.TextMeshProUGUI TxtStart;
	public TMPro.TextMeshProUGUI localization;
	public TMPro.TextMeshProUGUI LocaLabel;
	public TMPro.TextMeshProUGUI UpdateLabel;
	public TMPro.TextMeshProUGUI downloadLabel;
	public TMPro.TextMeshProUGUI Settings;
	public TMPro.TextMeshProUGUI About;
	public TMPro.TextMeshProUGUI Contact;
	public TMPro.TextMeshProUGUI Behind;
	public TMPro.TextMeshProUGUI Before;
	public TMPro.TextMeshProUGUI Yes;
	public TMPro.TextMeshProUGUI No;
	public TMPro.TextMeshProUGUI Sure;
	public TMPro.TextMeshProUGUI floor;
	public GameObject ArtmoLoader;
	public GameObject Content;
	public Button btnNext;
	private Sprite sp1;
	public Sprite sp2;
	public Sprite sp3;
	public Sprite sp4;
	public Sprite map1;
	private Sprite map2;
	public Image  Map;
	public Image  Tuts;
	public Slider slider;
	int current=1;
	// int c=0;
	private bool isDownload = false;
	void Start(){
		if(PlayerPrefs.GetString("localization")==null){
			PlayerPrefs.SetString("localization","english");
		}
        Register();
        Localization();
		sp1=Tuts.sprite;
		map2=Map.sprite;
	}
	void Update(){

	
		
	}
	public void LoadSettings(){
		ShowHidePanel(PanelSettings,PanelMap,PanelInfo);
	}
	public void LoadMap(){
		ShowHidePanel(PanelMap,PanelInfo,PanelSettings);
	}
	public void LoadInfo(){
		ShowHidePanel(PanelInfo,PanelSettings,PanelMap);
	}
	public void StartAR(){
		Canvas.SetActive(false);

		 StartCoroutine(LoadAsyncScene());
	}
		public void Exit(){
			ExitPanel.SetActive(!ExitPanel.activeSelf);
		// Application.Quit();
	}
	public void ExitARTMO(){
		Application.Quit();
	}
	private void ShowHidePanel(GameObject ToShow,GameObject ToHide1,GameObject ToHide2){
		ToShow.SetActive(!ToShow.activeSelf);
		ToHide1.SetActive(false);
		ToHide2.SetActive(false);
	}
	IEnumerator DownloadData () {
	
        string file_name="artmo";
        string url = "http://www.nvartmo.com/Artmo/DownloadFile";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
			isDownload = true;
            StartCoroutine(progress(www));
            yield return www.SendWebRequest();
			isDownload = false;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
				downloadLabel.text="ERROR";
            }
            else
            {
				if(PlayerPrefs.GetString("localization")=="ilocano"){
				downloadLabel.text="Nalpasen iti panagdownload";
				}else{
					downloadLabel.text="Dowload Done";
				}
                string savePath = string.Format("{0}/{1}.xml", Application.persistentDataPath, file_name);
                System.IO.File.WriteAllText(savePath, www.downloadHandler.text);
            }
			
        }
		yield return null;
    }
	IEnumerator progress(UnityWebRequest req)
    {
        while (isDownload)
        {
			if(PlayerPrefs.GetString("localization")=="ilocano"){
			 downloadLabel.text=("Agdowndownload : " + req.downloadProgress * 100 + "%");
			}else{
			 downloadLabel.text=("Downloading : " + req.downloadProgress * 100 + "%");
			}
           
            yield return new WaitForSeconds(0.1f);
        }
    }

	public void DL(){
		StopAllCoroutines();
		StartCoroutine(DownloadData());
	}
	private void Localization(){
		if(PlayerPrefs.GetString("localization")=="ilocano"){
			localization.text="ilocano";
			TxtStart.text="Irugi";
			LocaLabel.text="Lokalisasion";
			UpdateLabel.text="Agupdate";
			downloadLabel.text="Agownload iti Data";
			About.text="Maipapan";
			Behind.text="Likud ti ARTMO";
			Contact.text="Makiuman kadakami : nvartmo@gmail.com";
			Settings.text="Dagiti Setting";
			Before.text="Sakbay Mo Nga Irugi";
			Yes.text="Wen";
			No.text="Madi";
			Sure.text="<b>Paysu?</b> \n Kayat mo ba isara daytoy ARTMO?";
		}else{
			localization.text="english";
			TxtStart.text="Start";
			LocaLabel.text="Localization";
			UpdateLabel.text="Update";
			downloadLabel.text="Download Data";
			About.text="About";
			Behind.text="Behind ARTMO";
			Contact.text="Contact us : nvartmo@gmail.com";
			Settings.text="Settings";
			Before.text="Before You Start";
			Yes.text="Yes";
			No.text="No";
			Sure.text="<b>Are You Sure?</b> \n Do you want to exit ARTMO?";
		}
		PlayerPrefs.SetString("localization",localization.text);	
	}
	public void LocalSwitch(){
		if(localization.text=="ilocano"){
			PlayerPrefs.SetString("localization","english");	
		}else{
			PlayerPrefs.SetString("localization","ilocano");	
		}
		Localization();
	}
	IEnumerator LoadAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AugmentScene");
		ArtmoLoader.SetActive(true);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
	public void NextTuts(){
		if(current==1){
			Tuts.sprite=sp2;
			current=2;
		}else if(current==2){
			Tuts.sprite=sp3;
			current=3;
		}else if(current==3){
			Tuts.sprite=sp4;
			current=4;
		}else if(current==4){
			Tuts.sprite=sp1;
			current=1;
		}
	}
	public void MapView(){
		floor.text=(floor.text=="ground floor")?"2nd floor":"ground floor";
		Map.sprite=(floor.text=="ground floor")?map2:map1;
	}
	public void MapZoom(){
		if(slider.value==5){
			Content.gameObject.transform.localScale=new Vector3(1,1,1);
		}else if(slider.value==4){
			Content.gameObject.transform.localScale=new Vector3(1.5f,1.5f,1.5f);
		}else if(slider.value==3){
			Content.gameObject.transform.localScale=new Vector3(1.8f,1.8f,1.8f);
		}else if(slider.value==2){
			Content.gameObject.transform.localScale=new Vector3(2,2,2);
		}else{
			Content.gameObject.transform.localScale=new Vector3(2.5f,2.5f,2.5f);
		}
	}
    void Register()
    {
        PlayerPrefs.SetString("181", "https://www.youtube.com/watch?v=6z1H884LIGs");
        PlayerPrefs.SetString("147", "https://www.youtube.com/watch?v=RdX-iitwFwI");
        PlayerPrefs.SetString("173", "https://www.youtube.com/watch?v=2Wgu5hnrAnI");
        PlayerPrefs.SetString("174", "https://www.youtube.com/watch?v=8OsFqcYcT1k");
        PlayerPrefs.SetString("157", "https://www.youtube.com/watch?v=V_AaVnx-StQ");
        PlayerPrefs.SetString("203", "https://www.youtube.com/watch?v=V_AaVnx-StQ");
        PlayerPrefs.SetString("136", "https://youtu.be/tPSbbgLopO8");
        PlayerPrefs.SetString("27", "https://www.youtube.com/watch?v=Avs_0CDk_Kg");
        PlayerPrefs.SetString("97", "https://www.youtube.com/watch?v=3FDKu2xlNR8");
        PlayerPrefs.SetString("23", "https://www.youtube.com/watch?v=jWuyFUvv7xQ");
        PlayerPrefs.SetString("105", "https://www.youtube.com/watch?v=3xQY_Ic_Rdo");
        PlayerPrefs.SetString("104", "https://www.youtube.com/watch?v=srW1ZMb3wqA");
        PlayerPrefs.SetString("93", "https://www.youtube.com/watch?v=3wHAwj6izCg");
        PlayerPrefs.SetString("92", "https://www.youtube.com/watch?v=zWDa6XrVyos");
        PlayerPrefs.SetString("87", "https://www.youtube.com/watch?v=M0oiBV6N71g");
        PlayerPrefs.SetString("55", "https://www.youtube.com/watch?v=L4Y_mBKxSD8");
        PlayerPrefs.SetString("56", "https://www.youtube.com/watch?v=mrgaXgsFgGE");
        PlayerPrefs.SetString("19", "https://www.youtube.com/watch?v=X_6TY1SbpZI");
        PlayerPrefs.SetString("199", "https://www.youtube.com/watch?v=oQwRjZByaok");
        PlayerPrefs.SetString("129", "https://www.youtube.com/watch?v=thwNiSlMM6I");
        PlayerPrefs.SetString("126", "https://www.youtube.com/watch?v=Z1GbyKFuFtw");
    }
}
