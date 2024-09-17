using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    class Listener<TKey>
    {
        private List<ListEntry> collectionsList = new List<ListEntry>();
        public void OnChanged (MagazinesChangedEventArgs<TKey> args)
        {
            ListEntry newEntry = new ListEntry(args.CollectionName, args.EventType, args.ChangedProperty, args.ElementKey.ToString());
            collectionsList.Add(newEntry);
        }

        public override string ToString()
        {
            string result = "";
            foreach(ListEntry entry in collectionsList)
            {
                result += entry.ToString() + "\n\n";
            }
            return result;
        }
    }

    class ListEntry
    {
        public string CollectionName { get; set; }
        public ChangeCollectionEventType EventType { get; set; }
        public string ChangedProperty { get; set; }
        public string ElementKey { get; set; }

        public ListEntry(string collectionName, ChangeCollectionEventType eventType, string changedProperty, string elementKey)
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
                ElementKey
            );
        }
    }
}
