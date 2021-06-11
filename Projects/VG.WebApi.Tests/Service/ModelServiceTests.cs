using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using VG.Domain.Dto;
using VG.Domain.Services;
using VG.Infra.Data.Context;
using VG.Infra.Data.Entities;
using VG.Infra.Data.Repositories;

namespace VG.WebApi.Tests.Service
{
    [TestFixture]
    class ModelServiceTests
    {
        [Test]
        public void AddNewModel_ModelService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<ModelEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Models).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ModelEntity>(It.IsAny<ModelDto>())).Returns(new ModelEntity() { Name = "New Model", Id = default });


            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);
            var mockModelRepository = new Mock<ModelRepository>(mockContext.Object);


            var service = new ModelService(mockModelRepository.Object, mockTruckRepository.Object, mapperMock.Object);
            service.AddAsync(new ModelDto() { Name = "New Model" }).Wait();

            mockContext.Verify(m => m.AddAsync(It.IsAny<ModelEntity>(), It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Delete_ModelService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<ModelEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Models).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ModelEntity>(It.IsAny<ModelDto>())).Returns(new ModelEntity());

            var testObject = new ModelEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);
            var mockModelRepository = new Mock<ModelRepository>(mockContext.Object) { CallBase = true }.As<IModelRepository>();

            mockModelRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testObject);
            mockContext.Setup(x => x.Remove(It.IsAny<ModelEntity>()));
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


            var service = new ModelService(mockModelRepository.Object, mockTruckRepository.Object, mapperMock.Object);
            service.DeleteAsync(1).Wait();

            mockContext.Verify(m => m.Remove(It.IsAny<ModelEntity>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Update_ModelService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<ModelEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Models).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ModelEntity>(It.IsAny<ModelDto>())).Returns(new ModelEntity());

            var testObject = new ModelEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);
            var mockModelRepository = new Mock<ModelRepository>(mockContext.Object) { CallBase = true }.As<IModelRepository>();

            mockModelRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testObject);
            mockContext.Setup(x => x.Update(It.IsAny<ModelEntity>()));
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


            var service = new ModelService(mockModelRepository.Object, mockTruckRepository.Object, mapperMock.Object);
            service.UpdateAsync(new ModelDto()).Wait();

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void GetById_ModelService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockDbSet = new Mock<DbSet<ModelEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Models).Returns(mockDbSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ModelEntity>(It.IsAny<ModelDto>())).Returns(new ModelEntity());

            var testObject = new ModelEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);
            var mockModelRepository = new Mock<ModelRepository>(mockContext.Object) { CallBase = true }.As<IModelRepository>();

            mockDbSet.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(testObject);


            var service = new ModelService(mockModelRepository.Object, mockTruckRepository.Object, mapperMock.Object);
            service.GetByIdAsync(1).Wait();

            mockContext.Verify(x => x.Set<ModelEntity>());
            //mockDbSet.Verify(m => m.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ModelService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockDbSet = new Mock<DbSet<ModelEntity>>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ModelEntity>(It.IsAny<ModelDto>())).Returns(new ModelEntity());

            var testObject = new List<ModelEntity>() { new ModelEntity() };

            mockDbSet.As<IQueryable<ModelEntity>>().Setup(x => x.Provider).Returns(testObject.AsQueryable().Provider);
            mockDbSet.As<IQueryable<ModelEntity>>().Setup(x => x.Expression).Returns(testObject.AsQueryable().Expression);
            mockDbSet.As<IQueryable<ModelEntity>>().Setup(x => x.ElementType).Returns(testObject.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<ModelEntity>>().Setup(x => x.GetEnumerator()).Returns(testObject.AsQueryable().GetEnumerator());

            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Models).Returns(mockDbSet.Object);


            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);
            var mockModelRepository = new Mock<ModelRepository>(mockContext.Object) { CallBase = true }.As<IModelRepository>();


            var service = new ModelService(mockModelRepository.Object, mockTruckRepository.Object, mapperMock.Object);
            var result = service.GetAllAsync().Result;

            Assert.IsNotNull(result);
        }
    }
}
