using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Caching;

namespace UnitTestHelper
{
  public class MyCache : ObjectCache
  {

    private Hashtable _Items = null;

    public CacheItemPolicy DefaultPolicy { get; set; }

    public MyCache()
    {
      _Items = new Hashtable();
    }

    public MyCache(int test, string test2, DateTime test3)
    {
      _Items = new Hashtable();
    }

    public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName = null)
    {
      object result = _Items[key];
      _Items.Add(key, value);
      return result;
    }

    public override CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy)
    {
      object result = this.AddOrGetExisting(value.Key, value.Value, policy, value.RegionName);
      if (result == null) { return null; }
      return new CacheItem(value.Key, result, value.RegionName);
    }

    public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
    {
      CacheItemPolicy policy = new CacheItemPolicy();
      policy.AbsoluteExpiration = absoluteExpiration;
      return this.AddOrGetExisting(key, value, policy, regionName);
    }

    public override bool Contains(string key, string regionName = null)
    {
      return _Items[key] != null;
    }

    public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName = null)
    {
      throw new NotImplementedException();
    }

    public override DefaultCacheCapabilities DefaultCacheCapabilities
    {
      get
      {
        return DefaultCacheCapabilities.AbsoluteExpirations | DefaultCacheCapabilities.SlidingExpirations;
      }
    }

    public override object Get(string key, string regionName = null)
    {
      return _Items[key];
    }

    public override CacheItem GetCacheItem(string key, string regionName = null)
    {
      return new CacheItem(key, this.Get(key, regionName), regionName);
    }

    public override long GetCount(string regionName = null)
    {
      return _Items.Count;
    }

    protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
      System.Collections.IDictionaryEnumerator em = _Items.GetEnumerator();
      while (em.MoveNext())
      {
        result.Add(new KeyValuePair<string, object>(em.Key.ToString(), em.Value));
      }
      return result.GetEnumerator();
    }

    public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
    {
      Dictionary<string, object> result = new Dictionary<string, object>();
      foreach (string key in keys)
      {
        result[key] = this.Get(key, regionName);
      }
      return result;
    }

    public override string Name
    {
      get { return "CustomCache"; }
    }

    public override object Remove(string key, string regionName = null)
    {
      var result = _Items[key];
      _Items.Remove(key);
      return result;
    }

    public override void Set(string key, object value, CacheItemPolicy policy, string regionName = null)
    {
      base.Add(key, value, policy, regionName);
    }

    public override void Set(CacheItem item, CacheItemPolicy policy)
    {
      base.Add(item, policy);
    }

    public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
    {
      base.Add(key, value, absoluteExpiration, regionName);
    }

    public override object this[string key]
    {
      get
      {
        return this.Get(key, null);
      }
      set
      {
        this.Set(key, value, this.DefaultPolicy, null);
      }
    }

  }
}
