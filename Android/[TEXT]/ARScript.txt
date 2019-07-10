using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class ARScript : MonoBehaviour
{

    // Use this for initialization
    // private GameObject loadedObj;
    private GameObject loadedObj;
    private GameObject obj;
    private GameObject focus;
    public TMPro.TextMeshProUGUI nodata;
    public TMPro.TextMeshProUGUI scan;
    // private string markerID;
    bool noItem=false;
    int ctr = 0;
    int tmp = 0;
    int c=-1;
    public Button btnMore;
    public Sprite newSprite;
    public UnityEngine.UI.Image img;
    public UnityEngine.UI.Image imgRotate;
    private Sprite oldSprite;
    int spriteChanger=0;
    public Button fcb;
    public Sprite focusSprite;
    public UnityEngine.UI.Image focusImg;
    int focusClick=0;
    private bool onShow=false;
    // Update is called once per frame
    void Start()
    {
        oldSprite=img.sprite;
        scan.gameObject.SetActive(true);
        if(PlayerPrefs.GetString("localization")=="iloco"){
            nodata.text="Awan iti Data";
        }
    }

    // Update is called once per frame
    void Update()
    {
        spriteChanger++;
        if(spriteChanger==50){
            imgRotate.transform.Rotate(0,0,-45);
            img.sprite=newSprite;
        }else if(spriteChanger==100){
            imgRotate.transform.Rotate(0,0,45);
            img.sprite=oldSprite;
            spriteChanger=0;
        }
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        if(!onShow){
            foreach (TrackableBehaviour tb in activeTrackables)
            {
                if (ctr == 0 && tmp == 0)
                {

                    PlayerPrefs.SetString("markerID", tb.TrackableName);
                    loadedObj = Resources.Load("Multimedia/Models/" + tb.TrackableName) as GameObject;
                    if (loadedObj == null)
                    {
                        c++;
                        GetNone3D(System.Convert.ToInt32(tb.TrackableName));
                    }
                    if (loadedObj != null)
                    {
                        obj = Instantiate(loadedObj) as GameObject;
                        obj.gameObject.AddComponent<BoxCollider>();
                        obj.gameObject.AddComponent<Lean.Touch.LeanScale>();
                        obj.gameObject.AddComponent<RotateControl>();
                        obj.transform.parent = tb.transform;
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.gameObject.SetActive(true);
                        btnMore.gameObject.SetActive(true);
                        img.gameObject.SetActive(true);
                        imgRotate.gameObject.SetActive(true);
                        fcb.gameObject.SetActive(true);
                        if (scan.gameObject.activeSelf == true)
                            scan.gameObject.SetActive(false);

                    }
                }
                ctr++;
            }
        }else{
            img.gameObject.SetActive(true);
           imgRotate.gameObject.SetActive(true);
        }
       

        if (ctr > tmp)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Input.GetMouseButton(0)) {
			if(Physics.Raycast(ray, out hit)) {
                     img.gameObject.SetActive(false);
                     imgRotate.gameObject.SetActive(false);
				}
            }
            tmp++;
           
        }
        else
        {
            //if target lost object will be destroyed
            if (ctr > 1)
            {
                if(!noItem){
                    obj.SetActive(false);
                    Destroy(obj);
                }else{
                     nodata.gameObject.SetActive(false);
                }
                  img.gameObject.SetActive(false);
                imgRotate.gameObject.SetActive(false);
            }
            // Debug.Log("LOST"+tmp+" "+ctr);
            ctr = 0;
            tmp = 0;
        }
         if (Input.GetKeyDown(KeyCode.Escape)) 
         Menu(); 
    }
    public void Menu()
    {
        SceneManager.LoadScene("TutorialMenu");
    }
    private void GetNone3D(int markerID){
        //getting base 64 image
        var item=ArtmoLoader.GetListByMarker(markerID);
       
        if(item.Count>0){
              loadedObj= GameObject.CreatePrimitive(PrimitiveType.Cube);
            noItem=false;
            for(var i=c;i<item.Count;i++){
                    ArtmoModel model=item[i];
                    if(model.Image.Length>10){
                        Debug.Log(model.Image);
                        Texture2D tex = new Texture2D(2, 2);
                        tex.LoadImage(System.Convert.FromBase64String(model.Image));
			            loadedObj.GetComponent<MeshRenderer> ().material.mainTexture=tex;
                        loadedObj.transform.Rotate(0,90,0);
                        break;
                    }
                }
                if(c+1==item.Count){
                    c=-1;
                }
        }else{
            //if there is no items in marker
             btnMore.gameObject.SetActive(false);
             nodata.gameObject.SetActive(true);
             noItem=true;
             c=-1;
        }
    }
    public void LoadItems(){
        SceneManager.LoadSceneAsync("ItemsScene");
    }
    public void Focus3D(){
        focusClick++;   
        if(focusClick%2!=0){
        if(focusClick>3){
            Destroy(focus);
        }
        loadedObj = Resources.Load("Multimedia/Models/" + PlayerPrefs.GetString("markerID")) as GameObject;
        if (loadedObj == null)
        {
                    c++;
                    GetNone3D(System.Convert.ToInt32(PlayerPrefs.GetString("markerID")));
        }
        focus = Instantiate(loadedObj) as GameObject;
        focus.gameObject.AddComponent<BoxCollider>();
        focus.gameObject.AddComponent<Lean.Touch.LeanScale>();
        focus.gameObject.AddComponent<RotateControl>();
        focus.transform.localPosition = new Vector3(0, 0, 3);
        focus.transform.localScale = new Vector3(.5f, .5f, .5f);
        focus.gameObject.SetActive(true);
        onShow=true;
        }else{
            focus.gameObject.SetActive(false);
             img.gameObject.SetActive(false);
                     imgRotate.gameObject.SetActive(false);
            onShow=false;
        }
        Sprite tmp=focusImg.sprite;
        focusImg.sprite=focusSprite;
        focusSprite=tmp;
           
    }
}
