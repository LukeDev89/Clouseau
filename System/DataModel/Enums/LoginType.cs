using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Enums
{
    public enum LoginType
    {
        Correcto,
        UsuarioIncorrecto,
        PasswordIncorrecto,
        UsuarioInactivo,
        ErrorDesconocido,
        RequestError
    }
}
