namespace Smart.Service
{
    public interface IMessageService : IService<MessageViewModel>
    {
        MessageViewModel Add(MessageViewModel instance);
        GetAllMessagesForChatResponse GetAllMessagesFromChat(string id);
        MessageViewModel GetMessageById(string id);
        bool MarkAsFavourite(string id);

    }
}