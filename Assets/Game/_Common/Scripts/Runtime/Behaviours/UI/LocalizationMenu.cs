using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;

namespace DanielLochner.Assets.CreatureCreator
{
    public class LocalizationMenu : MenuSingleton<LocalizationMenu>
    {
        #region Fields
        [SerializeField] private LanguageUI languagePrefab;
        [SerializeField] private RectTransform languagesRT;
        [SerializeField] private RectTransform unofficialLanguagesRT;
        [SerializeField] private GameObject disclaimer;
        [SerializeField] private List<string> officialLanguages;
        #endregion

        #region Properties
        private bool AutoDetectLanguage
        {
            get => PlayerPrefs.GetInt("AUTO_DETECT_LANGUAGE", 1) == 1;
            set => PlayerPrefs.SetInt("AUTO_DETECT_LANGUAGE", value ? 1 : 0);
        }
        #endregion

        #region Methods
        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;
            Setup();
        }

        private void Setup()
        {
            LocalizationSettings.SelectedLocaleChanged += delegate (Locale locale)
            {
                disclaimer.SetActive(!officialLanguages.Contains(locale.Identifier.Code));
            };

            if (AutoDetectLanguage)
            {
                ILocalesProvider locales = LocalizationSettings.AvailableLocales;

                Locale locale = locales.GetLocale("en");
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Chinese:
                        locale = locales.GetLocale("zh-Hans");
                        break;
                    case SystemLanguage.Russian:
                        locale = locales.GetLocale("ru");
                        break;
                    case SystemLanguage.Spanish:
                        locale = locales.GetLocale("es");
                        break;
                    case SystemLanguage.Portuguese:
                        locale = locales.GetLocale("pt-BR");
                        break;
                    case SystemLanguage.German:
                        locale = locales.GetLocale("de");
                        break;
                    case SystemLanguage.French:
                        locale = locales.GetLocale("fr");
                        break;
                    case SystemLanguage.Japanese:
                        locale = locales.GetLocale("ja");
                        break;
                    case SystemLanguage.Polish:
                        locale = locales.GetLocale("pl");
                        break;
                    case SystemLanguage.Korean:
                        locale = locales.GetLocale("ko");
                        break;
                    case SystemLanguage.Thai:
                        locale = locales.GetLocale("th");
                        break;
                    case SystemLanguage.Italian:
                        locale = locales.GetLocale("it");
                        break;
                }
                SettingsManager.Instance.SetLocale(locale.Identifier.Code);

                AutoDetectLanguage = false;
            }
            else
            {
                SettingsManager.Instance.SetLocale(SettingsManager.Data.Locale);
            }

            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                LanguageUI languageUI = Instantiate(languagePrefab, languagesRT);
                languageUI.Setup(locale);

                if (officialLanguages.Contains(locale.Identifier.Code))
                {
                    languageUI.transform.SetSiblingIndex(unofficialLanguagesRT.GetSiblingIndex());
                }
                else
                {
                    unofficialLanguagesRT.gameObject.SetActive(true);
                    languageUI.transform.SetAsLastSibling();
                }
            }
        }
        #endregion
    }
}