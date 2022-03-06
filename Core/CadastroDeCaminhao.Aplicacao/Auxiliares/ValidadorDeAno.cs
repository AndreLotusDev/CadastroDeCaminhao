using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeCaminhao.Aplicacao.Auxiliares
{
    public class ValidadorDeAnoAtual : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataAgora = DateTime.Now;

            if (value != null)
            {
                if ((value as DateTime?)?.Year < dataAgora.Year || (value as DateTime?)?.Year > dataAgora.Year)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

    public class ValidadorDeAnoAtualESubsequente : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataAgora = DateTime.Now;

            if (value != null)
            {
                if ((value as DateTime?)?.Year < dataAgora.Year || (value as DateTime?)?.Year > dataAgora.Year + 1)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
