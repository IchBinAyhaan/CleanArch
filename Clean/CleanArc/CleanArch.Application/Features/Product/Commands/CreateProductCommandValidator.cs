﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Features.Product.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad mutleq daxil edilmelidir");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Say en azi 1 olmalidir");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Qiymet 0-dan boyuk olmalidir");

            RuleFor(x => x.Description)
                .MinimumLength(20)
                .MaximumLength(200)
                .WithMessage("Aciqlama 20-200 simvol araliginda olmalidir");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Tip duzgun daxil edilmeyib");

            RuleFor(x => x.Photo)
                .NotEmpty()
                .WithMessage("Sekil  daxil edilmelidir");

            RuleFor(x => x.Photo)
                .Must(IsCorrectFormat)
                .WithMessage("Sekil duzgun daxil edilmelidir");
        }
        private bool IsCorrectFormat(string photo)
        {
            try
            {
                _ = Convert.FromBase64String(photo);
                var data = photo.Substring(0, 5);

                switch (data.ToUpper())
                {
                    case "IVBOR":
                    case "/9J/4":
                        return true;
                    default:
                        return false;
                };
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
