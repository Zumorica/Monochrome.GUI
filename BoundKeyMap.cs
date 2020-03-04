using System;
using System.Collections.Generic;
using System.Reflection;

namespace Monochrome.GUI
{
    /// <summary>
    ///     A networked identifier for a <see cref="BoundKeyFunction"/>.
    /// </summary>
    [Serializable]
    public readonly struct KeyFunctionId : IEquatable<KeyFunctionId>
    {
        private readonly int _value;

        public KeyFunctionId(int id)
        {
            _value = id;
        }

        public static explicit operator int(KeyFunctionId funcId)
        {
            return funcId._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(KeyFunctionId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is KeyFunctionId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public static bool operator ==(KeyFunctionId left, KeyFunctionId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(KeyFunctionId left, KeyFunctionId right)
        {
            return !left.Equals(right);
        }
    }
}
