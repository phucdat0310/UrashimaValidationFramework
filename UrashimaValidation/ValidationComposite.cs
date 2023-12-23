using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public class ValidationComposite<T> : IValidation<T>
    {
        private readonly List<IValidation<T>> _validations;

        public ValidationComposite(List<IValidation<T>> validations)
        {
            _validations = validations;
        }

        public string Name => throw new NotImplementedException();

        public Func<T, bool> ValidationFunction => throw new NotImplementedException();

        public string MessageOnError => throw new NotImplementedException();

        public Func<T, object> OriginalValue => throw new NotImplementedException();

        public bool IsValid(T value)
            => _validations.All(rule => rule.IsValid(value));

        public IEnumerable<ValidationResponse> Validate(T value)
            => _validations.SelectMany(rule => rule.Validate(value)).ToList();
    }
}
