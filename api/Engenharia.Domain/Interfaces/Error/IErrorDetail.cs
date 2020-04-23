using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia.Domain.Interfaces.Error
{
    public interface IErrorDetail
    {
        int StatusCode { get; set; }
        string ErrorMessage { get; set; }

        string ToString();
    }
}
