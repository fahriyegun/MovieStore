using MovieStore.WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.WebApi.Tests
{
    public class DummyDataGenerator
    {
        public static List<ActorViewModel> ActorList = new List<ActorViewModel>
            {
                new ActorViewModel
                {
                    FullName = "Scarlett Johnson",
                    Movies = new List<MovieActorModel> {
                        new MovieActorModel
                        {
                            Name = "Black Widow"
                        },
                        new MovieActorModel
                        {
                            Name = "Lucy"
                        }
                    }
                },
                new ActorViewModel
                {
                    FullName = "Nicolas Cage",
                    Movies = new List<MovieActorModel> {
                        new MovieActorModel
                        {
                            Name = "Next"
                        },
                        new MovieActorModel
                        {
                            Name = "Ghost Rider"
                        }
                    }
                }

            };

    }
}
