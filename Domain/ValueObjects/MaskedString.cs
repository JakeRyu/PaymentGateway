using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Common;

namespace Domain.ValueObjects
{
    public class MaskedString : ValueObject
    {
        public string OriginalValue { get; private set; }
        public string Value { get; private set; }

        private MaskedString()
        {
        }

        public static MaskedString For(string value)
        {
            var maskedString = new MaskedString {OriginalValue = value};

            Random rnd = new Random();
            var resultBuilder = new StringBuilder();
            var stringLength = value.Length;

            var randomIndices = Enumerable.Range(0, stringLength)
                .OrderBy(x => rnd.Next())
                .Take(stringLength / 4)
                .ToList();

            for (int i = 0; i < stringLength; i++)
            {
                resultBuilder.Append(randomIndices.Contains(i) ? '*' : value[i]);
            }

            maskedString.Value = resultBuilder.ToString();

            return maskedString;
        }

        public static implicit operator string(MaskedString maskedString)
        {
            return maskedString.ToString();
        }
        
        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return OriginalValue;
        }
    }
}