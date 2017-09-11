using System;
using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Classes
{
    public abstract class Values
    {
        public int IdxValue { get; }
        public string Name { get; }
        public bool ToIgnore { get; }
        public bool ToRound { get; }

        protected Values(int idxValue, string name, bool toIgnore, bool toRound)
        {
            IdxValue = idxValue;
            Name = name;
            ToIgnore = toIgnore;
            ToRound = toRound;
        }
        protected Values() { }

        public abstract IEnumerable<Values> GetValues { get; }

        public Values GetValue(int key)
        {
            try
            {
                return GetValues.First(v => v.IdxValue == key);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
