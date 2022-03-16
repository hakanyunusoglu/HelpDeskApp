using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPPORTMVC.BLL.Results
{
    public class ErrorsResults<T> where  T: class
    {
        public List<string> Errors { get; set; }
        public T Result { get; set; }

        public ErrorsResults()
        {
            Errors = new List<string>();
        }
    }
}