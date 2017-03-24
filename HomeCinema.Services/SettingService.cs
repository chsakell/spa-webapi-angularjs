using AutoMapper;
using HomeCinema.Data.Abstract;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using HomeCinema.Entities.Settings;
using HomeCinema.Services.Abstract;
using HomeCinema.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace HomeCinema.Services
{
    public partial class SettingService : ISettingService
    {
        private readonly IEntityBaseRepository<Setting> _settingRepository;
        private readonly IUnitOfWork _unitOfWork;


        #region Constructor

        public SettingService(IEntityBaseRepository<Setting> settingRepository, IUnitOfWork unitOfWork)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion Constructor

        #region Settings CRUD

        /// <summary>
        /// Insert a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="loginame">User Who did this</param>
        public Setting InsertSetting(Setting setting, string loginame = "system")
        {
                if (setting == null)
                    throw new ArgumentNullException("setting");
                setting.Name = setting.Name.ToLowerInvariant();
                setting.CreatedAt = DateTime.Now;
                setting.UpdatedAt = DateTime.Now;
                setting.CreatedBy = loginame;
                setting.UpdatedBy = loginame;
            _settingRepository.Add(setting);
            _unitOfWork.Commit();
            return setting;
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="loginame">User Who did this</param>
        public Setting UpdateSetting(Setting setting, string loginame = "system")
        {
            //var settingDb = _settingRepository.GetSingle(setting.ID);
            if (setting == null)
                throw new ArgumentNullException("setting");
            else
            {

                //movieDb.UpdateMovie(movie);

                //settingDb = Mapper.Map<Setting, Setting>(setting);
                setting.UpdatedAt = DateTime.Now;
                setting.UpdatedBy = loginame;
                _settingRepository.Edit(setting);
                _unitOfWork.Commit();

                return setting;
            }
        }

        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        public void DeleteSetting(Setting setting, string loginame = "system")
        {
                if (setting == null)
                    throw new ArgumentNullException("setting");
            _settingRepository.Delete(setting);
        }

        /// <summary>
        /// Deletes settings
        /// </summary>
        /// <param name="settings">Settings</param>
        public bool DeleteSettings(IList<Setting> settings, string loginame = "system")
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            _settingRepository.Delete(settings);
            return true;

        }

        /// <summary>
        /// Gets a setting by identifier
        /// </summary>
        /// <param name="settingId">Setting identifier</param>
        /// <returns>Setting</returns>
        public Setting GetSettingById(int settingId, string loginame = "system")
        {
            return _settingRepository.GetById(settingId);
            //try
            //{
            //    if (settingId == 0)
            //        return null;
            //    var query = from s in dataContext.Setting
            //                where s.Id == settingId
            //                select s;
            //    return query.FirstOrDefault();
            //}
            //catch (Exception ex)
            //{
            //    srvException.CreateException(ex, "GetSettingById", loginame);
            //    return null;
            //}
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="AppScope">Id Societe</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>Setting</returns>
        public virtual Setting GetSetting(string key, int AppScope = 0, bool loadSharedValueIfNotFound = false)
        {
            try
            {
                if (String.IsNullOrEmpty(key))
                    return null;

                key = key.Trim().ToLowerInvariant();
                if (_settingRepository.Any(s => s.Name == key))
                {
                    var settingsByKey = _settingRepository.FindBy(t => t.Name == key);

                    var setting = settingsByKey.FirstOrDefault(x => x.AppScope == AppScope);

                    //load shared value?
                    if (setting == null && AppScope > 0 && loadSharedValueIfNotFound)
                        setting = settingsByKey.FirstOrDefault(x => x.AppScope == 0);

                    if (setting != null)
                        return setting;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //srvException.CreateException(ex, "GetSetting", "system");
                return null;
            }
        }

        /// <summary>
        /// Get setting value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="AppScope">Id Societe</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>Setting value</returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T),
            int AppScope = 0, bool loadSharedValueIfNotFound = false)
        {
            try
            {
                if (String.IsNullOrEmpty(key))
                    return defaultValue;

                key = key.Trim().ToLowerInvariant();
                if (_settingRepository.Any(s => s.Name == key))
                {
                    var settingsByKey = _settingRepository.FindBy(t => t.Name == key);

                    var setting = settingsByKey.FirstOrDefault(x => x.AppScope == AppScope);

                    //load shared value?
                    if (setting == null && AppScope > 0 && loadSharedValueIfNotFound)
                        setting = settingsByKey.FirstOrDefault(x => x.AppScope == 0);

                    if (setting != null)
                        return CommonHelper.To<T>(setting.Value);
                }

                return defaultValue;
            }
            catch (Exception ex)
            {
                throw ex;
                //srvException.CreateException(ex, "GetSettingByKey", "system");
                return defaultValue;
            }
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="AppScope">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void SetSetting<T>(string key, T value, string description, int AppScope = 0, string loginame = "system")
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (description == null)
                    description = string.Empty;
                key = key.Trim().ToLowerInvariant();
                string valueStr = CommonHelper.GetNopCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);

                var settingsByKey = _settingRepository.FindBy(t => t.Name == key);

                var setting = settingsByKey.FirstOrDefault(x => x.AppScope == AppScope);
                if (setting != null)
                {
                    //update
                    setting.Value = valueStr;
                    setting.Description = description;
                    UpdateSetting(setting, loginame);
                }
                else
                {
                    //insert
                    var newsetting = new Setting
                    {
                        Name = key,
                        Value = valueStr,
                        Description = description,
                        AppScope = AppScope
                    };
                    InsertSetting(newsetting, loginame);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //srvException.CreateException(ex, "SetSetting", "system");
            }
        }

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        public virtual IList<Setting> GetAllSettings()
        {
            try
            {
                var query = from s in _settingRepository.All
                            orderby s.Name, s.AppScope
                            select s;
                var settings = query.ToList();
                return settings;
            }
            catch (Exception ex)
            {
                //srvException.CreateException(ex, "GetAllSettings", "system");
                throw ex;
                return new List<Setting>();
            }
        }

        /// <summary>
        /// Determines whether a setting exists
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="AppScope">AppScope</param>
        /// <returns>true -setting exists; false - does not exist</returns>
        public virtual bool SettingExists<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int AppScope = 0)
            where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);
            var setting = GetSettingByKey<string>(key, AppScope: AppScope);
            return setting != null;
        }
        /// <summary>
        /// Determines whether a setting exists
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="AppScope">AppScope</param>
        /// <returns>true -setting exists; false - does not exist</returns>
        public virtual dynamic GetSettingByKey<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int AppScope = 0)
            where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);
            var setting = GetSettingByKey<string>(key, AppScope: AppScope);
            return setting;
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="AppScope">Store identifier for which settigns should be loaded</param>
        public virtual T LoadSetting<T>(int AppScope = 0) where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                //load by store
                var setting = GetSettingByKey<string>(key, AppScope: AppScope, loadSharedValueIfNotFound: true);
                if (setting == null)
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).IsValid(setting))
                    continue;

                object value = CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="AppScope">Store identifier</param>
        /// <param name="settings">Setting instance</param>
        public virtual void SaveSetting<T>(T settings, int AppScope = 0, string loginame = "system") where T : ISettings, new()
        {
            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared
             * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That's why we're using dynamic type
                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value, "", AppScope, loginame);
                else
                    SetSetting(key, "", "", AppScope, loginame);
            }

            //and now clear cache
        }

        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="AppScope">Store ID</param>
        /// <param name="loginame">A value indicating whether to clear cache after setting update</param>
        public virtual void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            int AppScope = 0, string loginame = "system") where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            string key = settings.GetSettingKey(keySelector);
            //Duck typing is not supported in C#. That's why we're using dynamic type
            dynamic value = propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value, "", AppScope, loginame);
            else
                SetSetting(key, "", "", AppScope, loginame);
        }

        /// <summary>
        /// Save settings object (per store). If the setting is not overridden per storem then it'll be delete
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="overrideForStore">A value indicating whether to setting is overridden in some store</param>
        /// <param name="AppScope">Store ID</param>
        /// <param name="loginame">A value indicating whether to clear cache after setting update</param>
        public virtual void SaveSettingOverridablePerStore<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            bool overrideForStore, int AppScope = 0, string loginame = "system") where T : ISettings, new()
        {
            if (overrideForStore || AppScope == 0)
                SaveSetting(settings, keySelector, AppScope, loginame);
            else if (AppScope > 0)
                DeleteSetting(settings, keySelector, AppScope);
        }

        /// <summary>
        /// Delete all settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public virtual void DeleteSetting<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                string key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            DeleteSettings(settingsToDelete);
        }

        /// <summary>
        /// Delete settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="AppScope">Store ID</param>
        public virtual void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int AppScope = 0) where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);
            key = key.Trim().ToLowerInvariant();
            var settingForCaching = _settingRepository.GetSingle(t => t.AppScope == AppScope && t.Name == key);
                //(from s in dataContext.Setting
                //                     where s.Name == key && s.AppScope == AppScope
                //                     select s).FirstOrDefault();
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.ID);
                DeleteSetting(setting);
            }
        }

        #endregion Settings CRUD

        //#region Dispose

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            dataContext.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //#endregion Dispose
    }
}
