using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class PlayMedia : MonoBehaviour {

	// Use this for initialization
	public RawImage image;
    public Slider slider;
    public Canvas canvas;
    public Canvas c;
    int count=0;
    int ctr = 0;
    private bool isPlayAnother=false;
	private VideoPlayer videoPlayer;
	private VideoSource videoSource;
	private AudioSource audioSource;
    void Start(){
        Play(PlayerPrefs.GetString("id"));
    }
    private void Play(string id){
        Application.runInBackground=true;
		StartCoroutine(playVideo(id));
    }
    private IEnumerator playVideo(string id)
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url

        videoPlayer.source = VideoSource.VideoClip;

        // Vide clip from Url
        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";


        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        // string path=Application.streamingAssetsPath
        if (PlayerPrefs.GetString("mode") == "internal")
        {
            if (PlayerPrefs.GetString("localization") == "english")
            {
                videoPlayer.clip = Resources.Load("Multimedia/Videos/English/" + id) as VideoClip;
            }
            else
            {
                videoPlayer.clip = Resources.Load("Multimedia/Videos/Ilocano/" + id) as VideoClip;
            }
            if (videoPlayer == null)
            {
                ExternalVid(id);
            }
            else
            {
                c.gameObject.SetActive(true);
            }
        }
        else
        {
            ExternalVid(id);
        }
 
    
        videoPlayer.Prepare();
       
        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            //Prepare/Wait for 5 sceonds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }
        if(!isPlayAnother){
            videoPlayer.time=slider.value;
        }
        slider.maxValue= Mathf.FloorToInt((float)videoPlayer.clip.length)-1;
        Debug.Log("Done Preparing Video");
 
        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video

        videoPlayer.Play();
 
        //Play Sound
        audioSource.Play();
 
        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
             Debug.Log(slider.value+" - "+Mathf.FloorToInt((float)videoPlayer.time));
            slider.value=Mathf.FloorToInt((float)videoPlayer.time);
           
            // Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
    }
    void ExternalVid(string id)
    {
        videoPlayer.clip = Resources.Load("Multimedia/Videos/Ext/" + id) as VideoClip;
        if (videoPlayer.clip == null)
        {
            if (PlayerPrefs.GetString(id) != null)
            {
                Application.OpenURL(PlayerPrefs.GetString(id));
            }
            ReturnToItems();
        }
        else
        {
            c.gameObject.SetActive(false);
        }
    }
    public void VideoSlider(){
        if(Math.Abs(videoPlayer.time-slider.value)>5)
        videoPlayer.time=slider.value;
    }
    public void ReturnToItems(){
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ItemsScene");
    }
    public void PlayPause(){
        
        if(videoPlayer.isPlaying){
            videoPlayer.Pause();
        }else{
            if(slider.value==slider.maxValue){
                isPlayAnother=true;
            }else{
                isPlayAnother=false;
            }
             StopAllCoroutines();
            Play(PlayerPrefs.GetString("id"));
        }
    }
    public void Fwd(){
        if((slider.maxValue-videoPlayer.time)>1){
            videoPlayer.time+=2;
        }else{
             videoPlayer.time=(float)slider.maxValue;
        }
         slider.value=Mathf.FloorToInt((float)videoPlayer.time);
    }
    public void Bwd(){
            videoPlayer.time-=2;
             slider.value=Mathf.FloorToInt((float)videoPlayer.time);
    }

    void Update()
    {
        count++;
        if(count==150){
             canvas.gameObject.SetActive(false);
        }
        if (Input.touchCount > 0)
        {
            count=0;
            canvas.gameObject.SetActive(true);
        }
        if(!videoPlayer.isPlaying){
             canvas.gameObject.SetActive(true);
        }
         if (Input.GetKeyDown(KeyCode.Escape)) 
         ReturnToItems();
        if (slider.maxValue==slider.value)
        {
            ctr++;
        }
        if (ctr > 100)
        {
            ctr = 0;
            isPlayAnother = false;
            slider.value = 0;
            PlayerPrefs.SetString("mode", "ext");
            StopAllCoroutines();
            Play(PlayerPrefs.GetString("id"));
        }
    }
}
