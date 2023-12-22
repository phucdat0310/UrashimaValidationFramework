using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    using System;
    using System.Collections.Generic;

    interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string data);
    }

    class Data : ISubject
    {
        private string value;
        private List<IObserver> observers = new List<IObserver>();

        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                Notify(value);
            }
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string data)
        {
            foreach (IObserver observer in observers)
            {
                data = observer.Validate(data);
            }
            Console.WriteLine($"Validated data: {data}");
        }
    }

    interface IObserver
    {
        string Validate(string data);
    }

    class LengthValidator : IObserver
    {
        private int length;

        public LengthValidator(int length)
        {
            this.length = length;
        }

        public string Validate(string data)
        {
            if (data.Length != length)
            {
                Console.WriteLine($"Length must be {length} characters");
            }
            return data;
        }
    }

    class NumericValidator : IObserver
    {
        public string Validate(string data)
        {
            if (!int.TryParse(data, out _))
            {
                Console.WriteLine("Value must be numeric");
            }
            return data;
        }
    }

    class Decorator : IObserver
    {
        private IObserver validator;

        public Decorator(IObserver validator)
        {
            this.validator = validator;
        }

        public string Validate(string data)
        {
            return validator.Validate(data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            data.Attach(new Decorator(new LengthValidator(5)));
            data.Attach(new Decorator(new NumericValidator()));

            data.Value = "123";
            data.Value = "12345";
            data.Value = "123456";
        }
    }
}
