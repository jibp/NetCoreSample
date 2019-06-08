using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthSample.Entity;
using JwtAuthSample.Entity.MoreToMore;
using JwtAuthSample.Models;
using JwtAuthSample.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthSample.Controllers
{
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    //[HiddenApi]
    public class ValuesController : ControllerBase
    {
        private readonly JwtDbContext _context;
        public ValuesController(JwtDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (ModelState.IsValid)
            {
                //TODO:...
            }
            /*
            var products = from m in _context.Categorys
                           from n in m.ProductList
                           select new
                           {
                               m.CategoryName,
                               n.Name
                           };
            foreach (var item in products)
            {
                Console.WriteLine("分类：{0}\t{1}",item.CategoryName,item.Name);
            }
            **/
            /*
            var products = _context.Categorys.SelectMany(c => c.ProductList.Select(
                p => new
                {
                    CategoryName = c.CategoryName,
                    ProductName = p.Name
                }
                ));
                **/
            /*
        var cbooks = from c in _context.Categorys
                     join d in _context.Products
                     on
                     c.Id equals d.CategoryId select new {
                         BookCategory = c.CategoryName,
                         BookName = d.Name

                     } into kk orderby kk.BookName select kk;

        **/

            #region Join 内关联
            var books = _context.Categorys.Join(_context.Products,
                    c => c.Id,
                    d => d.CategoryId, (c, d) => new
                    {
                        BookCategory = c.CategoryName,
                        BookName = d.Name
                    }

                    ).OrderByDescending(x => x.BookCategory);


            foreach (var item in books)
            {
                Console.WriteLine("书籍分类：{0},书名:{1}", item.BookCategory, item.BookName);
            }

            #endregion


            #region GroupJon 会同时取出Left所有的Outer的部分
            /* 
            List<Category> categories = new List<Category>()
            {
                new Category(){Id="11",CategoryName="分类1" },
                new Category(){Id="22",CategoryName="分类2" },
                 new Category(){Id="33",CategoryName="分类3" }

            };

            List<Product> products = new List<Product>()
            {
                new Product(){Id="1",Name="asp.net book1",Price=30,CategoryId="11" },
                new Product(){Id="2",Name="asp.net book2",Price=40,CategoryId="11" },
                new Product(){Id="3",Name="asp.net book3",Price=50,CategoryId="22" },
                new Product(){Id="4",Name="asp.net book4",Price=60,CategoryId="22" },
                new Product(){Id="5",Name="asp.net book5",Price=70,CategoryId="11" },

            };
            var books = categories.GroupJoin(products,
                c => c.Id,
                d => d.CategoryId,
                (c, bgroup) => new
                {
                    BookCategory = c.CategoryName,
                    GBook = bgroup
                }
            );

            foreach (var item in books)
            {
                Console.WriteLine("书籍分类：{0}", item.BookCategory);
                foreach (var c in item.GBook)
                {
                    System.Console.WriteLine("{0}\t{1}", c.CategoryId, c.Name);
                }
            }
            **/
            #endregion

            var tags = new[]
            {
                new Tag { Text = "C#" },
                new Tag { Text = "Python" },
                new Tag { Text = "java" },
                new Tag { Text = "react" }
            };

            var posts = new[]
            {
                new Post { Title = "博客1" },
                new Post { Title = "博客2" },
                new Post { Title = "博客3" }
            };

            _context.AddRange(
                new PostTag { Post = posts[0], Tag = tags[0] },
                new PostTag { Post = posts[0], Tag = tags[1] },
                new PostTag { Post = posts[1], Tag = tags[2] },
                new PostTag { Post = posts[1], Tag = tags[3] },
                new PostTag { Post = posts[2], Tag = tags[0] },
                new PostTag { Post = posts[2], Tag = tags[1] },
                new PostTag { Post = posts[2], Tag = tags[2] },
                new PostTag { Post = posts[2], Tag = tags[3] });

          //  _context.SaveChanges();

            var postlst = _context.Posts
           .Include(e => e.postTags)
           .ThenInclude(e => e.Tag)
           .ToList();

            foreach (var post in postlst)
            {
                Console.WriteLine($"  Post {post.Title}");
                foreach (var tag in post.postTags.Select(e => e.Tag))
                {
                    Console.WriteLine($"    Tag {tag.Text}");
                }
            }

            Console.WriteLine();



            return new string[] { "value1", "value22" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
