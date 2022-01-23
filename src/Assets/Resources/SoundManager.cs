using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip jump;
    public static AudioClip sleep;
    public static AudioClip awake;
    public static AudioClip blacklanding;
    public static AudioClip whitelanding;
    public static AudioClip blacksnoring;
    public static AudioClip whitesnoring;
    public static AudioClip walk;
    public static AudioClip stepon;
    public static AudioClip win;
    public static AudioClip bgm;
    public static AudioSource BGM;
    private AudioSource whiteAS;
    private AudioSource blackAS;

    private Animator whiteAnime;
    private Animator blackAnime;

    private bool isJump1 = false;
    private bool isLand1 = false;
    private bool isMove1 = false;
    private bool isJK1 = false;
    private bool isJump2 = false;
    private bool isLand2 = false;
    private bool isMove2 = false;
    private bool isJK2 = false;
    private float whiteLastWalkTime = -1;
    private float whiteLastSnorTime = -1;
    private float blackLastWalkTime = -10;
    private float blackLastSnorTime = -10;
    private float bgmLastTime = -214;


    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        jump = Resources.Load<AudioClip>("jump");
        sleep = Resources.Load<AudioClip>("sleep");
        awake = Resources.Load<AudioClip>("awake");
        blacklanding = Resources.Load<AudioClip>("blacklanding");
        whitelanding = Resources.Load<AudioClip>("whitelanding");
        blacksnoring = Resources.Load<AudioClip>("blacksnoring");
        whitesnoring = Resources.Load<AudioClip>("whitesnoring");
        walk = Resources.Load<AudioClip>("walk");
        stepon = Resources.Load<AudioClip>("stepon");
        win = Resources.Load<AudioClip>("win");
        bgm = Resources.Load<AudioClip>("BGM");
        whiteAnime = GameObject.Find("whiteSlime").GetComponent<Animator>();
        blackAnime = GameObject.Find("blackSlime").GetComponent<Animator>();
        whiteAS = GameObject.Find("whiteSound").GetComponent<AudioSource>();
        blackAS = GameObject.Find("blackSound").GetComponent<AudioSource>();
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        BGMplayer();
        blackSound();
        whiteSound();
        BGMplayer();
        refreshState();
    }

    void refreshState()
    {
        isJump1 = whiteAnime.GetBool("jump");
        isLand1 = whiteAnime.GetBool("land");
        isMove1 = whiteAnime.GetBool("moveleft") || whiteAnime.GetBool("moveright");
        isJK1 = whiteAnime.GetBool("JK");
        isJump2 = blackAnime.GetBool("jump");
        isLand2 = blackAnime.GetBool("land");
        isMove2 = blackAnime.GetBool("moveleft") || blackAnime.GetBool("moveright");
        isJK2 = blackAnime.GetBool("JK");

    }

    void blackSound()
    {
        //跳
        if (!isJump2 && isLand2 && blackAnime.GetBool("jump"))
        {
            audioSrc.PlayOneShot(jump);
        }
        //落地
        if (!isLand2 && blackAnime.GetBool("land"))
        {
            if (GameObject.Find("blackSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("block")))
            {
                audioSrc.PlayOneShot(stepon);
            }
            if (GameObject.Find("blackSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("ground1")) || GameObject.Find("blackSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("ground2")))
            {
                audioSrc.PlayOneShot(blacklanding, 0.5f);
            }
        }
        //行走
        if (isLand2 && blackAnime.GetBool("land") && isMove2 && (blackAnime.GetBool("moveleft") || blackAnime.GetBool("moveright")))
        {
            if ((Time.time - blackLastWalkTime) > 1f)
            {
                // Debug.Log(4);
                blackAS.PlayOneShot(walk);
                blackLastWalkTime = Time.time;
            }
        }
        else
        {
            if (blackAS.isPlaying)
                blackAS.Stop();
        }
        //睡眠
        if (isJK2 && isLand2 && blackAnime.GetBool("JK") && blackAnime.GetBool("land"))
        {
            if ((Time.time - blackLastSnorTime) > 10f)
            {
                Debug.Log(5);
                blackAS.PlayOneShot(blacksnoring);
                blackLastSnorTime = Time.time;
            }
        }
        else
        {
            if (blackAS.isPlaying)
                blackAS.Stop();
        }
        //醒来
        if (isJK2 && isLand2 && !blackAnime.GetBool("JK"))
        {
            audioSrc.PlayOneShot(awake);
        }
        //变饼
        if (!isJK2 && !isLand2 && blackAnime.GetBool("JK") && !blackAnime.GetBool("land"))
        {
            audioSrc.PlayOneShot(stepon);
        }
    }
    void whiteSound()
    {
        //跳
        if (!isJump1 && isLand1 && whiteAnime.GetBool("jump"))
        {
            audioSrc.PlayOneShot(jump);
        }
        //落地
        if (!isLand1 && whiteAnime.GetBool("land"))
        {
            if (GameObject.Find("whiteSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("block")))
            {
                audioSrc.PlayOneShot(stepon);
            }
            if (GameObject.Find("whiteSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("ground1")) || GameObject.Find("whiteSlime").GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("ground2")))
            {
                audioSrc.PlayOneShot(whitelanding);
            }
        }
        //行走
        if (isLand1 && whiteAnime.GetBool("land") && isMove1 && (whiteAnime.GetBool("moveleft") || whiteAnime.GetBool("moveright")))
        {
            if ((Time.time - whiteLastWalkTime) > 1f)
            {
                whiteAS.PlayOneShot(walk);
                whiteLastWalkTime = Time.time;
            }
        }
        else
        {
            if (whiteAS.isPlaying)
                whiteAS.Stop();
        }
        //睡眠
        if (isJK1 && isLand1 && whiteAnime.GetBool("JK") && whiteAnime.GetBool("land"))
        {
            if ((Time.time - whiteLastSnorTime) > 10f)
            {
                whiteAS.PlayOneShot(whitesnoring, 0.5f);
                whiteLastSnorTime = Time.time;
            }
        }
        else
        {
            if (whiteAS.isPlaying)
                whiteAS.Stop();
        }
        //醒来
        if (isJK1 && isLand1 && !whiteAnime.GetBool("JK"))
        {
            audioSrc.PlayOneShot(awake);
        }
        //变饼
        if (!isJK1 && !isLand1 && whiteAnime.GetBool("JK") && !whiteAnime.GetBool("land"))
        {
            audioSrc.PlayOneShot(stepon);
        }
    }

    void BGMplayer()
    {
        if ((Time.time - bgmLastTime) > 151f)
        {
            BGM.PlayOneShot(bgm);
            bgmLastTime = Time.time;
        }
    }
}
