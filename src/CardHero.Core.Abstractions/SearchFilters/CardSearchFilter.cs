﻿using System.Collections.Generic;

using CardHero.Core.Models;

namespace CardHero.Core.Abstractions
{
    /// <summary>
    /// Filter for searching cards.
    /// </summary>
    public class CardSearchFilter : SearchFilter<CardModel>
    {
        /// <summary>
        /// A list of card ids to search for.
        /// </summary>
        public IEnumerable<int> Ids { get; set; }

        /// <summary>
        /// Name to search for.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Card pack to filter by.
        /// </summary>
        public int? CardPackId { get; set; }
    }
}
