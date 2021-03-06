﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CardHero.Data.Abstractions;
using CardHero.NetCoreApp.TypeScript;

using Xunit;

namespace CardHero.NetCoreApp.IntegrationTests
{
    public partial class GameApiControllerTests
    {
        [Fact]
        public async Task MoveAsync_Unauthorized_ReturnsUnauthorized()
        {
            await RunAsync(async factory =>
            {
                await factory.AddDataAsync(
                    new GameData
                    {
                        Columns = 3,
                        MaxPlayers = 2,
                        Id = 701,
                        Rows = 3,
                    }
                );

                var client = factory.CreateClient();

                var response = await client.PostJsonAsync("api/games/701/move", new GameMoveViewModel { });

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            });
        }

        [Fact]
        public async Task MoveAsync_InvalidGame_ReturnsNotFound()
        {
            await RunAsync(async factory =>
            {
                var client = factory.CreateClientWithAuth();

                var response = await client.PostJsonAsync("api/games/701/move", new GameMoveViewModel
                {
                    Column = 1,
                    GameDeckCardCollectionId = 1,
                    Row = 1,
                });
                var model = await response.Content.ReadAsAsync<ErrorViewModel>();

                //TODO: Replace with NotFound
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Equal("Game 701 does not exist.", model.Message);
            });
        }

        [Fact]
        public async Task MoveAsync_GameNotStarted_ReturnsBadRequest()
        {
            await RunAsync(async factory =>
            {
                await factory.AddDataAsync(
                    new GameData
                    {
                        Columns = 3,
                        MaxPlayers = 2,
                        Id = 701,
                        Rows = 3,
                    }
                );

                await factory.AddDataAsync(
                    new GameUserData
                    {
                        GameId = 701,
                        Id = 801,
                        UserId = 1,
                    }
                );

                await factory.AddDataAsync(
                    new GameDeckData
                    {
                        GameUserId = 801,
                        Id = 901,
                        Name = "First game",
                    }
                );

                var client = factory.CreateClientWithAuth();

                var response = await client.PostJsonAsync("api/games/701/move", new GameMoveViewModel
                {
                    Column = 1,
                    GameDeckCardCollectionId = 1,
                    Row = 1,
                });
                var model = await response.Content.ReadAsAsync<ErrorViewModel>();

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Equal("Game 701 has not started.", model.Message);
            });
        }

        /*[Fact]
        public async Task MoveAsync_YourTurnFirstMove_ReturnsSuccess()
        {
            await RunAsync(async factory =>
            {
                await factory.AddDataAsync(
                    new GameData
                    {
                        Columns = 3,
                        CurrentGameUserId = 1,
                        MaxPlayers = 2,
                        Id = 701,
                        Rows = 3,
                    }
                );

                var client = factory.CreateClientWithAuth();

                var response = await client.PostJsonAsync("api/games/701/move", new GameMoveViewModel
                {
                    Column = 0,
                    GameDeckCardCollectionId = 1,
                    Row = 2,
                });
                response.EnsureSuccessStatusCode();
                var model = await response.Content.ReadAsAsync<GameMoveViewModel>();

                Assert.Equal(0, model.Column);
                Assert.Equal(1, model.GameDeckCardCollectionId);
                Assert.Equal(2, model.Row);
            });
        }*/
    }
}
