using System;
using System.Collections.Generic;
using AcademiaCerului.Core.Objects;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace AcademiaCerului.Core
{
    public class BlogRepository : IBlogRepository
    {
        public readonly ISession _session;

        public BlogRepository(ISession session)
        {
            _session = session;
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>().FirstOrDefault(x => x.UrlSlug.Equals(categorySlug));
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                                .Where(p => p.Published)
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Fetch(p => p.Category)
                                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                    .Where(p => postIds.Contains(p.Id))
                    .OrderByDescending(p => p.PostedOn)
                    .FetchMany(p => p.Tags)
                    .ToList();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                .Fetch(p => p.Category)
                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                .Where(p => postIds.Contains(p.Id))
                .OrderByDescending(p => p.PostedOn)
                .FetchMany(p => p.Tags)
                .ToList();
        }

        public int TotalPosts()
        {
            return _session.Query<Post>().Where(p => p.Published).Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>().Where(x => x.Published && x.Category.UrlSlug.Equals(categorySlug)).Count();
        }
    }
}
