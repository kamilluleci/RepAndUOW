using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepAndUOW.Data.Context;
using RepAndUOW.Data.Model;
using RepAndUOW.Data.Repositories;
using RepAndUOW.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepAndUOW.Presentation.UnitTest
{
    /// <summary>
    /// Summary description for EntityTest
    /// </summary>
    [TestClass]
    public class EntityTest
    {
        private EFBlogContext _dbContext;

        private IUnitOfWork _uow;
        private IRepository<User> _userRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Article> _articleRepository;

        [TestInitialize()]
        public void TestInitialize()
        {
            _dbContext = new EFBlogContext();
            _uow = new EFUnitOfWork(_dbContext);
            _userRepository = new EFRepository<User>(_dbContext);
            _categoryRepository = new EFRepository<Category>(_dbContext);
            _articleRepository = new EFRepository<Article>(_dbContext);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            _dbContext = null;
            _uow.Dispose();
        }

        [TestMethod]
        public void GetUser()
        {
            User user = _userRepository.GetById(1);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = _userRepository.GetById(1);
            user.FirstName = "Kamil";
            _userRepository.Update(user);
            int process = _uow.SaveChanges();
            Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void DeleteUser()
        {
            User user = _userRepository.GetById(1);
            _userRepository.Delete(user);
            int process = _uow.SaveChanges();
            Assert.AreNotEqual(-1, process);
        }
    }
}
