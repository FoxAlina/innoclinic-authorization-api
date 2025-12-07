using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Application.Interfaces
{
    public interface IPasswordHasherNode
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
