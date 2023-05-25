using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// �t�F�[�h�C���A�E�g�̊Ǘ�
/// </summary>
public class FadeManager : MonoBehaviour
{
    /// <summary>
    /// �t�F�[�h�C���A�E�g��������I�u�W�F�N�g
    /// </summary>
    [SerializeField] private Image _fadeImage;

    /// <summary>
    /// �t�F�[�h�A�E�g�X�e�[�^�X
    /// </summary>
    private enum FADE_MODE
    {
        NONE,
        FADE_IN,
        FADE_OUT,
    }

    private FADE_MODE _fadeMode = FADE_MODE.NONE;

    /// <summary>
    /// �O������A�N�Z�X���邽��static�ɂ���
    /// </summary>
    private static FadeManager _main;

    public static FadeManager GetInstance()
    {
        return _main;
    }

    /// <summary>
    /// Start��葁�����s�����
    /// </summary>
    private void Awake()
    {
        //�F��������
        _fadeImage.color = new Color(0, 0, 0, 255);
        //�I�u�W�F�N�g������
        _fadeImage.gameObject.SetActive(true);
        _main = this;
    }



    /// <summary>
    /// �t�F�[�h�C���J�n
    /// ���񂾂�Â��Ȃ�
    /// </summary>
    public void StartFadeIn()
    {
        _fadeMode = FADE_MODE.FADE_IN;
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.gameObject.SetActive(true);
        StartCoroutine(StartFadeInMain());
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�J�n
    /// ���񂾂񖾂邭
    /// </summary>
    public void StartFadeOut(string name)
    {
        _fadeMode = FADE_MODE.FADE_OUT;
        _fadeImage.color = new Color(0, 0, 0, 0);
        _fadeImage.gameObject.SetActive(true);
        StartCoroutine(StartFadeOutMain(name));
    }

    IEnumerator StartFadeOutMain(string sceneName)
    {
        var color = _fadeImage.color;

        while (true)
        {

            //�����x��Z�����Ă���
            color.a += Time.deltaTime;
            _fadeImage.color = color;
            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                _fadeMode = FADE_MODE.NONE;
                SceneManager.LoadScene(sceneName);
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator StartFadeInMain()
    {
        var color = _fadeImage.color;

        while (true)
        {

            //�����x���グ�Ă���
            color.a -= Time.deltaTime;
            _fadeImage.color = color;

            //LogSystem.Log(" color.a " + color.a);
            if (color.a <= 0.0f)
            {
                color.a = 0.0f;
                _fadeMode = FADE_MODE.NONE;
                _fadeImage.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

}