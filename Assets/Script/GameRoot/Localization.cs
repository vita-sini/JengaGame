using System.Collections.Generic;
using UnityEngine;

public static class LocalizationManager
{
    public static string CurrentLanguage { get; private set; } = "ru"; // язык по умолчанию

    private static Dictionary<string, Dictionary<string, string>> _texts = new Dictionary<string, Dictionary<string, string>>
    {
        {
            "ru", new Dictionary<string, string>
            {
                { "WindCard", "Торнадо!!!" },
                { "EarthquakeCard", "Дрожь земли!" },
                { "GlitchCard", "Случайные блоки зависают и не поддаются извлечению" },
                { "RotatingPlatformCard", "Башня начинает крутиться" },
                { "MagneticCard", "Блоки притягиваются друг к другу" },
                { "GhostCard", "Блоки становятся невидимыми" },
                { "HeavyCard", "Блоки становятся тяжёлыми" },
                { "SlipperyCard", "Блоки становятся скользкими" },
                { "ExplosiveCard", "Один блок взорвется" },
                { "SmokeCard", "Стало дымно" },
                { "Score", "Очки: " },
                { "The light block", "Светлый блок: " }
            }
        },
        {
            "en", new Dictionary<string, string>
            {
                { "WindCard", "Tornado!!!" },
                { "EarthquakeCard", "Earthquake!" },
                { "GlitchCard", "Random blocks freeze and can't be removed" },
                { "RotatingPlatformCard", "The tower starts spinning" },
                { "MagneticCard", "Blocks attract each other" },
                { "GhostCard", "Blocks become invisible" },
                { "HeavyCard", "Blocks become heavy" },
                { "SlipperyCard", "Blocks become slippery" },
                { "ExplosiveCard", "One block will explode" },
                { "SmokeCard", "It's smoky now" },
                { "Score", "Score: " },
                { "The light block", "The light block: " }
            }
        },
        {
            "tr", new Dictionary<string, string>
            {
                { "WindCard", "Kasırga!!!" },
                { "EarthquakeCard", "Deprem!" },
                { "GlitchCard", "Rastgele bloklar donuyor ve çıkarılamıyor" },
                { "RotatingPlatformCard", "Kule dönmeye başlıyor" },
                { "MagneticCard", "Bloklar birbirine çekiliyor" },
                { "GhostCard", "Bloklar görünmez hale geliyor" },
                { "HeavyCard", "Bloklar ağırlaşıyor" },
                { "SlipperyCard", "Bloklar kaygan hale geliyor" },
                { "ExplosiveCard", "Bir blok patlayacak" },
                { "SmokeCard", "Her yer duman oldu" },
                 { "Score", "Puan: " }
            }
        }
    };

    public static void SetLanguage(string langCode)
    {
        if (_texts.ContainsKey(langCode))
            CurrentLanguage = langCode;
        else
            CurrentLanguage = "en";
    }

    public static string GetText(string key)
    {
        if (_texts.TryGetValue(CurrentLanguage, out var langDict))
            if (langDict.TryGetValue(key, out var text))
                return text;

        return key;
    }
}
