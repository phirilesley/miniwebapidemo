using miniwebapidemo.API.Model;

namespace miniwebapidemo.API.Services
{
    public class BlogService : IBlogService
    {
        private readonly List<Blog> _blogs;
        public BlogService()
        {
            _blogs = new List<Blog>();
        }

        public Blog Create(Blog blog)
        {
            blog.Id = GenerateNewId();
            _blogs.Add(blog);
            return blog;
        }

        public void Delete(int id)
        {
            var todoItem = _blogs.FirstOrDefault(item => item.Id == id);
            if (todoItem != null)
            {
                _blogs.Remove(todoItem);
            }
        }

        public IEnumerable<Blog> GetAll()
        {
            return _blogs;
        }

        public Blog GetById(int id)
        {
            return _blogs.FirstOrDefault(item => item.Id == id);
        }

        public void Update(int id, Blog blog)
        {
            var existingTodoItem = _blogs.FirstOrDefault(item => item.Id == id);
            if (existingTodoItem != null)
            {
                existingTodoItem.Title = blog.Title;
                existingTodoItem.Description = blog.Description;
            }
        }

        private int GenerateNewId()
        {
            if (_blogs.Count > 0)
            {
                return _blogs.Max(item => item.Id) + 1;
            }
            return 1;
        }
    }
}