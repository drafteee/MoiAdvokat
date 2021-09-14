using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.BL.Interfaces
{
    public interface ILocalizationManager
    {
        string GetCountryCode();
        string GetLanguageCode();
        string GetString(string sectionName, string itemName, string defaultValue = null);
    }
}
