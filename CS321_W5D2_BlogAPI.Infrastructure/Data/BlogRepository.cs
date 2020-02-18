using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogRepository(AppDbContext dbContext) 
        {
            // inject AppDbContext
            _dbContext = dbContext;
        }

        public IEnumerable<Blog> GetAll()
        {
            // Retrieve all blogs. Include Blog.User.
            return _dbContext.Blogs
                .Include(b => b.User)
                .ToList();
        }

        public Blog Get(int id)
        {
            //  Retrieve the blog by id. Include Blog.User.
            Blog blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);

            if (blog == null)
                return null;

            return _dbContext.Blogs
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);
        }

        public Blog Add(Blog blog)
        {
            // Add new blog
            Blog newBlog = _dbContext.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (newBlog != null)
                return null;

            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();

            return blog;
        }

        public Blog Update(Blog updatedItem)
        {
            // update blog
            var existingItem = _dbContext.Blogs.Find(updatedItem);
            if (existingItem == null) return null;
            _dbContext.Entry(existingItem)
               .CurrentValues
               .SetValues(updatedItem);
            _dbContext.Blogs.Update(existingItem);
            _dbContext.SaveChanges();
            return existingItem;
        }

        public void Remove(int id)
        {
            // remove blog
            Blog blog = Get(id);

            _dbContext.Blogs.Remove(blog);
            _dbContext.SaveChanges();
        }
    }
}
