using System.Collections.Generic;
using System.Text.Json;

namespace BookShop.Notifications.Manager.Models
{
    /// <summary>
    /// Класс показывает необходимость уведомления для каждого типа сообщений
    /// </summary>
    public class NSettings
    {
        public NSettings()
        {
            _items = new List<bool>() { true, true, true };
        }
        public bool Info
        {
            get
            {
                return _items[0];
            }
            set
            {
                _items[0] = value;
            }
        }
        public bool Warning
        {
            get
            {
                return _items[1];
            }
            set
            {
                _items[1] = value;
            }
        }
        public bool Ads
        {
            get
            {
                return _items[2];
            }
            set
            {
                _items[2] = value;
            }
        }

        // лист необходим для реализации обращения по индексу
        private List<bool> _items;
        public bool Get(MessageType index) => _items[(int)index];

        // для настройки NSettingsTypeHandler для хранения дажжных в джейсон с БД
        public override string ToString()
        {
            var result = JsonSerializer.Serialize(this);
            return result;
        }

        public static NSettings FromString(string value)
        {
            var result = JsonSerializer.Deserialize<NSettings>(value);            
            return result;
        }

    }
}
