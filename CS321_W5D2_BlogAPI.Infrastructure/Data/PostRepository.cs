using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;
        public PostRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
        // Implement Get(id). Include related Blog and Blog.User
        Post post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

        if (post == null)
            return null;

        return _dbContext.Posts
            .Include(p => p.Blog)
                .ThenInclude(b => b.User)
            .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
        //  Implement GetBlogPosts, return all posts for given blog id
        // Include related Blog and AppUser
        return _dbContext.Posts
            .Include(p => p.Blog)
                .ThenInclude(b => b.User)
            .Where(p => p.BlogId == blogId)
            .ToList();
        }

        public Post Add(Post Post)
        {
        // add Post
        Post newPost = _dbContext.Posts.FirstOrDefault(p => p.Id == Post.Id);

        if (newPost != null)
            return null;

        _dbContext.Posts.Add(Post);
        _dbContext.SaveChanges();

        return Post;
        }

        public Post Update(Post Post)
        {
        // update Post
        var existingPost = _dbContext.Posts.FirstOrDefault(p => p.Id == Post.Id);

        if (existingPost == null)
            return null;

        _dbContext.Entry(existingPost)
            .CurrentValues
            .SetValues(Post);
        _dbContext.Posts.Update(existingPost);
        _dbContext.SaveChanges();

        return existingPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // get all posts
            return _dbContext.Posts
                .Include(p => p.Blog)
                    .ThenInclude(b => b.User)
                .ToList();
        }

        public void Remove(int id)
        {
        // remove Post
        Post post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

        _dbContext.Posts.Remove(post);
        _dbContext.SaveChanges();
        }

    }
}
