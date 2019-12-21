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
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static void Information(string message, string caption)
        {
            MessageBox((IntPtr)0, message, caption, 0);
        }

        public static Result OkAbort(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 1);

            if (a == 1)
                return Result.Ok;
            else
                return Result.Abort;
        }

        public static Result AbortRetryIgnore(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 2);

            if (a == 3)
                return Result.Abort;
            else if (a == 4)
                return Result.Retry;
            else
                return Result.Ignore;
        }

        public static Result YesNoAbort(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 3);

            if (a == 6)
                return Result.Yes;
            else if (a == 7)
                return Result.No;
            else
                return Result.Abort;
        }

        public static Result YesNo(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 4);

            if (a == 6)
                return Result.Yes;
            else
                return Result.No;
        }

        public static Result RetryAbort(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 5);

            if (a == 4)
                return Result.Retry;
            else
                return Result.Abort;
        }

        public static Result AbortRetryNext(string message, string caption)
        {
            var a = MessageBox((IntPtr)0, message, caption, 6);

            if (a == 2)
                return Result.Abort;
            else if (a == 10)
                return Result.Retry;
            else
                return Result.Next;
        }

        public enum Result
        {
            Ok,
            Abort,
            Retry,
            Ignore,
            Yes,
            No,
            Next,



        }
    }
}
