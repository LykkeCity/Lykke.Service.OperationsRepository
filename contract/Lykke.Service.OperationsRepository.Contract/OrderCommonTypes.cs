namespace Lykke.Service.OperationsRepository.Contract
{
    public enum OrderType
    {
        Buy,
        Sell
    }

    public enum OrderStatus
    {
        /// <summary>
        /// Initial status, limit order in order book
        /// </summary>
        InOrderBook,
        /// <summary>
        /// Partially matched
        /// </summary>
        Processing,
        /// <summary>
        /// Fully matched
        /// </summary>
        Matched,
        /// <summary>
        /// Not enough funds on account
        /// </summary>
        NotEnoughFunds,
        /// <summary>
        /// Reserved volume greater than balance
        /// </summary>
        ReservedVolumeGreaterThanBalance,
        /// <summary>
        /// No liquidity
        /// </summary>
        NoLiquidity,
        /// <summary>
        /// Unknown asset
        /// </summary>
        UnknownAsset,
        /// <summary>
        /// One of trades or whole order has volume/price*volume less then configured dust
        /// </summary>
        /// <remarks>Not used anymore. See 'TooSmallVolume'</remarks>
        Dust,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled,
        /// <summary>
        /// Lead to negative spread
        /// </summary>
        LeadToNegativeSpread,
        /// <summary>
        /// Too small volume
        /// </summary>
        TooSmallVolume,
        /// <summary>
        /// Unexpected status code
        /// </summary>
        Runtime,
        /// <summary>
        /// Invalid fee
        /// </summary>
        InvalidFee
    }
}