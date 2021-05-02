using FluentValidation;
using MainApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.Validators
{
    class AddPageVmValidator : AbstractValidator<AddPageVM>
    {
        public AddPageVmValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("The City Name is too short!");

            RuleFor(x => x.Latitude)
                .NotEmpty()
                .Must(x =>
                {
                    if (float.TryParse(x, out float lat))
                    {
                        return -90 <= lat && lat <= 90;
                    }
                    return false;
                })
                .WithMessage("The Latitude is invalid!");

            RuleFor(x => x.Longitude)
                .NotEmpty()
                .Must(x =>
                {
                    if (float.TryParse(x, out float lon))
                    {
                        return -180 <= lon && lon <= 180;
                    }
                    return false;
                })
                .WithMessage("The Longitude is invalid!");
        }
    }
}
