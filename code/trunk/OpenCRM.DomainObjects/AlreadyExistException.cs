﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCRM.DB.DomainObjects
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message)
            : base(message)
        {

        }
    }
}