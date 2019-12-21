using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.WinApi
{
    public static class Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static void Information(string message, string caption)
        {
            try
            {
                MessageBox(GetActiveWindow(), message, caption, 0);
            }
            catch
            {
                MessageBox((IntPtr)0, message, caption, 0);
            }
        }

        public static Result OkAbort(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 1);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 1);
                return GetResult(a);
            }
        }

        public static Result AbortRetryIgnore(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 2);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 2);
                return GetResult(a);
            }
        }

        public static Result YesNoAbort(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 3);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 3);
                return GetResult(a);
            }
        }

        public static Result YesNo(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 4);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 4);
                return GetResult(a);
            }
        }

        public static Result RetryAbort(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 5);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 5);
                return GetResult(a);
            }
        }

        public static Result AbortRetryNext(string message, string caption)
        {
            try
            {
                var a = MessageBox(GetActiveWindow(), message, caption, 6);
                return GetResult(a);
            }
            catch
            {
                var a = MessageBox((IntPtr)0, message, caption, 6);
                return GetResult(a);
            }
        }

        public enum Result
        {
            Ok,
            Abort,
            Cancel,
            Retry,
            Ignore,
            Yes,
            No,
            Next,
        }

        private static Result GetResult(int val)
        {
            switch (val)
            {
                case 1:
                    return Result.Ok;
                case 2:
                    return Result.Cancel;
                case 3:
                    return Result.Abort;
                case 4:
                    return Result.Retry;
                case 5:
                    return Result.Ignore;
                case 6:
                    return Result.Yes;
                case 7:
                    return Result.No;
                default:
                    return Result.No;
            }
        }
    }
}
