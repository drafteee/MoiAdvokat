namespace LawyerService.BL.Enums.TransactionStatus
{
    /// <summary>
    /// Коды статусов денежных транзакций в сервисе
    /// </summary>
    public enum TransactionStatusEnum
    {
        /// <summary>
        /// В обработке
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// Транзакция пройдена успешно
        /// </summary>
        IsSuccessful,
        /// <summary>
        /// Транзакция пройдена неудачно
        /// </summary>
        IsUnsuccessful,
    }
}
