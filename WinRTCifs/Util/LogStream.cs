// This code is derived from jcifs smb client library <jcifs at samba dot org>
// Ported by J. Arturo <webmaster at komodosoft dot net>
//  
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.UI.Notifications;
using WinrtCifs.Util.Sharpen;

namespace WinrtCifs.Util
{
    /// <summary>
    /// 0 - nothing
    /// 1 - critical [default]
    /// 2 - basic info can be logged under load
    /// 3 - almost everything
    /// N - debugging
    /// </summary>
    public class LogStream
    {
        private static LogStream _inst = null;

        public int Level = 1;

        public void SetLevel(int level)
        {
            this.Level = level;
        }


        public static void SetInstance()
        {
            if (_inst == null)
            {
                _inst = new LogStream();
            }
        }

        public static LogStream GetInstance()
        {
            if (_inst == null)
            {
                _inst = new LogStream();
            }
            return _inst;
        }


        public void WriteLine(string s)
        {
            Debug.WriteLine(s);
        }

        public void WriteLine(object s)
        {
            Debug.WriteLine(s);
        }

        public void WriteLine(EventArgs s)
        {
            Debug.WriteLine(s.ToString());
        }

        public void WriteLine(Char[] s)
        {
            Debug.WriteLine(new string(s));
        }

    }
}
