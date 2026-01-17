using BlogApi.Application.Commands.Category;
using BlogApi.Application.Commands.Category.AddCategory;
using BlogApi.Domain.Common;
using BlogApi.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Application.Commands.Category
{
    public class AddCategoryCommandHandler(IAppDbContext context) : IRequestHandler<AddCategoyCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddCategoyCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result<int>.Failure("Category name is required");
            }


            var normalizedName = request.Name.Trim();


            var existingCategory = await context.Categories
                .FirstOrDefaultAsync(s => s.Name!.ToLower() == normalizedName.ToLower(), cancellationToken);

            if (existingCategory != null)
            {
                return Result<int>.Conflict();
            }


            var slug = SlugHelper.Generate(normalizedName);

         
            var slugExists = await context.Categories
                .AnyAsync(s => s.Slug == slug, cancellationToken);

            if (slugExists)
            {
               
                slug = $"{slug}-{Guid.NewGuid().ToString().Substring(0, 8)}";
            }


            var category = new BlogApi.Domain.Entities.Category
            {
                Name = normalizedName,
                Slug = slug,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow.AddHours(8)
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync(cancellationToken);
      
            return Result<int>.Success(category.Id);
        }
    }
}