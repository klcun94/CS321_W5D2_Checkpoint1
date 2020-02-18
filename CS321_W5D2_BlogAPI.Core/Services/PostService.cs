using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;
        }

        public Post Add(Post newPost)
        {
            // Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            //     You may have to retrieve the blog in order to check user id
            // assign the current date to DatePublished
            Blog blog = _blogRepository.Get(newPost.BlogId);
            if (blog == null)
            {
                throw new ApplicationException("Cannot find the requested blog.");
            }

            var postUserID = blog.UserId;

            if (_userService.CurrentUserId != postUserID)
            {
                throw new ApplicationException("You can only make posts to your blog.");
            }

            newPost.DatePublished = DateTime.Now;
            return _postRepository.Add(newPost);
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            var post = this.Get(id);
            // prevent user from deleting from a blog that isn't theirs
            if (post.Blog?.UserId != _userService.CurrentUserId)
            {
                throw new ApplicationException("You can only modify blogs that you created.");
            }
            _postRepository.Remove(id);
        }

        public Post Update(Post updatedPost)
        {
            var postUserId = updatedPost.Blog?.UserId;
            // prevent user from updating a blog that isn't theirs
            if (postUserId != _userService.CurrentUserId)
            {
                throw new ApplicationException("You can only modify blog posts that you created.");
            }
            return _postRepository.Update(updatedPost);
        }

    }
}
