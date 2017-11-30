using System.Collections.Generic;
using AcademiaCerului.Core.Objects;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System;

namespace AcademiaCerului.Core
{
    public class BlogRepository : IBlogRepository
    {
        public readonly ISession _session;

        public BlogRepository(ISession session)
        {
            _session = session;
        }

        public IList<Category> Categories()
        {
            return _session.Query<Category>().OrderBy(x => x.Name).ToList();
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>().FirstOrDefault(x => x.UrlSlug.Equals(categorySlug));
        }

        public Post Post(int year, int month, string titleSlug)
        {
            var query = _session.Query<Post>()
                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().Single();
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

        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
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

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
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

        public Tag Tag(string tagSlug)
        {
            return _session.Query<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public int TotalPosts()
        {
            return _session.Query<Post>().Where(p => p.Published).Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>().Where(x => x.Published && x.Category.UrlSlug.Equals(categorySlug)).Count();
        }

        public int TotalPostsForSearch(string search)
        {
            return _session.Query<Post>().Where(x => x.Published && (x.Title.Contains(search) || x.Category.Name.Equals(search) || x.Tags.Any(t => t.Name.Equals(search)))).Count();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _session.Query<Post>().Where(x => x.Published && x.Tags.Any(t => t.UrlSlug.Equals(tagSlug))).Count();
        }
    }
}
