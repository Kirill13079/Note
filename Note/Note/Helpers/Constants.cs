using Note.Models;
using System.Collections.ObjectModel;

namespace Note.Helpers
{
    public class Constants
    {
        public static ObservableCollection<ThemeModel> ThemeConstant { get; } = new ObservableCollection<ThemeModel>()
        {
            {
                new ThemeModel { Name = "День", Value = "light" }
            },
            {
                new ThemeModel { Name = "Ночь", Value = "dark" }
            }
        };


        public static ObservableCollection<LanguageModel> LanguageConstant { get; } = new ObservableCollection<LanguageModel>()
        { 
            { 
                new LanguageModel { Name = "English", Value = "en" }  
            },
            {
                new LanguageModel { Name = "Русский", Value = "ru" }
            }
        };
    }
}
