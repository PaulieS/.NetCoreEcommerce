using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService
{

    public class ProductServiceException : Exception
    {
        public ProductServiceException() { }
        public ProductServiceException(string message) : base(message) { }
        public ProductServiceException(string message, Exception inner) : base(message, inner) { }
    }
    public class ProductNotFoundException : ProductServiceException
    {
        public ProductNotFoundException() { }
        public ProductNotFoundException(string message) : base(message) { }
        public ProductNotFoundException(string message, Exception inner) : base(message, inner) { }
      
    }

    public class ProductsServiceNotInitialisedException : ProductServiceException
    {
        public ProductsServiceNotInitialisedException() { }
        public ProductsServiceNotInitialisedException(string message) : base(message) { }
        public ProductsServiceNotInitialisedException(string message, Exception inner) : base(message, inner) { }
 
    }
}
