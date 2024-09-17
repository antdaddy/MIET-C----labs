using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace lr5
{
    delegate TKey KeySelector<TKey>(Magazine mg);

    class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;
        private Dictionary<TKey, PropertyChangedEventHandler> handlers;
        private KeySelector<TKey> keySelector;

        public string MagazineCollectionName { get; set; }
        public event MagazinesChangedHandler<TKey> Notify;

        public MagazineCollection(KeySelector<TKey> keySelector, string collectionName)
        {
            this.keySelector = keySelector;
            MagazineCollectionName = collectionName;
            magazines = new Dictionary<TKey, Magazine>();
            handlers = new Dictionary<TKey, PropertyChangedEventHandler>();
        }
        public bool Replace(Magazine mOld, Magazine mNew)
        {
            if (!magazines.ContainsValue(mOld))
            {
                return false;
            }
            Dictionary<TKey, Magazine>.KeyCollection keys = magazines.Keys;

            foreach (TKey key in keys)
            {
                if (Equals(magazines[key], mOld))
                {
                    magazines.Remove(key);
                    magazines.Add(key, mNew);
                    PropertyChangedEventHandler handler;
                    handlers.TryGetValue(key, out handler);
                    mOld.PropertyChanged -= handler;
                    handlers.Remove(key);
                    PropertyChangedEventHandler newHandler = GetHandler(key);

                    handlers.Add(key, newHandler);
                    mNew.PropertyChanged += newHandler;
                    Notify?.Invoke(new MagazinesChangedEventArgs<TKey>(MagazineCollectionName, ChangeCollectionEventType.Replace, "", key));
                    return true;
                }
            }
            return true;
        }
        public PropertyChangedEventHandler GetHandler(TKey key)
        {
            return (sender, args) => Notify?.Invoke(new MagazinesChangedEventArgs<TKey>(MagazineCollectionName, ChangeCollectionEventType.Property, args.PropertyName, key));
        }
        public void AddDefaults() { }
        public void AddMagazines(params Magazine[] magazines)
        {
            foreach (Magazine entry in magazines)
            {
                TKey key = keySelector(entry);
                Notify?.Invoke(new MagazinesChangedEventArgs<TKey>(MagazineCollectionName, ChangeCollectionEventType.Add, "", key));
                PropertyChangedEventHandler handler = GetHandler(key);
                handlers.Add(key, handler);
                entry.PropertyChanged += handler;
                this.magazines.Add(key, entry);
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<TKey, Magazine> entry in magazines)
            {
                result += entry.Value.ToString() + '\n';
            }
            return result;
        }
        public string ToShortString()
        {
            string result = "";
            foreach (KeyValuePair<TKey, Magazine> entry in magazines)
            {
                result += entry.Value.ToShortString() + '\n';
            }
            return result;
        }

        public double MaxScore
        {
            get
            {
                return magazines.Max(x => x.Value.AverageScore);
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency period)
        {
            return magazines.Where(x => x.Value.Period == period);
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> Groups
        {
            get
            {
                return magazines.GroupBy(x => x.Value.Period);
            }
        }
    }
}
