using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ListArtifacts : MonoBehaviour {

	// Use this for initializati
	public Transform contentPanel;
	public SimpleObjectPool btnObjectPool;
	public TMPro.TextMeshProUGUI Artifacts;
	public GameObject canvas;
	public GameObject artmo;
	private int id;
	void Start () {
        PlayerPrefs.SetString("mode", "internal");
        if (PlayerPrefs.GetString("localization")=="ilocano"){
			Artifacts.text="Dagiti Artipakto";
		}else{
			Artifacts.text="Artifacts";
		}
        id=Convert.ToInt32(PlayerPrefs.GetString("markerID"));
		Refresh();
	}
	public void Refresh(){	
		AddArtifactButtons();
	}
	private void AddArtifactButtons(){
	 ArtmoModel model=new ArtmoModel();
		List<ArtmoModel> list=ArtmoLoader.GetListByMarker(id);
		for (int i = 0; i < list.Count; i++) 
        {
		  model=list[i];
          GameObject newBtn=btnObjectPool.GetObject();

		  newBtn.transform.SetParent(contentPanel);

		  ButtonArtifacts buttonArtifacts=newBtn.GetComponent<ButtonArtifacts>();
		  
		  buttonArtifacts.Setup(model);
        }
	}
	public void ReturnARScene(){
		StartCoroutine(LoadScene("AugmentScene"));
	}
	IEnumerator LoadScene(string scene){
		AsyncOperation operation=SceneManager.LoadSceneAsync(scene);
			artmo.SetActive(true);
		canvas.SetActive(false);
		 while (!operation.isDone)
        {
            yield return null;
        }
	}
	void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) 
         ReturnARScene(); 
    }

}
