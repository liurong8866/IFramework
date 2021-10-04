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
