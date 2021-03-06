﻿using System;
using System.Threading;
using System.Threading.Tasks;

using CardHero.Data.Abstractions;
using CardHero.Data.SqlServer.EntityFramework;

namespace CardHero.Data.SqlServer
{
    internal class GameUserRepository : IGameUserRepository
    {
        private readonly CardHeroDataDbContext _context;

        public GameUserRepository(CardHeroDataDbContext context)
        {
            _context = context;
        }

        async Task<GameUserData> IGameUserRepository.AddGameUserAsync(int gameId, int userId, CancellationToken cancellationToken)
        {
            var gameUser = new GameUser
            {
                GameFk = gameId,
                JoinedTime = DateTime.UtcNow,
                UserFk = userId,
            };

            _context.GameUser.Add(gameUser);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);

            var newData = new GameUserData
            {
                GameId = gameId,
                Id = gameUser.GameUserPk,
                JoinedTime = gameUser.JoinedTime,
                Order = gameUser.Order,
                UserId = userId,
            };

            return newData;
        }
    }
}
