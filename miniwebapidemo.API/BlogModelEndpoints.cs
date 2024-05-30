using Microsoft.AspNetCore.Http.HttpResults;
using miniwebapidemo.API.Model;
using miniwebapidemo.API.Services;

namespace miniwebapidemo.API
{
    public static class BlogModelEndpoints
    {
        public static void MapBlogModelEndPoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Blog").WithTags(nameof(Blog));

            group.MapGet("/" , async (IBlogService blogService) =>
            {
                return blogService.GetAll();
            }).WithName("GetAllBlogs").
            WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<Blog>, NotFound>> (int id, IBlogService blogService) =>
            {
                var todoItem = blogService.GetById(id);

                if (todoItem == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(todoItem);


            })
           .WithName("GetBlogById")
           .WithOpenApi();

            group.MapPost("/", async (Blog blogModel, IBlogService blogService) =>
            {
                var createdTodoItem = blogService.Create(blogModel);
                return TypedResults.Created($"/api/GetBlogById/{blogModel.Id}", blogModel);
            })
           .WithName("CreateBlog")
           .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Blog blogModel, IBlogService blogService) =>
            {
                blogService.Update(id, blogModel);
                return TypedResults.Ok();

            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, IBlogService blogService) =>
            {
                blogService.Delete(id);
                return TypedResults.Ok();
            })
           .WithName("DeleteBlog")
           .WithOpenApi();

        }
    }
}
