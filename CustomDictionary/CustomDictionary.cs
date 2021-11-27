using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Channels;

namespace CustomDictionary
{
    public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private LinkedList<KeyValuePair<TKey, TValue>>[] _hashTable;
        private ICollection<TKey> _keys;
        private ICollection<TValue> _values;

        private int _count = 0;
        private int _capacity = 16;

        public CustomDictionary()
        {
            _hashTable = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];

            _keys = new List<TKey>();
            _values = new List<TValue>();

            for (int i = 0; i < _hashTable.Length; ++i)
            {
                _hashTable[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
        }

        public CustomDictionary(int capacity)
        {
            _capacity = capacity;
            _hashTable = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
            
            _keys = new List<TKey>();
            _values = new List<TValue>();
            
            for (int i = 0; i < _hashTable.Length; ++i)
            {
                _hashTable[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int hash = Math.Abs(key.GetHashCode()) % _capacity;

                if (ContainsKey(key))
                {
                    TValue result = default(TValue);

                    foreach (var item in _hashTable[hash])
                    {
                        if (key.Equals(item.Key))
                        {
                            result = item.Value;
                        }
                    }

                    return result;
                }
                else
                {
                    throw new Exception($"There's no element with such key: {key}");
                }
            }

            set
            {
                int hash = Math.Abs(key.GetHashCode()) % _capacity;
                
                if (ContainsKey(key))
                {
                    for (var cur = _hashTable[hash].First; !cur.Equals(_hashTable[hash].Last); cur = cur.Next)
                    {
                        if (key.Equals(cur.Value.Key))
                        {
                            LinkedListNode<KeyValuePair<TKey, TValue>> newNode =
                                new LinkedListNode<KeyValuePair<TKey, TValue>>(
                                    new KeyValuePair<TKey, TValue>(key, value));
                            cur = newNode;
                        }
                    }
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return _keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return _values;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        private void AllocateHashTable()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] newHashTable =
                new LinkedList<KeyValuePair<TKey, TValue>>[_capacity * 2];

            for (int i = 0; i < _hashTable.Length; ++i)
            {
                newHashTable[i] = _hashTable[i];
            }

            _hashTable = newHashTable;
        }

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % _capacity;

            if (!ContainsKey(key))
            {
                _count++;
                
                if (_count >= _capacity)
                {
                    AllocateHashTable();
                }

                KeyValuePair<TKey, TValue> pair = new KeyValuePair<TKey, TValue>(key, value);
                _hashTable[hash].AddLast(pair);
                
                _keys.Add(key);
                _values.Add(value);
            }
            else
            {
                throw new Exception($"There's already an element with key: {key}");
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % _capacity;

            var keys = _hashTable[hash].Select(item => item.Key);

            if (keys.Contains(key))
            {
                return true;
            }

            return false;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public bool Remove(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % _capacity;

            if (ContainsKey(key))
            {
                _count--;

                foreach (var item in _hashTable[hash])
                {
                    if (key.Equals(item.Key))
                    {
                        _hashTable[hash].Remove(item);
                        break;
                    }
                }

                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % _capacity;

            if (ContainsKey(key))
            {
                foreach (var item in _hashTable[hash])
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);

            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < _hashTable.Length; ++i)
            {
                _hashTable[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            _count = 0;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex - 1 < _count)
            {
                throw new Exception("There's no space to insert elements into array.");
            }

            if (array == null)
            {
                throw new NullReferenceException($"Array 'array' has null reference.");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentException("Negative argument 'arrayIndex' is not valid.");
            }

            List<KeyValuePair<TKey, TValue>> allPairs = new List<KeyValuePair<TKey, TValue>>();

            for (int i = 0; i < _hashTable.Length; ++i)
            {
                foreach (var item in _hashTable[i])
                {
                    allPairs.Add(item);
                }
            }

            int curIndex = 0;
            for (int i = arrayIndex; i < array.Length; ++i)
            {
                array[i] = allPairs[curIndex];
                curIndex++;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _hashTable.Length; ++i)
            {
                foreach (var item in _hashTable[i])
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
