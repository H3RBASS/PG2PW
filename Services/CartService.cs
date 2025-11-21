using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto.Models;

namespace Proyecto.Services
{
    public class CartService
    {
        private readonly Dictionary<int, int> _items = new();

        public event Action OnChange;

        public void Add(Product p, int qty = 1)
        {
            if (_items.ContainsKey(p.Id)) _items[p.Id] += qty;
            else _items[p.Id] = qty;
            OnChange?.Invoke();
        }

        public void Remove(Product p)
        {
            if (_items.ContainsKey(p.Id))
            {
                _items.Remove(p.Id);
                OnChange?.Invoke();
            }
        }

        public IReadOnlyDictionary<int,int> GetItems() => _items;

        public int GetCount() => _items.Values.Sum();

        public void Clear()
        {
            _items.Clear();
            OnChange?.Invoke();
        }
    }
}
