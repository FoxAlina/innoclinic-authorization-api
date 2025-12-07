using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Domain.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException(string email) : base($"User with email '{email}' already exists.") { }
    }
}
