using System.Collections;
using System.Collections.Generic;
using BrunoMikoski.TextJuicer;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    
    public List<string> Langs = new List<string>();
    public float SpeedAnim;

    public Button b_Uzb;
    public Button b_Rus;

    public RawImage RawImage;
    public RawImage RawImage_Standby;
    public VideoPlayer VideoPlayer;
    public VideoPlayer VideoPlayer_Standby;
    public List<VideoClip> VideoClips = new List<VideoClip>();
    public GameObject LangParent;
    public Image HomeParent;
    public Image HomeImage;
    public TMP_TextJuicer TextJuicer;
    
    
    private int _currentImage = 0;
    private int _currentLang;
    private Button _b_Home;
    
    void Start()
    {
        _currentLang = 1;
        VideoPlayer.loopPointReached += OnVideoFinished;
        RawImage.gameObject.SetActive(false);
        VideoPlayer.Stop();
        b_Uzb.onClick.AddListener(OnLangUzb);
        b_Rus.onClick.AddListener(OnLangRus);
        HomeParent.gameObject.SetActive(false);
        VideoPlayer_Standby.Play();
        _b_Home = HomeParent.GetComponentInChildren<Button>(true);
        _b_Home.onClick.AddListener(OnHome);
    }

   
    private void OnLangUzb()
    {
        _currentLang = 0;
        b_Rus.enabled = false;
        b_Uzb.enabled = false;
        //VideoPlayer_Standby.Pause();
        RawImage.gameObject.SetActive(true);
        RawImage.color = new Color(1f, 1f, 1f, 0f);
        HomeParent.color = new Color(1f, 1f, 1f, 0f);
        HomeImage.color = new Color(1f, 1f, 1f, 0f);
        VideoPlayer.clip = VideoClips[0];
        VideoPlayer.Play();
        HomeParent.gameObject.SetActive(true);
        StartCoroutine(StartVideo());
        b_Uzb.image.DOFade(1f, 0.3f);
        b_Uzb.image.DOFade(0f, 0.3f).SetDelay(0.3f).OnComplete(FinishStartVideo);
    }

    private void OnLangRus()
    {
        _currentLang = 1;
        b_Rus.enabled = false;
        b_Uzb.enabled = false;
        //VideoPlayer_Standby.Pause();
        RawImage.gameObject.SetActive(true);
        RawImage.color = new Color(1f, 1f, 1f, 0f);
        HomeParent.color = new Color(1f, 1f, 1f, 0f);
        HomeImage.color = new Color(1f, 1f, 1f, 0f);
        VideoPlayer.clip = VideoClips[1];
        VideoPlayer.Play();
        HomeParent.gameObject.SetActive(true);
        StartCoroutine(StartVideo());
        b_Rus.image.DOFade(1f, 0.3f);
        b_Rus.image.DOFade(0f, 0.3f).SetDelay(0.3f).OnComplete(FinishStartVideo);
    }

    private void OnHome()
    {
        _b_Home.enabled = false;
        VideoPlayer.Stop();
        VideoPlayer.frame = 0;
        
        //VideoPlayer_Standby.Play();
        _b_Home.image.DOFade(1f, 0.3f);
        _b_Home.image.DOFade(0f, 0.3f).SetDelay(0.3f);
        RawImage.DOFade(0f, 0.3f).SetDelay(0.6f).OnComplete(FinishHome);
        HomeParent.DOFade(0f, 0.3f).SetDelay(0.6f);
        HomeImage.DOFade(0f, 0.3f).SetDelay(0.6f);
    }

    private void FinishHome()
    {
        RawImage.gameObject.SetActive(false);
        HomeParent.gameObject.SetActive(false);
        _b_Home.enabled = true;
    }

    private void OnVideoFinished(VideoPlayer v)
    {
        v.frame = 0;
        RawImage.gameObject.SetActive(false);
        HomeParent.gameObject.SetActive(false);
        //VideoPlayer_Standby.Play();
    }

    private void FinishStartVideo()
    {
        RawImage.DOFade(1f, 0.3f);
        HomeParent.DOFade(1f, 0.3f);
        HomeImage.DOFade(1f, 0.3f);
        b_Rus.enabled = true;
        b_Uzb.enabled = true;
    }

    IEnumerator StartVideo()
    {
        float progress = 1f;
        while (progress>0f)
        {
            progress -= Time.deltaTime * SpeedAnim;
            TextJuicer.SetProgress(progress);
            TextJuicer.Update();
            yield return null;
        }
        TextJuicer.Text = Langs[_currentLang];
        progress = 0f;
        while (progress<1f)
        {
            progress += Time.deltaTime * SpeedAnim;
            TextJuicer.SetProgress(progress);
            TextJuicer.Update();
            yield return null;
        }
    }
}
