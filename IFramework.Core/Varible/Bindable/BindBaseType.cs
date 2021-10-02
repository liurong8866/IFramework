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

namespace IFramework.Core
{
    // 字符串、Bool类型
    public class BindBool : Bindable<bool>
    {
        public BindBool() { }

        public BindBool(bool value) : base(value) { }

        //重载运算符"true"
        public static bool operator true(BindBool value)
        {
            return value.Value;
        }

        //重载运算符"false"
        public static bool operator false(BindBool value)
        {
            return value.Value;
        }

        //重载运算符"!"
        public static bool operator !(BindBool value)
        {
            return !value.Value;
        }

        //重载运算符"&"
        public static bool operator &(BindBool m, BindBool n)
        {
            return m.Value & n.Value;
        }

        public static bool operator &(BindBool m, bool n)
        {
            return m.Value & n;
        }

        public static bool operator &(bool m, BindBool n)
        {
            return m & n.Value;
        }

        //重载运算符"|"
        public static bool operator |(BindBool m, BindBool n)
        {
            return m.Value | n.Value;
        }

        public static bool operator |(BindBool m, bool n)
        {
            return m.Value | n;
        }

        public static bool operator |(bool m, BindBool n)
        {
            return m | n.Value;
        }
    }

    public class BindString : Bindable<string>
    {
        public BindString() { }

        public BindString(string value) : base(value) { }
    }

    public class BindChar : Bindable<char>
    {
        public BindChar() { }

        public BindChar(char value) : base(value) { }
    }

    // 数字类型
    public class BindInt : BindableNumeric<int>
    {
        public BindInt() { }

        public BindInt(int value) : base(value) { }

        //重载运算符"++"
        public static BindInt operator ++(BindInt self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindInt operator --(BindInt self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }

    public class BindShort : BindableNumeric<short>
    {
        public BindShort() { }

        public BindShort(short value) : base(value) { }

        //重载运算符"++"
        public static BindShort operator ++(BindShort self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindShort operator --(BindShort self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }

    public class BindLong : BindableNumeric<long>
    {
        public BindLong() { }

        public BindLong(long value) : base(value) { }

        //重载运算符"++"
        public static BindLong operator ++(BindLong self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindLong operator --(BindLong self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }

    public class BindFloat : BindableNumeric<float>
    {
        public BindFloat() { }

        public BindFloat(float value) : base(value) { }

        //重载运算符"++"
        public static BindFloat operator ++(BindFloat self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindFloat operator --(BindFloat self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }

    public class BindDouble : BindableNumeric<double>
    {
        public BindDouble() { }

        public BindDouble(double value) : base(value) { }

        //重载运算符"++"
        public static BindDouble operator ++(BindDouble self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindDouble operator --(BindDouble self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }

    public class BindDecimal : BindableNumeric<decimal>
    {
        public BindDecimal() { }

        public BindDecimal(decimal value) : base(value) { }

        //重载运算符"++"
        public static BindDecimal operator ++(BindDecimal self)
        {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static BindDecimal operator --(BindDecimal self)
        {
            self.Value = Subtraction(self.Value, 1);
            return self;
        }
    }
}
