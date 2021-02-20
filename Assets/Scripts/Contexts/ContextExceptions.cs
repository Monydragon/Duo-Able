using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextExceptions : MonoBehaviour
{
    public class ContextNotFoundException : Exception 
    {
        public ContextNotFoundException()
        {

        }

        public ContextNotFoundException(string message) : base(message)
        {

        }

        public ContextNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
