using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonArtifacts : MonoBehaviour
{

    // Use this for initialization
    public Button btnItem;
    public TextMeshProUGUI textItem;
    public Button btnPlay;
    private ArtmoModel artmo;
    RawImage image;
    void Start()
    {
        btnItem.onClick.AddListener(delegate { ItemClick(btnItem.name); });
        btnPlay.onClick.AddListener(delegate { PlayClick(btnPlay.name); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ItemClick(string id)
    {
        PlayerPrefs.SetString("id", id);
        SceneManager.LoadScene("InfoScene");
    }
    void PlayClick(string id)
    {
        PlayerPrefs.SetString("id", id);
        SceneManager.LoadScene("PlayMode");
    }
    public void Setup(ArtmoModel current)
    {
        artmo = current;
        btnItem.name = artmo.id.ToString();
        textItem.text = artmo.Name;
        btnPlay.name = artmo.id.ToString();
        if (PlayerPrefs.GetString("localization") == "english")
        {
            if (Resources.Load("Multimedia/Videos/English/" + artmo.id.ToString()) == null)
            {
                btnPlay.gameObject.SetActive(false);
            }
        }
        else
        {
			 if (Resources.Load("Multimedia/Videos/Ilocano/" + artmo.id.ToString()) == null)
            {
                btnPlay.gameObject.SetActive(false);
            }
        }
        if(Resources.Load("Multimedia/Videos/Ext/" + artmo.id.ToString()) != null)
        {
            btnPlay.gameObject.SetActive(true);
        }
    }
}
