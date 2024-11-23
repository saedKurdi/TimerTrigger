﻿using Azure.Storage.Queues.Models;

namespace TimerTriggerLesson.Services.Abstract;
public interface IQueueService
{
    Task SendMessageAsync(string message);
    Task<QueueMessage> ReceiveMessageAsync();
    Task DeleteMessageAsync(string messageId, string popReceipt);
}
