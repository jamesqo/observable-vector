using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Collections
{
    public sealed class ObservableVector : IObservableVector<object>, IReadOnlyList<object>, INotifyPropertyChanged
    {
        private const string IndexerName = "Item[]";

        // Cached EventArgs

        private static readonly ChangedArgs ResetArgs = new ChangedArgs(CollectionChange.Reset, 0);

        private static readonly PropertyChangedEventArgs IndexerArgs = new PropertyChangedEventArgs(IndexerName);
        private static readonly PropertyChangedEventArgs CountArgs = new PropertyChangedEventArgs(nameof(Count));

        // Members

        private readonly List<object> list;

        // Properties

        public object this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        // Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event VectorChangedEventHandler<object> VectorChanged;

        // Constructors

        public ObservableVector()
        {
            this.list = new List<object>();
        }

        // Can't use the List(int) overload, since
        // WinRT doesn't allow having 2 constructor
        // overloads with the same # of parameters.

        public ObservableVector(IEnumerable<object> items)
        {
            this.list = new List<object>(items);
        }

        // Methods

        public void Add(object item) =>
            Insert(this.Count, item);

        public void Clear()
        {
            list.Clear();
            OnCountChanged();
            OnIndexerChanged();
            OnVectorChanged(ResetArgs);
        }

        public bool Contains(object item) => list.Contains(item);

        public void CopyTo(object[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<object> GetEnumerator() => list.GetEnumerator();

        public int IndexOf(object item) => list.IndexOf(item);

        public void Insert(int index, object item)
        {
            list.Insert(index, item);
            OnCountChanged();
            OnIndexerChanged();
            OnVectorChanged(CollectionChange.ItemInserted, (uint)index);
        }

        public bool Remove(object item)
        {
            int index = this.IndexOf(item);
            if (index != -1)
                this.RemoveAt(index);
            return index != -1;
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            OnCountChanged();
            OnIndexerChanged();
            OnVectorChanged(CollectionChange.ItemRemoved, (uint)index);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        int IReadOnlyCollection<object>.Count => Count;

        object IReadOnlyList<object>.this[int index] => this[index];

        private void OnVectorChanged(CollectionChange change, uint index)
        {
            OnVectorChanged(new ChangedArgs(change, index));
        }

        private void OnVectorChanged(ChangedArgs e) => VectorChanged?.Invoke(this, e);

        private void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        private void OnCountChanged() => OnPropertyChanged(CountArgs);
        private void OnIndexerChanged() => OnPropertyChanged(IndexerArgs);

        private class ChangedArgs : IVectorChangedEventArgs
        {
            public uint Index { get; }
            public CollectionChange CollectionChange { get; }

            public ChangedArgs(CollectionChange change, uint index)
            {
                this.Index = index;
                this.CollectionChange = change;
            }
        }
    }
}
