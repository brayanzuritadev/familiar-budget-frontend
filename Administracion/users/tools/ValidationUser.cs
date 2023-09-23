using Data.Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion.users.tools
{
    public class ValidationUser
    {
        public ValidationUser() { }

        public Result userValid(User user)
        {
            var result = new Result();

            var userService = new UserService();

            var userEntity = userService.Get(user);

            if (userEntity?.Ci == user.Ci && user.UserId == 0)
            {
                result.messages[0] = "Esta cedula de identidad ya existe en la base de datos";
                MessageBox.Show(result.messages[0]);
                result.success = false;
            }

            if (userEntity?.Ci == user.Ci && user.UserId != userEntity.UserId)
            {
                result.messages[0] = "Esta cedula de identidad pertence a otro usuario ya registrado";
                MessageBox.Show(result.messages[0]);
                result.success = false;
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                result.messages[1] = "no se esta enviando el nombre";
                MessageBox.Show(result.messages[1]);
                result.success = false;
            }

            if ((userEntity?.Email == user.Email && userEntity?.UserId == 0) || !Regex.IsMatch(user.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
            {
                result.messages[2] = "Este email ya existe o se esta enviando un formato incorrecto de email";
                MessageBox.Show(result.messages[2]);
                result.success = false;
            }


            return result;
        }
    }
}
