﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.StorageSystem.Concrete.Validation
{
    [Serializable]
    public abstract class ValidatorBase<T> : MarshalByRefObject
    {
        public class Rule
        {
            public Func<T,bool> Test { get; set; }
            public string Message { get; set; }
          
        }
        protected abstract IEnumerable<Rule> Rules { get; }

        public IEnumerable<string> Validate(T t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t),"User must not be a null");
            return Rules?.Where(r => !r.Test(t)).Select(r => r.Message);
        }

        //public void Validate(T t)
        //{
        //    var errorMessages = Validate(t);

        //    if (errorMessages == null)
        //        return;

        //    var messages = errorMessages as IList<string> ?? errorMessages.ToList();

        //    if (messages.Any())
        //    {
        //        throw new ArgumentException("Entity is not valid:\n" + string.Join("\n", messages));
        //    }
        //}

    }
}
