using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Rene.Xam.Extensions.Base;
using StickyNotes.Model;
using System;

namespace StickyNotes.Settings
{
    public interface IUserSettings
    {
        List<Todo> Todos { get; set; }
        int MaxId { get; set; }
    }

    public class UserSettings : UserSettingsManager, IUserSettings
    {
        #region UserSettingsProperties

        public List<Todo> Todos
        {
            get => GetSetting() != null ? JsonConvert.DeserializeObject<List<Todo>>(GetSetting() as string) : null;
            set => SetSetting(JsonConvert.SerializeObject(value));
        }

        public int MaxId
        {
            get => GetSetting() != null ? Convert.ToInt32(GetSetting()) : 0;
            set => SetSetting(value);
        }

        #endregion

        #region constructores

        public UserSettings()
        {

        }

        #endregion

        #region  Helpers


        public IEnumerable<KeyValuePair<string, string>> GetAll()
        {
            var props = typeof(UserSettings).GetProperties();

            foreach (PropertyInfo p in props)
            {
                yield return new KeyValuePair<string, string>(p.Name, p.GetValue(this, null)?.ToString());
            }
        }

        #endregion

    }
}
