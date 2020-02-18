using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.ApiModels
{
    public static class BlogMappingExtenstions
    {

        public static BlogModel ToApiModel(this Blog blog)
        {
            return new BlogModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Description = blog.Description,
                // map user's FullName to authorName
                AuthorName = blog.User != null ? blog.User.FullName : null
            };
        }

        public static Blog ToDomainModel(this BlogModel blogModel)
        {
            return new Blog
            {
                Id = blogModel.Id,
                Name = blogModel.Name,
                Description = blogModel.Description,
            };
        }

        public static IEnumerable<BlogModel> ToApiModels(this IEnumerable<Blog> Users)
        {
            return Users.Select(a => a.ToApiModel());
        }

        public static IEnumerable<Blog> ToDomainModels(this IEnumerable<BlogModel> UserModels)
        {
            return UserModels.Select(a => a.ToDomainModel());
        }
    }
}
