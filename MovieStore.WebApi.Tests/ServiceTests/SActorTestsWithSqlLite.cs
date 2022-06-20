using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbContexts;
using MovieStore.WebApi.Mappings;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MovieStore.WebApi.Tests.ServiceTests
{
    public class SActorTestsWithSqlLite: DummyDbGenerator
    {
        private readonly IMapper mapper;
        
        public SActorTestsWithSqlLite()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ObjectMapperProfile());
            });
            mapper = mockMapper.CreateMapper();

            //sql lite connection configuration
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            SetContextOptions(new DbContextOptionsBuilder<MovieStoreDbContext>().UseSqlite(connection).Options);
            Seed();
        }

        [Fact]
        public void Add_ValidModel_ReturnCreated()
        {
            ActorCreateModel model = new ActorCreateModel { 
                Name = "Actor4", 
                Surname = "Surname4", 
                Movies = new List<MovieActorCreateModel> 
                { 
                    new MovieActorCreateModel { Id = 1} 
                }
            };

            using (var context = new MovieStoreDbContext(_contextOption))
            {
                var service = new SActor(context, mapper);
                service.ActorCreateModel = model;

                var responseResult = service.Add();                
            }

            using (var context = new MovieStoreDbContext(_contextOption))
            {
                var hasActor = context.Actors.Any(p => p.Name == model.Name);
                Assert.True(hasActor);
            }
        }
    }
}
