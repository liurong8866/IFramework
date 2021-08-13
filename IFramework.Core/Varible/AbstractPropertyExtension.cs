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
    public static class AbstractPropertyExtension
    {
        // int
        public static AbstractPropertyNumeric<int> Add(this AbstractPropertyNumeric<int> self, object value)
        {
            self.Value = AbstractPropertyNumeric<int>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<int> Sub(this AbstractPropertyNumeric<int> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<int>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<int> Mul(this AbstractPropertyNumeric<int> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<int>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<int> Div(this AbstractPropertyNumeric<int> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<int>.Division(self.Value, value);
            return self;
        }
        
        // short
        public static AbstractPropertyNumeric<short> Add(this AbstractPropertyNumeric<short> self, object value)
        {
            self.Value = AbstractPropertyNumeric<short>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<short> Sub(this AbstractPropertyNumeric<short> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<short>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<short> Mul(this AbstractPropertyNumeric<short> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<short>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<short> Div(this AbstractPropertyNumeric<short> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<short>.Division(self.Value, value);
            return self;
        }
        
        // long
        public static AbstractPropertyNumeric<long> Add(this AbstractPropertyNumeric<long> self, object value)
        {
            self.Value = AbstractPropertyNumeric<long>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<long> Sub(this AbstractPropertyNumeric<long> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<long>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<long> Mul(this AbstractPropertyNumeric<long> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<long>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<long> Div(this AbstractPropertyNumeric<long> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<long>.Division(self.Value, value);
            return self;
        }
        
        // float
        public static AbstractPropertyNumeric<float> Add(this AbstractPropertyNumeric<float> self, object value)
        {
            self.Value = AbstractPropertyNumeric<float>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<float> Sub(this AbstractPropertyNumeric<float> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<float>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<float> Mul(this AbstractPropertyNumeric<float> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<float>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<float> Div(this AbstractPropertyNumeric<float> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<float>.Division(self.Value, value);
            return self;
        }
        
        // double
        public static AbstractPropertyNumeric<double> Add(this AbstractPropertyNumeric<double> self, object value)
        {
            self.Value = AbstractPropertyNumeric<double>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<double> Sub(this AbstractPropertyNumeric<double> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<double>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<double> Mul(this AbstractPropertyNumeric<double> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<double>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<double> Div(this AbstractPropertyNumeric<double> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<double>.Division(self.Value, value);
            return self;
        }
        
        // decimal
        public static AbstractPropertyNumeric<decimal> Add(this AbstractPropertyNumeric<decimal> self, object value)
        {
            self.Value = AbstractPropertyNumeric<decimal>.Addition(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<decimal> Sub(this AbstractPropertyNumeric<decimal> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<decimal>.Subtraction(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<decimal> Mul(this AbstractPropertyNumeric<decimal> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<decimal>.Multiply(self.Value, value);
            return self;
        }
        public static AbstractPropertyNumeric<decimal> Div(this AbstractPropertyNumeric<decimal> self, object value)
        {
            self.Value =  AbstractPropertyNumeric<decimal>.Division(self.Value, value);
            return self;
        }
    }
}