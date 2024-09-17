using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    delegate void MagazinesChangedHandler<TKey> (MagazinesChangedEventArgs<TKey> args);
    class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get; set; }
        public ChangeCollectionEventType EventType { get; set; }
        public string ChangedProperty { get; set; }
        public TKey ElementKey { get; set; }

        public MagazinesChangedEventArgs(string collectionName, ChangeCollectionEventType eventType, string changedProperty, TKey elementKey)
        {
            CollectionName = collectionName;
            EventType = eventType;
            ChangedProperty = changedProperty;
            ElementKey = elementKey;
        }

        public override string ToString()
        {
            return string.Format(
                "Collection name: {0}\nEvent type: {1}\nChanged property: {2}\nElement key: {3}",
                CollectionName,
                EventType,
                ChangedProperty,
                ElementKey.ToString()
            );
        }
    }
}
