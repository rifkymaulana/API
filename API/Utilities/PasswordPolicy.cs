using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.Utilities;

public class PasswordPolicy : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var password = value as string;
        if (password == null)
        {
            return false;
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");

        bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) &&
               hasMinimum8Chars.IsMatch(password);
        ErrorMessage = "Password must contain at least 8 characters, one uppercase, one lowercase and one number";
        return isValidated;
    }
}