using System;
using System.Reflection;
using UnityEngine;

namespace IFramework.Core
{
    public enum LogLevel
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        All = 4
    }

    public static class Log
    {
        public static LogLevel Level { get; set; } = LogLevel.All;

        // INFO
        public static void Info(object content)
        {
            if (Level >= LogLevel.Info) { Debug.Log(content.ToString()); }
        }

        public static void Info(string format, params object[] param)
        {
            if (Level >= LogLevel.Info) { Debug.LogFormat(format, param); }
        }

        public static void LogInfo(this object self)
        {
            Info(self);
        }

        public static void LogInfo(this object self, string format)
        {
            Info(format, self);
        }

        // WARNING
        public static void Warning(object self)
        {
            if (Level >= LogLevel.Warning) { Debug.LogWarning(self); }
        }

        public static void Warning(string format, params object[] param)
        {
            if (Level >= LogLevel.Warning) { Debug.LogWarningFormat(format, param); }
        }

        public static void LogWarning(this object self)
        {
            Warning(self);
        }

        public static void LogWarning(this object self, string format)
        {
            Warning(format, self);
        }

        // ERROR
        public static void Error(object self)
        {
            if (Level >= LogLevel.Error) { Debug.LogError(self); }
        }

        public static void Error(string format, params object[] param)
        {
            if (Level >= LogLevel.Error) { Debug.LogErrorFormat(format, param); }
        }

        public static void LogError(this object self)
        {
            Error(self);
        }

        public static void LogError(this object self, string format)
        {
            Error(format, self);
        }

        public static void Clear()
        {
            // var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            // var type = assembly.GetType("UnityEditor.LogEntries");

            //获取UnityEditor程序集里面的UnityEditorInternal.LogEntries类型，也就是把关于Console的类提出来
            Type entries = Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");

            //在logEntries类里面找到名为Clear的方法，且其属性必须是public static的，等同于得到了Console控制台左上角的clear，然后通过Invoke进行点击实现
            MethodInfo method = entries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            method?.Invoke(null, null);
        }
    }
}
