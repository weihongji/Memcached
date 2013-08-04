using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeIT.MemCached;
using System.Reflection;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args) {
			MemcachedClient.Setup("TestCache", new string[] { "192.168.1.2" });
			MemcachedClient cache = MemcachedClient.GetInstance("TestCache");
			cache.SendReceiveTimeout = 5000;
			cache.MinPoolSize = 1;
			cache.MaxPoolSize = 5;

			var me = cache.Get("jesse") as Person;
			if (me == null) {
				Console.WriteLine("Not cached. Add it...");
				me = new Person { First = "Jesse", Last = "Wei" };
				cache.Set("jesse", me);
			}
			else {
				Console.WriteLine("Getting \"jesse\" from cache...");
			}
			Console.WriteLine(me.ToString());

			Console.ReadKey();
		}
	}

	[Serializable]
	class Person
	{
		public string First { get; set; }
		public string Last { get; set; }

		public override string ToString() {
			return string.Format("{0} {1}", First, Last); ;
		}
	}
}
