using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public abstract class AbstractValidation<T>
    {
        public abstract string Name { get; }

        public abstract Func<T, bool> ValidationFunction { get; }

        public abstract string MessageOnError { get; protected set; }

        public abstract Func<T, object> OriginalValue { get; }
        public abstract string MessageOnSuccess { get; }
        public bool IsValid(T value) => ValidationFunction(arg: value);

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
