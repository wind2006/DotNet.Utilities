using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YanZhiwei.DotNet.ServiceStack.RedisExamples
{
    internal class Program
    {
        private static readonly RedisClient redis = new RedisClient("192.168.1.240", 6379);

        private static void Main(string[] args)
        {
            try
            {
                redis.FlushAll();
                var redisUsers = redis.As<User>();
                var redisBlogs = redis.As<Blog>();
                var redisBlogPosts = redis.As<BlogPost>();

                var yangUser = new User { Id = redisUsers.GetNextSequence(), Name = "Eric Yang" };
                var zhangUser = new User { Id = redisUsers.GetNextSequence(), Name = "Fish Zhang" };

                var yangBlog = new Blog
                {
                    Id = redisBlogs.GetNextSequence(),
                    UserId = yangUser.Id,
                    UserName = yangUser.Name,
                    Tags = new List<string> { "Architecture", ".NET", "Databases" },
                };

                var zhangBlog = new Blog
                {
                    Id = redisBlogs.GetNextSequence(),
                    UserId = zhangUser.Id,
                    UserName = zhangUser.Name,
                    Tags = new List<string> { "Architecture", ".NET", "Databases" },
                };

                var blogPosts = new List<BlogPost>
                {
                    new BlogPost
                    {
                    Id = redisBlogPosts.GetNextSequence(),
                    BlogId = yangBlog.Id,
                    Title = "Memcache",
                    Categories = new List<string> { "NoSQL", "DocumentDB" },
                    Tags = new List<string> {"Memcache", "NoSQL", "JSON", ".NET"} ,
                        Comments = new List<BlogPostComment>
                        {
                            new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,},
                            new BlogPostComment { Content = "Second Comment!", CreatedDate = DateTime.UtcNow,},
                        }
                    },
                    new BlogPost
                    {
                        Id = redisBlogPosts.GetNextSequence(),
                        BlogId = zhangBlog.Id,
                        Title = "Redis",
                        Categories = new List<string> { "NoSQL", "Cache" },
                        Tags = new List<string> {"Redis", "NoSQL", "Scalability", "Performance"},
                        Comments = new List<BlogPostComment>
                        {
                            new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
                        }
                    },
                    new BlogPost
                    {
                        Id = redisBlogPosts.GetNextSequence(),
                        BlogId = yangBlog.Id,
                        Title = "Cassandra",
                        Categories = new List<string> { "NoSQL", "Cluster" },
                        Tags = new List<string> {"Cassandra", "NoSQL", "Scalability", "Hashing"},
                        Comments = new List<BlogPostComment>
                        {
                            new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
                        }
                    },
                    new BlogPost
                    {
                        Id = redisBlogPosts.GetNextSequence(),
                        BlogId = zhangBlog.Id,
                        Title = "Couch Db",
                        Categories = new List<string> { "NoSQL", "DocumentDB" },
                        Tags = new List<string> {"CouchDb", "NoSQL", "JSON"},
                        Comments = new List<BlogPostComment>
                        {
                            new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
                        }
                    },
                };

                yangUser.BlogIds.Add(yangBlog.Id);
                yangBlog.BlogPostIds.AddRange(blogPosts.Where(x => x.BlogId == yangBlog.Id).Select(x => x.Id));

                zhangUser.BlogIds.Add(zhangBlog.Id);
                zhangBlog.BlogPostIds.AddRange(blogPosts.Where(x => x.BlogId == zhangBlog.Id).Select(x => x.Id));

                redisUsers.Store(yangUser);
                redisUsers.Store(zhangUser);
                redisBlogs.StoreAll(new[] { yangBlog, zhangBlog });
                redisBlogPosts.StoreAll(blogPosts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

      
    }
}