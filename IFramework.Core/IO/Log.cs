/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;
using System.ComponentModel;
using UnityEngine;

namespace IFramework.Core
{
    public enum LogLevel
    {
        None = 0,
        Exception = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
        All =5
    }
    
    public static class Log
    {
        private static LogLevel logLevel = LogLevel.All;
        
        public static LogLevel Level
        {
            get { return logLevel; }
            set { logLevel = value; }
        }

        // INFO
        public static void Info(object content)
        {
            if (logLevel >= LogLevel.Info)
            {
                Debug.Log(content.ToString());
            }
        }

        public static void Info(string format, params object[] param)
        {
            if (logLevel >= LogLevel.Info)
            {
                Debug.LogFormat(format, param); 
            }
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
            if (logLevel >= LogLevel.Warning)
            {
                Debug.LogWarning(self);
            }
        }
        
        public static void Warning(string format, params object[] param)
        {
            if (logLevel >= LogLevel.Warning)
            {
                Debug.LogWarningFormat(format, param); 
            }
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
            if (logLevel >= LogLevel.Error)
            {
                Debug.LogError(self);
            }
        }
        
        public static void Error(string format, params object[] param)
        {
            if (logLevel >= LogLevel.Error)
            {
                Debug.LogErrorFormat(format, param); 
            }
        }
        
        public static void LogError(this object self)
        {
            Error(self);
        }
        
        public static void LogError(this object self, string format)
        {
            Error(format, self);
        }
        
        // EXCEPTION
        public static void Exception(Exception self)
        {
            if (logLevel >= LogLevel.Exception)
            {
                Debug.LogException(self);
            }
        }
        
        public static void LogException(this Exception self)
        {
            LogException(self);
        }
    }
}