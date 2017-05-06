using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WinrtCifs.Util.Sharpen
{
    public class Runtime
    {
        private static Runtime _instance;

        private static Hashtable _properties;


        public static long CurrentTimeMillis()
        {
            return DateTime.UtcNow.ToMillisecondsSinceEpoch();
        }

        public static Hashtable GetProperties()
        {
            if (_properties == null)
            {
                _properties = new Hashtable();
                _properties["jgit.fs.debug"] = "false";
                _properties["file.encoding"] = "UTF-8";
                _properties["os.name"] = "Windows";
            }
            return _properties;
        }

        public static string GetProperty(string key)
        {
            if (GetProperties().Keys.Contains(key))
            {
                return (string) GetProperties()[key];
            }
            return null;
        }

        public static int IdentityHashCode(object ob)
        {
            return RuntimeHelpers.GetHashCode(ob);
        }


        public static byte[] GetBytesForString(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static byte[] GetBytesForString(string str, string encoding)
        {
            return Encoding.GetEncoding(encoding).GetBytes(str);
        }


        public static void NotifyAll(object ob)
        {
            try {
                Monitor.PulseAll(ob);
            }
            catch (Exception) {
            }
         
        }

        public static void Notify(object obj)
        {
            try
            {
                Monitor.Pulse(obj);
            }
            catch (Exception)
            {
            }
        }

        public static void PrintStackTrace(Exception ex)
        {
            LogStream.GetInstance().WriteLine(ex);
        }

        public static void PrintStackTrace(Exception ex, LogStream tw)
        {
            tw.WriteLine(ex);
        }

        public static string Substring(string str, int index)
        {
            return str.Substring(index);
        }

        public static string Substring(string str, int index, int endIndex)
        {
            return str.Substring(index, endIndex - index);
        }

        public static void Wait(object ob)
        {
            Monitor.Wait(ob);
        }

        public static bool Wait(object ob, long milis)
        {
            return Monitor.Wait(ob, (int) milis);
        }

        public static bool EqualsIgnoreCase(string s1, string s2)
        {
            return s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string GetStringForBytes(byte[] chars, string encoding)
        {
            return GetEncoding(encoding).GetString(chars, 0, chars.Length);
        }

        public static string GetStringForBytes(byte[] chars, int start, int len)
        {
            return Encoding.UTF8.GetString(chars, start, len);
        }

        public static string GetStringForBytes(byte[] chars, int start, int len, string encoding)
        {
            return GetEncoding(encoding).Decode(chars, start, len);
        }

        public static Encoding GetEncoding(string name)
        {
            var e = Encoding.GetEncoding(name.Replace('_', '-'));
            if (e is UTF8Encoding)
                return new UTF8Encoding(false, true);
            return e;
        }
    }
}