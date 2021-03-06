using LawyerService.BL.Helpers;
using LawyerService.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;

namespace LawyerService.BL
{
    public class LocalizationManager : ILocalizationManager
    { 
    private readonly IMemoryCacheManager _memoryCacheManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger _logger;

    private const string _fileNameFormat = "{0}-{1}.localisation.json";
    private const string _defaultCountryCode = "eng";
    private const string _defaultLanguageCode = "eng";

    private readonly string _countryCode;
    private readonly string _languageCode;

    public LocalizationManager(IMemoryCacheManager memoryCacheManager, IHttpContextAccessor httpContextAccessor, ILogger<LocalizationManager> logger)
    {
        _memoryCacheManager = memoryCacheManager;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

        _countryCode = GetCountryCode();
        _languageCode = GetLanguageCode();
    }

    public string GetCountryCode()
    {
        if (string.IsNullOrEmpty(_countryCode))
        {
            var code = _defaultCountryCode;

            try
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var assignmentType = request?.Query["assignmentType"];
                var countryCode = request?.Query["countryCode"];

                if (!string.IsNullOrEmpty(assignmentType))
                {
                }
                else if (!string.IsNullOrEmpty(countryCode))
                {
                    code = countryCode;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get country code");
            }

            return code;
        }
        else
        {
            return _countryCode;
        }
    }

    public string GetLanguageCode()
    {
        if (string.IsNullOrEmpty(_languageCode))
        {
            return _defaultLanguageCode;//TODO check userContext
        }
        else
        {
            return _languageCode;
        }
    }

    public string GetString(string sectionName, string itemName, string defaultValue = null)
    {
        string result = null;

        try
        {
            result = GetValue(_countryCode, _languageCode, sectionName, itemName);
        }
        catch
        {
            //ignore
        }

        try
        {
            if (string.IsNullOrEmpty(result) && $"{_countryCode}-{_languageCode}" != $"{_defaultCountryCode}-{_defaultLanguageCode}")
            {
                result = GetValue(_defaultCountryCode, _defaultLanguageCode, sectionName, itemName);
            }
        }
        catch
        {
            //ignore
        }

        if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(defaultValue))
        {
            result = defaultValue;
        }

        if (string.IsNullOrEmpty(result))
        {
            result = $"[Value for key {itemName} wasn't found]";
        }

        return result;
    }

    private string GetValue(string countryCode, string languageCode, string sectionName, string itemName)
    {
        var fileName = string.Format(_fileNameFormat, countryCode, languageCode);
        var cachedObject = GetOrAddJsonObject(fileName);

        return GetFromJsonObject(cachedObject, sectionName, itemName);
    }

        private JObject GetOrAddJsonObject(string fileName)
        {

            return _memoryCacheManager.GetOrAdd(_memoryCacheManager.GetCacheKey(nameof(LocalizationManager), fileName),
             () =>
             {
                 var resource = ResourceHelper.ReadManifestDataFromAssembly(typeof(Resources.Resources).Assembly.FullName, fileName);
                 return JObject.Parse(resource);
             });
        }

    private string GetFromJsonObject(JObject jObject, string sectionName, string itemName)
    {
        if (string.IsNullOrEmpty(sectionName) || string.IsNullOrEmpty(itemName))
        {
            return null;
        }

        JToken token;

        token = jObject?.SelectToken($"$.{sectionName}.{itemName}._");
        if (token != null)
        {
            return (string)token;
        }

        token = jObject?.SelectToken($"$.{sectionName}.{itemName}");
        if (token != null)
        {
            return (string)token;
        }

        return null;
    }
}
}
