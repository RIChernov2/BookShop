using System.Collections.Generic;

namespace Data.Entities
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
        private List<bool> _items;

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

        public bool Get(int index) => _items[index];

    }
}
