using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Structures
{
    public class HashTable<T, TU>
    {

        private LinkedList<Tuple<T, TU>>[] _items;
        private int _fillFactor = 3;
        private int _size;

        public HashTable()
        {
            _items = new LinkedList<Tuple<T, TU>>[4];
        }

        public void Add(T key, TU value)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<T, TU>>();
            }
            if (_items[pos].Any(x => x.Item1.Equals(key)))
            {
                throw new KeyNotFoundException("Duplicate key, cannot insert.");
            }
            _size++;
            if (NeedToGrow())
            {
                GrowAndReHash();
            }
            pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<T, TU>>();
            }
            _items[pos].AddFirst(new Tuple<T, TU>(key, value));
        }

        public void Remove(T key)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] != null)
            {
                var objToRemove = _items[pos].FirstOrDefault(item => item.Item1.Equals(key));
                if (objToRemove == null) return;
                _items[pos].Remove(objToRemove);
                _size--;
            }
            else
            {
                throw new Exception("Value not in HashTable.");
            }
        }

        public TU Get(T key)
        {
            var pos = GetPosition(key, _items.Length);
            foreach (var item in _items[pos].Where(item => item.Item1.Equals(key)))
            {
                return item.Item2;
            }
            throw new Exception("Key does not exist in HashTable.");
        }

        private void GrowAndReHash()
        {
            _fillFactor *= 2;
            var newItems = new LinkedList<Tuple<T, TU>>[_items.Length * 2];
            foreach (var item in _items.Where(x => x != null))
            {
                foreach (var value in item)
                {
                    var pos = GetPosition(value.Item1, newItems.Length);
                    if (newItems[pos] == null)
                    {
                        newItems[pos] = new LinkedList<Tuple<T, TU>>();
                    }
                    newItems[pos].AddFirst(new Tuple<T, TU>(value.Item1, value.Item2));
                }
            }
            _items = newItems;
        }

        private int GetPosition(T key, int length)
        {
            var hash = key.GetHashCode();
            var pos = Math.Abs(hash % length);
            return pos;
        }

        private bool NeedToGrow()
        {
            return _size >= _fillFactor;
        }
    }
}
