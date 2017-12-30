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

        public int AddPost(Post post)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(post);
                transaction.Commit();
                return post.Id;
            }
        }

        public IList<Category> Categories()
        {
            return _session.Query<Category>().OrderBy(x => x.Name).ToList();
        }

        public Category Category(int id)
        {
            return _session.Query<Category>().FirstOrDefault(x => x.Id == id);
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>().FirstOrDefault(x => x.UrlSlug.Equals(categorySlug));
        }

        public void EditPost(Post post)
        {
            using(var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(post);
                transaction.Commit();
            }
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

        public IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Post> posts;
            IList<int> postIds;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                    {
                        posts = _session.Query<Post>()
                            .OrderBy(p => p.Title)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderBy(p => p.Title)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = _session.Query<Post>()
                            .OrderByDescending(p => p.Title)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.Title)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    break;

                case "Published":
                    if (sortByAscending)
                    {
                        posts = _session.Query<Post>()
                            .OrderBy(p => p.Published)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.Published)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = _session.Query<Post>()
                            .OrderByDescending(p => p.Published)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.Published)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    break;

                case "PostedOn":
                    if (sortByAscending)
                    {
                        posts = _session.Query<Post>()
                            .OrderBy(p => p.PostedOn)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderBy(p => p.PostedOn)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = _session.Query<Post>()
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.PostedOn)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    break;

                case "Modified":
                    if (sortByAscending)
                    {
                        posts = _session.Query<Post>()
                            .OrderBy(p => p.Modified)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderBy(p => p.Modified)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = _session.Query<Post>()
                            .OrderByDescending(p => p.Modified)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.Modified)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    break;

                case "Category":
                    if (sortByAscending)
                    {
                        posts = _session.Query<Post>()
                            .OrderBy(p => p.Category.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderBy(p => p.Category.Name)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    else
                    {
                        posts = _session.Query<Post>()
                            .OrderByDescending(p => p.Category.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(p => p.Category)
                            .ToList();

                        postIds = posts.Select(p => p.Id).ToList();

                        posts = _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.Category.Name)
                            .FetchMany(p => p.Tags)
                            .ToList();
                    }
                    break;

                default:
                    posts = _session.Query<Post>()
                        .OrderByDescending(p => p.PostedOn)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Fetch(p => p.Category)
                        .ToList();

                    postIds = posts.Select(p => p.Id).ToList();

                    posts = _session.Query<Post>()
                        .Where(p => postIds.Contains(p.Id))
                        .OrderByDescending(p => p.PostedOn)
                        .FetchMany(p => p.Tags)
                        .ToList();
                    break;
            }

            return posts;
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

        public Tag Tag(int id)
        {
            return _session.Query<Tag>().FirstOrDefault(x => x.Id == id);
        }

        public Tag Tag(string tagSlug)
        {
            return _session.Query<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public IList<Tag> Tags()
        {
            return _session.Query<Tag>().OrderBy(x => x.Name).ToList();
        }

        public int TotalPosts(bool isPublished = true)
        {
            return _session.Query<Post>()
                .Where(p => !isPublished || p.Published == true)
                .Count();
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
