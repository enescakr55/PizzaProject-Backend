using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PizzaOrderValidation:AbstractValidator<PizzaOrder>
    {
        public PizzaOrderValidation()
        {
            int i;
            RuleFor(p => p.creditCard.CardNumber).Length(12).When(p => p.order.PayWithCard == true).WithMessage("Kart numarası 12 haneli olmalı");
            RuleFor(p => p.creditCard.CardNumber).Must(isNumeric).When(p => p.order.PayWithCard == true).WithMessage("Kart numarası yalnızca sayı olabilir");
            RuleFor(p => p.creditCard.Cvv).Length(3).When(p => p.order.PayWithCard == true).WithMessage("CVV 3 haneli olmalı");
            RuleFor(p => p.creditCard.Cvv).Must(isNumeric).When(p => p.order.PayWithCard == true).WithMessage("CVV yalnızca sayı olabilir");
            RuleFor(p => p.creditCard.FullName).MinimumLength(2).When(p => p.order.PayWithCard == true).WithMessage("Kredi kartı sahibi ismi minimum 2 karakter olabilir");
            RuleFor(p => p.creditCard.LastDate).Must(LastDateController).When(p => p.order.PayWithCard == true).WithMessage("Son kullanım tarihini mm/yy şeklinde giriniz");
            RuleFor(p => p.order.Address).NotEmpty().WithMessage("Adres bilgisi boş geçilemez");
            RuleFor(p => p.order.FirstName).NotEmpty().WithMessage("Ad bilgisi boş geçilemez");
            RuleFor(p => p.order.LastName).NotEmpty().WithMessage("Soyad bilgisi boş geçilemez");
            RuleFor(p => p.order.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez");
            RuleFor(p => p.order.PhoneNumber).Must(phoneNumberControl).WithMessage("Telefon numarası 5XX XXX XX XX şeklinde yazılmalıdır");
            RuleFor(p => p.order.PhoneNumber).Must(isNumeric).WithMessage("Telefon numarası yalnızca sayı olabilir");
            
        }
        public bool phoneNumberControl(string phoneNumber=null)
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
        public bool isNumeric(string str=null)
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
        public bool LastDateController(string lastDate=null)
        {
            if(lastDate != null)
            {
                if (lastDate.Length == 5)
                {
                    int n;
                    bool isNumeric = int.TryParse(lastDate.Substring(0, 2),out n);
                    bool isNumeric2 = int.TryParse(lastDate.Substring(3, 2),out n);
                    bool isSlash = lastDate[2] == '/' ? true : false;
                    if(isNumeric && isNumeric2 && isSlash)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
