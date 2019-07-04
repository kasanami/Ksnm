using System.Runtime.CompilerServices;

namespace Ksnm
{
    /// <summary>
    /// デバッグ情報
    /// </summary>
    public class Debug
    {
        public static string GetFilePathAndLineNumber([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            return $"{filePath} {lineNumber}";
        }
    }
}
