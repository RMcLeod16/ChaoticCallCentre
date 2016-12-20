using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaoticCallCentreV2
{
    /// <summary>
    /// Class with debug-related utilities
    /// </summary>
    static class myDebug
    {
        // http://stackoverflow.com/questions/171970/how-can-i-find-the-method-that-called-the-current-method
        // http://stackoverflow.com/questions/5080477/c-sharp-debug-only-code-that-should-run-only-when-turned-on
        // https://msdn.microsoft.com/en-us/library/mt653988.aspx
        /// <summary>
        /// Static method used as a wrapper for Debug.WriteLine(), but adds extra information to the output,
        /// such as a timestamp and the method that called it, along with the string that the calling method pass to it.
        /// </summary>
        /// <param name="line">The string to be printed to the debug console.</param>
        /// <param name="memberName">The name of the method that called this one</param>
        [ConditionalAttribute("DEBUG")]     // Only used in Debug build of application, not in Release build
        public static void WriteLine(string line, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            // Get name of calling class from StackFrame
            // Probably not the best way to do things, but since this code only runs in Debug build, it should be OK.
            string className = new StackFrame(1).GetMethod().ReflectedType.Name;

            // Print supplied string line with timestamp and calling class and method names.
            Debug.WriteLine("[" + DateTime.Now.ToString() + "]\t[" + className + "." + memberName + "()]\t" + line);
        }
    }
}
