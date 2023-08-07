using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;

namespace CashMachineTask.Model
{
    internal class DialogParametrs : IDialogParametrs
    {
        private Dictionary<string, object> _mapping = new Dictionary<string, object>();
        private Dictionary<string, Type> _mappingType = new Dictionary<string, Type>();

        public void Register<T>(string propertyName, T value)
        {
            _mapping.Add(propertyName, value);
            _mappingType.Add(propertyName, typeof(T));
        }

        public void Register<T>(string propertyName, object value)
        {
            _mapping.Add(propertyName, (T)value);
            _mappingType.Add(propertyName, typeof(T));
        }

        public void Register(string propertyName, object value, Type type)
        {
            _mapping.Add(propertyName, value);
            _mappingType.Add(propertyName, type);
        }

        public DialogParametrs() { }

        public T GetValue<T>(string parametrName)
        {
            var value = _mapping[parametrName];

            return (T)value;
        }

        public object GetValue(string parametrName)
        {
            return _mapping[parametrName];
        }
    }
}
