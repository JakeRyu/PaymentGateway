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

        public string Value
        {
            get
            {
                Random rnd = new Random();
                var resultBuilder = new StringBuilder();
                var stringLength = OriginalValue.Length;

                var randomIndices = Enumerable.Range(0, stringLength)
                    .OrderBy(x => rnd.Next())
                    .Take(stringLength / 4)
                    .ToList();

                for (int i = 0; i < stringLength; i++)
                {
                    resultBuilder.Append(randomIndices.Contains(i) ? '*' : OriginalValue[i]);
                }

                return resultBuilder.ToString();
            }
        }

        public MaskedString(string originalValue)
        {
            OriginalValue = originalValue;
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