using System;
using System.Collections.Generic;
using System.Text;

namespace BitPhoenix.OmniShift
{
    public interface ISkinProperty
    {
        public string Name { get; }
        public object Value { get; }
    }

    public sealed class SkinProperty<T> : ISkinProperty
    {
        private T _value;
        private string _name = "";

        public SkinProperty(string name, T value)
        {
            _name = name;
            _value = value;
        }

        object ISkinProperty.Value => _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public string Name => _name;
    }
}
