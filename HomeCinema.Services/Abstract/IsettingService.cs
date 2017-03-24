using HomeCinema.Entities;
using HomeCinema.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Services.Abstract
{
    public partial interface ISettingService 
        //: IDisposable
    {
        Setting InsertSetting(Setting setting, string loginame = "system");

        Setting UpdateSetting(Setting setting, string loginame = "system");

        void DeleteSetting(Setting setting, string loginame = "system");

        bool DeleteSettings(IList<Setting> settings, string loginame = "system");

        Setting GetSettingById(int settingId, string loginame = "system");

        Setting GetSetting(string key, int appScope = 0, bool loadSharedValueIfNotFound = false);

        T GetSettingByKey<T>(string key, T defaultValue = default(T), int storeId = 0, bool loadSharedValueIfNotFound = false);
        dynamic GetSettingByKey<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, int appScope = 0) where T : ISettings, new();

        void SetSetting<T>(string key, T value, string description, int appScope = 0, string loginame = "system");

        IList<Setting> GetAllSettings();

        bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, int appScope = 0) where T : ISettings, new();

        T LoadSetting<T>(int storeId = 0) where T : ISettings, new();

        void SaveSetting<T>(T settings, int appScope = 0, string loginame = "system") where T : ISettings, new();

        void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, int appScope = 0, string loginame = "system") where T : ISettings, new();

        void SaveSettingOverridablePerStore<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, bool overrideForStore, int appScope = 0, string loginame = "system") where T : ISettings, new();

        void DeleteSetting<T>() where T : ISettings, new();

        void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, int storeId = 0) where T : ISettings, new();
    }
}
