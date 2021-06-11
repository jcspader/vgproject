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
    class TruckServiceTests
    {
        [Test]
        public void AddNewTruck_TruckService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<TruckEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Trucks).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TruckEntity>(It.IsAny<TruckDto>())).Returns(new TruckEntity() { Color ="white", ManufactureYear = 2020, ModelYear = 2021, ModelId =1 });


            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object);


            var service = new TruckService(mockTruckRepository.Object, mapperMock.Object);
            service.AddAsync(new TruckDto()).Wait();

            mockContext.Verify(m => m.AddAsync(It.IsAny<TruckEntity>(), It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Delete_TruckService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<TruckEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Trucks).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TruckEntity>(It.IsAny<TruckDto>())).Returns(new TruckEntity());

            var testObject = new TruckEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object) { CallBase = true }.As<ITruckRepository>();

            mockTruckRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testObject);
            mockContext.Setup(x => x.Remove(It.IsAny<TruckEntity>()));
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


            var service = new TruckService(mockTruckRepository.Object, mapperMock.Object);
            service.DeleteAsync(1).Wait();

            mockContext.Verify(m => m.Remove(It.IsAny<TruckEntity>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Update_TruckService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockSet = new Mock<DbSet<TruckEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Trucks).Returns(mockSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TruckEntity>(It.IsAny<TruckDto>())).Returns(new TruckEntity());

            var testObject = new TruckEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object) { CallBase = true }.As<ITruckRepository>();

            mockTruckRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testObject);
            mockContext.Setup(x => x.Update(It.IsAny<TruckEntity>()));
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


            var service = new TruckService(mockTruckRepository.Object, mapperMock.Object);
            service.UpdateAsync(new TruckDto()).Wait();

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void GetById_TruckService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockDbSet = new Mock<DbSet<TruckEntity>>();
            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Trucks).Returns(mockDbSet.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TruckEntity>(It.IsAny<TruckDto>())).Returns(new TruckEntity());

            var testObject = new TruckEntity();

            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object) { CallBase = true }.As<ITruckRepository>();

            mockDbSet.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(testObject);


            var service = new TruckService(mockTruckRepository.Object, mapperMock.Object);
            service.GetByIdAsync(1).Wait();

            mockContext.Verify(x => x.Set<TruckEntity>());
            //mockDbSet.Verify(m => m.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_TruckService()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var mockDbSet = new Mock<DbSet<TruckEntity>>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TruckEntity>(It.IsAny<TruckDto>())).Returns(new TruckEntity());

            var testObject = new List<TruckEntity>() { new TruckEntity() };

            mockDbSet.As<IQueryable<TruckEntity>>().Setup(x => x.Provider).Returns(testObject.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TruckEntity>>().Setup(x => x.Expression).Returns(testObject.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TruckEntity>>().Setup(x => x.ElementType).Returns(testObject.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TruckEntity>>().Setup(x => x.GetEnumerator()).Returns(testObject.AsQueryable().GetEnumerator());

            var mockContext = new Mock<DataBaseContext>(options) { CallBase = true };
            mockContext.Setup(m => m.Trucks).Returns(mockDbSet.Object);


            var mockTruckRepository = new Mock<TruckRepository>(mockContext.Object) { CallBase = true }.As<ITruckRepository>();


            var service = new TruckService(mockTruckRepository.Object, mapperMock.Object);
            var result = service.GetAllAsync().Result;

            Assert.IsNotNull(result);
        }
    }
}
