using BlogApi.Application.Commands.Tags.AddTag;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Tags.AddTag
{
    public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
    {
        public AddTagCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name is required")
                .MaximumLength(50).WithMessage("Tag name must not exceed 50 characters")
              .Matches("^[a-zA-Z0-9]+([a-zA-Z0-9 _-]*[a-zA-Z0-9]+)?$").WithMessage("Tag name can only contain letters, numbers, hyphens, underscores, and spaces");
        }
    }
}
