using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using Deniverse.UnityLocalizationSample.Presentation.Presenter;

public class Option : MonoBehaviour
{
    int selectedLanguageNo;

    [SerializeField]
    private List<Button> buttonList;

    // Start is called before the first frame update
    void Start()
    {
        //LogSystem.isdebug = true;
        selectedLanguageNo = 0;
        for (int i = 0; i < buttonList.Count; i++)
        {
            int no = i;
            buttonList[i].onClick.AddListener(() =>
            {
                selectedLanguageNo = no;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[no];
                ES3.Save<int>(SaveLoadManager.LANGUAGE, no);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClickLanguage(int no)
    {

    }


}
