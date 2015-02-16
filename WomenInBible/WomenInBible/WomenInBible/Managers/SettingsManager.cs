﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WomenInBible.Managers
{
    public class SettingsManager
    {
        public static ISettings AppSettings
        {
            get
            {
                return DependencyService.Get<ISettings>();
            }
        }

        #region Setting Constants

        public const string SettingsKey = "settings_key";
        private readonly string SettingsDefault = string.Empty;

        #endregion

        public Guid GuidSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("guid_setting", Guid.Empty);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("guid_setting", value);
            }
        }

        public decimal DecimalSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("decimal_setting", (decimal)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("decimal_setting", value);
            }
        }

        public int IntSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("int_setting", (int)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("int_setting", value);
            }
        }

        public float FloatSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("float_setting", (float)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("float_setting", value);
            }
        }

        public Int64 Int64Setting
        {
            get
            {
                return AppSettings.GetValueOrDefault("int64_setting", (Int64)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("int64_setting", value);
            }
        }

        public Int32 Int32Setting
        {
            get
            {
                return AppSettings.GetValueOrDefault("int32_setting", (Int32)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("int32_setting", value);
            }
        }

        public DateTime? DateTimeSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault<DateTime?>("date_setting");
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("date_setting", value);
            }
        }

        public double DoubleSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("double_setting", (double)0);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue("double_setting", value);
            }
        }

        public bool BoolSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault("bool_setting", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("bool_setting", value);
            }
        }

        public string StringSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                //if value has changed then save it!
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public void Remove(string key)
        {
            AppSettings.Remove(key);
        }
    }
}