using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.BL.Interfaces
{
    public interface ILocalisationManager
    {
        string GetCountryCode();
        string GetLanguageCode();
        string GetString(string sectionName, string itemName, string assignmentType = null, string defaultValue = null);
    }
}
