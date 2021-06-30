using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidation:AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName).Must(OnlyLetter).WithMessage("Adınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.LastName).Must(OnlyLetter).WithMessage("Soyadınızda yalnızca harfler kullanılabilir");
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifreniz minimum 8 karakter olmalıdır");
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MinimumLength(8).WithMessage("Kullanıcı adınız minimum 8 karakter olmalıdır");
            RuleFor(p => p.Username).Must(LetterNumberAndUnderScore).WithMessage("Kullanıcı adında yalnızca harfler sayılar ve alt tire kullanılabilir.");
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).Must(phoneNumberControl).WithMessage("Telefon numarası 5XX XXX XX XX şeklinde yazılmalıdır");
        }
        public bool OnlyLetter(string str="")
        {
            try
            {
                return Regex.IsMatch(str, @"^[a-zA-ZçöşğüÜĞŞıİÇÖ]+$");
            }
            catch
            {
                return false;
            }
            
        }
        public bool LetterNumberAndUnderScore(string str="")
        {
            try
            {
                return Regex.IsMatch(str, @"^[a-zA-Z0-9_]+$");
            }
            catch
            {
                return false;
            }
            
        }
        public bool phoneNumberControl(string phoneNumber = null)
        {
            if (phoneNumber == null)
                return false;
            string number = phoneNumber.Replace(" ", "");
            if (isNumeric(number))
            {
                if (number.StartsWith("5"))
                {
                    if (number.Length == 10)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool isNumeric(string str = null)
        {
            if (str == null)
                return false;
            str = str.Replace(" ", "");
            long n;
            bool numeric = long.TryParse(str, out n);
            if (numeric)
                return true;
            return false;
        }
    }

}
