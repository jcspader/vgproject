using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using VG.Infra.Data.Context;
using VG.Infra.Data.Entities;
using VG.Infra.Data.Repositories;

namespace VG.Infra.Data.Tests.Repository
{
    class RepositoryTests
    {
        [Test]
        public void Add_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testObject = new ModelEntity();

            var context = new Mock<DataBaseContext>();
            var dbSetMock = new Mock<DbSet<ModelEntity>>();
            context.Setup(x => x.Set<ModelEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<ModelEntity>()));

            // Act
            var repository = new ModelRepository(context.Object);
            repository.AddAsync(testObject).Wait();

            //Assert
            context.Verify(x => x.Set<ModelEntity>());
            dbSetMock.Verify(x => x.Add(It.Is<ModelEntity>(y => y == testObject)));
        }
    }
}
